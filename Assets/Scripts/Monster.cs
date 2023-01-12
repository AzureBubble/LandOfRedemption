using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Monster : MonoBehaviour
{
    private Animator anim; // Monster 动画控制
    private Rigidbody2D rbody; // 刚体运动确定位置
    float changeTimer;  // 定义一个定时器 到了时间就改变方向
    Vector2 moveDirection;
    private PlayerMovement pm; // 这里是 玩家脚本
    private static float y;  // 记录初始y轴位置

    [Header("Monster 属性")]
    [Tooltip("Monster 移动速度")]
    [SerializeField]
    public float moveSpeed = 1f;
    [Tooltip("计时器长度")]
    [SerializeField]
    public float changeDirectionTime = 2f;
    [Header("追逐属性")]
    [Tooltip("是否自动追逐玩家")]
    [SerializeField]
    public bool isChasing = false;
    [Tooltip("拖入玩家")]
    [SerializeField]
    public Transform target;
    [Tooltip("追击范围")]
    [SerializeField]
    public float rangDistance = 1f;
    [Tooltip("Monster 追及速度")]
    [SerializeField]
    public float chaseSpeed = 1.5f;

    public Animator playerAnim;



    void Start()
    {
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody2D>();
        changeTimer = changeDirectionTime;  // 初始化计时器
        moveDirection = Vector2.left;
        //pm = GameObject.Find("Player").GetComponent<PlayerMovement>();  // 找到玩家脚本
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {

        if (isChasing && Vector2.Distance(transform.position, target.position) < rangDistance)
        {
            ChasingAnim();
            Chase();
            //Debug.Log("开始追逐");
        }
        else
        {

            if (transform.position.y != y)
            {
               //transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, y, 0), 2 * Time.deltaTime);
            }
            ChangeMoving();
        }

    }

    #region 触发碰撞死亡
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            // 在这里调用 主角 脚本 的 主角死亡 代码
            playerAnim.SetTrigger("die");
            Debug.Log("主角完蛋");
            Invoke("LoadScene",1f);
        }

        if (collision.gameObject.tag == "Monster")
        {
            gameObject.SetActive(false);
            Debug.Log("怪物完蛋");
        }

    }
    #endregion

    void LoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    #region 改变移动方向
    private void ChangeMoving()
    {
        changeTimer -= Time.deltaTime;  // 时间一点点减少
        if (changeTimer < 0)  // 当时间小于0 改变方向
        {
            moveDirection = moveDirection * -1;
            changeTimer = changeDirectionTime; // 重新初始化
            //Debug.Log("转变方向");
        }

        UsingAnim();
        rbody.velocity = moveDirection * moveSpeed;

    }
    #endregion

    #region 追逐方法
    public void Chase()
    {

        transform.position = Vector3.MoveTowards(transform.position, target.position, chaseSpeed * Time.deltaTime);

    }

    #endregion

    #region 调用追逐动画方法
    public void ChasingAnim()
    {
        if (transform.position.x > target.position.x)
        {
            anim.SetBool("turn", false);
        }
        else
        {
            anim.SetBool("turn", true);
        }
    }
    #endregion

    #region 调用巡逻动画
    public void UsingAnim()
    {
        if (moveDirection == Vector2.left)
        {
            anim.SetBool("turn", false);
        }
        else
        {
            anim.SetBool("turn", true);
        }
    }
    #endregion

}
