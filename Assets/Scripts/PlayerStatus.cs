using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance;

    // 玩家属性
    public int health = 100;    // 生命值
    public int mana = 10;     // 

    // 显示属性的 UI 元素
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI manaText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // 初始化 UI 显示
        UpdateUI();
    }

    /// <summary>
    /// 增加生命值
    /// </summary>
    public void ChangeHealth(int amount)
    {
        health += amount;
        health = Mathf.Max(0, health); // 防止生命值低于 0
        UpdateUI();
    }

    /// <summary>
    /// 增加攻击力
    /// </summary>
    public void ChangeMana(int amount)
    {
        mana += amount;
        UpdateUI();
    }

    /// <summary>
    /// 更新 UI 显示
    /// </summary>
    private void UpdateUI()
    {
        if (healthText != null)
        {
            healthText.text = $"Health: {health}";
        }

        if (manaText != null)
        {
            manaText.text = $"Mana: {mana}";
        }
    }
}
