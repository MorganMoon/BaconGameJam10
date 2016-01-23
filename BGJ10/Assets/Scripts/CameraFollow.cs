using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform tFocus;
	public float fSpeed;
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector3 vLoc = transform.position;
		vLoc = Vector2.Lerp(vLoc, tFocus.position, fSpeed*Time.deltaTime);
		vLoc.z = -10;
		transform.position = vLoc;
	
	}

    ///<summary>
    /// Method GetMousePos returns the position of the mouse as a Vector2
    ///</summary>
    public Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
    }
	
}
