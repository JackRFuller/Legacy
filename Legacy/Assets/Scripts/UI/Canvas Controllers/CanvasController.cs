using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : Photon.MonoBehaviour
{
    protected NetworkManager networkManager;

    private void Start()
    {
        networkManager = transform.parent.root.GetComponent<NetworkManager>();
    }

    protected virtual void EnableCanvasController()
    {
        this.gameObject.SetActive(true);
    }

    protected virtual void DisableCanvasController()
    {
        this.gameObject.SetActive(false);
    }
	
}
