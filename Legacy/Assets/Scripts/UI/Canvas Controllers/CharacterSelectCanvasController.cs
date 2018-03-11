using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelectCanvasController : CanvasController
{
    private float characterTeamSelectTimer = 150;
    private CharacterTeamManager characterTeamManager;

    [SerializeField]
    private TMP_Text teamCountText;
    [SerializeField]
    private Image characterteamSelectTimerImage;

    private void Start()
    {
        characterTeamManager = GetComponent<CharacterTeamManager>();
    }

    public void UpdateTeamCountText()
    {
        if(characterTeamManager.Team.Count < 6)
        {
            teamCountText.text = characterTeamManager.Team.Count.ToString() + " / 6 Characters chosen";
        }
        else
        {
            teamCountText.text = "Ready";
        }
    }


    private void Update()
    {
        characterTeamSelectTimer -= Time.deltaTime;
        float percenatgeComplete = characterTeamSelectTimer / 150;
        characterteamSelectTimerImage.fillAmount = percenatgeComplete;
    }
}
