using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Pidgeotto : PokemonDemo1
{  

     #region 构造方法
        public Pidgeotto(int level, string nameIn, StateEnum stateIn, 
        int HPIN, int exp, bool isdead, bool iswild):base(nameIn, level, 75, 100, 110, 45, 55, 65, RaceEnum.Flying, stateIn, HPIN, exp, iswild, isdead)
        {
            if(string.Equals("比比鸟",nameIn)||string.Equals("比雕",nameIn)){
                this.nameList = new List<string>();
                this.nameList.Add("比比鸟");
                this.nameList.Add("比雕");
                for(int i = 0; i<2; i++){
                    if(string.Equals(nameList[i], nameIn)){
                        this.name = nameIn;
                        this.nextName = i+1;
                        break;
                    }
                }

            allList = new List<SkillDemo1>();
            allList.Add( new SkillDemo1("啄", 35, 4, 0, 1, 0));
            allList.Add( new SkillDemo1("起风", 40, 4, 0, 2, 0));
            allList.Add( new SkillDemo1("啄钻", 80, 4, 0, 1, 0));
            allList.Add( new SkillDemo1("翅膀攻击", 35,  4, 0, 1, 0));
            allList.Add( new SkillDemo1("神鸟", 140, 4, 0, 2, 0));
            allList.Add( new SkillDemo1("羽毛舞", 0, 4, 0, 0, -1));

            curList = new SkillDemo1[4];
            curList[0] = new SkillDemo1("啄", 35, 4, 0, 1, 0);
            curList[1] = new SkillDemo1("起风", 40, 4, 0, 2, 0);
            curList[2] = new SkillDemo1("啄钻", 80, 4, 0, 1, 0);
            curList[3] = new SkillDemo1("翅膀攻击", 35,  4, 0, 1, 0);
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



