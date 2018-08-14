using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDComponent : MonoBehaviour {

    public PlayerObject player;
    public Image flag;
    public Slider progressBarHUD;
    public ProgressBar progressBar;
    public Text percentValue;


	void Start () {
        flag.sprite = player.selectedCharacter.flag;
	}
	
	void Update () {
        progressBarHUD.value = progressBar.progress;
        percentValue.text = (progressBar.progress * 100).ToString("0");
    }
}
