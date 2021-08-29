using UnityEngine;
using UnityEngine.Events;

public class DeadControl : MonoBehaviour
{
    [Header("死亡特效")]
    public GameObject goDeadEffect;
    [Header("碰到後會死亡的圖層編號")]
    public int layerDead = 6;
    [Header("死亡事件")]
    public UnityEvent onDead;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == layerDead) Dead();
    }

    /// <summary>
    /// 死亡
    /// </summary>
    private void Dead()
    {
        Instantiate(goDeadEffect, transform.position, Quaternion.identity);
        onDead.Invoke();
        Destroy(gameObject);
    }
}
