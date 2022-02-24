using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Squirtle : PokemonDemo1
{  

    #region 构造方法
        public Squirtle(int level, string nameIn, StateEnum stateIn, 
        int HPIN, int exp, bool isdead, bool iswild):base(nameIn, level, 78, 84, 78, 109, 85, 100, RaceEnum.Water, stateIn, HPIN, exp, iswild, isdead)
        {   
            if(string.Equals("杰尼龟",nameIn)||string.Equals("水箭龟",nameIn)){
                this.nameList = new List<string>();
                this.nameList.Add("杰尼龟");
                this.nameList.Add("水箭龟");
                for(int i = 0; i<2; i++){
                    if(string.Equals(nameList[i], nameIn)){
                        this.name = nameIn;
                        this.nextName = i+1;
                        break;
                    }
                }

                allList = new List<SkillDemo1>();
                allList.Add( new SkillDemo1("水枪", 40,  1, 0, 1, 0));
                allList.Add( new SkillDemo1("泡沫", 40,  1, 0, 2, 0));
                allList.Add( new SkillDemo1("高压水炮", 110, 1, 0, 1, 0));
                allList.Add( new SkillDemo1("泡沫光线", 65,  1, 0, 2, 0));
                allList.Add( new SkillDemo1("求雨", 0, 1, 0, 0, 1));
                allList.Add( new SkillDemo1("水流裂破", 120,  1, 0, 1, 0));

                curList = new SkillDemo1[4];
                curList[0] = new SkillDemo1("水枪", 40, 1, 0, 1, 0);
                curList[1] = new SkillDemo1("泡沫", 40,  1, 0, 2, 0);
                curList[2] = new SkillDemo1("高压水炮", 110,  1, 0, 1, 0);
                curList[3] = new SkillDemo1("泡沫光线", 65,  1, 0, 2, 0);
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