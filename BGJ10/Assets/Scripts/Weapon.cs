using UnityEngine;
using System.Collections;

public enum Colortype
{
    White,
    Pink,
    Blue,
    Green,
    Yellow
}

public class Weapon : MonoBehaviour {

    //Weapon Control Variables
    Vector2 MousePos = Vector2.zero;

    //Weapon Variables
    Colortype type;
    public GameObject shooter;

    //Weapon projectile
    public GameObject smokePuff;
    public GameObject grenade;

    void Start()
    {
        type = Colortype.White;
    }
    void Update()
    {
        ReadyWeapon();
    }
    void FixedUpdate()
    {
        this.Aim();
    }
    ///<summary>
    /// Method SetType sets the weaponType type
    ///</summary>
    public void SetType(Colortype type)
    {
        this.type = type;
    }
    ///<summary>
    /// Method Aim will aim the weapon toward the verticle position of the mouse
    ///</summary>
    void Aim()
    {
        MousePos = Camera.main.GetComponent<CameraFollow>().GetMousePos();
        float arrowAngle = AngleLookAt(MousePos, transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, arrowAngle)), Time.deltaTime * 5);
    }
    ///<summary>
    /// Method AngleLookAt returns a float value for the Euler Angle
    ///</summary>
    public float AngleLookAt(Vector3 p1, Vector3 p2)
    {
        float deltaY = p2.y - p1.y;
        float deltaX = p2.x - p1.x;
        return Mathf.Atan2(deltaY, deltaX) * 180 / Mathf.PI + 180;
    }

    //weapon timers
    float grenadeTimer = 1.5f;

    ///<summary>
    /// Method ReadyWeapon checks for input and fired according to the color of the weapon
    ///</summary>
    void ReadyWeapon()
    {
        if (Input.GetButton("Fire1"))
        {
            switch (type)
            {
                case Colortype.Blue:
                    //laser gun
                    Debug.Log("Shoot blue");
                    break;
                case Colortype.Green:
                    //grenade launcher
                    if (grenadeTimer <= 0)
                    {
                        GameObject projectile2 = Instantiate(grenade, shooter.transform.position, transform.rotation) as GameObject;
                        projectile2.GetComponent<Rigidbody2D>().AddForce(transform.right * 300);
                        grenadeTimer = 1.5f;
                    }
                    break;
                case Colortype.Pink:
                    //pew pew
                    Debug.Log("Shoot pink");
                    break;
                case Colortype.Yellow:
                    //flame thrower
                    Debug.Log("Shoot yellow");
                    break;
                case Colortype.White:
                    GameObject projectile5 = Instantiate(smokePuff, shooter.transform.position, transform.rotation) as GameObject;
                    projectile5.transform.parent = this.transform;
                    break;
                default: Debug.Log("Gun is broken"); break;
            }
        }
        //Weapon timers
        grenadeTimer -= Time.deltaTime;
    }
}
