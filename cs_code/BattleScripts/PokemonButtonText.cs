using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PokemonButtonText : MonoBehaviour
{
    public PokBag bag;
    public List<Text> buttonText;
    public List<GameObject> button;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click()
    {
        for (int i = 0; i < bag.realList.Count; i++)
        {
            buttonText[i].text = bag.realList[i].name;
            button[i].SetActive(true);
        }
        for (int i = bag.realList.Count; i < 9; i++)
        {
            button[i].SetActive(false);
        }
    }
}
