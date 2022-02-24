using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Grimer : PokemonDemo1
{  
    #region 构造方法
        public Grimer(int level, string nameIn, StateEnum stateIn, 
        int HPIN, int exp, bool isdead, bool iswild):base(nameIn, level, 105, 105, 75, 65, 100, 50, RaceEnum.Poison, stateIn, HPIN, exp, iswild, isdead)
        {
            if(string.Equals("臭泥",nameIn)||string.Equals("臭臭泥",nameIn)){
                this.nameList = new List<string>();
                this.nameList.Add("臭泥");
                this.nameList.Add("臭臭泥");
                for(int i = 0; i<2; i++){
                    if(string.Equals(nameList[i], nameIn)){
                        this.name = nameIn;
                        this.nextName = i+1;
                        break;
                    }
                }

            allList = new List<SkillDemo1>();
            allList.Add( new SkillDemo1("毒瓦斯", 0, 3, 1, 0, 0));
            allList.Add( new SkillDemo1("污泥攻击", 65, 3, 0, 1, 0));
            allList.Add( new SkillDemo1("污泥炸弹", 90, 3, 0, 2, 0));
            allList.Add( new SkillDemo1("污泥波", 95, 3, 0, 2, 0));
            allList.Add( new SkillDemo1("垃圾射击", 120, 3, 1, 1, 0));
            allList.Add( new SkillDemo1("打嗝", 120, 3, 0, 2, 0));

            curList = new SkillDemo1[4];
            curList[0] = new SkillDemo1("毒瓦斯", 0, 3, 1, 0, 0);
            curList[1] = new SkillDemo1("污泥攻击", 65, 3, 0, 1, 0);
            curList[2] = new SkillDemo1("污泥炸弹", 90, 3, 0, 2, 0);
            curList[3] = new SkillDemo1("污泥波", 95, 3, 0, 2, 0);
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