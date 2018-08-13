using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyAfterTimer : MonoBehaviour {

    public float timer;
	void Start () {
        StartCoroutine(KillSelf());
	}
	

    private IEnumerator KillSelf()
    {
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
    }
}
