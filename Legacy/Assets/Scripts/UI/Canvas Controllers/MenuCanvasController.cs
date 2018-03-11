using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuCanvasController : CanvasController
{
    [Header("Menu Screen")]
    [SerializeField]
    private TMP_Text gameTitleText;
    [SerializeField]
    private TMP_Text gameSubTitleText;

    [Header("Join Lobby")]
    [SerializeField]
    private GameObject joinLobbyButton;
    [SerializeField]
    private TMP_Text joinLobbyDebugText;
    [SerializeField]
    private TMP_Text waitingForPlayerText;

    [Header("Character Select Menu")]
    [SerializeField]
    private GameObject characterSelectMenuPrefab;
    

    public virtual void OnConnectedToMaster()
    {
        StartCoroutine(ShowJoinOrCreateLobbyButton());
    }

    public void ONCLICKJoinOrCreateLobby()
    {
        joinLobbyButton.SetActive(false);
    }

    private void Update()
    {
        if (PhotonNetwork.connectionState != ConnectionState.Disconnected)
            joinLobbyDebugText.text = PhotonNetwork.connectionState.ToString();
        else
            this.enabled = false;
    }

    IEnumerator ShowJoinOrCreateLobbyButton()
    {
        yield return new WaitForSeconds(0.2f);
        joinLobbyDebugText.enabled = false;
        joinLobbyButton.SetActive(true);
    }

    public virtual void OnJoinedRoom()
    {
        
        gameTitleText.enabled = false;
        gameSubTitleText.enabled = false;

       
        if (PhotonNetwork.playerList.Length < 2)
        {
            waitingForPlayerText.enabled = true;
        }
        else
        {
            StartCoroutine(ShowCharacterSelectMenu());
        }
    }

    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.Log("Player Joined");
        waitingForPlayerText.text = "Player Joined";
        StartCoroutine(ShowCharacterSelectMenu());
    }

    IEnumerator ShowCharacterSelectMenu()
    { 
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
        GameObject characterSelectMenu = Instantiate(characterSelectMenuPrefab, this.transform.parent.root) as GameObject;
    }
}
