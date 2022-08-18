using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel; //chest panel
    public DynamicInventoryDisplay playerBackpackPanel;


    private void Awake()
    {
        inventoryPanel.gameObject.SetActive(false);
        playerBackpackPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested += DisplayPlayerBackpack;
    }

    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        PlayerInventoryHolder.OnPlayerBackpackDisplayRequested -= DisplayPlayerBackpack;
    }

    // Update is called once per frame
    void Update()
    {
        if (inventoryPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            inventoryPanel.gameObject.SetActive(false);
        }
        if (playerBackpackPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            playerBackpackPanel.gameObject.SetActive(false);
        }
        //do fucking something here to be able to close the damn inv if we walk away....
        //temp fix where if the player "moves" in any way shape or form then the inventory closes.
        if (inventoryPanel.gameObject.activeInHierarchy && Keyboard.current.wKey.wasPressedThisFrame || inventoryPanel.gameObject.activeInHierarchy && Keyboard.current.aKey.wasPressedThisFrame || inventoryPanel.gameObject.activeInHierarchy && Keyboard.current.sKey.wasPressedThisFrame || inventoryPanel.gameObject.activeInHierarchy && Keyboard.current.dKey.wasPressedThisFrame)
        {
            inventoryPanel.gameObject.SetActive(false);
            Debug.Log("exited the inventory due to player movement detection.");
            //exited the inventory due to player movement detection.
        }
        
    }

    void DisplayInventory(InventorySystem invToDisplay)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(invToDisplay);
    }

    void DisplayPlayerBackpack(InventorySystem invToDisplay)
    {
        playerBackpackPanel.gameObject.SetActive(true);
        playerBackpackPanel.RefreshDynamicInventory(invToDisplay);
    }
}
