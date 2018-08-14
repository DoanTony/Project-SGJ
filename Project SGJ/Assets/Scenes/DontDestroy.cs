using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour {

    public AudioSource music;

	void Awake () {
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (music)
        {
            if (SceneManager.GetActiveScene().buildIndex > 4)
            {
                if (music.isPlaying)
                {
                    music.Stop();
                }

            }
            else
            {
                if (!music.isPlaying)
                {
                    music.Play();
                }
            }
        }

        if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
    }
}
