using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    int floorLayer = 1 << 8;

    //Player Control Variables
    private float curSpeed = 0;
    private float maxSpeed = 10;
    private bool grounded;
    private bool canDoubleJump;
    Vector2 mousePos = Vector2.zero;

    //Player Variables
    public Weapon weapon;
    public float hp = 100;

    void Update()
    {
        this.LookAtMouse();
        if (hp <= 0)
        {
            Die();
        }
        HandleAnimation();
    }
	
	void FixedUpdate () {
        this.GiveControl();
	}

    ///<summary>
    /// Method Die removes the object and does game over stuff
    ///</summary>
    void Die()
    {
        Destroy(this.gameObject);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
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
        else
        {
            curSpeed = Mathf.Lerp(curSpeed, 0, 0.1f); 
        }

        if (curSpeed <= maxSpeed * -1)
            curSpeed = maxSpeed * -1;
        if (curSpeed >= maxSpeed)
            curSpeed = maxSpeed;

        //Jumpy jump junk
        Debug.DrawLine(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, transform.position.y - 0.7f), Color.red);
        grounded = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), -Vector2.up, 0.7f, floorLayer);

        if (grounded && Input.GetButtonDown("Jump"))
        {
            canDoubleJump = true;
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 350));
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity + new Vector2(0, 5f);
        }
        else if (!grounded && canDoubleJump == true && Input.GetButtonDown("Jump"))
        {
            canDoubleJump = false;
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, GetComponent<Rigidbody2D>().velocity.y / 2);
            //GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 350));
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
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            if (mousePos.x < transform.position.x)
                transform.eulerAngles = new Vector3(transform.rotation.x, 180, transform.rotation.z);
            else transform.eulerAngles = new Vector3(transform.rotation.x, 0, transform.rotation.z);
        }

        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 0, transform.rotation.z);
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.eulerAngles = new Vector3(transform.rotation.x, 180, transform.rotation.z);
        }
    }

    ///<summary>
    /// Method HandleAnimation will play the appropriate animation based on input or state of the Player
    ///</summary>
    void HandleAnimation()
    {
        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            if(grounded)
                GetComponent<Animator>().Play("Doodlebob_idle");
            else GetComponent<Animator>().Play("Doodlebob_fall");
        }
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1)
        {
            if (grounded)
                GetComponent<Animator>().Play("Doodlebob_walk");
            else GetComponent<Animator>().Play("Doodlebob_fall");
        }
    }

}
