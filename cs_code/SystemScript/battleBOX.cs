using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class battleBOX : MonoBehaviour
{

    public GameObject battle;
    public GameObject player;
   public BattleSystem battlesys;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void act()
    {
        int ran = Random.Range(1, 2000);
        if (ran < 15)
        {
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
            battlesys.startBat(1);
        }
    }

    void OnTriggerStay(Collider col)
    {
        if(Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.D)|| Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.A))
        {
            act();
        }
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
