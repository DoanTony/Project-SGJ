using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode]
public class BuildScene : MonoBehaviour
{
#if UNITY_EDITOR
    public SceneAsset mainScene;
    public List<SceneAsset> additiveScenes = new List<SceneAsset>();
#endif
    [SerializeField] private string buildMainScene;
    [SerializeField] private List<string> buildScenes = new List<string>();
    [SerializeField] private bool isMainSceneLoaded;
    private void Awake()
    {
        AddScenes();
        if (!Application.isEditor)
        {
            StartCoroutine(LoadYourAsyncScene(buildMainScene,LoadSceneMode.Single));
        }
    }

    private void OnValidate()
    {
        AddScenes();
    }

    private void AddScenes()
    {
#if UNITY_EDITOR

        buildScenes.Clear();
        buildMainScene = mainScene.name;
        for (int i = 0; i < additiveScenes.Count; i++)
        {
            buildScenes.Add(additiveScenes[i].name);
        }
#endif
    }

    private void LoadAdditiveScenes()
    {
        for (int i = 0; i < buildScenes.Count; i++)
        {
            LoadYourAsyncScene(buildScenes[i], LoadSceneMode.Additive);
        }
    }

    IEnumerator LoadYourAsyncScene(string _sceneName, LoadSceneMode _mode)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneName, _mode);
        for (int i = 0; i < buildScenes.Count; i++)
        {
            SceneManager.LoadScene(buildScenes[i], LoadSceneMode.Additive);
        }
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
