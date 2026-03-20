using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector2 minPosition;   //相机能到达的最小边界   
    public Vector2 maxPosition;   //相机能到达的最大边界
    public Transform target;      //相机跟随的目标——玩家
    //2D游戏中：玩家一般在 z = 0  相机必须在 z = -10 才能看到画面
    public Vector3 distance = new Vector3(0, 0, -10);   //相机跟随玩家的距离（偏移量）           
    public float smoothSpeed = 5;    //平滑速度 速度越大跟得越紧
    //相机要在“玩家移动完之后”再跟随  相机要在“玩家移动完之后”再跟随
    private void LateUpdate()  //LateUpdate永远在Update之后执行
    {
        if (target == null)  //如果没拖Player进target槽 就是null
        {
            return;
        }
        //核心跟随逻辑
        Vector3 desiredPos = target.position + distance;  //相机要去的位置 = 玩家位置 + 偏移量
        //Lerp：线性插值
        //transform.position：起点（当前相机位置）
        //desiredPos：终点（目标位置）
        //smoothSpeed*Time.deltaTime： 走多少比例
        Vector3 smoothPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime); //Time.deltaTime = 每一帧的时间
        float clampX = Mathf.Clamp(smoothPos.x, minPosition.x, maxPosition.x);     //控制左右边界
        float clampY = Mathf.Clamp(smoothPos.y, minPosition.y, maxPosition.y);     //控制上下边界
        transform.position = new Vector3(clampX, clampY, distance.z); //把算好的位置赋值给相机。
    }
}