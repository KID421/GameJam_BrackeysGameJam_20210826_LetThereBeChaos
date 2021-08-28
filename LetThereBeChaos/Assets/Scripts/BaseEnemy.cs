using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(AudioSource))]
public class BaseEnemy : MonoBehaviour, IChaos
{
    #region ���G���}
    [Header("���ʳt��"), Range(0, 100)]
    public float speed = 1;
    [Header("���ݻP�����̤p�̤j�ɶ�")]
    public Vector2 v2TimeLimitIdle = new Vector2(0.5f, 3);
    public Vector2 v2TimeLimitWalk = new Vector2(1, 3.5f);
    #endregion

    #region ���G�p�H
    private Rigidbody2D rig;
    private Animator ani;
    private AudioSource aud;
    private float timerIdle;
    private float timeIdle;
    private float timerWalk;
    private float timeWalk;
    /// <summary>
    /// ������V
    /// </summary>
    private Vector3 v3WalkDirection;
    #endregion

    #region ���G�O�@
    /// <summary>
    /// �O�_�V�ä�
    /// </summary>
    protected bool isChaos;
    /// <summary>
    /// ���A
    /// </summary>
    protected StateEnemy state;
    #endregion

    #region �ƥ�G�p�H
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        aud = GetComponent<AudioSource>();
        aud.playOnAwake = false;

        FirstIdle();
    }

    private void Update()
    {
        CheckState();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        WalkFixedUpdate();
    }
    #endregion

    #region �ƥ�G�O�@
    #endregion

    #region ��k�G���}
    public virtual void Chaos()
    {

    }
    #endregion

    #region ��k�G�p�H
    /// <summary>
    /// �ˬd���A
    /// </summary>
    private void CheckState()
    {
        switch (state)
        {
            case StateEnemy.idle:
                Idle();
                break;
            case StateEnemy.walk:
                Walk();
                break;
            case StateEnemy.chaos:
                break;
            case StateEnemy.skill:
                break;
            default:
                Debug.LogWarning("���Aĵ���I");
                break;
        }
    }

    /// <summary>
    /// �Ĥ@�����ݮɶ�
    /// </summary>
    private void FirstIdle()
    {
        timeIdle = Random.Range(v2TimeLimitIdle.x, v2TimeLimitIdle.y);
    }

    /// <summary>
    /// ���ݡG�i�H�e������
    /// </summary>
    private void Idle()
    {
        if (timerIdle < timeIdle)
        {
            timerIdle += Time.deltaTime;
        }
        else
        {
            timerIdle = 0;
            timeWalk = Random.Range(v2TimeLimitWalk.x, v2TimeLimitWalk.y);
            RandomWalkDirection();
            state = StateEnemy.walk;
        }
    }

    /// <summary>
    /// �����G�i�H�e������
    /// </summary>
    private void Walk()
    {
        if (timerWalk < timeWalk)
        {
            timerWalk += Time.deltaTime;
        }
        else
        {
            timerWalk = 0;
            timeIdle = Random.Range(v2TimeLimitIdle.x, v2TimeLimitIdle.y);
            state = StateEnemy.idle;
            rig.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// �b Fixed Update ���樫�����z����
    /// </summary>
    private void WalkFixedUpdate()
    {
        if (state == StateEnemy.walk) rig.velocity = v3WalkDirection * speed * Time.deltaTime;
    }

    /// <summary>
    /// �H�����ʤ�V
    /// </summary>
    private void RandomWalkDirection()
    {
        int r = Random.Range(0, 4);

        v3WalkDirection = r == 0 ? transform.up : r == 1 ? -transform.up : r == 2 ? transform.right : -transform.right;
    }

    /// <summary>
    /// ��s�ʵe
    /// </summary>
    private void UpdateAnimation()
    {
        if (Mathf.Abs(rig.velocity.x) > 0.1f)
        {
            ani.SetFloat("����", rig.velocity.x);
        }
        if (Mathf.Abs(rig.velocity.y) > 0.1f)
        {
            ani.SetFloat("����", rig.velocity.y);
        }
    }
    #endregion

    #region ��k�G�O�@
    #endregion
}

/// <summary>
/// �Ǫ����A�G���ݡB�����B�V�áB�ϥΧޯ�
/// </summary>
public enum StateEnemy
{
    idle, walk, chaos, skill
}