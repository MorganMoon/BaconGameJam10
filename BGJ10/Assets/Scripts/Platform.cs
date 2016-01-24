using UnityEngine;
using System.Collections;

public class Platform : MonoBehaviour {

    public bool horizontal = true;
	void Start () {
        if(horizontal)
            GetComponent<Renderer>().material.mainTextureScale = new Vector2(transform.localScale.x, 1);
        else
            GetComponent<Renderer>().material.mainTextureScale = new Vector2(1, transform.localScale.y);
	}

}
