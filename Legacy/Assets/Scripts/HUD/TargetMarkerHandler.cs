using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetMarkerHandler : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private GameObject pathCornerPrefab;
    [SerializeField] 
    private LineRenderer pathLineRenderer;

    private List<GameObject> pathCorners;
    
    private void Start()
    {
        pathCorners = new List<GameObject>();

        spriteRenderer = this.GetComponent<SpriteRenderer>();
        pathLineRenderer.enabled = false;

        TurnOffPath();
    }

    public void TurnOnTargetMarker()
    {
        spriteRenderer.enabled = true;
    }

    public void TurnOffTargetMarker()
    {
        spriteRenderer.enabled = false;
    }

    public void MoveTargetMarker(Vector3 targetPosition)
    {
        Vector3 newPosition = new Vector3(targetPosition.x, targetPosition.y + 0.1f, targetPosition.z);
        this.transform.position = newPosition;
    }

    public void CreatePath(NavMeshPath path)
    {
        //int numberOfPathCorners = path.corners.Length - 2;

        //if(numberOfPathCorners > 0 && numberOfPathCorners > pathCorners.Count)
        //{
        //    //Calculate Difference
        //    float difference = numberOfPathCorners - pathCorners.Count;

        //    for (int i = 0; i < difference; i++)
        //    {
        //        GameObject corner = Instantiate(pathCornerPrefab) as GameObject;
        //        pathCorners.Add(corner);
        //    }
        //}

        ////Position Path Corners
        //for(int i = 0; i < pathCorners.Count; i++)
        //{
        //    if(i < numberOfPathCorners)
        //    {
        //        pathCorners[i].transform.position = new Vector3(path.corners[i].x,
        //                                                        path.corners[i].y + 0.1f,
        //                                                        path.corners[i].z);

        //        pathCorners[i].SetActive(true);
        //    }
        //    else
        //    {
        //        pathCorners[i].SetActive(false);
        //    }
        //}

        //if (pathLineRenderer.enabled == false)
        //    pathLineRenderer.enabled = true;

        //pathLineRenderer.SetPositions(path.corners);

        //Position End Target
        Vector3 newPosition = new Vector3(path.corners[path.corners.Length - 1].x,
                                          path.corners[path.corners.Length - 1].y + 0.1f,
                                          path.corners[path.corners.Length - 1].z);

        this.transform.position = newPosition;
    }

    public void TurnOffPath()
    {
        //if(pathLineRenderer == null)
        //    pathLineRenderer = this.GetComponent<LineRenderer>();

        //pathLineRenderer.enabled = false;

        //for(int i = 0; i < pathCorners.Count; i++)
        //{
        //    pathCorners[i].SetActive(false);
        //}

        spriteRenderer.enabled = false;
    }
}
