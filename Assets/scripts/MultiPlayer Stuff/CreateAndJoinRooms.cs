using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInput;
    public TMP_InputField joinInput;

    public void CreateRoom()
    {
        //note: when you create a room you automatically join the room as well.
        PhotonNetwork.CreateRoom(createInput.text);
        Debug.Log("Creating room!");
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
        Debug.Log("Joining room!");
    }
//THIS MUST BE A FUCKING OVERRIDE FOR FUCKS SAKE OR IT CAUSES ALL HELL TO BREAK LOOSE!!@@!@!@!!@@!
    public override void OnJoinedRoom()
    {
        //will get called when we join a room
        //currently local on fresh join; if local therefore itll be a new scene however multiplayer is active.
        // RPC handles the object load in however this is causing issues on person joining
        //rpc handles server talk what gets picked up etc;
        PhotonNetwork.LoadLevel("Default");
        //this is what we will need to call in the future to load levels n stuff.
    }
}
