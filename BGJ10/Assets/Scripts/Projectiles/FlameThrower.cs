using UnityEngine;
using System.Collections;

public class FlameThrower : MonoBehaviour {
    public GameObject player;
    void Start()
    {
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }
    void OnTriggerStay2D(Collider2D hit)
    {
        Debug.Log("Hit something with tag: " + hit.gameObject.tag);
        if (hit.gameObject.tag == "Enemy" && hit.gameObject.GetComponent<Enemy>().type == Colortype.Yellow)
        {
            Debug.Log("flamne burning jelly");
            hit.gameObject.GetComponent<Enemy>().hp -= 1f;
        }
    }
}
