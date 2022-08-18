using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Photon.Pun;

public class PlayerInventoryHolder : InventoryHolder
{
    [SerializeField] protected int secondaryInventorySize;
    [SerializeField] protected InventorySystem secondaryInventorySystem;

    public InventorySystem SecondaryInventorySystem => secondaryInventorySystem;
    public static UnityAction<InventorySystem> OnPlayerBackpackDisplayRequested;

    public PhotonView view;

    public void Start()
    {
        view = GetComponent<PhotonView>();
    }

    protected override void Awake()
    {
        base.Awake();
        secondaryInventorySystem = new InventorySystem(secondaryInventorySize);
    }


    void Update()
    {
        if (view.IsMine)
        {
            if (Keyboard.current.iKey.wasPressedThisFrame) OnPlayerBackpackDisplayRequested?.Invoke(secondaryInventorySystem);
            //Debug.Log("it works atm.");
        }
        //if (Keyboard.current.iKey.wasPressedThisFrame) OnPlayerBackpackDisplayRequested?.Invoke(secondaryInventorySystem);
    }

    public bool AddToInventory(InventoryItemData data, int amount)
    {
        if (InventorySystem.AddToInventory(data, amount))
        {
            return true;
        }
        else if (secondaryInventorySystem.AddToInventory(data, amount))
        {
            return true;
        }
        return false;
    }
}
