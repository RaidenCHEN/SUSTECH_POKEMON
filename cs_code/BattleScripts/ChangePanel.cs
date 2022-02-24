using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanel : MonoBehaviour
{
    public GameObject from;
    public GameObject to;
    
    public void Click()
    {
        from.SetActive(false);
        to.SetActive(true);
    }
}
