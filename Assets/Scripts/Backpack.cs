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
    public TextAsset csvFile; // 需要通过 Unity 编辑器拖拽 CSV 文件

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
        // 初始化背包并加载 CSV 文件中的物品
        LoadItemsFromCSV();
    }

    /// <summary>
    /// 从 CSV 文件读取道具并初始化背包
    /// </summary>
    private void LoadItemsFromCSV()
    {
        if (csvFile == null)
        {
            Debug.LogError("CSV file not assigned! Please assign a valid CSV file.");
            return;
        }

        // 使用 CSVReader 读取道具列表
        List<Item> loadedItems = CSVReader.ReadItemsFromCSV(csvFile);

        // 将读取的道具添加到背包
        foreach (Item item in loadedItems)
        {
            AddItem(item);
        }
    }


    /// <summary>
    /// 添加道具到背包
    /// </summary>
    public void AddItem(Item item)
    {
        items.Add(item);
        CreateItemButton(item);
    }

    /// <summary>
    /// 创建道具对应的 UI 按钮
    /// </summary>
    private void CreateItemButton(Item item)
    {
        GameObject button = Instantiate(itemButtonPrefab, itemButtonParent);
        itemButtonMapping[item] = button;

        // 设置名称
        TextMeshProUGUI nameText = button.transform.Find("NameText")?.GetComponent<TextMeshProUGUI>();
        if (nameText != null) nameText.text = item.name;

        // 设置描述
        TextMeshProUGUI descriptionText = button.transform.Find("DescriptionText")?.GetComponent<TextMeshProUGUI>();
        if (descriptionText != null) descriptionText.text = item.description;

        // 设置数量
        TextMeshProUGUI quantityText = button.transform.Find("QuantityText")?.GetComponent<TextMeshProUGUI>();
        if (quantityText != null) quantityText.text = $"x{item.quantity}";

        // 设置图标
        Image iconImage = button.transform.Find("Button")?.GetComponent<Image>();
        if (iconImage != null)
        {
            iconImage.sprite = item.icon; // 设置图标
        }

        // 绑定点击事件，调用道具的 Use() 方法
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
    /// 更新道具的 UI 显示
    /// </summary>
    public void UpdateItemUI(Item item)
    {
        if (itemButtonMapping.TryGetValue(item, out GameObject button))
        {
            TextMeshProUGUI quantityText = button.transform.Find("QuantityText")?.GetComponent<TextMeshProUGUI>();
            if (quantityText != null)
            {
                quantityText.text = $"x{item.quantity}";
                quantityText.ForceMeshUpdate(); // 强制刷新 UI
            }
        }
    }

    public List<Item> GetItems()
    {
        return items;
    }
}
