using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    int floorLayer = 1 << 8;

    //Player Control Variables
    private float curSpeed = 0;
    private float maxSpeed = 10;
    private bool grounded;
    private bool jumping = false;
    private bool canDoubleJump;
    Vector2 mousePos = Vector2.zero;

    //Player Variables
    public Weapon weapon;
    public float hp = 100;

    void Start()
    {
    }

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
        Debug.DrawLine(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, transform.position.y - 0.6f), Color.red);
        grounded = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), -Vector2.up, 0.6f, floorLayer);

        if (grounded && Input.GetButtonDown("Jump"))
        {
            canDoubleJump = true;
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 350));
            Debug.Log("Jump");
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + new Vector2(0, 5f);
        }
        else if (!grounded && canDoubleJump == true && Input.GetButtonDown("Jump"))
        {
            canDoubleJump = false;
            //GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y / 2);
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 350));
            Debug.Log("DubJump");
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + new Vector2(0, 8f);
        }
    }
    ///<summary>
    /// Method LookAtMouse causes the player to look at the horizontal side the mouse is at
    ///</summary>
    void LookAtMouse()
    {
        mousePos = Camera.main.GetComponent<CameraFollow>().GetMousePos();

        //if (mousePos.x < transform.position.x)
        //    transform.localScale = new Vector2(-startSize, transform.localScale.y);
        //else transform.localScale = new Vector2(startSize, transform.localScale.y);

        if (mousePos.x < transform.position.x)
            transform.eulerAngles = new Vector3(transform.rotation.x, 180, transform.rotation.z);
        else transform.eulerAngles = new Vector3(transform.rotation.x, 0, transform.rotation.z);
    }

}
