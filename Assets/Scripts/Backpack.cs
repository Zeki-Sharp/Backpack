using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Backpack : MonoBehaviour
{
    public static Backpack Instance;

    private List<Item> items = new List<Item>();
    private Dictionary<Item, GameObject> itemButtonMapping = new Dictionary<Item, GameObject>();

    public Transform itemButtonParent;
    public GameObject itemButtonPrefab;
    public TextAsset csvFile; // ��Ҫͨ�� Unity �༭����ק CSV �ļ�

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

    void Start()
    {
        // ��ʼ������������ CSV �ļ��е���Ʒ
        LoadItemsFromCSV();
    }

    /// <summary>
    /// �� CSV �ļ���ȡ���߲���ʼ������
    /// </summary>
    private void LoadItemsFromCSV()
    {
        if (csvFile == null)
        {
            Debug.LogError("CSV file not assigned! Please assign a valid CSV file.");
            return;
        }

        // ʹ�� CSVReader ��ȡ�����б�
        List<Item> loadedItems = CSVReader.ReadItemsFromCSV(csvFile);

        // ����ȡ�ĵ�����ӵ�����
        foreach (Item item in loadedItems)
        {
            AddItem(item);
        }
    }


    /// <summary>
    /// ��ӵ��ߵ�����
    /// </summary>
    public void AddItem(Item item)
    {
        items.Add(item);
        CreateItemButton(item);
    }

    /// <summary>
    /// �������߶�Ӧ�� UI ��ť
    /// </summary>
    private void CreateItemButton(Item item)
    {
        GameObject button = Instantiate(itemButtonPrefab, itemButtonParent);
        itemButtonMapping[item] = button;

        // ��������
        TextMeshProUGUI nameText = button.transform.Find("NameText")?.GetComponent<TextMeshProUGUI>();
        if (nameText != null) nameText.text = item.name;

        // ��������
        TextMeshProUGUI descriptionText = button.transform.Find("DescriptionText")?.GetComponent<TextMeshProUGUI>();
        if (descriptionText != null) descriptionText.text = item.description;

        // ��������
        TextMeshProUGUI quantityText = button.transform.Find("QuantityText")?.GetComponent<TextMeshProUGUI>();
        if (quantityText != null) quantityText.text = $"x{item.quantity}";

        // ����ͼ��
        Image iconImage = button.transform.Find("Button")?.GetComponent<Image>();
        if (iconImage != null)
        {
            iconImage.sprite = item.icon; // ����ͼ��
        }

        // �󶨵���¼������õ��ߵ� Use() ����
        Button uiButton = button.transform.Find("Button")?.GetComponent<Button>();
        if (uiButton != null)
        {
            uiButton.onClick.AddListener(() =>
            {
                Debug.Log($"Button clicked for item: {item.name}");
                item.Use();
            });
        }
        else
        {
            Debug.LogWarning("Button component not found on ItemButtonPrefab.");
        }
    }

    /// <summary>
    /// ���µ��ߵ� UI ��ʾ
    /// </summary>
    public void UpdateItemUI(Item item)
    {
        if (itemButtonMapping.TryGetValue(item, out GameObject button))
        {
            TextMeshProUGUI quantityText = button.transform.Find("QuantityText")?.GetComponent<TextMeshProUGUI>();
            if (quantityText != null)
            {
                quantityText.text = $"x{item.quantity}";
                quantityText.ForceMeshUpdate(); // ǿ��ˢ�� UI
            }
        }
    }

    public List<Item> GetItems()
    {
        return items;
    }
}
