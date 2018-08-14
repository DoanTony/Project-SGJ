using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinConditionManager : MonoBehaviour
{

    public PlayerObject player1;
    public PlayerObject player2;
    public bool hasWin = false;
    public Text winText;

    private void Start()
    {
        winText.text = "";
    }
    void Update()
    {
        if (!hasWin)
        {
            if (player1.progressBar.progress >= 1 || player2.progressBar.progress >= 1)
            {
                if (player1.progressBar.progress >= 1)
                {
                    winText.text = player1.selectedCharacter.characterName + " Survived!";
                }
                else if (player2.progressBar.progress >= 1)
                {
                    winText.text = player2.selectedCharacter.characterName + " Survived";
                }
                player1.stopAll = true;
                player2.stopAll = true;
                StopAllCoroutines();
                hasWin = true;
            }
        }
        else if (hasWin)
        {
            if (Input.anyKey || Input.GetButtonDown("J_Dash_P1") || Input.GetButtonDown("J_Dash_P2"))
            {
                player2.progressBar.progress = 0;
                player1.progressBar.progress = 0;
                player1.stopAll = false;
                player2.stopAll = false;
                SceneManager.LoadScene("CharacterSelect");
            }
        }
    }
}
