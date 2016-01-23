using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    //Control Variables
    private float curSpeed = 0;
    private float maxSpeed = 10;
    public bool grounded;
    private bool jumping = false;
    public bool canDoubleJump;
	
	void FixedUpdate () {
        this.GiveControl();
	}

    ///<summary>
    /// Method GiveControl gives the player control over object
    ///</summary>
    void GiveControl()
    {
        // Side to side/horizontal movement
        GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Lerp(0, curSpeed, 0.8f), GetComponent<Rigidbody2D>().velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            curSpeed += 0.5f;
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            curSpeed -= 0.5f;
        }
        else curSpeed = Mathf.Lerp(curSpeed, 0, 0.1f);

        if (curSpeed <= maxSpeed * -1)
            curSpeed = maxSpeed * -1;
        if (curSpeed >= maxSpeed)
            curSpeed = maxSpeed;

        //Jumpy jump junk
        Debug.DrawLine(new Vector2(transform.position.x, transform.position.y - 0.6f), new Vector2(transform.position.x, transform.position.y - 1.1f), Color.red);
        grounded = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.6f), -Vector2.up, 0.5f);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            canDoubleJump = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 400));
        }
        else if (Input.GetButtonDown("Jump") && !grounded && canDoubleJump == true)
        {
            canDoubleJump = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.x / 2);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 400));
        }
        
    }
}
