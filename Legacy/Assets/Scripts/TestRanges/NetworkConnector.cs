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

    private void OnJoinedRoom()
    {
        Debug.Log("Joined Room");

        Vector3 cameraSpawnPosition = Vector3.zero;
        Vector3 cameraSpawnRotation = Vector3.zero;

        if (PhotonNetwork.playerList.Length == 1)
        {
            cameraSpawnPosition = new Vector3(0, 6.75f, -6.88f);
            cameraSpawnRotation = new Vector3(45, 0, 0);
        }
        else
        {
            cameraSpawnPosition = new Vector3(0, 6.75f, 17.93f);
            cameraSpawnRotation = new Vector3(45, 180, 0);
        }

        PhotonNetwork.Instantiate("PlayerCamera", cameraSpawnPosition, Quaternion.Euler(cameraSpawnRotation), 0);
    }

    public virtual void OnCreatedRoom()
    {
       
    }
}


