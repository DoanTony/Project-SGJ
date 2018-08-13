using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Player", order = 0)]
public class PlayerObject : ScriptableObject
{
    public CharacterObject selectedCharacter;
    public ProgressBar progressBar;
    [HideInInspector] public bool stopAll = false;

    public void SetCharacter(CharacterObject _character)
    {
        selectedCharacter = _character;
    }

    private void Awake()
    {
        stopAll = false;
    }

    private void OnEnable()
    {
        stopAll = false;
    }

    private void OnDestroy()
    {
        stopAll = false;
    }
}