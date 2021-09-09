using UnityEngine;

/// <summary>
/// 上下模式 2D 玩家控制器
/// </summary>
public class TopDownController : MonoBehaviour
{
    #region 欄位：公開
    [Header("速度"), Range(0, 1000)]
    public float speed;
    
    #endregion

    #region 欄位：私人
    private Rigidbody2D rig;
    private Animator ani;
    private AudioSource aud;
    private float inputH;
    private float inputV;
    #endregion

    #region 欄位：事件
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

    #region 方法：私人
    /// <summary>
    /// 移動輸入：水平與垂直
    /// </summary>
    private void InputMove()
    {
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");
    }

    /// <summary>
    /// 移動：剛體加速度移動
    /// </summary>
    private void Move()
    {
        rig.velocity = (transform.right * inputH + transform.up * inputV) * Time.fixedDeltaTime * speed;
    }

    /// <summary>
    /// 更新動畫
    /// 控制水平與垂直值
    /// 並且在按下水平時將垂直歸零，反之也是
    /// </summary>
    private void UpdateAnimation()
    {
        ani.SetBool("走路開關", inputH != 0 || inputV != 0);
        if (Mathf.Abs(inputH) > 0.1f)
        {
            ani.SetFloat("水平", inputH);
            ani.SetFloat("垂直", 0);
        }

        if (Mathf.Abs(inputV) > 0.1f)
        {
            ani.SetFloat("水平", 0);
            ani.SetFloat("垂直", inputV);
        }
    }
    #endregion

    #region 方法：公開
    /// <summary>
    /// 停止
    /// </summary>
    public void Stop()
    {
        rig.velocity = Vector3.zero;
        enabled = false;
    }
    #endregion
}
