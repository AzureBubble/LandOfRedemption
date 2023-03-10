using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb; // Player 的刚体
    public float inputX; // 获取玩家的 X 轴输入
    public float inputY;
    //private Animator anim; // Player 动画控制器


    [Header("Player 属性")]
    [Tooltip("Player 移动速度")]
    [SerializeField]
    public float moveSpeed; // 移动速度
    [Tooltip("Player 跳跃力")]
    [SerializeField]
    public float jumpForce; // 跳跃力
    [Tooltip("Player 可跳跃次数")]
    [SerializeField]
    private float jumpCount; // 可跳跃次数
    [Tooltip("Player 跳跃")]
    [SerializeField]
    private bool isJump; // 是否跳跃
    [Tooltip("Player 最小跳跃高度")]
    [SerializeField]
    private float minJumpTime = 0.2f; // 最小跳跃高度
    [Tooltip("Player 最大跳跃高度")]
    [SerializeField]
    private float maxJumpTime = 0.5f; // 最大跳跃高度
    [Tooltip("Player 跳跃时间")]
    [SerializeField]
    private float currentJumpTime = 0.5f; // 跳跃时间
    [Tooltip("Player 跳跃重力")]
    [SerializeField]
    public float jumpUpGraivity = 3; // 跳跃重力
    [Tooltip("Player 下落重力")]
    [SerializeField]
    public float fallDownGraivity = 6; // 下落重力
    [Tooltip("Player 落地检测")]
    [SerializeField]
    public bool isGround = true; // 落地检测
    /*[Tooltip("Player 落地检测半径")]
    [Range(0, 1.0f)]
    [SerializeField]
    private float checkRadius = .5f; // 落地检测半径*/
    [Tooltip("Player 检测地面的 Layer")]
    [SerializeField]
    private LayerMask layer; // 检测地面的 Layer
    [Tooltip("获取player脚的位置")]
    [SerializeField]
    [Range(0, 1f)]
    private float footOffset = 0.3f;
    [Tooltip("游戏人物到地面的距离")]
    [SerializeField]
    [Range(0, 1f)]
    private float groundDistance = 0.6f;


    void Start()
    {
        // 获取角色刚体组件
        rb = GetComponent<Rigidbody2D>();
        // 获取动画控制器组件
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentJumpTime += Time.deltaTime;
        // 获取玩家的 X 轴输入
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");
        Jump();
    }

    void FixedUpdate()
    {
        GroundCheck();
        ChangeGraivity();
        Flip();
        Move();
    }

    #region 修改 Player 的跳跃和下落重力数值
    void ChangeGraivity()
    {
        if (rb.velocity.y > 0)
        {
            rb.gravityScale = jumpUpGraivity;
        }
        else
        {
            rb.gravityScale = fallDownGraivity;
        }
    }
    #endregion

    #region Player 朝向
    void Flip()
    {
        // 判断朝向
        if (inputX == 1)
        {
            // 翻转
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (inputX == -1)
        {
            // 翻转
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
    #endregion

    #region 检测地面
    void GroundCheck()
    {
        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset + 0.1f, -0.2f), Vector2.down, groundDistance, layer);
        RaycastHit2D rightCheck = Raycast(new Vector2(footOffset + 0.05f, -0.2f), Vector2.down, groundDistance, layer);
        if (leftCheck || rightCheck)
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
    #endregion

    #region 获取人物较低发出的射线碰撞图层
    RaycastHit2D Raycast(Vector2 offset, Vector2 direction, float length, LayerMask layer)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, direction, length, layer);
        Color color = hit ? Color.red : Color.green;
        Debug.DrawRay(pos + offset, direction * length, color);
        return hit;
    }
    #endregion

    #region Player 跳跃
    void Jump()
    {
        //检测 Player 是否落地
        //isGround = Physics2D.OverlapCircle(transform.position, checkRadius, layer);
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            isJump = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJump = false;
        }
    }
    #endregion

    #region Player 移动
    void Move()
    {
        // Player X 轴移动
        rb.velocity = new Vector2(inputX * moveSpeed, rb.velocity.y);
    }
    #endregion

    /*#region 画出检测图形
    private void OnDrawGizmos()
    {
        // 画笔颜色
        Gizmos.color = Color.red;
        // 画出地面检测范围圆(圆心，半径)
        Gizmos.DrawWireSphere(transform.position, checkRadius);
    }
    #endregion*/
}
