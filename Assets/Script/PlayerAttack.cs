using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("把Attack子对象拖过来")]
    public GameObject attackArea;
    //注意：直接写0.2是错的 因为0.2是double类型 double不可以直接转换成float 所以要写0.2f
    public float attackTime = 0.2f;  //攻击持续时间
    private bool isAttacking = false;
    //攻击动画：
    private Player_AnimationManager playerAnim;
    private void Start()
    {
        playerAnim = GetComponent<Player_AnimationManager>();
        attackArea.SetActive(false); //一开始关闭攻击碰撞
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && !isAttacking)  //GetKeyDown(KeyCode.J):只在按下的那一帧返回true
        {
            StartCoroutine(Attack());  //StartCoroutine()：调用协程（协程可以在执行过程中 “暂停” 适合处理有持续时间的逻辑）
        }
    }
    System.Collections.IEnumerator Attack()//协程的返回值是IEnumerator 要通过yield实现暂停
    {
        isAttacking = true;  //标记为攻击中 锁定输入
        attackArea.SetActive(true);  //开启激活攻击碰撞
        playerAnim.AttackAnimation();  //播放攻击动画  Trigger的特点是触发完毕后自动重置
        yield return new WaitForSeconds(attackTime);//暂停0.2秒（对应攻击动画的关键帧时长）这段时间内attackArea保持激活
        attackArea.SetActive(false); //关闭攻击碰撞
        isAttacking = false;  //解除攻击锁定 允许下次攻击
    }
}