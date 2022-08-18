using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticInventoryDisplay : InventoryDisplay
{

    [SerializeField] private InventoryHolder inventoryHolder;
    //public GameObject playerPrefab;
    [SerializeField] private InventorySlot_UI[] slots;

    protected override void Start()
    {
        base.Start();
        
        //find the player inventory upon startup
        inventoryHolder = GameObject.Find("Player(Clone)").GetComponent<InventoryHolder>();
        inventorySystem = inventoryHolder.InventorySystem;
        inventorySystem.OnInventorySlotChanged += UpdateSlot;
        /*
        if (inventoryHolder != null)
        {
            inventorySystem = inventoryHolder.InventorySystem;
            inventorySystem.OnInventorySlotChanged += UpdateSlot;
        }
        else Debug.LogWarning($"No inventory assigned to {this.gameObject}");
        */

        AssignSlot(InventorySystem); //pass in the inventory holders inventory system.
    }
    public override void AssignSlot(InventorySystem invToDisplay)
    {
        slotDictionary = new Dictionary<InventorySlot_UI, InventorySlot>();

        if (slots.Length != inventorySystem.InventorySize) Debug.Log($"Inventory slots out of sync on {this.gameObject}");


        for (int i=0; i<inventorySystem.InventorySize; i++)
        {
            //key value pair
            slotDictionary.Add(slots[i], inventorySystem.InventorySlots[i]);
            //initialize the ui slot
            slots[i].Init(inventorySystem.InventorySlots[i]);
        }
    }
}
