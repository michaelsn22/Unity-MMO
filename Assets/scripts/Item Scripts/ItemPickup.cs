using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.ProBuilder.Shapes;


[RequireComponent(typeof(SphereCollider))]
public class ItemPickup : MonoBehaviourPunCallbacks
{
    public float PickUpRadius = 1f;
    public InventoryItemData ItemData;

    private SphereCollider myCollider;

    PhotonView view;
    
    private void Awake()
    {
        myCollider = GetComponent<SphereCollider>();
        myCollider.isTrigger = true;
        myCollider.radius = PickUpRadius;
    }

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        PhotonView photonView = other.GetComponent<PhotonView>();
        //make some RPC call that tells the server what inventory to add the item to.
        var inventory = other.transform.GetComponent<PlayerInventoryHolder>();
        //if the thing it touches has an inv

        if (!inventory) return;
        
        //if there is an open slot in the inventory, then pick up the object and AddToInventory
        if (inventory.AddToInventory(ItemData, 1))
        {
            //should send to all clients even after join late due to ALLBufferedViaServer
            view.RPC("destroyObject", RpcTarget.AllBufferedViaServer, null);
           // view.RPC("destroyObject", RpcTarget.AllBufferedViaServer, null);

        }
        
    }
    
    [PunRPC] 
    private void destroyObject()
    {
        Destroy(this.gameObject);
    }


}
