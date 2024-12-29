using System.Collections.Generic;
using UnityEngine;

public class BackpackInitializer : MonoBehaviour
{
    public TextAsset csvFile; // �� CSV �ļ��ϵ����ֶ�
    public Backpack backpack; // ������� Backpack �ű�

    void Start()
    {
        // ���� CSV ���ݲ����ݵ� Backpack
        List<Item> items = LoadItemsFromCSV(csvFile);
        InitializeBackpack(items);
    }

    /// <summary>
    /// �� CSV �ļ����ص����б�
    /// </summary>
    /// <param name="csvFile">�����������ݵ� CSV �ļ�</param>
    /// <returns>�����б�</returns>
    private List<Item> LoadItemsFromCSV(TextAsset csvFile)
    {
        List<Item> items = new List<Item>();
        string[] lines = csvFile.text.Split('\n'); // ���ж�ȡ
        for (int i = 1; i < lines.Length; i++) // ����������
        {
            string[] values = lines[i].Split(',');

            // ȷ����������ȷ
            if (values.Length < 6) continue;

            // �����ֶ�
            int id = int.Parse(values[0].Trim());
            string name = values[1].Trim();
            string effect = values[2].Trim();
            int effectQuantity = int.Parse(values[3].Trim());
            string description = values[4].Trim();
            int quantity = int.Parse(values[5].Trim());
            string iconPath = values[6].Trim();

            // ���� Item ʵ��
            Item item = new Item(id, name, effect, effectQuantity, description, quantity, iconPath);
            items.Add(item);
        }

        Debug.Log($"Loaded {items.Count} items from CSV.");
        return items;
    }

    /// <summary>
    /// ��ʼ���������������б���ӵ�������
    /// </summary>
    /// <param name="items">�����б�</param>
    private void InitializeBackpack(List<Item> items)
    {
        foreach (Item item in items)
        {
            backpack.AddItem(item); // ���� Backpack �� AddItem ����
        }
    }
}