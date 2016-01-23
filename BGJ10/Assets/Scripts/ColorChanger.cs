using UnityEngine;
using System.Collections;

public class ColorChanger : MonoBehaviour {

    public Colortype color;
    bool playerInside;
    Player player;
    public Light glower;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        SetLightColor(glower, color);
    }
	
	// Update is called once per frame
	void Update () {
        this.UseColorChanger(player, color);
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            playerInside = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            playerInside = false;
    }
    ///<summary>
    /// Method UseColorChanger sets Player player's Colortype to Colortype color
    ///</summary>
    void UseColorChanger(Player player, Colortype color)
    {
        if(playerInside && Input.GetButtonDown("Use"))
            player.weapon.SetType(color);
    }

    ///<summary>
    /// Method SetLightColor sets the color property of Light light to the corisponding color of Colortype type
    ///</summary>
    void SetLightColor(Light light, Colortype type)
    {
        switch (type)
        {
            case Colortype.Blue: light.color = Color.blue; break;
            case Colortype.Green: light.color = Color.green; break;
            case Colortype.Pink: light.color = Color.magenta; break;
            case Colortype.Yellow: light.color = Color.yellow; break;
            default: Debug.Log("ColorChanger is broken"); break;
        }
    }
}
