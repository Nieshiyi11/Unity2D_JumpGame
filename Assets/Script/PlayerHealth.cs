using JetBrains.Annotations;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 5;
    private float currentHealth;
    [Header("把五个血量Image拖过来")]
    public Image[] hearts;     //数组 在Inspector里把五个心形拖过来
    public Sprite fullHeart;   //满血心形精灵
    public Sprite emptyHeart;  //空白心形精灵
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHeartsUI();
    }
    public void TakeDamage(int damage)  //玩家受到伤害
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);//防止血量超出范围
        UpdateHeartsUI();
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    private void UpdateHeartsUI()   //更新血量UI
    {
        for(int i = 0;i < hearts.Length; i++)
        {
            if(i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
    private void Die()
    {
        Debug.Log("玩家已死亡");
    }
}
    