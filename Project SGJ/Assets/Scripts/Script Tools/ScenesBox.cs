using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine;

[ExecuteInEditMode]
public class ScenesBox : MonoBehaviour {
    public List<SceneAsset> scenes = new List<SceneAsset>();

    private void Awake()
    {
        LoadScenes();
        StartCoroutine(Execute());
    }

    private void OnValidate()
    {
      //  LoadScenes();
    }

    private void LoadScenes()
    {
        if (EditorApplication.isPlaying)
        {
            return;
        }
        foreach (SceneAsset scene in scenes)
        {
            if (scene)
            {
                EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(scene), OpenSceneMode.Additive);
            }
        }

    }

    public IEnumerator Execute()
    {
        EditorSceneManager.preventCrossSceneReferences = false;
        yield break;
    }
}
