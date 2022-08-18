using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Temp : MonoBehaviour
{
    public Player player;

    public GameObject craftingInventory;

    void Start()
    {
        player = GameObject.Find("Player(Clone)").GetComponent<Player>();
    }
    public void OpenQuestWindow()
    {
        //do we want to check the inventory and compare those items to the enum of crafting lists. if the items
        // required are in the inventory show the option to craft?
        //or do we want them to select an item and it'll display can craft or not cause items req are in the inventory.
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        craftingInventory.SetActive(true);
        player.CraftingSystem.isCrafting("", 3);
    }

    public void AcceptQuest()
    {
        craftingInventory.SetActive(false);
        Debug.Log("crafting begin");
        //player.quest += questList;

    }
    public void CloseQuestWindow()
    {
        craftingInventory.SetActive(false);
    }
}
