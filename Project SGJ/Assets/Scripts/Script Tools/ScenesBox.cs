#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

using UnityEditor.SceneManagement;
using UnityEditor;
[ExecuteInEditMode]
public class ScenesBox : MonoBehaviour {
    public List<SceneAsset> scenes = new List<SceneAsset>();
    private void Awake()
    {
        LoadScenes();
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
}
#endif