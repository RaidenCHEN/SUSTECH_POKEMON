using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnWorld : MonoBehaviour
{
    public PokItem thisItem;
    public PokBag myBag;
    // Start is called before the first frame update
    public GameObject Botton;



    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Botton.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Botton.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.F) && other.gameObject.tag == "Player")
        {
            AddNewItem();
            Destroy(gameObject);
            Botton.SetActive(false);
        }
    }
    private void AddNewItem()
    {
        if (!myBag.realList.Contains(thisItem))
        {
            //myBag.realList.Add(thisItem);
            //myBag.itemList.Add(thisItem);
            int ptr = myBag.addNewPokInst(this.thisItem);
            CreatItemInBag.CreatItem(myBag.itemList[ptr]);
        }
    }
}
