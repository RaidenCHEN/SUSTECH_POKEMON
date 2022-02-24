using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    public GameObject npc;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("act", 5, 5f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void act()
    {
        //Debug.Log(npc.transform.rotation.y);
        npc.transform.rotation= Quaternion.Euler(npc.transform.eulerAngles.x,npc.transform.eulerAngles.y+180, npc.transform.eulerAngles.z);
    }
}
