using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Fungus;


public class creat : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameObject;
    public GameObject player;
    public GameObject trigger;
    public GameObject E;
    public Flowchart flowchart;
    public string boolname;
    public string isControll;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!flowchart.GetBooleanVariable(isControll))
        {
            player.GetComponent<playerController>().enabled=false;
            E.SetActive(false);
        }
        else
        {
            player.GetComponent<playerController>().enabled = true;
        }
        if (flowchart.GetBooleanVariable(boolname))
        {
            gameObject.SetActive(true);
            trigger.SetActive(false);
            player.GetComponent<playerController>().enabled = true;
        }
        
    }
}
