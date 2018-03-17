using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNetworkedCharacter : Photon.MonoBehaviour
{
    public string[] charactersToSpawn;
    public Transform[] characterSpawnPointsPlayerOne;
    public Transform[] characterSpawnPointsPlayerTwo;

    private void OnJoinedRoom()
    {
        SpawnCharacters();
    }

    private void SpawnCharacters()
    {
        Transform[] characterSpawnPoints;

        if (PhotonNetwork.playerList.Length == 1)
        {
            characterSpawnPoints = characterSpawnPointsPlayerOne;
        }
        else
            characterSpawnPoints = characterSpawnPointsPlayerTwo;

        for (int i = 0; i < charactersToSpawn.Length; i++)
        {
            PhotonNetwork.Instantiate(charactersToSpawn[i], characterSpawnPoints[i].position, Quaternion.identity, 0);
        }
    }
	
}
