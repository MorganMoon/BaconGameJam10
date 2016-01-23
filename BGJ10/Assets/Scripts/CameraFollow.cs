using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform tFocus;
	public float fSpeed;
	
	// Update is called once per frame
	void Update () {

		Vector3 vLoc = transform.position;
		vLoc = Vector2.Lerp(vLoc, tFocus.position, fSpeed*Time.deltaTime);
		vLoc.z = -10;
		transform.position = vLoc;
	
	}
	
}
