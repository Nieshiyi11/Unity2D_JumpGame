using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//销毁敌人
public class AttackEnemy_Die : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyManager>().Die();
        }
    }
}