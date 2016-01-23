using UnityEngine;
using System.Collections;

public class ParticleRemover : MonoBehaviour {
	void Update () {
        if (!GetComponent<ParticleSystem>().IsAlive())
            Destroy(gameObject);
	}
}
