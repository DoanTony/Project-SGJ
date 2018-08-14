using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour {

    public float delayAllowSkip;
    private bool canSkip = false;


    private void Start()
    {
        StartCoroutine(DelaySkipAllow());
    }

    void Update () {
        if (canSkip)
        {
            if (Input.anyKey || Input.GetButtonDown("J_Dash_P1") || Input.GetButtonDown("J_Dash_P2"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
	}

    private IEnumerator DelaySkipAllow()
    {
        yield return new WaitForSeconds(delayAllowSkip);
        canSkip = true;
    }


}
