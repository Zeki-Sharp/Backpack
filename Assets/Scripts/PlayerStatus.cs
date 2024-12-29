using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance;

    // 玩家属性
    public int health = 100;    // 生命值
    public int attack = 10;     // 攻击力

    // 显示属性的 UI 元素
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI attackText;

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
    public void ChangeAttack(int amount)
    {
        attack += amount;
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

        if (attackText != null)
        {
            attackText.text = $"Attack: {attack}";
        }
    }
}
