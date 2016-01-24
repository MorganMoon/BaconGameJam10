using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    //Player Stuff
    public Player player;
    public float score = 0;

    //Map Stuff
    public GameObject Enemy;
    public GameObject[] ColorOrbSpawns;
    public GameObject[] EnemySpawns;

    void Start()
    {
        PlaceColorOrbs();
    }
	void Update () {
        UpdateScore();
	}

    ///<summary>
    /// Method UpdateScore adds to the score as time passes, as long as the player is alive
    ///</summary>
    void UpdateScore()
    {
        if (player)
        {
            score += Time.deltaTime;

            if ((int)score % 10 == 0)
            {
                SpawnEnemy();
            }
        }
    }
    ///<summary>
    /// Method PlaceColorOrbs places one orb of each color around the map
    ///</summary>
    void PlaceColorOrbs()
    {
        List<int> colorNumber = new List<int> { 1, 2, 3, 4};

        for (int i = 0; i < colorNumber.Count; i++)
        {
            int temp = colorNumber[i];
            int randomIndex = Random.Range(i, colorNumber.Count);
            colorNumber[i] = colorNumber[randomIndex];
            colorNumber[randomIndex] = temp;
        }

        int index = 0;
        foreach (GameObject colorOrb in ColorOrbSpawns)
        {
            ColorOrbSpawns[index].GetComponent<ColorChanger>().color = (Colortype)colorNumber[index];
            ColorOrbSpawns[index].GetComponent<ColorChanger>().SetLightColor(ColorOrbSpawns[index].GetComponent<ColorChanger>().color);
            index++;
        }
    }

    ///<summary>
    /// Method SpawnEnemy will spawn an enemy at a random spawnpoint
    ///</summary>
    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(Enemy, EnemySpawns[Random.Range(0, EnemySpawns.Length)].transform.position, Quaternion.identity) as GameObject;
        enemy.GetComponent<Enemy>().type = (Colortype)Random.Range(1, 5);
    }
}
