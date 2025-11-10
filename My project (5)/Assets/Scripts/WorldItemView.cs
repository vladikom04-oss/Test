using System;
using UnityEngine;

public class WorldItemView : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    public string ItemId { get; private set; }
    public void SetItem(string itemId)
    {
        ItemId = itemId;
        ItemData itemData = Resources.Load<ItemData>($"Items/{itemId}");
        if (itemData != null)
        {
            Sprite sprite = Resources.Load<Sprite>(itemData.iconPath);
            spriteRenderer.sprite = sprite;
        }
    }

    public void Collect()
    {
        Destroy(gameObject);
    }
}