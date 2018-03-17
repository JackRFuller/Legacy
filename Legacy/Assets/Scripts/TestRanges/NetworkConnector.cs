using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkConnector : Photon.MonoBehaviour
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings("legacyServer");
    }

    private void OnConnectedToMaster()
    {
        CreateRoom();
    }

    private void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.JoinOrCreateRoom("legacyRoom", roomOptions, null);
    }

    public virtual void OnCreatedRoom()
    {
        Debug.Log("Created Room");
    }
}


