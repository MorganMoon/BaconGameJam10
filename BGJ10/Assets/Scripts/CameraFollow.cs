using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform tFocus;
	public float fSpeed;

    //Level Ends
    public float top, bottom, left, right;

    void Update()
    {
        
    }
	
	void FixedUpdate () {
		Vector3 vLoc = transform.position;
        if (tFocus)
        {
            vLoc = Vector2.Lerp(vLoc, tFocus.position, fSpeed * Time.deltaTime);
            vLoc.z = -10;
            transform.position = vLoc;
            KeepCameraInLevel();
        }
	
	}
    ///<summary>
    /// Method KeepCameraInLevel does allow the camera to see out of the level
    ///</summary>
    public void KeepCameraInLevel()
    {
        if (transform.position.y > top) gameObject.transform.position = new Vector3(transform.position.x, top, -10);
        if (transform.position.y < bottom) gameObject.transform.position = new Vector3(transform.position.x, bottom, -10);
        if (transform.position.x > right) gameObject.transform.position = new Vector3(right, transform.position.y, -10);
        if (transform.position.x < left) gameObject.transform.position = new Vector3(left, transform.position.y, -10);
    }

    ///<summary>
    /// Method GetMousePos returns the position of the mouse as a Vector2
    ///</summary>
    public Vector2 GetMousePos()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
    }
	
}
