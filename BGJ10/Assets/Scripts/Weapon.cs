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
    int laserLayerMask = 1 << 9;

    //Weapon projectile
    public GameObject smokePuff;
    public GameObject grenade;
    private LineRenderer laserBeam;
    public GameObject laserHitEffect;
    public GameObject bullet;
    public GameObject flames;

    //Weapon position stuff
    public Transform player;

    void Awake()
    {
        laserBeam = gameObject.GetComponent<LineRenderer>();
    }
    void Start()
    {
        type = Colortype.White;
        laserLayerMask = ~laserLayerMask;
    }
    void Update()
    {
        ReadyWeapon();
        PositionFollow(player);
    }
    void FixedUpdate()
    {
        this.Aim();
    }
    public void PositionFollow(Transform followed)
    {
        this.transform.position = followed.position;
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
    float grenadeTimer = 1f;
    float laserTimer = 1.5f;
    float bulletTimer = 0.5f;
    float flameTimer = 1f;

    ///<summary>
    /// Method ReadyWeapon checks for input and fires according to the color of the weapon
    ///</summary>
    void ReadyWeapon()
    {
        if (Input.GetButton("Fire1"))
        {
            switch (type)
            {
                case Colortype.Blue:
                    //laser gun
                    if (laserTimer > 0)
                    {
                        RaycastHit2D end = Physics2D.Raycast(shooter.transform.position, transform.right, 1000, laserLayerMask);
                        laserBeam.enabled = true;
                        laserBeam.SetPosition(0, shooter.transform.position);
                        if (Physics2D.Raycast(shooter.transform.position, transform.right, 1000, laserLayerMask))
                        {
                            laserBeam.SetPosition(1, end.point);
                            Instantiate(laserHitEffect, end.point, transform.rotation);
                        }
                        else laserBeam.SetPosition(1, Camera.main.GetComponent<CameraFollow>().GetMousePos());
                        if (end.collider != null && end.collider.gameObject.tag == "Enemy" && end.collider.gameObject.GetComponent<Enemy>().type == Colortype.Blue)
                        {
                            end.collider.gameObject.GetComponent<Enemy>().hp -= 1;
                        }
                    }
                    else
                    {
                        laserBeam.enabled = false;
                    }
                    laserTimer -= Time.deltaTime;
                    break;
                case Colortype.Green:
                    //grenade launcher
                    if (grenadeTimer <= 0)
                    {
                        GameObject projectile2 = Instantiate(grenade, shooter.transform.position, transform.rotation) as GameObject;
                        projectile2.GetComponent<Rigidbody2D>().AddForce(transform.right * 200);
                        grenadeTimer = 1.5f;
                    }
                    break;
                case Colortype.Pink:
                    //pew pew
                    if (bulletTimer <= 0)
                    {
                        GameObject projectile3 = Instantiate(bullet, shooter.transform.position, transform.rotation) as GameObject;
                        projectile3.GetComponent<Rigidbody2D>().AddForce(transform.right * 850);
                        bulletTimer = 0.5f;
                    }
                    break;
                case Colortype.Yellow:
                    //flame thrower
                    if (flameTimer <= 0)
                    {
                        GameObject projectile4 = Instantiate(flames, shooter.transform.position, transform.rotation) as GameObject;
                        projectile4.transform.parent = transform;
                        //projectile4.transform.localEulerAngles = new Vector3(projectile4.transform.localEulerAngles.x, projectile4.transform.localEulerAngles.y, projectile4.transform.localEulerAngles.z -90);
                        flameTimer = 1f;
                    }
                    break;
                case Colortype.White:
                    GameObject projectile5 = Instantiate(smokePuff, shooter.transform.position , transform.rotation) as GameObject;
                    projectile5.transform.parent = this.transform; 
                    break;
                default: Debug.Log("Gun is broken"); break;
            }
        }
        else
        {
            laserBeam.enabled = false;
        }
        //Weapon timers
        grenadeTimer -= Time.deltaTime;
        bulletTimer -= Time.deltaTime;
        flameTimer -= Time.deltaTime;
        if (!Input.GetButton("Fire1")) laserTimer += Time.deltaTime;
        if (laserTimer > 1.5f) laserTimer = 1.5f;
    }
}
