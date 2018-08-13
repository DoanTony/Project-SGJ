using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinConditionManager : MonoBehaviour {

    public PlayerObject player1;
    public PlayerObject player2;
    public bool hasWin = false;
    public Text winText;

    private void Start()
    {
        winText.text = "";
    }
    void Update () {
        if (!hasWin)
            if (player1.progressBar.progress >= 1 || player2.progressBar.progress >= 1)
            {
                if(player1.progressBar.progress >= 1)
                {
                    winText.text = player1.selectedCharacter.characterName + " Win";
                }
                else if(player2.progressBar.progress >= 1)
                {
                    winText.text = player2.selectedCharacter.characterName + " Win";
                }
                player1.stopAll = true;
                player2.stopAll = true;
                StopAllCoroutines();
                hasWin = true;
            }
        }
	}
