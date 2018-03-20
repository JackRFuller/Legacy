using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAction : MonoBehaviour
{
    protected CharacterHandler characterHandler;

    public virtual void InitiateAction(CharacterHandler _characterHandler)
    {
        characterHandler = _characterHandler;

        //Disable Player Interaction
        characterHandler.PlayerInteractionHandler.DisablePlayerInteraction();

    }

    // Use this for initialization
    protected virtual void Start ()
    {
		
	}
	
	// Update is called once per frame
	protected virtual void Update ()
    {
		
	}

    protected virtual void CancelAction()
    {
        //Enable Player Interaction
        characterHandler.PlayerInteractionHandler.EnablePlayerInteraction();
    }
}
