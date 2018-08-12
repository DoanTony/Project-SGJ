using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Player", order = 0)]
public class PlayerObject : ScriptableObject
{
    public CharacterObject selectedCharacter;

    public void SetCharacter(CharacterObject _character)
    {
        selectedCharacter = _character;
    }

}