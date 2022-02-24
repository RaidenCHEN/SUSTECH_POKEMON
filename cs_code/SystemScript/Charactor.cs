using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Charactor : MonoBehaviour
{
    public GameObject Talk;
    public GameObject Botton;
    private bool isEnter = false;
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isEnter == true)
        {
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Talk.SetActive(true);
            isEnter = true;
            Botton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Talk.SetActive(false);
            isEnter = false;
            Botton.SetActive(false);
        }
    }
}
