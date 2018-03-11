using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterButton : MonoBehaviour
{
    [SerializeField]
    private CharacterTeamManager characterTeamManager;

    [Header("Button Components")]
    [SerializeField]
    private Character character;
    [SerializeField]
    private Image selectedIcon;
    [SerializeField]
    private TMP_Text characterName;

    private void Start()
    {
        characterName.text = character.characterName;
    }

    public void ONCLICKSelectOrRemoveCharacter()
    {
        
        if(selectedIcon.enabled)
        {
            selectedIcon.enabled = false;
            characterTeamManager.RemoveCharacterFromTeam(character);
        }
        else
        {
            if(characterTeamManager.Team.Count < 6)
            {
                selectedIcon.enabled = true;
                characterTeamManager.AddCharacterToTeam(character);
            }
        }
        

    }
}
