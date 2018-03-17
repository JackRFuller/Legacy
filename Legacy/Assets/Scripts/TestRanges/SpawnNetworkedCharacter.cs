using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNetworkedCharacter : Photon.MonoBehaviour
{
    public string[] charactersToSpawn;
    public Transform[] characterSpawnPoints;

    private void OnCreatedRoom()
    {
        SpawnCharacters();
    }

    private void SpawnCharacters()
    {
        for (int i = 0; i < charactersToSpawn.Length; i++)
        {
            PhotonNetwork.Instantiate(charactersToSpawn[i], characterSpawnPoints[i].position, Quaternion.identity, 0);
        }
    }
	
}
