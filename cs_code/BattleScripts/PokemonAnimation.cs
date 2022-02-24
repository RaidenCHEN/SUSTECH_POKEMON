using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokemonAnimation : MonoBehaviour
{
    public GameObject animation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animation.GetComponent<Animator>().SetInteger("state", 1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            animation.GetComponent<Animator>().SetInteger("state", 2);
        }
        else
        {
            animation.GetComponent<Animator>().SetInteger("state", 0);
        }
    }
}
