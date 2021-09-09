using UnityEngine;
using System.Linq;
using System.Collections;

public class Slime : BaseEnemy
{
    [Header("偵測半徑")]
    public float radiusChaos = 1;
    [Header("混亂移動速度")]
    public float speedChaosMove = 1;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.5f, 0, 0, 0.3f);
        Gizmos.DrawSphere(transform.position, radiusChaos);
    }

    public override void Chaos()
    {
        if (isChaos) return;

        base.Chaos();

        isChaos = true;
        StartCoroutine(Stick());
    }

    /// <summary>
    /// 黏著到附近其他生物上並使其沒有移動也不能混亂
    /// </summary>
    private IEnumerator Stick()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radiusChaos, 1 << 6);

        speed = 0;
        GetComponent<CircleCollider2D>().enabled = false;

        Collider2D[] result = hits.Where(x => x.GetComponent<BaseEnemy>().isChaos == false).ToArray();

        if (result.Length > 0)
        {
            // 鎖定的生物進入停止狀態
            result[0].GetComponent<BaseEnemy>().state = StateEnemy.stop;

            Vector3 pos = result[0].transform.position;
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
