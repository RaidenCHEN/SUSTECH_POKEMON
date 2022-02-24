using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class ChangeScene : MonoBehaviour
{
    public GameObject player;
    public GameObject F;
    public Flowchart flowchart;
    public Bag myBag;
    public string itemNeed;
    public string isControll;
    public string chatFinish;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!flowchart.GetBooleanVariable(isControll))
        {
            player.GetComponent<playerController>().enabled = false;
            F.SetActive(false);
        }
        else
        {
            player.GetComponent<playerController>().enabled = true;
        }
        

    }
    public GameObject Talk;
    public GameObject Botton;
    private bool haveBerger=true;//如果为假，证明没有汉堡，会触发对话
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {            
            Botton.SetActive(true);
            int i;
            for (i = 0; i< myBag.itemList.Count; i++)
            {
                if (myBag.itemList[i].name == itemNeed)
                {
                    break;
                }
            }
            if (i== myBag.itemList.Count)
            {
                haveBerger = false;
                Talk.SetActive(true);
            }
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!haveBerger) {
            if (flowchart.GetBooleanVariable(chatFinish))
            {
                Application.LoadLevel("MainScene");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Application.LoadLevel("MainScene");
            }
        }
        
            
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Talk.SetActive(false);
            Botton.SetActive(false);
        }
    }
}
