using UnityEngine;
using System.Linq;
using System.Collections;

public class Cow : BaseEnemy
{
    [Header("衝刺速度")]
    public float speedSprint = 10;
    [Header("偵測玩家方形尺寸")]
    public float checkPlayerSize = 3;
    [Header("偵測半徑")]
    public float radiusChaos = 1;
    [Header("混亂移動速度")]
    public float speedChaosMove = 1;

    /// <summary>
    /// 目標座標
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

        // 檢查立方體會隨著角色換方向改變
        Vector3 offset = v3WalkDirection * checkPlayerSize / 2;
        Vector3 size = v3WalkDirection * checkPlayerSize + Vector3.one;
        Gizmos.DrawCube(transform.position + offset, size);
    }

    /// <summary>
    /// 檢查玩家是否在前方
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
    /// 衝刺
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
    /// 黏著到附近其他生物上並使其沒有移動也不能混亂
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
