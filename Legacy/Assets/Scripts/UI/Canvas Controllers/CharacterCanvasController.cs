using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCanvasController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    private GameObject characterActions;

    private CharacterHandler currentChosenCharacter;

    public void EnableCharacterCanvas(PhotonView photonView, CharacterHandler characterHandler)
    {
        this.gameObject.SetActive(true);

        if (photonView.isMine)
        {
            characterActions.SetActive(true);
        }

        currentChosenCharacter = characterHandler;
    }

    public void ONCLICKCharacterMoveAction()
    {
        currentChosenCharacter.InitiateMovement();
    }



}
