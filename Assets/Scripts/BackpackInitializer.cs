using System.Collections.Generic;
using UnityEngine;

public class BackpackInitializer : MonoBehaviour
{
    public TextAsset csvFile; // 将 CSV 文件拖到此字段
    public Backpack backpack; // 关联你的 Backpack 脚本

    void Start()
    {
        // 加载 CSV 数据并传递到 Backpack
        List<Item> items = LoadItemsFromCSV(csvFile);
        InitializeBackpack(items);
    }

    /// <summary>
    /// 从 CSV 文件加载道具列表
    /// </summary>
    /// <param name="csvFile">包含道具数据的 CSV 文件</param>
    /// <returns>道具列表</returns>
    private List<Item> LoadItemsFromCSV(TextAsset csvFile)
    {
        List<Item> items = new List<Item>();
        string[] lines = csvFile.text.Split('\n'); // 按行读取
        for (int i = 1; i < lines.Length; i++) // 跳过标题行
        {
            string[] values = lines[i].Split(',');

            // 确保行数据正确
            if (values.Length < 6) continue;

            // 解析字段
            int id = int.Parse(values[0].Trim());
            string name = values[1].Trim();
            string effect = values[2].Trim();
            int effectQuantity = int.Parse(values[3].Trim());
            string description = values[4].Trim();
            int quantity = int.Parse(values[5].Trim());
            string iconPath = values[6].Trim();

            // 创建 Item 实例
            Item item = new Item(id, name, effect, effectQuantity, description, quantity, iconPath);
            items.Add(item);
        }

        Debug.Log($"Loaded {items.Count} items from CSV.");
        return items;
    }

    /// <summary>
    /// 初始化背包，将道具列表添加到背包中
    /// </summary>
    /// <param name="items">道具列表</param>
    private void InitializeBackpack(List<Item> items)
    {
        foreach (Item item in items)
        {
            backpack.AddItem(item); // 调用 Backpack 的 AddItem 方法
        }
    }
}