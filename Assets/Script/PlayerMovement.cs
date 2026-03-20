using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("移动参数")]
    public float moveSpeed = 5;
    public float jumpForce = 6;
    private Rigidbody2D rb;
    private int jumpCount = 0;
    private int maxJumpCount = 2;
    //动画：
    /*添：*/ private Player_AnimationManager playerAnim;
    private bool isGround = false;   //是否在地面或者平台上
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Player_AnimationManager>();
    }
    private void Update()
    {
        Move();
        Jump();
        //播放状态动画：
        playerAnim.MoveAnimation();   //移动动画
        playerAnim.JumpAnimation(isGround);   //跳跃动画
    }
    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);
        //角色翻转
        float originalX = Mathf.Abs(transform.localScale.x);
        float originalY = transform.localScale.y;
        float originalZ = transform.localScale.z;
        if (x > 0)  //右移
        {
            transform.localScale = new Vector3(originalX,originalY,originalZ);
        }else if (x < 0)  //左移
        {
            transform.localScale = new Vector3(-originalX,originalY,originalZ);
        }
    }
    void Jump()
    {
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
            isGround = false; //起跳以后一定是先离开地面
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            jumpCount = 0;
            isGround = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            isGround = false;
        }
    }
}