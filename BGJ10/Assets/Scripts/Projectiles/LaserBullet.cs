using UnityEngine;
using System.Collections;

public class LaserBullet : MonoBehaviour {
    public GameObject hitEffect;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            Instantiate(hitEffect, transform.position, transform.rotation);
            if (collision.gameObject.tag == "Enemy" && collision.gameObject.GetComponent<Enemy>().type == Colortype.Pink)
            {
                collision.gameObject.GetComponent<Enemy>().hp -= 5;
            }
            Destroy(gameObject);
        }
    }
}
