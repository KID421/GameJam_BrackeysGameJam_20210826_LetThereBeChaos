using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// 技能控制器
/// </summary>
public class SkillController : MonoBehaviour
{
    #region 欄位：公開
    [Header("技能效果")]
    public GameObject goSkillEffect;
    [Header("技能按鍵")]
    public KeyCode kcSKill = KeyCode.Mouse0;
    [Header("技能飛行速度"), Range(0, 500)]
    public float speedSkill = 10;
    [Header("技能冷卻")]
    public float cdSkill = 3;
    [Header("技能感應半徑")]
    public float radiusSkill = 1;
    [Header("技能冷卻圖片與文字")]
    public Image imgCD;
    public Text textCD;
    /// <summary>
    /// 技能次數
    /// </summary>
    public int countSKill = 3;
    [Header("音效")]
    public AudioClip soundShoot;
    #endregion

    #region 欄位：私人
    /// <summary>
    /// 技能計時器
    /// </summary>
    private float timerSkill;
    /// <summary>
    /// 是否能使用技能
    /// </summary>
    private bool canUseSkill = true;
    /// <summary>
    /// 技能物件暫存
    /// </summary>
    private Transform traSKillTemp;
    /// <summary>
    /// 滑鼠座標
    /// </summary>
    private Vector3 posMouse;
    /// <summary>
    /// 滑鼠世界座標
    /// </summary>
    private Vector3 posMouseWorld;
    private AudioSource aud;
    #endregion

    #region 事件
    private void OnDrawGizmos()
    {
        posMouse = Input.mousePosition;
        posMouseWorld = Camera.main.ScreenToWorldPoint(posMouse);
        posMouseWorld.z = 0;
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(posMouseWorld, 0.3f);

        if (traSKillTemp)
        {
            Gizmos.color = new Color(0, 0, 1, 0.3f);
            Gizmos.DrawSphere(traSKillTemp.position, radiusSkill);
        }
    }

    private void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Skill();
    }
    #endregion

    #region 方法：私人
    /// <summary>
    /// 技能
    /// 按下按鍵後生成技能特效並飛往滑鼠位置
    /// </summary>
    private void Skill()
    {
        if (countSKill > 0 && canUseSkill && Input.GetKeyDown(kcSKill))
        {
            aud.PlayOneShot(soundShoot, Random.Range(0.7f, 1.2f));
            countSKill--;
            textCD.text = countSKill + "";
            canUseSkill = false;
            imgCD.fillAmount = 1;

            traSKillTemp = Instantiate(goSkillEffect, transform.position, Quaternion.identity).transform;

            posMouse = Input.mousePosition;
            posMouse.z = 0;
            posMouseWorld = Camera.main.ScreenToWorldPoint(posMouse);
            posMouseWorld.z = 0;

            StopAllCoroutines();
            StartCoroutine(SkillFlyToMousePosition(posMouseWorld));
        }
        else if (!canUseSkill)
        {
            if (timerSkill < cdSkill)
            {
                timerSkill += Time.deltaTime;
                imgCD.fillAmount = 1 - timerSkill / cdSkill;
            }
            else
            {
                timerSkill = 0;
                imgCD.fillAmount = 0;
                canUseSkill = true;
            }
        }
    }

    /// <summary>
    /// 技能飛往指定座標
    /// </summary>
    /// <param name="posTarget">指定座標</param>
    private IEnumerator SkillFlyToMousePosition(Vector3 posTarget)
    {
        float dis = Vector3.Distance(traSKillTemp.position, posTarget);

        while (dis > 0)
        {
            dis = Vector3.Distance(traSKillTemp.position, posTarget);
            traSKillTemp.position = Vector3.MoveTowards(traSKillTemp.position, posTarget, speedSkill * Time.deltaTime);
            yield return new WaitForSeconds(0.02f);
        }

        traSKillTemp.position = posTarget;

        ParticleSystem ps = traSKillTemp.GetComponent<ParticleSystem>();
        ParticleSystem.MainModule main = ps.main;
        main.startSpeed = 20;
        ParticleSystem.EmissionModule emission = ps.emission;
        yield return new WaitForSeconds(0.1f);
        emission.rateOverTime = 0;

        Collider2D hit = Physics2D.OverlapCircle(traSKillTemp.position, radiusSkill, 1 << 6);
        if (hit) hit.GetComponent<IChaos>().Chaos();
    }
    #endregion

    #region 方法：公開
    #endregion
}
