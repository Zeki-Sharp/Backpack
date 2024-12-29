using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    /// <summary>
    /// 从 CSV 文件读取道具列表
    /// </summary>
    /// <param name="csvFile">包含道具数据的 CSV 文件</param>
    /// <returns>解析后的 Item 列表</returns>
    public static List<Item> ReadItemsFromCSV(TextAsset csvFile)
    {
        List<Item> items = new List<Item>();

        // 检查 CSV 文件是否为空
        if (csvFile == null)
        {
            Debug.LogError("CSV file is null. Please assign a valid CSV file.");
            return items;
        }

        Debug.Log($"CSV File Content:\n{csvFile.text}");

        // 按行读取 CSV 文件
        string[] lines = csvFile.text.Split('\n');
        Debug.Log($"CSV file contains {lines.Length} lines (including header).");

        if (lines.Length <= 1)
        {
            Debug.LogWarning("CSV file contains no data!");
            return items;
        }

        // 遍历每一行（跳过第一行表头）
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            Debug.Log($"Processing line {i + 1}: {line}");

            // 跳过空行或无效行
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) // 忽略空行和注释
            {
                Debug.LogWarning($"Line {i + 1} is empty or invalid. Skipping...");
                continue;
            }

            string[] values = line.Split(',');

            // 检查列数是否足够
            if (values.Length < 7)
            {
                Debug.LogWarning($"Line {i + 1} does not have enough columns. Expected 7, got {values.Length}. Skipping...");
                continue;
            }

            try
            {
                // 打印解析的原始字段
                for (int j = 0; j < values.Length; j++)
                {
                    Debug.Log($"Line {i + 1}, Column {j + 1}: {values[j]}");
                }

                // 解析字段
                int id = int.Parse(values[0].Trim());
                string name = values[1].Trim();
                string effect = values[2].Trim();
                int effectQuantity = int.Parse(values[3].Trim());
                string description = values[4].Trim();
                int quantity = int.Parse(values[5].Trim());
                string iconPath = values[6].Trim();

                // 创建 Item 对象
                Item item = new Item(id, name, effect, effectQuantity, description, quantity, iconPath);
                items.Add(item);

                Debug.Log($"Parsed Item: ID={id}, Name={name}, Effect={effect}, EffectQuantity={effectQuantity}, Quantity={quantity}, IconPath={iconPath}");
            }
            catch (System.FormatException ex)
            {
                Debug.LogError($"FormatException on line {i + 1}: {line}. Exception: {ex.Message}");
            }
            catch (System.Exception ex)
            {
                Debug.LogError($"Error parsing line {i + 1}: {line}. Exception: {ex.Message}");
            }
        }

        Debug.Log($"Successfully loaded {items.Count} items from CSV.");
        return items;
    }
}
