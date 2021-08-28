using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D), typeof(AudioSource))]
public class BaseEnemy : MonoBehaviour, IChaos
{
    #region 欄位：公開
    [Header("移動速度"), Range(0, 100)]
    public float speed = 1;
    [Header("等待與走路最小最大時間")]
    public Vector2 v2TimeLimitIdle = new Vector2(0.5f, 3);
    public Vector2 v2TimeLimitWalk = new Vector2(1, 3.5f);
    #endregion

    #region 欄位：私人
    private Rigidbody2D rig;
    private Animator ani;
    private AudioSource aud;
    private float timerIdle;
    private float timeIdle;
    private float timerWalk;
    private float timeWalk;
    /// <summary>
    /// 走路方向
    /// </summary>
    private Vector3 v3WalkDirection;
    #endregion

    #region 欄位：保護
    /// <summary>
    /// 是否混亂中
    /// </summary>
    protected bool isChaos;
    /// <summary>
    /// 狀態
    /// </summary>
    protected StateEnemy state;
    #endregion

    #region 事件：私人
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

    #region 事件：保護
    #endregion

    #region 方法：公開
    public virtual void Chaos()
    {

    }
    #endregion

    #region 方法：私人
    /// <summary>
    /// 檢查狀態
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
                Debug.LogWarning("狀態警報！");
                break;
        }
    }

    /// <summary>
    /// 第一次等待時間
    /// </summary>
    private void FirstIdle()
    {
        timeIdle = Random.Range(v2TimeLimitIdle.x, v2TimeLimitIdle.y);
    }

    /// <summary>
    /// 等待：可以前往走路
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
    /// 走路：可以前往等待
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
    /// 在 Fixed Update 執行走路物理移動
    /// </summary>
    private void WalkFixedUpdate()
    {
        if (state == StateEnemy.walk) rig.velocity = v3WalkDirection * speed * Time.deltaTime;
    }

    /// <summary>
    /// 隨機移動方向
    /// </summary>
    private void RandomWalkDirection()
    {
        int r = Random.Range(0, 4);

        v3WalkDirection = r == 0 ? transform.up : r == 1 ? -transform.up : r == 2 ? transform.right : -transform.right;
    }

    /// <summary>
    /// 更新動畫
    /// </summary>
    private void UpdateAnimation()
    {
        if (Mathf.Abs(rig.velocity.x) > 0.1f)
        {
            ani.SetFloat("水平", rig.velocity.x);
        }
        if (Mathf.Abs(rig.velocity.y) > 0.1f)
        {
            ani.SetFloat("垂直", rig.velocity.y);
        }
    }
    #endregion

    #region 方法：保護
    #endregion
}

/// <summary>
/// 怪物狀態：等待、走路、混亂、使用技能
/// </summary>
public enum StateEnemy
{
    idle, walk, chaos, skill
}