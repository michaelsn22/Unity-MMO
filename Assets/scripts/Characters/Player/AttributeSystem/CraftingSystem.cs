using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSystem : Player
{
  public Player myPlayer;
  
  //convert to json at a later point;
  public List<CraftingRequirements> craftingList = new List<CraftingRequirements>();
  #region Levels

  private int crafting;
  private int blackSmithing;
  

  #endregion

  #region Experience

  private double craftingxp = 0.0;
  private double blackSmithingxp = 0.0;
  

  #endregion
  
  public CraftingSystem(Player player)
  {
    myPlayer = player;
    getValues();
    //change to function to load in from json file 
    craftingList.Add(new CraftingRequirements("Sword Shaft", 1));
    craftingList.Add((new CraftingRequirements("Shoes", 2)));
    craftingList.Add(new CraftingRequirements("an item", 1));
   
  }

  public void getValues()
  {
    this.crafting = myPlayer.craftingLevel;
    this.blackSmithing = myPlayer.smithingLevel;
    this.craftingxp = myPlayer.craftingXp;
    this.blackSmithingxp = myPlayer.smithingXp;
  }

  public void addLevel()
  {
    myPlayer.craftingLevel += 1;
    myPlayer.craftingXp += 102.3;
  }

  public bool isCrafting(string CraftingItem, int craftingLevel)
  {
    foreach (CraftingRequirements i in craftingList)
    {
      if (CraftingItem.Equals(i.itemName))
      {
        if (canCraft(craftingLevel, i.craftingLevel))
        {
          Debug.Log("Can Craft; Begin Craft Debug");
          return true;
        }
        if (craftingLevel != i.craftingLevel)
        {
          Debug.Log("You do not have the required Crafting Level");
          return false;
        }
      }
    }
    return false;
  }

  private bool canCraft(int playerLevel, int craftLevel)
  {
    if (craftLevel == playerLevel) return true;

    return false;
  }

  private void loadFromJson()
  {
    //load data from json file for future for all proficiencies
  }
  
}
