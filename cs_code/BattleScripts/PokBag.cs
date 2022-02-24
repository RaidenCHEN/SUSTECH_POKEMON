using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Bag", menuName = "Inventory/New Pok Bag")]
public class PokBag : ScriptableObject
{
   
    public List<PokItem> realList = new List<PokItem>();
    public List<PokItem> itemList = new List<PokItem>();
    public int poknum(){
        return realList.Count;
    }

    void Start()
    {
    }

    public int addNewPokInst(PokItem mem)
    {
        int ptr = 0;
        for (int i = 0; i < this.itemList.Count; i++)
        {
            if (this.itemList[i].name == "")
            {
                itemList[i].name = mem.name;
                itemList[i].ModelList = mem.ModelList;
                itemList[i].level = mem.level;
                itemList[i].isInhand = true;
                itemList[i].state = StateEnum.NORMAL;
                itemList[i].curexp = mem.curexp;
                itemList[i].iswild = false;
                itemList[i].Info = mem.Info;
                itemList[i].isDead = false;
                itemList[i].curHP = mem.curHP;
                itemList[i].Image = mem.Image;
                ptr = i;
                break;
            }
        }
        return ptr;
    }

    public PokItem getItemByName(string n){
        for (int i = 0; i < itemList.Count; i++)
        {
            if(string.Equals(n,itemList[i].name)){
                return itemList[i];
            }
        }
        return null;
    }

    public void putBag (int type){
        this.realList.Clear();

        if(type == 1){//野怪背包
            System.Random rand = new System.Random();
            int ptr = rand.Next(0, this.itemList.Count-3);
            this.itemList[ptr].iswild=true;
            this.realList.Add(itemList[ptr]);

        }else if (type == 0){//玩家背包
            for (int i = 0; i < this.itemList.Count; i++)
            {   
                if(string.Equals(this.itemList[i].name, "") || this.itemList[i].name==null){
                    break;
                }
                this.itemList[i].iswild=false;
                this.realList.Add(itemList[i]);
            }
        }else if (type == 2){//npc背包
            for (int i = this.itemList.Count-3; i < this.itemList.Count; i++)
            {   
                if(string.Equals(this.itemList[i].name, "") || this.itemList[i].name==null){
                    break;
                }
                this.itemList[i].iswild=false;
                this.realList.Add(itemList[i]);
            }
        }else if (type == 3){//PVP背包
            // int a,b,c;
            // System.Random rand = new System.Random((int)DateTime.Now.Ticks);
            // a = rand.Next(0,7);
            // b=a;
            // while(b==a){
            //     b=rand.Next(0,7);
            // }
            // c=a;
            // while(c==a || c==b){
            //     c=rand.Next(0,1);
            // }
            // this.itemList[a].iswild = false;
            // this.realList.Add(itemList[a]);
            // this.itemList[b].iswild = false;
            // this.realList.Add(itemList[b]);
            // this.itemList[c].iswild = false;
            // this.realList.Add(itemList[c]);
            for (int i = 0; i < 3; i++)
            {
                this.realList.Add(this.itemList[i]);
            }
        }
    }

    public void updateBag(List<PokemonDemo1> list){
        for (int i = 0; i < list.Count; i++)
        {
            this.itemList[i].update(list[i]);
        }
    }

    public void cUpdateBag(List<PokemonDemo1> list, PokemonDemo1 nMem, PokItem p){
        int length = list.Count;
        for (int i = 0; i < length; i++)
        {
            this.itemList[i].update(list[i]);
        }
        //创建数据
        PokItem newPok = this.itemList[length];
        //赋值
        newPok.update(nMem);
        newPok.ModelList = p.ModelList;
        newPok.Image = p.Image;
        newPok.isInhand = true;
        newPok.Info = p.Info;
        CreatItemInBag.CreatItem(newPok);
        // assetPath = "F:\\Sustech\\CS309_OOAD\\Project_PokemonInSUSTech\\New Unity Project\\Assets\\PlayerBag";
        // //检查保存路径
        // if (!Directory.Exists(assetPath))
        //     Directory.CreateDirectory(assetPath);

        // //删除原有文件，生成新文件
        // string fullPath = assetPath + "/" + ".asset";
        // UnityEditor.AssetDatabase.DeleteAsset(fullPath);
        // UnityEditor.AssetDatabase.CreateAsset(newPok, fullPath);
        // UnityEditor.AssetDatabase.Refresh();
        // this.itemList.Add(newPok);
    }
}
