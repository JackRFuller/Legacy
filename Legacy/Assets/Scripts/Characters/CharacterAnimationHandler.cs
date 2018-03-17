using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimationHandler : MonoBehaviour {

    [SerializeField]
    private Animator characterAnimator;
    

    public void CharacterMovement(float characterSpeed)
    {
        characterAnimator.SetFloat("speed", characterSpeed);
    }
}
