using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Progress Bar", order = 0) ]
public class ProgressBar : ScriptableObject
{
    [Range(0, 1)]
    public float progress = 0;

    private void Awake()
    {
        progress = 0;
    }

    private void OnEnable()
    {
        progress = 0;
    }

    private void OnDestroy()
    {
        progress = 0;
    }
}
