using UnityEngine;
using System.Collections;

public class FlameThrower : MonoBehaviour {
    void OnTriggerStay2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Enemy" && hit.gameObject.GetComponent<Enemy>().type == Colortype.Yellow)
        {
            Debug.Log("flamne burning jelly");
            hit.gameObject.GetComponent<Enemy>().hp -= 1f;
        }
    }
}
