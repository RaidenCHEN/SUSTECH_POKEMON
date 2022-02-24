using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Item",menuName="Inventory/New Pok Item")]
public class PokItem : ScriptableObject
{


    //---
    public Sprite Image;
    public bool isInhand;
    [TextArea]
    public string Info;
    //---


    public string name;

    public bool isDead;
    
    public int level;

    public int curexp;

    public bool iswild;

    public int curHP;

    public StateEnum state;

    public GameObject[] ModelList;
    
    public GameObject getModel(){
        if(
            string.Equals(this.name, "喷火龙") ||
            string.Equals(this.name, "雷丘") ||
            string.Equals(this.name, "穿山王") ||
            string.Equals(this.name, "水箭龟") ||
            string.Equals(this.name, "妙蛙花") ||
            string.Equals(this.name, "比雕") ||
            string.Equals(this.name, "臭臭泥") 
        ){
            return this.ModelList[1];
        }
        else{
            return this.ModelList[0];
        }
    }
    // public PokItem (string name, int level, int curHP, int curEXP=0, StateEnum state = StateEnum.NORMAL, bool dead = false, bool isWild= true){
    //     this.state= state;
    //     this.curHP = curHP;
    //     this.level = level;
    //     this.name = name;
    //     this.isDead = dead;
    //     this.iswild = isWild;
    //     this.curexp = curEXP;
    // }

    public void update (PokemonDemo1 pokInstance, bool wild=false){
        this.state= pokInstance.state;
        this.curHP = pokInstance.curHP;
        this.level = pokInstance.level;
        this.name = pokInstance.name;
        this.isDead = pokInstance.dead;
        this.iswild = wild;
        this.curexp = pokInstance.getCurExp();
    }

    public void update ( int level, int curHP, int curEXP=0, StateEnum state = StateEnum.NORMAL, bool dead = false, bool isWild= true){
        this.state= state;
        this.curHP = curHP;
        this.level = level;
        this.isDead = dead;
        this.iswild = isWild;
        this.curexp = curEXP;
    }

    
}
