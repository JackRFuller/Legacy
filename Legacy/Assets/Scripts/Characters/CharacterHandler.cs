using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : Photon.MonoBehaviour
{
    [SerializeField]
    private Character characterAttributes;
    public Character CharacterAttributes { get { return characterAttributes; } }

    private PlayerInteractionHandler playerInteractionHandler;
    public PlayerInteractionHandler PlayerInteractionHandler { get { return playerInteractionHandler; } }

    private CharacterCanvasController characterCanvasController;
    public CharacterCanvasController GetCharacterCanvasController { get { return characterCanvasController; } }


    private CharacterAnimationHandler characterAnimHandler;
    public CharacterAnimationHandler GetCharacterAnimationHandler { get { return characterAnimHandler; } }

    //Character Action Components    
    private CharacterMovementHandler characterMovementHandler;

    //Character Attributes
    private float currentHealth;
    public float CurrentHealth { get { return currentHealth; } }
    private float currentStamina;
    public float CurrentStamina {  get { return currentStamina; } }

    //Actions
    private bool hasMovedThisTurn = false;



    [Header("Visual Components")]
    [SerializeField]
    private SpriteRenderer selectedSprite;

    private void Start()
    {
        characterAnimHandler = this.GetComponent<CharacterAnimationHandler>();

        AssignStartingAttributes();
    }

    private void AssignStartingAttributes()
    {
        currentHealth = characterAttributes.maxHealth;
        currentStamina = characterAttributes.maxStamina;
    }

    public void CharacterSelectedByPlayer()
    {
        selectedSprite.enabled = true;
    }

    public void CharacterDeselectedByPlayer()
    {
        selectedSprite.enabled = false;
    }

    /// <summary>
    /// Called when player presses on movement action in UI
    /// </summary>
    public void InitiateMovement(CharacterCanvasController _characterCanvasController)
    {
        if(!hasMovedThisTurn)
        {
            if (characterCanvasController == null)
                characterCanvasController = _characterCanvasController;

            if (currentStamina > 0)
            {
                if (characterMovementHandler == null)
                {
                    characterMovementHandler = this.gameObject.AddComponent<CharacterMovementHandler>();
                }

                characterMovementHandler.StartMovementProcess(this);

            }
        }        
    }

    private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Send Data
        if(stream.isWriting)
        {
            stream.SendNext(currentStamina);
        }
        else if(stream.isReading)
        {
            currentStamina = (float)stream.ReceiveNext();
        }
    }

    public void PassInPlayerInteractionHandler(PlayerInteractionHandler _playerInteractionHandler)
    {
        playerInteractionHandler = _playerInteractionHandler;
    }

    public void RemoveStamina(float staminaCost)
    {
        currentStamina -= staminaCost;
    }

    public void HasMovedThisTurn()
    {
        hasMovedThisTurn = true;
    }

    private void ResetTurnActions()
    {
        hasMovedThisTurn = false;
    }

}
