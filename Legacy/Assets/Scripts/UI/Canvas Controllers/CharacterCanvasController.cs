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

    [Header("Action Buttons")]
    [SerializeField]
    private Button walkButton;
    [SerializeField]
    private Image walkImage;
    [SerializeField]
    private Button shootButton;
    [SerializeField]
    private Image shootImage;

    [Header("Stamina UI Elements")]
    [SerializeField]
    private TMP_Text staminaCostText;
    [SerializeField]
    private Image staminaBarFill;
    [SerializeField]
    private Image staminaBarCost;

    [Header("Target Stats")]
    [SerializeField]
    private GameObject targetStatsObject;
    [SerializeField]
    private Image targetStaminaValueImage;
    [SerializeField]
    private Image targetHealthValueImage;
    [SerializeField]
    private TMP_Text targetNameText;

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

        currentChosenCharacter = characterHandler;

        if (photonView.isMine)
        {
            characterActions.SetActive(true);
            SetButtonStates();
        }
        else
        {
            characterActions.SetActive(false);
        }

        //Turn Off Unnecessary UI Elements
        staminaCostText.enabled = false;
        SetStaminaBar();
        
    }

    public void SetButtonStates()
    {
        if(!currentChosenCharacter.GetHasMovedThisTurn)
        {
            walkButton.enabled = true;
            walkImage.color = Color.white;
        }
        else
        {
            walkButton.enabled = false;
            walkImage.color = Color.grey;
        }

        if (!currentChosenCharacter.GetHasShotThisTurn)
        {
            shootButton.enabled = true;
            shootImage.color = Color.white;
        }
        else
        {
            shootButton.enabled = false;
            shootImage.color = Color.grey;
        }
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

    public void SetTargetStats(CharacterHandler target)
    {
        targetStatsObject.SetActive(true);

        targetNameText.text = target.CharacterAttributes.characterName;

        float healthFillPercentage = target.CurrentHealth / target.CharacterAttributes.maxHealth;
        targetHealthValueImage.fillAmount = healthFillPercentage;

        float staminaFillPercentage = target.CurrentStamina / target.CharacterAttributes.maxStamina;
        targetStaminaValueImage.fillAmount = staminaFillPercentage;
    }

    public void TurnTargetStatsOff()
    {
        targetStatsObject.SetActive(false);
    }

    

    public float CalculateStaminaPercentageCost(float staminaCost)
    {
        float percentageCost = staminaCost / currentChosenCharacter.CharacterAttributes.maxStamina;
        return percentageCost;
    }

    public void ONCLICKCharacterMoveAction()
    {
        currentChosenCharacter.InitiateMovement(this);
    }

    public void ONCLICKCharacterShootAction()
    {
        currentChosenCharacter.InitiateShooting(this);
    }



}
