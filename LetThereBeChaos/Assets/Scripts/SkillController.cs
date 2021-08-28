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
    [Header("�ޯ�N�o�Ϥ��P��r")]
    public Image imgCD;
    public Text textCD;
    #endregion

    #region ���G�p�H
    /// <summary>
    /// �ޯস��
    /// </summary>
    private int countSKill = 3;
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
    #endregion

    #region �ƥ�
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

    #region ��k�G�p�H
    /// <summary>
    /// �ޯ�
    /// ���U�����ͦ��ޯ�S�Ĩí����ƹ���m
    /// </summary>
    private void Skill()
    {
        if (countSKill > 0 && canUseSkill && Input.GetKeyDown(kcSKill))
        {
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
            traSKillTemp.position = Vector3.MoveTowards(traSKillTemp.position, posTarget, speedSkill);
            yield return new WaitForSeconds(0.02f);
        }

        traSKillTemp.position = posTarget;
    }
    #endregion

    #region ��k�G���}
    #endregion
}
