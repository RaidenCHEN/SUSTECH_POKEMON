using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Sandshrew : PokemonDemo1
{  

    #region 构造方法
        public Sandshrew(int level, string nameIn, StateEnum stateIn, 
        int HPIN, int exp, bool isdead, bool iswild):base(nameIn, level, 83, 80, 75, 70, 70, 101, RaceEnum.Ground, stateIn, HPIN, exp, iswild, isdead)
        {
            if(string.Equals("穿山鼠",nameIn)||string.Equals("穿山王",nameIn)){
                this.nameList = new List<string>();
                this.nameList.Add("穿山鼠");
                this.nameList.Add("穿山王");
                for(int i = 0; i<2; i++){
                    if(string.Equals(nameList[i], nameIn)){
                        this.name = nameIn;
                        this.nextName = i+1;
                        break;
                    }
                }

                allList = new List<SkillDemo1>();
                allList.Add( new SkillDemo1("流沙地狱", 35, 5, 0, 2, 0));
                allList.Add( new SkillDemo1("重踏", 60, 5, 0, 1, 0));
                allList.Add( new SkillDemo1("岩崩", 75, 5, 0, 1, 0));
                allList.Add( new SkillDemo1("挖洞", 80, 5, 0, 1, 0));
                allList.Add( new SkillDemo1("地震", 100, 5, 0, 1, 0));
                allList.Add( new SkillDemo1("剑舞", 0, 5, 0, 0, 2));

                curList = new SkillDemo1[4];
                curList[0] = new SkillDemo1("流沙地狱", 35, 5, 0, 2, 0);
                curList[1] = new SkillDemo1("重踏", 60, 5, 0, 1, 0);
                curList[2] = new SkillDemo1("岩崩", 75, 5, 0, 1, 0);
                curList[3] = new SkillDemo1("挖洞", 80, 5, 0, 1, 0);
                if (level >= 5){
                    curList[0] = allList[4];
                    nextSkill = 5;
                }
                if (level >= 7){
                    curList[1] = allList[5];
                    nextSkill = 6;
                }
            }
            
           
        }
    #endregion

}