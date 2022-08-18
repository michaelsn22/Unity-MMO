using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;


[System.Serializable]
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;

    public List<InventorySlot> InventorySlots => inventorySlots;
    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        //make enough slots to fill the inv system
        //create a slot list
        inventorySlots = new List<InventorySlot>(size);

        for (int i=0; i < size; i++){
            inventorySlots.Add(new InventorySlot());
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        if (ContainsItem(itemToAdd, out List<InventorySlot> invSlot)) //check whether item exists in inventory
        //if it contains, get a list of all the slots that have that item in it.
        {
            foreach(var slot in invSlot)
            {
                //check the list for a slot that has room in it
                if(slot.RoomLeftInStack(amountToAdd))
                {
                    Debug.Log("Added to an inventory!");
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
        }

        if (HasFreeSlot(out InventorySlot freeSlot)) //gets first available slot
        {
            Debug.Log("Added to an inventory since we have a free slot!");
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }
        //if inv doesnt contain item and doesnt have free slot
        return false;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlot)
    {
        //comes from system.linq functionality
        //checks all inv slots, and create a list of inv slots and fill it WHERE the item or 'i'.data is equal to the item we want to add
        //then just put that in a list.
        invSlot = InventorySlots.Where(i => i.ItemData == itemToAdd).ToList();
        //check if contains item
        return invSlot == null ? false : true;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null);
        return freeSlot == null ? false : true;
    }
    
}
