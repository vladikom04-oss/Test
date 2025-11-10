using System;
using UnityEngine;

[Serializable]

[CreateAssetMenu(fileName = "Items", menuName = "ScriptableObjects/InventoryItem", order = 1)]
public class ItemData : ScriptableObject
{
    public string id;
    public string name;
    public int maxStack;
    public string iconPath;
}