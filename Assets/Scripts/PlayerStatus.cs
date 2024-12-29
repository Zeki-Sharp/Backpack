using TMPro;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance;

    // �������
    public int health = 100;    // ����ֵ
    public int mana = 10;     // 

    // ��ʾ���Ե� UI Ԫ��
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
    public void ChangeMana(int amount)
    {
        mana += amount;
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

        if (manaText != null)
        {
            manaText.text = $"Mana: {mana}";
        }
    }
}
