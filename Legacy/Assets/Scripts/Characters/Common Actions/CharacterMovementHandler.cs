using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovementHandler : MonoBehaviour
{
    private CharacterHandler characterHandler;  
    

    //Navigation Components
    private NavMeshPath navMeshPath;
    private NavMeshAgent navMeshAgent;

    private MovementState movementState = MovementState.None;
    private enum MovementState
    {
        None,
        FindingDestination,
        MovingTowardsDestination,
    }

    public void StartMovementProcess(CharacterHandler _characterHandler)
    {
        characterHandler = _characterHandler;

        //Disable Player Interaction
        characterHandler.PlayerInteractionHandler.DisablePlayerInteraction();
        navMeshPath = new NavMeshPath();

        //Turn on Target Marker
        characterHandler.PlayerInteractionHandler.GetTargetMarkerHandler.TurnOnTargetMarker();

        if(navMeshAgent == null)
            navMeshAgent = this.GetComponent<NavMeshAgent>();

        movementState = MovementState.FindingDestination;
        this.enabled = true;
    }

    private void Update()
    {
        switch(movementState)
        {
            case MovementState.FindingDestination:
                SearchForDestination();
                break;
            case MovementState.MovingTowardsDestination:
                MoveTowardsPoint();
                break;
        }

      
    }

    private void MoveTowardsPoint()
    {
        characterHandler.GetCharacterAnimationHandler.CharacterMovement(2);
        //Check if we're at destination
        if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            characterHandler.GetCharacterAnimationHandler.CharacterMovement(0);
            characterHandler.PlayerInteractionHandler.GetTargetMarkerHandler.TurnOffTargetMarker();
            movementState = MovementState.None;
            this.enabled = false;
        }
    }

    private void SearchForDestination()
    {
        Ray ray = characterHandler.PlayerInteractionHandler.MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            navMeshPath = new NavMeshPath();

            NavMesh.CalculatePath(this.transform.position, hit.point,NavMesh.AllAreas, navMeshPath);

            if(navMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                //Move Target Marker
                characterHandler.PlayerInteractionHandler.GetTargetMarkerHandler.TurnOnTargetMarker();
                characterHandler.PlayerInteractionHandler.GetTargetMarkerHandler.CreatePath(navMeshPath);

                //Calculate Distance
                float pathLength = GetPathLength();

                //Debug.Log("Path Length " + pathLength);

                //Calculate Stamina Cost
                float staminaCost = GetStaminaCost(pathLength);

                //Update UI
                characterHandler.GetCharacterCanvasController.ShowStaminaCostOnStaminaBar(staminaCost);

                if (Input.GetMouseButton(0))
                {
                    navMeshAgent.destination = hit.point;                   
                    movementState = MovementState.MovingTowardsDestination;

                    characterHandler.RemoveStamina(staminaCost);

                    //Take Away Stamina in UI
                    characterHandler.GetCharacterCanvasController.TakeAwayStaminaCost(staminaCost);
                    characterHandler.PlayerInteractionHandler.EnablePlayerInteraction();
                    characterHandler.HasMovedThisTurn();
                }
            }
            else
            {
               // Debug.Log("Invalid Path");
                characterHandler.PlayerInteractionHandler.GetTargetMarkerHandler.TurnOffPath();
            }

            //Draw Path in Editor
            for (int i = 0; i < navMeshPath.corners.Length - 1; i++)
                Debug.DrawLine(navMeshPath.corners[i], navMeshPath.corners[i + 1], Color.red);
        }
    }

    public float GetPathLength()
    {
        float distance = 0;

        if(navMeshPath.corners.Length == 2)
        {
            distance += Vector3.Distance(transform.position, navMeshPath.corners[1]);
        }
        else
        {
            for (int i = 1; i < navMeshPath.corners.Length - 1; i++)
                distance += Vector3.Distance(navMeshPath.corners[i - 1], navMeshPath.corners[i]);
        }       
        return distance;
    }

    public float GetStaminaCost(float pathLength)
    {
        float staminaCost = 0;

        float percentageOfOptimalDistance = (pathLength / characterHandler.CharacterAttributes.optimalMovementDistancePerTurn) * 100;
        float costOfOptimalDistance = (characterHandler.CharacterAttributes.staminaCostOfFullMovementPerTurn / characterHandler.CharacterAttributes.maxStamina) * 100;
                
        staminaCost = (percentageOfOptimalDistance * 0.01f) * costOfOptimalDistance;
        
        return staminaCost;
    }

}
