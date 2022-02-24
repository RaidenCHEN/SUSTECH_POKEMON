using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class slot : MonoBehaviour
{
    public PokItem slotItem;
    public Image Image;
    public bool isInhand;
    [TextArea]
    public string Info;
    public void ItemOnClicked()
    {
        if(slotItem.curHP<4921)
            CreatItemInBag.UpdateItemInfo(slotItem.Info + "   HP:" + slotItem.curHP + "   EXP:" + slotItem.curexp);
        else
            CreatItemInBag.UpdateItemInfo(slotItem.Info + "   HP:" + "Full" + "   EXP:" + slotItem.curexp);
    }
}
