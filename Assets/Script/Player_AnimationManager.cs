using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AnimationManager : MonoBehaviour
{
    private Animator anim;     //注意组件是Animator 而不是Animation
    private Rigidbody2D rb;
    private void Awake()
    {
        //从当前挂载该脚本的游戏对象上 获取指定的组件并赋值给变量 这样后续就能通过anim和rb操作动画和物理组件
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();  
    }
    public void MoveAnimation()
    {
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));    //anim.SetFloat("Speed", 值):给Animator参数面板中名为Speed的浮点型参数赋值
    }
    public void JumpAnimation(bool isGround)   //代表踩在地面上或者平台上
    {
        anim.SetBool("isGround", isGround);
    }
    public void AttackAnimation()
    {
        anim.SetTrigger("Attack"); //anim.SetTrigger("参数名")：给Animator中名为Attack/Hurt的触发器参数赋
    }
    public void HurtAnimation()
    {
        anim.SetTrigger("Hurt"); //触发器（Trigger）的特点：触发后会自动重置为未触发状态 适合播放一次性动画(比如攻击动作/受伤动作) 无需手动重置
    }
}
