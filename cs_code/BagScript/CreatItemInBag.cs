using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatItemInBag : MonoBehaviour
{
    static CreatItemInBag instance;

    public PokBag myBag;
    public GameObject slotGrid;
    public slot slotPrefab;
    public Text itemInfo;

    private static List<slot> slots = new List<slot>();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }
    private void OnEnable()
    {
        instance.itemInfo.text = "";
    }
    public static void UpdateItemInfo(string x)
    {
        instance.itemInfo.text = x;
    }
    public static void CreatItem(PokItem item)
    {
        slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.Image.sprite = item.Image;

        slots.Add(newItem);
    }

    private void Start()
    {
        myBag.realList.Clear();
        for (int i = 0; i < myBag.itemList.Count; i++)
        {
            myBag.itemList[i].name = "";
        }
        myBag.putBag(0);
        for (int i = 0; i < myBag.poknum(); i++)
        {
            if (myBag.realList[i].name == "")
            {
                continue;
            }
            CreatItem(myBag.realList[i]);
        }
    }
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.B))
        {
            //myBag.putBag(0);
            //for (int i = 0; i < myBag.poknum(); i++)
            {
                //slots[i].Info = myBag.realList[i].Info;
            }
        }
    }
}
