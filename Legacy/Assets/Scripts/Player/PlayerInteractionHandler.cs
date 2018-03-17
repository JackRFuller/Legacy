using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteractionHandler : MonoBehaviour
{
    private Camera mainCamera;
    public Camera MainCamera {  get { return mainCamera; } }

    //Canvases
    [Header("Components")]
    [SerializeField]
    private CharacterCanvasController characterCanvasController;

    //Character Components
    private Transform characterTransform;
    private PhotonView characterPhotonView;
    private CharacterHandler characterHandler;
   
    private void Start()
    {
        //Components
        mainCamera = this.GetComponent<Camera>();
    }

    private void Update()
    {
        SelectCharacterInput();
    }

    private void SelectCharacterInput()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.transform.tag == "Character")
                {
                    Transform characterTransformTemp = hit.transform;

                    if (characterTransform == null || characterTransform != characterTransformTemp)
                    {
                        characterTransform = characterTransformTemp;
                        characterPhotonView = characterTransform.GetComponent<PhotonView>();
                        characterHandler = characterTransform.GetComponent<CharacterHandler>();

                        //Give character access to interactrion so they can disable it if necessary
                        if (characterHandler.PlayerInteractionHandler == null)
                            characterHandler.PassInPlayerInteractionHandler(this);

                        characterHandler.CharacterSelectedByPlayer();
                        characterCanvasController.EnableCharacterCanvas(characterPhotonView, characterHandler);
                    }
                }
            }
        }
    } 

    private void DeselectCharacter()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            if (characterHandler != null)
            {
                characterHandler.CharacterDeselectedByPlayer();

                characterTransform = null;
                characterPhotonView = null;
                characterHandler = null;
            }
        }
    }

    public void DisablePlayerInteraction()
    {
        this.enabled = false;
    }

    public void EnablePlayerInteraction()
    {
        this.enabled = true;
    }
}
