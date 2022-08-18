using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRequirements : MonoBehaviour
{
    public string itemName;
    public int craftingLevel;

    public CraftingRequirements(string name, int level)
    {
        itemName = name;
        craftingLevel = level;
    }

    public override string ToString()
    {
        return "Item: " + itemName + "       " + "Item Requirement Level: "  + craftingLevel;
    }
}
