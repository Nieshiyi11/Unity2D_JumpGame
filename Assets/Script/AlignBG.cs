using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//背景图2自动对齐背景图1
public class AlignBG : MonoBehaviour
{
    public Transform BG1;
    private void Start()
    {
        float bgWidth = BG1.GetComponent<SpriteRenderer>().bounds.size.x;
        transform.position = new Vector3(BG1.position.x+bgWidth,BG1.position.y,BG1.position.z);
    }
}
