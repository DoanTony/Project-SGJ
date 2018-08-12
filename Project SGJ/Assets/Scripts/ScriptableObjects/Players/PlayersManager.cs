using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Manager/Players", order = 2 )]
public class PlayersManager : ScriptableObject {

    #region Singleton
    public static PlayersManager Instance;

    public uint nbPlayers = 2;


    private void OnEnable()
    {
        if (!Instance)
        {
            Instance = FindObjectOfType<PlayersManager>();
        }
        if (!Instance)
        {
            Instance = this;
        }
    }
    #endregion
}
