using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class recoverHP : MonoBehaviour
{
    public PokBag myBag;
    // Start is called before the first frame update
    public void HP()
    {
        for (int i = 0; i < myBag.poknum(); i++)
        {
           PokItem pok = myBag.itemList[i];
            pok.curHP = 4921;
            pok.isDead = false;
            pok.state = StateEnum.NORMAL;
        }
    }
}
