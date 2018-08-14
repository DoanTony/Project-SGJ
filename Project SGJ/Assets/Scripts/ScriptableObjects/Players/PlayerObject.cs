using UnityEngine;
using UnityEditor;

[CreateAssetMenu(menuName = "Player", order = 0)]
public class PlayerObject : ScriptableObject
{
    public CharacterObject selectedCharacter;
    public ProgressBar progressBar;
    [HideInInspector] public bool stopAll = false;

    public CharacterObject USAChar;
    public CharacterObject RussiaChar;
    public CharacterObject IraqChar;
    public CharacterObject ChinaChar;

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

    public void SetUSA()
    {
        selectedCharacter = USAChar;
    }
    public void SetRussia()
    {
        selectedCharacter = RussiaChar;
    }
    public void SetChina()
    {
        selectedCharacter = ChinaChar;
    }
    public void SetIraq()
    {
        selectedCharacter = IraqChar;
    }
}