using UnityEngine;

/// <summary>
/// �W�U�Ҧ� 2D ���a���
/// </summary>
public class TopDownController : MonoBehaviour
{
    #region ���G���}
    [Header("�t��"), Range(0, 1000)]
    public float speed;
    
    #endregion

    #region ���G�p�H
    private Rigidbody2D rig;
    private Animator ani;
    private AudioSource aud;
    private float inputH;
    private float inputV;
    #endregion

    #region ���G�ƥ�
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
    }

    private void Update()
    {
        InputMove();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        Move();
    }
    #endregion

    #region ��k�G�p�H
    /// <summary>
    /// ���ʿ�J�G�����P����
    /// </summary>
    private void InputMove()
    {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
    }

    /// <summary>
    /// ���ʡG����[�t�ײ���
    /// </summary>
    private void Move()
    {
        rig.velocity = (transform.right * inputH + transform.up * inputV) * Time.fixedDeltaTime * speed;
    }

    /// <summary>
    /// ��s�ʵe
    /// ��������P������
    /// �åB�b���U�����ɱN�����k�s�A�Ϥ��]�O
    /// </summary>
    private void UpdateAnimation()
    {
        ani.SetBool("�����}��", inputH != 0 || inputV != 0);
        if (Mathf.Abs(inputH) > 0.1f)
        {
            ani.SetFloat("����", inputH);
            ani.SetFloat("����", 0);
        }

        if (Mathf.Abs(inputV) > 0.1f)
        {
            ani.SetFloat("����", 0);
            ani.SetFloat("����", inputV);
        }
    }
    #endregion

    #region ��k�G���}
    /// <summary>
    /// ����
    /// </summary>
    public void Stop()
    {
        rig.velocity = Vector3.zero;
        enabled = false;
    }
    #endregion
}
