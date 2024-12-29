using UnityEngine;

public class Item
{
    public int id;
    public string name;
    public string effect;
    public int effectQuantity;
    public string description;
    public int quantity;
    public string iconPath; // ͼ��·��
    public Sprite icon;     // ͼ�����

    public Item(int id, string name, string effect, int effectQuantity, string description, int quantity, string iconPath)
    {
        this.id = id;
        this.name = name;
        this.effect = effect;
        this.effectQuantity = effectQuantity;
        this.description = description;
        this.quantity = quantity;
        this.iconPath = iconPath;

        // �� Resources �ļ��м���ͼ��
        this.icon = Resources.Load<Sprite>(iconPath);
        if (this.icon == null)
        {
            Debug.LogWarning($"Failed to load icon for path: {iconPath}");
        }
        else
        {
            Debug.Log($"Successfully loaded icon for path: {iconPath}");
        }
    }

    public void Use()
    {
        if (quantity <= 0)
        {
            Debug.LogWarning($"Cannot use {name}, no items left!");
            return;
        }

        Debug.Log($"Using {name} ({effect}: {effectQuantity}).");

        switch (effect.ToLower())
        {
            case "heal":
                PlayerStatus.Instance.ChangeHealth(effectQuantity);
                break;
            case "powerup":
                PlayerStatus.Instance.ChangeMana(effectQuantity);
                break;
            default:
                Debug.LogWarning($"Unknown effect: {effect}");
                break;
        }

        quantity--;
        Debug.Log($"{name} remaining: {quantity}");

        // ���±��� UI
        Backpack.Instance.UpdateItemUI(this);
    }
}
