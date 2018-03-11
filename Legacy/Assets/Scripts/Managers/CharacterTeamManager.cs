using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTeamManager : MonoBehaviour
{
    private List<Character> team;
    public List<Character> Team { get { return team; } }

    private void Start()
    {
        team = new List<Character>();
    }

    public void AddCharacterToTeam(Character character)
    {
        team.Add(character);
    }

    public void RemoveCharacterFromTeam(Character character)
    {
        if(team.Contains(character))
        {
            team.Remove(character);
        }
    }

}
