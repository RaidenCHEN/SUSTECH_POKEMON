using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : MonoBehaviour
{
    public GameObject door;
    public GameObject text;
    private bool isEnter=false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&isEnter==true)
        {
            bool x = door.GetComponent<Animator>().GetBool("Open");
            x = !x;
            door.GetComponent<Animator>().SetBool("Open", x);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isEnter = true;
            text.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            isEnter = false;
            text.SetActive(false);
        }
    }

}
