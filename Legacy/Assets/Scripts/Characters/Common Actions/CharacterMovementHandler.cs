using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovementHandler : MonoBehaviour
{
    private CharacterHandler characterHandler;       

    //Navigation Components
    private NavMeshPath navMeshPath;

    public void StartMovementProcess(CharacterHandler _characterHandler)
    {
        characterHandler = _characterHandler;

        //Disable Player Interaction
        characterHandler.PlayerInteractionHandler.DisablePlayerInteraction();
        navMeshPath = new NavMeshPath();
    }

    private void Update()
    {
        SearchForDestination();
    }

    private void SearchForDestination()
    {
        Ray ray = characterHandler.PlayerInteractionHandler.MainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            NavMesh.CalculatePath(this.transform.position, hit.point, NavMesh.AllAreas, navMeshPath);

            if(navMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                //Calculate Distance
                float pathLength = GetPathLength();

                Debug.Log("Path Length " + pathLength);

                //Calculate Stamina Cost
                float staminaCost = GetStaminaCost(pathLength);

                if (Input.GetMouseButton(0))
                {

                }

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

        float calculatePercenatge = (pathLength / characterHandler.CharacterAttributes.optimalMovementDistancePerTurn);

        staminaCost = characterHandler.CharacterAttributes.optimalMovementDistancePerTurn * calculatePercenatge;


        Debug.Log("Percentage of Optimal Path " + calculatePercenatge);
      
        return staminaCost;
    }

}
