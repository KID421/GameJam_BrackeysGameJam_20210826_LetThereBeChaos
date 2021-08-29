using UnityEngine;
using UnityEngine.Events;

public class DeadControl : MonoBehaviour
{
    [Header("���`�S��")]
    public GameObject goDeadEffect;
    [Header("�I���|���`���ϼh�s��")]
    public int layerDead = 6;
    [Header("���`�ƥ�")]
    public UnityEvent onDead;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == layerDead) Dead();
    }

    /// <summary>
    /// ���`
    /// </summary>
    private void Dead()
    {
        Instantiate(goDeadEffect, transform.position, Quaternion.identity);
        onDead.Invoke();
        Destroy(gameObject);
    }
}
