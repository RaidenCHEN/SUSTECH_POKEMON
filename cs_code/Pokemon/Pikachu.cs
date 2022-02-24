using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Pikachu : PokemonDemo1
{  

    #region 构造方法
        public Pikachu(int level, string nameIn, StateEnum stateIn, 
        int HPIN, int exp, bool isdead, bool iswild):base(nameIn, level, 78, 84, 78, 109, 85, 100, RaceEnum.Electric, stateIn, HPIN, exp, iswild, isdead)
        {
            if(string.Equals("皮卡丘",nameIn)||string.Equals("雷丘",nameIn)){
                this.nameList = new List<string>();
                this.nameList.Add("皮卡丘");
                this.nameList.Add("雷丘");
                for(int i = 0; i<2; i++){
                    if(string.Equals(nameList[i], nameIn)){
                        this.name = nameIn;
                        this.nextName = i+1;
                        break;
                    }
                }

                allList = new List<SkillDemo1>();
                allList.Add( new SkillDemo1("电磁波", 0,  6, 1, 0, 0));
                allList.Add( new SkillDemo1("电击", 40, 6, 0, 2, 0));
                allList.Add( new SkillDemo1("电光", 60, 6, 0, 2, 0));
                allList.Add( new SkillDemo1("十万伏特", 90, 6, 0, 2, 0));
                allList.Add( new SkillDemo1("打雷", 120, 6, 1, 2, 0));
                allList.Add( new SkillDemo1("疯狂伏特", 90, 6, 0, 1, 0));

                curList = new SkillDemo1[4];
                curList[0] = new SkillDemo1("电磁波", 0,  6, 1, 0, 0);
                curList[1] = new SkillDemo1("电击", 40, 6, 0, 2, 0);
                curList[2] = new SkillDemo1("电光", 60, 6, 0, 2, 0);
                curList[3] = new SkillDemo1("十万伏特", 90, 6, 0, 2, 0);
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