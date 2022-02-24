using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBattle : MonoBehaviour
{


    public GameObject battle;
    public GameObject player;
    public BattleSystem battlesys;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startBattle()
    {
        player.transform.localPosition = new Vector3(player.transform.localPosition.x - 5, player.transform.localPosition.y, player.transform.localPosition.z);
        GameObject[] P = GameObject.FindGameObjectsWithTag("PlayerPokemonNow");
        GameObject[] E = GameObject.FindGameObjectsWithTag("EnemyPokemonNow");
        for (int i = 0; i < P.Length; i++)
        {
            Destroy(P[i]);
        }
        for (int i = 0; i < E.Length; i++)
        {
            Destroy(E[i]);
        }

        player.SetActive(false);
        battle.SetActive(true);
        battlesys.startBat(2);
    }

}
