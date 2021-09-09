using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// �ޯ౱�
/// </summary>
public class SkillController : MonoBehaviour
{
    #region ���G���}
    [Header("�ޯ�ĪG")]
    public GameObject goSkillEffect;
    [Header("�ޯ����")]
    public KeyCode kcSKill = KeyCode.Mouse0;
    [Header("�ޯ୸��t��"), Range(0, 500)]
    public float speedSkill = 10;
    [Header("�ޯ�N�o")]
    public float cdSkill = 3;
    [Header("�ޯ�P���b�|")]
    public float radiusSkill = 1;
    [Header("�ޯ�N�o�Ϥ��P��r")]
    public Image imgCD;
    public Text textCD;
    /// <summary>
    /// �ޯস��
    /// </summary>
    public int countSKill = 3;
    [Header("����")]
    public AudioClip soundShoot;
    #endregion

    #region ���G�p�H
    /// <summary>
    /// �ޯ�p�ɾ�
    /// </summary>
    private float timerSkill;
    /// <summary>
    /// �O�_��ϥΧޯ�
    /// </summary>
    private bool canUseSkill = true;
    /// <summary>
    /// �ޯફ��Ȧs
    /// </summary>
    private Transform traSKillTemp;
    /// <summary>
    /// �ƹ��y��
    /// </summary>
    private Vector3 posMouse;
    /// <summary>
    /// �ƹ��@�ɮy��
    /// </summary>
    private Vector3 posMouseWorld;
    private AudioSource aud;
    #endregion

    #region �ƥ�
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

    #region ��k�G�p�H
    /// <summary>
    /// �ޯ�
    /// ���U�����ͦ��ޯ�S�Ĩí����ƹ���m
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
    /// �ޯ୸�����w�y��
    /// </summary>
    /// <param name="posTarget">���w�y��</param>
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

    #region ��k�G���}
    #endregion
}
