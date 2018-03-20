using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterShootingHandler : CharacterAction
{
    private Transform targetTransform;
    private CharacterHandler targetHandler;


    private ShootingState shootingState = ShootingState.None;
    private enum ShootingState
    {
        None,
        SearchingForTarget,
        ShootingAtTarget,
    }

    public override void InitiateAction(CharacterHandler _characterHandler)
    {
        base.InitiateAction(_characterHandler);

        shootingState = ShootingState.SearchingForTarget;
        characterHandler.GetCharacterAnimationHandler.CharacterAimingShot();
    }

    protected override void Update()
    {
        switch(shootingState)
        {
            case (ShootingState.SearchingForTarget):
                SearchForTarget();
                break;
        }

        
    }

    private void SearchForTarget()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            CancelAction();
            return;
        }

        Ray ray = characterHandler.PlayerInteractionHandler.MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            characterHandler.PlayerInteractionHandler.GetTargetMarkerHandler.TurnOnTargetMarker();
            characterHandler.PlayerInteractionHandler.GetTargetMarkerHandler.ShowShootingTarget(hit.point);

            Vector3 targetPoint = hit.point;

            //Rotate Charcater To Face Target (only on Y)
            Vector3 characterRotation = new Vector3(targetPoint.x,
                                                    this.transform.position.y,
                                                    targetPoint.z);

            this.transform.LookAt(characterRotation);

            if (hit.transform.tag == "Character")
            {
                if(hit.transform != targetTransform || targetTransform == null)
                {
                    targetTransform = hit.transform;
                    targetHandler = targetTransform.GetComponent<CharacterHandler>();
                    characterHandler.GetCharacterCanvasController.SetTargetStats(targetHandler);
                }

                characterHandler.PlayerInteractionHandler.GetTargetMarkerHandler.ShowShootingTarget(hit.transform.position);                

                this.transform.LookAt(characterRotation);

                Ray lineOfSightRay = new Ray(transform.position,hit.transform.position - transform.position);
                RaycastHit lineOfSightHit;

                if(Physics.Raycast(lineOfSightRay, out lineOfSightHit, Mathf.Infinity))
                {
                    Debug.DrawRay(lineOfSightRay.origin, lineOfSightRay.direction, Color.red);
                }                
            }
            else
            {
                characterHandler.GetCharacterCanvasController.TurnTargetStatsOff();
                targetTransform = null;
            }
        }
    }

    protected override void CancelAction()
    {
        base.CancelAction();

        characterHandler.GetCharacterAnimationHandler.CancelShooting();
        shootingState = ShootingState.None;
        characterHandler.PlayerInteractionHandler.GetTargetMarkerHandler.TurnOffTargetMarker();
    }

}
