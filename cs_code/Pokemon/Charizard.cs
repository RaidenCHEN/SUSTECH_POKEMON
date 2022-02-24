using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Charizard : PokemonDemo1
{  

    #region 构造方法
        public Charizard(int level, string nameIn, StateEnum stateIn, 
        int HPIN, int exp, bool isdead, bool iswild):base(nameIn, level, 78, 84, 78, 109, 85, 100, RaceEnum.Fire, stateIn, HPIN, exp, iswild, isdead)
        {
            if(string.Equals("小火龙",nameIn)||string.Equals("喷火龙",nameIn)){
                this.nameList = new List<string>();
                this.nameList.Add("小火龙");
                this.nameList.Add("喷火龙");
                for(int i = 0; i<2; i++){
                    if(string.Equals(nameList[i], nameIn)){
                        this.name = nameIn;
                        this.nextName = i+1;
                        break;
                    }
                }

                allList = new List<SkillDemo1>();
                allList.Add( new SkillDemo1("火花", 40, 0, 0, 2, 0));
                allList.Add( new SkillDemo1("火焰拳", 75, 0, 0, 1, 0));
                allList.Add( new SkillDemo1("喷射火焰", 90, 0, 0, 2, 0));
                allList.Add( new SkillDemo1("大字爆", 110, 0, 0, 2, 0));
                allList.Add( new SkillDemo1("火焰车", 60, 0, 0, 1, 0));
                allList.Add( new SkillDemo1("火焰牙", 80, 0, 0, 1, 0));

                curList = new SkillDemo1[4];
                curList[0] = new SkillDemo1("火花", 40, 0, 0, 2, 0);
                curList[1] = new SkillDemo1("火焰拳", 75, 0, 0, 1, 0);
                curList[2] = new SkillDemo1("喷射火焰", 90, 0, 0, 2, 0);
                curList[3] = new SkillDemo1("大字爆", 110, 0, 0, 2, 0);
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