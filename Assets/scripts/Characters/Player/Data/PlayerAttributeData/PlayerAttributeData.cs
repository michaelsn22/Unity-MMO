using System;
using UnityEngine;


namespace Characters.Player.Data.PlayerAttributeData
{
    [Serializable]
    public class PlayerAttributeData
    {
        #region Levels
        [field: SerializeField] public int CraftingLevel { get; private set; } = 1;
        [field: SerializeField] public int SmithingLevel { get; private set; } = 1;

        [field: SerializeField] public int fishingLevel { get; private set; } = 1;
        

        #endregion

        #region Experience

        [field: SerializeField] public double craftingExperience { get; private set; } = 0.0;
        [field: SerializeField] public double smithingExperience { get; private set; } = 0.0;
        [field: SerializeField] public double fishingExperience { get; private set; } = 0.0;

        #endregion

    }
}