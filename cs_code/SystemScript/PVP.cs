using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PVP : MonoBehaviour
{

    public GameObject battle;
    public GameObject player;
    public PVPSystem battlesys;
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
        battle.SetActive(true);
        battlesys.startBat();
    }
}
