using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundRoll : MonoBehaviour    //这个脚本分别都挂在背景1和背景2上
{
    [Header("拖主相机过来")]
    public Camera mainCamera;  //保存对主相机的引用  记得拖Main Camera对象进MainCamera槽
    private float bgWidth;   //背景宽度
    private void Start()
    {
        GetBgWidth();   //获取游戏背景的宽度
    }
    private void Update()
    {
        BgMove();   //背景循环滚动
    }
    void GetBgWidth() // 固定写法： 获取实际背景宽度
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        bgWidth = spriteRenderer.bounds.size.x;
    }
    void BgMove()
    {
        float distance = mainCamera.transform.position.x - transform.position.x; // 计算相机和背景中心的距离
        if (Mathf.Abs(distance) > bgWidth)    //如果相机离背景中心的距离超过了背景的宽度
        {
            transform.position += Vector3.right * bgWidth * 2 * Mathf.Sign(distance);
        }
    }
}
// Vector3.right：代表向右方向
// bgWidth * 2：代表要跳过两张背景宽度
// sign函数：只返回1或者-1