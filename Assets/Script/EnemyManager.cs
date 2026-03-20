using UnityEngine;
public class EnemyManager : MonoBehaviour
{
    private Rigidbody2D rb;
    [Header("移动速度和追击速度")]
    public float moveSpeed = 2f;
    public float chaseSpeed = 3.5f;
    [Header("什么时候开始追击")]
    public float chaseDistance = 5f;
    [Header("PointA/B对象拖过来")]
    //A和B代表敌人巡逻的两个目标点
    public Transform pointA;
    public Transform pointB;
    [Header("把player拖过来")]
    public Transform player;
    private bool isDead = false;    //敌人的死亡状态：
    private bool movingToB = true;    //玩家是否该往B点移动？：
    private float currentDirection;   //敌人当前朝向
    //发射子弹
    [Header("把子弹预制体拖过来")]
    public GameObject bulletPrefab;
    public float shootInterval = 2f; //每隔2秒射击一次
    private float shootTimer = 0f; //计时器

    private enum EnemyState  //枚举敌人状态
    {
        Patrol,    //巡逻
        Chase,     //追逐
        Dead       //死亡
    }
    private EnemyState currentState;   //枚举变量：表示当前敌人状态
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
        currentState = EnemyState.Patrol;
    }
    //敌人被击杀后死亡
    public void Die()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
        Destroy(gameObject);
    }
    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab,transform.position,Quaternion.identity);
        bullet.GetComponent<Bullet>().Init(currentDirection);
    }
    private void Update()
    {
        if (isDead)
        {
            return ;
        }
        float distance = Vector2.Distance(transform.position,player.position);
        //必须同时满足【距离够近】&&【玩家在巡逻范围内】才追击
        //玩家在敌人的巡逻范围内：
        bool playerInBounds = player.position.x >= pointA.position.x && player.position.x <= pointB.position.x; 
        if (distance < chaseDistance && playerInBounds)
        {
            currentState = EnemyState.Chase;
        }
        else
        {
            currentState = EnemyState.Patrol;
        }
        //根据状态切换函数
        switch (currentState)
        {
            case EnemyState.Patrol:
                Patrol();
                break;
            case EnemyState.Chase:
                Chase();
                break;
        }
 /*------------------------------------------------------------------------------------------------------------------------*/
        void Patrol()  //巡逻函数
        {
            Transform target;  //方法内的局部变量不需要访问修饰符 只有类成员才需要
            if (movingToB)     //敌人生成在A点 条件位为真就移动去B点
            {
                target = pointB;
            }
            else
            {
                target =pointA;
            }
            // 条件 ? 条件为真时的值 : 条件为假时的值
            currentDirection = movingToB ? 1 : -1;      //A----敌人----------B
            rb.velocity = new Vector2(moveSpeed * currentDirection, rb.velocity.y);   //有速度了敌人才会开始移动
            //确保敌人朝向和它的移动方向一致
            if ((currentDirection > 0 && transform.localScale.x < 0) ||(currentDirection < 0 && transform.localScale.x > 0))
            {
                Flip();
            }
            //确保敌人是守地型
            if ((currentDirection > 0 && transform.position.x >= target.position.x) || (currentDirection < 0 && transform.position.x <= target.position.x))
            {
                movingToB = !movingToB;
                Flip();
            }
        }
 /*------------------------------------------------------------------------------------------------------------------------*/
        void Chase()   //追击函数
        {
            currentDirection = (player.position.x > transform.position.x) ? 1 : -1;  //玩家在右方 敌人向右追 玩家在左方 敌人向左追
            rb.velocity = new Vector2 (chaseSpeed * currentDirection, rb.velocity.y);
            if((currentDirection > 0 && transform.localScale.x < 0)||(currentDirection < 0 && transform.localScale.x > 0)) 
            {
                Flip();  //敌人应该移动的朝向和此时的不同 它的sprite就应该翻转
            }
            //【添】定时射击
            shootTimer += Time.deltaTime;
            if (shootTimer >= shootInterval)
            {
                shootTimer = 0f;
                Shoot();
            }
        }
        void Flip()  //敌人sprite翻转函数
        {
            float originalX = Mathf.Abs(transform.localScale.x);
            float originalY = transform.localScale.y;
            float originalZ = transform.localScale.z;
            if(currentDirection > 0)
            {
                transform.localScale = new Vector3 (originalX,originalY,originalZ);  //向右
            }
            else
            {
                transform.localScale = new Vector3(-originalX,originalY,originalZ);  //向左
            }
        }
    }
}