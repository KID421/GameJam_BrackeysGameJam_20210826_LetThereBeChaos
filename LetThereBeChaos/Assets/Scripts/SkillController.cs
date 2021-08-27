using UnityEngine;
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
    #endregion

    #region 欄位：私人
    /// <summary>
    /// 技能次數
    /// </summary>
    private int countSKill = 3;
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
    #endregion

    #region 事件
    private void OnDrawGizmos()
    {
        posMouse = Input.mousePosition;
        posMouseWorld = Camera.main.ScreenToWorldPoint(posMouse);
        posMouseWorld.z = 0;
        Gizmos.color = new Color(1, 0, 0, 0.3f);
        Gizmos.DrawSphere(posMouseWorld, 0.3f);
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
        if (Input.GetKeyDown(kcSKill))
        {
            traSKillTemp = Instantiate(goSkillEffect, transform.position, Quaternion.identity).transform;

            posMouse = Input.mousePosition;
            posMouse.z = 0;
            posMouseWorld = Camera.main.ScreenToWorldPoint(posMouse);
            posMouseWorld.z = 0;

            StopAllCoroutines();
            StartCoroutine(SkillFlyToMousePosition(posMouseWorld));
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
            print(dis);
            traSKillTemp.position = Vector3.MoveTowards(traSKillTemp.position, posTarget, speedSkill);
            yield return new WaitForSeconds(0.02f);
        }

        traSKillTemp.position = posTarget;
    }
    #endregion

    #region 方法：公開
    #endregion
}
