using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public PlayerInventorySlot slotPrefab;
    public List<ItemTempData> inventory = new List<ItemTempData>();
    public bool isOpen;
    public Transform panel;
    public void ToggleInventory()
    {
        isOpen = !isOpen;
        panel.gameObject.SetActive(isOpen);
    }
    public void AddItem(GroundItem item)
    {
        ItemTempData newItem = new ItemTempData();
        newItem.itemName = item.itemName;
        newItem.icon = item.icon;
        inventory.Add(newItem);

        PlayerInventorySlot slot = Instantiate(slotPrefab, panel);
        slot.itemName.text = newItem.itemName;
        slot.icon.sprite = newItem.icon;

        item.UnloadObjectServerRpc();
    }
}

[System.Serializable]
public class ItemTempData
{
    public string itemName;
    public Sprite icon;
}
