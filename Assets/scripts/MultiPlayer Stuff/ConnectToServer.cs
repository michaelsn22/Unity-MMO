using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        //will get called when we connect to sever successfully
        PhotonNetwork.JoinLobby();
        Debug.Log("OnConnectedMaster has been called. We should join lobby now.");
    }

    public override void OnJoinedLobby()
    {
        //once we have joined the lobby, load the scene.
        SceneManager.LoadScene("Lobby");
        //Debug.Log(" "+ Player.ActorNumber);
        //newPlayer.nickname = "Player " + PhotonNetwork.PlayerList.Length;
    }
}
