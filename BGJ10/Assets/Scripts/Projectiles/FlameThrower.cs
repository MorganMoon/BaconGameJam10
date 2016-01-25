using UnityEngine;
using System.Collections;

public class FlameThrower : MonoBehaviour {
    float deleteTimer = 1.0f;

    void Update(){
        if (deleteTimer <= 0)
        {
            Destroy(gameObject);
        }
        deleteTimer -= Time.deltaTime;
    }
    void OnTriggerStay2D(Collider2D hit)
    {
        if (hit.gameObject.tag == "Enemy" && hit.gameObject.GetComponent<Enemy>().type == Colortype.Yellow)
        {
            hit.gameObject.GetComponent<Enemy>().hp -= 1f;
        }
    }
}
