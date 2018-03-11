using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : Photon.MonoBehaviour
{
    private string connnectionStatus;
    public string ConnectionStatus { get { return connnectionStatus; } }

    public bool debugMode = false;


    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings("legacyServer");
    }

    public void ONCLICKJoinOrCreateLobby()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom("legacyRoom", roomOptions, null);
    }

    public virtual void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        Debug.Log("Number of Players in Room " + PhotonNetwork.playerList.Length);
    }

    public virtual void OnCreatedRoom()
    {
        Debug.Log("Created Room");
    }
}
