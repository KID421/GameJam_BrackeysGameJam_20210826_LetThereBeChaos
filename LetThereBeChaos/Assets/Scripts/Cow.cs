using UnityEngine;
using System.Linq;
using System.Collections;

public class Cow : BaseEnemy
{
    [Header("�Ĩ�t��")]
    public float speedSprint = 10;
    [Header("�������a��Τؤo")]
    public float checkPlayerSize = 3;
    [Header("�����b�|")]
    public float radiusChaos = 1;
    [Header("�V�ò��ʳt��")]
    public float speedChaosMove = 1;

    /// <summary>
    /// �ؼЮy��
    /// </summary>
    private Vector3 posTarget;

    protected override void Update()
    {
        base.Update();
        CheckPlayer();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, radiusChaos);

        Gizmos.color = new Color(0, 0, 0.5f, 0.3f);

        // �ˬd�ߤ���|�H�ۨ��⴫��V����
        Vector3 offset = v3WalkDirection * checkPlayerSize / 2;
        Vector3 size = v3WalkDirection * checkPlayerSize + Vector3.one;
        Gizmos.DrawCube(transform.position + offset, size);
    }

    /// <summary>
    /// �ˬd���a�O�_�b�e��
    /// </summary>
    private void CheckPlayer()
    {
        Vector3 offset = v3WalkDirection * checkPlayerSize / 2;
        Vector3 size = v3WalkDirection * checkPlayerSize + Vector3.one;
        size = new Vector3(Mathf.Abs(size.x), Mathf.Abs(size.y), Mathf.Abs(size.z));
        Collider2D hit = Physics2D.OverlapBox(transform.position + offset, size, 0, 1 << 3);

        if (hit)
        {
            print(hit.name);
            posTarget = hit.transform.position;
            state = StateEnemy.skill;
        }
    }

    protected override void Skill()
    {
        base.Skill();

        if (!useSkill) StartCoroutine(Sprint());
    }

    /// <summary>
    /// �Ĩ�
    /// </summary>
    private IEnumerator Sprint()
    {
        useSkill = true;

        float dis = Vector3.Distance(transform.position, posTarget);

        while (dis > 1)
        {
            dis = Vector3.Distance(transform.position, posTarget);
            transform.Translate(v3WalkDirection * speedSprint * Time.deltaTime);
            yield return new WaitForSeconds(0.02f);
        }
    }

    public override void Chaos()
    {
        base.Chaos();

        isChaos = true;
        StartCoroutine(ChaosSprint());
    }

    /// <summary>
    /// �H�ۨ�����L�ͪ��W�èϨ�S�����ʤ]����V��
    /// </summary>
    private IEnumerator ChaosSprint()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radiusChaos, 1 << 7);

        speed = 0;
        //GetComponent<CircleCollider2D>().enabled = false;

        if (hits.Length > 0)
        {
            Vector3 pos = hits[0].transform.position;
            float dis = Vector3.Distance(transform.position, pos);

            while (dis > 0)
            {
                dis = Vector3.Distance(transform.position, pos);
                transform.position = Vector3.MoveTowards(transform.position, pos, speedChaosMove * Time.deltaTime);
                yield return new WaitForSeconds(0.02f);
            }
        }
    }
}
