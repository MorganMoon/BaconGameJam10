using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour {

    public Colortype color;
    bool playerInside;
    Player player;
    public Light glower;

    //UI
    public Text useText;

    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Start()
    {
        SetLightColor(color);
        useText.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
        this.UseColorChanger(player, color);
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            useText.enabled = true;
            playerInside = true;
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            useText.enabled = false;
            playerInside = false;
        }
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
    public void SetLightColor(Colortype type)
    {
        switch (type)
        {
            case Colortype.Blue: glower.color = Color.blue; break;
            case Colortype.Green: glower.color = Color.green; break;
            case Colortype.Pink: glower.color = Color.magenta; break;
            case Colortype.Yellow: glower.color = Color.yellow; break;
            default: Debug.Log("ColorChanger is broken"); break;
        }
    }
}
