using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClearSky
{
    public class PlayController : MonoBehaviour
    {
        private Rigidbody2D rb;
        private float inputX, inputY;

        [Header("Player 属性")]
        [Tooltip("Player 移动速度")]
        [SerializeField]
        public float moveSpeed = 10.0f;               // 移动速度

        [Tooltip("KickBoard 移动速度")]
        [SerializeField]
        public float kickBoardMoveSpeed = 20.0f;      // 坐骑移动速度

        [Tooltip("Player 跳跃力")]
        [SerializeField]
        private float jumpForce = 5.0f;              // 跳跃力

        [Tooltip("Player 可跳跃次数")]
        [SerializeField]
        private float jumpCount = 2;              // 可跳跃次数

        [Tooltip("Player 跳跃")]
        [SerializeField]
        private bool isJump = false;                  // 是否跳跃
        
        [Tooltip("Player 最小跳跃高度")]
        [SerializeField]
        private float minJumpTime = 0.2f;     // 最小跳跃高度
        
        [Tooltip("Player 最大跳跃高度")]
        [SerializeField]
        private float maxJumpTime = 0.5f;     // 最大跳跃高度
        
        [Tooltip("Player 跳跃时间")]
        [SerializeField]
        private float currentJumpTime = 0.5f; // 跳跃时间
        
        [Tooltip("Player 跳跃重力")]
        [SerializeField]
        private float jumpUpGraivity = 3;     // 跳跃重力

        [Tooltip("Player 下落重力")]
        [SerializeField]
        private float fallDownGraivity = 6;   // 下落重力

        [Tooltip("Player 落地检测")]
        [SerializeField]
        private bool isGround = true;         // 落地检测

        [Tooltip("Player 落地检测半径")]
        [Range(0, 1.0f)]
        [SerializeField]
        private float checkRadius = .5f;      // 落地检测半径
    
        [Tooltip("Player 检测地面的 Layer")]
        [SerializeField]
        private LayerMask layer;             // 检测地面的 Layer


        private Animator anim;               // 动画
        Vector3 movement;
        private int direction = 1;
        private bool alive = true;
        private bool isKickboard = false;
        private bool isDie = false;

        void Start()
        {
            // 获取角色刚体组件
            rb = GetComponent<Rigidbody2D>();
            // 获取动画控制器组件
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            Restart();
            inputX = Input.GetAxisRaw("Horizontal");
            inputY = Input.GetAxisRaw("Vertical");
            currentJumpTime += Time.deltaTime;
            if (!isDie)
            {
                Die();
                //Hurt();
                //Attack();
                Jump();
                //KickBoard();
                Run();

            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            anim.SetBool("isJump", false);
        }
        void KickBoard()
        {
            if (Input.GetKeyDown(KeyCode.Alpha4) && isKickboard)
            {
                isKickboard = false;
                anim.SetBool("isKickBoard", false);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && !isKickboard )
            {
                isKickboard = true;
                anim.SetBool("isKickBoard", true);
            }
        }

        void Run()
        {
            if (!isKickboard)
            {
                Vector3 moveVelocity = Vector3.zero;
                anim.SetBool("isRun", false);
                transform.localScale = new Vector3(inputX, 1, 1);
                moveVelocity = new Vector3(inputX, 0, 0);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);
                transform.position += moveVelocity * moveSpeed * Time.deltaTime;

            }
            else
            {
                Vector3 moveVelocity = Vector3.zero;
                transform.localScale = new Vector3(inputX, 1, 1);
                moveVelocity = new Vector3(inputX, 0, 0);
                transform.position += moveVelocity * kickBoardMoveSpeed * Time.deltaTime;
            }
        }
        void Jump()
        {
            isGround = Physics2D.OverlapCircle(transform.position, checkRadius, layer);
            if(isGround)
                jumpCount = 2;
            if(Input.GetKeyDown(KeyCode.Space) && jumpCount > 0)
            {
                isJump = true;
                --jumpCount;
                anim.SetBool("isJump", true);
            }
            if(!isJump)
                return;
            Vector2 jumpVelocity = new Vector2(0, jumpForce);
            rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
            isJump = false;
        }
        void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                anim.SetTrigger("attack");
            }
        }
        /*void Hurt()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                anim.SetTrigger("hurt");
                if (direction == 1)
                    rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
                else
                    rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
            }
        }*/
        void Die()
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                isKickboard = false;
                anim.SetBool("isKickBoard", false);
                anim.SetTrigger("die");
                isDie = true;
            }
        }
        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                isKickboard = false;
                anim.SetBool("isKickBoard", false);
                anim.SetTrigger("idle");
                alive = true;
            }
        }
    }

}