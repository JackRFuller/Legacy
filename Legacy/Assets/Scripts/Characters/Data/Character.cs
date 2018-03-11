using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if UNITY_EDITOR
using UnityEditor;
[CreateAssetMenu(fileName = "CharacterData",menuName = "DATA/Character", order = 1)]
#endif
public class Character : ScriptableObject
{
    public string characterName;
}
