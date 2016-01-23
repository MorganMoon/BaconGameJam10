using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    int floorLayer = 1 << 8;

    //Control Variables
    private float curSpeed = 0;
    private float maxSpeed = 10;
    public bool grounded;
    private bool jumping = false;
    public bool canDoubleJump;
    Vector2 mousePos = Vector2.zero;

    void Update()
    {
        this.LookAtMouse();
    }
	
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
        grounded = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.6f), -Vector2.up, 0.5f, floorLayer);

        if (Input.GetButtonDown("Jump") && grounded)
        {
            canDoubleJump = true;
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 350));
        }
        else if (Input.GetButtonDown("Jump") && !grounded && canDoubleJump == true)
        {
            canDoubleJump = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y / 2);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 350));
        }
    }
    ///<summary>
    /// Method LookAtMouse causes the player to look at the horizontal side the mouse is at
    ///</summary>
    void LookAtMouse()
    {
        mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y));

        if (mousePos.x < transform.position.x)
            transform.localScale = new Vector2(-1, transform.localScale.y);
        else transform.localScale = new Vector2(1, transform.localScale.y);
    }
}
