using UnityEngine;

public class LootManager : MonoBehaviour
{
    [SerializeField] private GameObject worldItemPrefab;

    public void SpawnLoot(string itemId, Vector3 position)
    {
        GameObject worldItem = Instantiate(worldItemPrefab, position, Quaternion.identity);
        WorldItemView worldItemView = worldItem.GetComponent<WorldItemView>();
        worldItemView.SetItem(itemId);
    }
}