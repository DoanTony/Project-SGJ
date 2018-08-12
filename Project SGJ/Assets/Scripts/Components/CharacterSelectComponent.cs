using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectComponent : MonoBehaviour {

    public PlayersManager playerManager;
    [SerializeField] private List<GameObject> events = new List<GameObject>();

	void Start () {
        playerManager = PlayersManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
