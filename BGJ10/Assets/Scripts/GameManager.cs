using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //Player Stuff
    public Player player;
    public int wave = 0;
    public bool waveRunning = false;

    //Map Stuff
    public GameObject Enemy;
    public GameObject[] ColorOrbSpawns;
    public GameObject[] EnemySpawns;

    //UI stuff
    public Text roundText;
    public Slider hpSlider;

    void Start()
    {
        PlaceColorOrbs();
    }
	void Update () {
        if (player)
        {
            HandleWaves();
        }
        HandleUI();
	}

    ///<summary>
    /// Method HandleUI handles all changing UI elements during playtime
    ///</summary>
    void HandleUI()
    {
        roundText.text = "Wave: " + wave;
        hpSlider.value = player.GetComponent<Player>().hp / 100;
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

    void HandleWaves()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            waveRunning = false;
            wave += 1;
            SpawnEnemy(wave);
            waveRunning = true;
        }
    }

    ///<summary>
    /// Method SpawnEnemy will spawn an enemy at a random spawnpoint
    ///</summary>
    void SpawnEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (!waveRunning)
            {
                GameObject enemy = Instantiate(Enemy, EnemySpawns[Random.Range(0, EnemySpawns.Length)].transform.position, Quaternion.identity) as GameObject;
                enemy.GetComponent<Enemy>().type = (Colortype)Random.Range(1, 5);
            }
        }
    }
}
