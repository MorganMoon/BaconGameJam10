using UnityEngine;
using System.Collections;

public class Grenade : MonoBehaviour {
    public GameObject explosion;
    bool Exploded;
    float countdown = 0;
    GameObject[] enemies;

    void Update()
    {
        PullPin();
    }

    ///<summary>
    /// Method PullPin begins the countdown for the grenade to explode
    ///</summary>
    void PullPin()
    {
        if (countdown >= 1f)
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                if (Vector2.Distance(enemy.transform.position, gameObject.transform.position) < 3f && enemy.GetComponent<Enemy>().type == Colortype.Green)
                {
                    enemy.GetComponent<Enemy>().Die();
                }
            }
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        countdown += Time.deltaTime;
    }
}
