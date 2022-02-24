using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bulbasaur : PokemonDemo1
{  
    #region 构造方法
        public Bulbasaur(int level, string nameIn, StateEnum stateIn, 
        int HPIN, int exp, bool isdead, bool iswild):base(nameIn, level, 80, 82, 83, 100, 100, 80, RaceEnum.Grass, stateIn, HPIN, exp, iswild, isdead)
        {
            if(string.Equals("妙蛙种子",nameIn)||string.Equals("妙蛙花",nameIn)){
                this.nameList = new List<string>();
                this.nameList.Add("妙蛙种子");
                this.nameList.Add("妙蛙花");
                for(int i = 0; i<2; i++){
                    if(string.Equals(nameList[i], nameIn)){
                        this.name = nameIn;
                        this.nextName = i+1;
                        break;
                    }
                }

                allList = new List<SkillDemo1>();
                allList.Add( new SkillDemo1("藤鞭", 40, 2, 0, 1, 0));
                allList.Add( new SkillDemo1("麻痹粉", 0, 2, 1, 0, 0));
                allList.Add( new SkillDemo1("光合作用", 0, 2, 0, 0, 1));
                allList.Add( new SkillDemo1("魔法叶", 60, 2, 0, 1, 0));
                allList.Add( new SkillDemo1("阳光烈焰", 120, 2, 0, 2, 0));
                allList.Add( new SkillDemo1("花瓣舞", 30, 2, 0, 2, 1));

                curList = new SkillDemo1[4];
                curList[0] = new SkillDemo1("藤鞭", 40, 2, 0, 1, 0);
                curList[1] = new SkillDemo1("麻痹粉", 0, 2, 1, 0, 0);
                curList[2] = new SkillDemo1("光合作用", 0, 2, 0, 0, 1);
                curList[3] = new SkillDemo1("魔法叶", 60, 2, 0, 1, 0);
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
