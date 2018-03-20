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

    public void CharacterAimingShot()
    {
        characterAnimator.SetBool("cancelledShooting", false);
        characterAnimator.SetBool("drawBow",true);
    }

    public void CharacterShooting()
    {
        characterAnimator.SetBool("shooting", true);
    }

    public void ResetCharacterShootingAnims()
    {
        characterAnimator.SetBool("drawBow", false);
        characterAnimator.SetBool("shooting", false);
    }

    public void CancelShooting()
    {
        characterAnimator.SetBool("cancelledShooting", true);
        characterAnimator.SetBool("drawBow", false);
        characterAnimator.SetBool("shooting", false);
    }
}
