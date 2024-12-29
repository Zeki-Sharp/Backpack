using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    /// <summary>
    /// �� CSV �ļ���ȡ�����б�
    /// </summary>
    /// <param name="csvFile">�����������ݵ� CSV �ļ�</param>
    /// <returns>������� Item �б�</returns>
    public static List<Item> ReadItemsFromCSV(TextAsset csvFile)
    {
        List<Item> items = new List<Item>();

        // ��� CSV �ļ��Ƿ�Ϊ��
        if (csvFile == null)
        {
            Debug.LogError("CSV file is null. Please assign a valid CSV file.");
            return items;
        }

        Debug.Log($"CSV File Content:\n{csvFile.text}");

        // ���ж�ȡ CSV �ļ�
        string[] lines = csvFile.text.Split('\n');
        Debug.Log($"CSV file contains {lines.Length} lines (including header).");

        if (lines.Length <= 1)
        {
            Debug.LogWarning("CSV file contains no data!");
            return items;
        }

        // ����ÿһ�У�������һ�б�ͷ��
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            Debug.Log($"Processing line {i + 1}: {line}");

            // �������л���Ч��
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#")) // ���Կ��к�ע��
            {
                Debug.LogWarning($"Line {i + 1} is empty or invalid. Skipping...");
                continue;
            }

            string[] values = line.Split(',');

            // ��������Ƿ��㹻
            if (values.Length < 7)
            {
                Debug.LogWarning($"Line {i + 1} does not have enough columns. Expected 7, got {values.Length}. Skipping...");
                continue;
            }

            try
            {
                // ��ӡ������ԭʼ�ֶ�
                for (int j = 0; j < values.Length; j++)
                {
                    Debug.Log($"Line {i + 1}, Column {j + 1}: {values[j]}");
                }

                // �����ֶ�
                int id = int.Parse(values[0].Trim());
                string name = values[1].Trim();
                string effect = values[2].Trim();
                int effectQuantity = int.Parse(values[3].Trim());
                string description = values[4].Trim();
                int quantity = int.Parse(values[5].Trim());
                string iconPath = values[6].Trim();

                // ���� Item ����
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
