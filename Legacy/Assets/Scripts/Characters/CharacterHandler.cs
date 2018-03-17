using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHandler : MonoBehaviour
{
    [SerializeField]
    private Character characterAttributes;
    public Character CharacterAttributes { get { return characterAttributes; } }

    private PlayerInteractionHandler playerInteractionHandler;
    public PlayerInteractionHandler PlayerInteractionHandler { get { return playerInteractionHandler; } }

    //Character Action Components    
    private CharacterMovementHandler characterMovementHandler;

    //Character Attributes
    private float currentHealth;
    public float CurrentHealth { get { return currentHealth; } }
    private float currentStamina;
    public float CurrentStamina {  get { return currentStamina; } }


    [Header("Visual Components")]
    [SerializeField]
    private SpriteRenderer selectedSprite;

    private void Start()
    {
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
    public void InitiateMovement()
    {
        if(currentStamina > 0)
        {
            if(characterMovementHandler == null)
            {
                characterMovementHandler = this.gameObject.AddComponent<CharacterMovementHandler>();
            }

            characterMovementHandler.StartMovementProcess(this);

        }
    }

    public void PassInPlayerInteractionHandler(PlayerInteractionHandler _playerInteractionHandler)
    {
        playerInteractionHandler = _playerInteractionHandler;
    }

}
