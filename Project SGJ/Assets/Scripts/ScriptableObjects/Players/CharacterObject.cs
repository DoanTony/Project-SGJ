using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Character", order = 0)]
public class CharacterObject : ScriptableObject
{
    public GameObject characterPrefab;
    public GameObject transmiterPrefab;

}