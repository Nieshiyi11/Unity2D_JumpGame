using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 3f;  //子弹最多存活3秒自动销毁
    private float direction;
    public void Init(float dir)  //由EnemyManager.cs调用 传入方向
    {
        direction = dir;
        Destroy(gameObject,lifeTime);
    }
    private void Update()
    {
        transform.Translate(Vector2.right*direction*speed*Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerHealth>().TakeDamage(1);
            Destroy(gameObject);
        }
        //碰到地面也销毁
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
