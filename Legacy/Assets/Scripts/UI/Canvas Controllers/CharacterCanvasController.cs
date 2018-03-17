using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterCanvasController : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField]
    private GameObject characterActions;

    private CharacterHandler currentChosenCharacter;

    [Header("Stamina UI Elements")]
    [SerializeField]
    private TMP_Text staminaCostText;
    [SerializeField]
    private Image staminaBarFill;
    [SerializeField]
    private Image staminaBarCost;

    //Lerping Attributes
    private float startingValue;
    private float targetValue;
    private float lerpSpeed = 3;
    private float timeStartedLerping;

    private LerpingBar lerpingBar = LerpingBar.None;
    private enum LerpingBar
    {
        None,
        HealthBar,
        StaminaBar,
    }

    public void EnableCharacterCanvas(PhotonView photonView, CharacterHandler characterHandler)
    {
        if(!this.gameObject.activeInHierarchy)
            this.gameObject.SetActive(true);

        lerpingBar = LerpingBar.None;
        if (photonView.isMine)
        {
            characterActions.SetActive(true);
        }
        else
        {
            characterActions.SetActive(false);
        }

        currentChosenCharacter = characterHandler;

        //Turn Off Unnecessary UI Elements
        staminaCostText.enabled = false;

        SetStaminaBar();
    }

    private void SetStaminaBar()
    {
        staminaBarFill.fillAmount = currentChosenCharacter.CurrentStamina / currentChosenCharacter.CharacterAttributes.maxStamina;
        staminaBarCost.fillAmount = staminaBarFill.fillAmount;
    }

    public void ShowStaminaCostOnStaminaBar(float staminaCost)
    {
        if(!staminaCostText.enabled)
            staminaCostText.enabled = true;

        staminaCostText.text = "-" + staminaCost.ToString("F0");
        staminaBarFill.fillAmount = 1 - CalculateStaminaPercentageCost(staminaCost);
    }

    public void TakeAwayStaminaCost(float staminaCost)
    {
        if (!staminaCostText.enabled)
            staminaCostText.enabled = true;

        staminaCostText.text = "-" + staminaCost.ToString("F0");

        staminaBarFill.fillAmount = 1 - CalculateStaminaPercentageCost(staminaCost);

        startingValue = staminaBarCost.fillAmount;
        targetValue = staminaBarFill.fillAmount;

        timeStartedLerping = Time.time;
        lerpingBar = LerpingBar.StaminaBar;

    }

    private void Update()
    {
        switch(lerpingBar)
        {
            case LerpingBar.StaminaBar:
                LerpStaminaBar();
                break;
        }
    }

    private void LerpStaminaBar()
    {
        float timeSinceStarted = Time.time - timeStartedLerping;
        float percenatgeComplete = timeSinceStarted / lerpSpeed;

        float fillAmount = Mathf.Lerp(startingValue, targetValue, percenatgeComplete);
        staminaBarCost.fillAmount = fillAmount;

        if(percenatgeComplete >= 1.0f)
        {
            lerpingBar = LerpingBar.None;
            staminaCostText.enabled = false;
        }
    }

    public void ONCLICKCharacterMoveAction()
    {
        currentChosenCharacter.InitiateMovement(this);
    }

    public float CalculateStaminaPercentageCost(float staminaCost)
    {
        float percentageCost = staminaCost / currentChosenCharacter.CharacterAttributes.maxStamina;
        return percentageCost;
    }



}
