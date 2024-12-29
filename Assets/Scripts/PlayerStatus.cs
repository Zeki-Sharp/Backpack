using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance;

    // �������
    public int health = 100;    // ����ֵ
    public int attack = 10;     // ������

    // ��ʾ���Ե� UI Ԫ��
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
        // ��ʼ�� UI ��ʾ
        UpdateUI();
    }

    /// <summary>
    /// ��������ֵ
    /// </summary>
    public void ChangeHealth(int amount)
    {
        health += amount;
        health = Mathf.Max(0, health); // ��ֹ����ֵ���� 0
        UpdateUI();
    }

    /// <summary>
    /// ���ӹ�����
    /// </summary>
    public void ChangeAttack(int amount)
    {
        attack += amount;
        UpdateUI();
    }

    /// <summary>
    /// ���� UI ��ʾ
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
