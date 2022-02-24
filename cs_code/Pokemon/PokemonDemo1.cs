using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public abstract class PokemonDemo1 : MonoBehaviour
{   
    #region 字段

        /// <summary>
        /// 最大等级
        /// </summary>
        protected int maxLevel;

        /// <summary>
        /// 是否野生
        /// </summary>
        public bool wild;

        /// <summary>
        /// 是否濒死
        /// </summary>
        public bool dead;

        /// <summary>
        /// 累计经验值
        /// </summary>
        protected int curExp; 
        public int getCurExp(){
            return this.curExp;
        }

        /// <summary>
        /// 升级到下一等级所需要的累计经验值
        /// </summary>
        protected int nextExp;
        public int getNextExp(){
            return this.nextExp;
        }

        /// <summary>
        /// 属性
        /// </summary>
        public RaceEnum race;

        /// <summary>
        /// 所有可能习得的技能列表
        /// </summary>
        protected List<SkillDemo1> allList;

                /// <summary>
        /// 下一个习得技能的索引
        /// </summary>
        protected int nextSkill;

        /// <summary>
        /// 不同形态下的名字列表
        /// </summary>
        protected List<string> nameList;

        /// <summary>
        /// 下一个进化的索引
        /// </summary>
        public int nextName;

        #region 升级参数列表

            /// <summary>
            /// 血量参数值
            /// </summary>
            protected int constHP;

            /// <summary>
            /// 物理攻击参数值
            /// </summary>
            protected int constPhyAtk;

            /// <summary>
            /// 物理防御参数值
            /// </summary>
            protected int constPhyDef;

            /// <summary>
            /// 特殊攻击参数值
            /// </summary>
            protected int constMagAtk;


            /// <summary>
            /// 特殊防御参数值
            /// </summary>
            protected int constMagDef;

            /// <summary>
            /// 速度参数值
            /// </summary>
            protected int constSpd;

        #endregion

    #endregion

    #region 属性

        #region 战斗显示

            /// <summary>
            /// 名字
            /// </summary>
            public string name;

            /// <summary>
            /// 当前等级下，最大血量
            /// </summary>
            public int maxHP;

            /// <summary>
            /// 当前等级下，当前血量
            /// </summary>
            public int curHP;

            /// <summary>
            /// 当前状态
            /// </summary>
            public StateEnum state;

            /// <summary>
            /// 当前精灵习得的技能列表
            /// </summary>
            public SkillDemo1[] curList;

            /// <summary>
            /// 被替换技能的索引值
            /// </summary>
            public int replaceSkill;
            
        #endregion

 
        #region 伤害计算

            /// <summary>
            /// 物理攻击
            /// </summary>
            public int phyAtk;

            /// <summary>
            /// 物理防御
            /// </summary>
            public int phyDef; 
            
            /// <summary>
            /// 攻击buff
            /// </summary>
            public int ATKbuff; 

            /// <summary>
            /// 特殊攻击
            /// </summary>
            public int magAtk;
            
            /// <summary>
            /// 特殊防御
            /// </summary>
            public int magDef;
            /// <summary>
            /// 等级
            /// </summary>
            public int level;

        #endregion

        #region 命中判定

            /// <summary>
            /// 速度
            /// </summary>
            public int spd;

        #endregion
   #endregion
    
    #region 构造方法
        public PokemonDemo1(string namenow, int level, int constHP, int constPhyAtk, int constPhyDef, int constMagAtk, 
        int constMagDef, int constSpd, RaceEnum race, StateEnum stateIn, int HPIN, int CURExp, bool iswild, bool isdead)
        {
            this.maxLevel = 10;
            this.curExp = CURExp;
            this.wild = iswild;

            this.level = level;
            this.nextExp = (int)Math.Pow((level+1),3);
            this.nextSkill = 4;

            this.race = race;
            this.constHP = 45;
            this.constPhyAtk = 49;
            this.constPhyDef = 49;
            this.constMagAtk = 65;
            this.constMagDef = 65;
            this.constSpd = 45;

            this.ATKbuff = 0;

            SetAttributes();
            this.state = stateIn;
            if(HPIN>=this.maxHP){
                this.curHP = this.maxHP;
                this.dead = false;
            }else if(HPIN<=0||isdead){
                this.dead = true;
                this.curHP = 0;
            }else{
                this.dead = false;
                this.curHP = HPIN;
            }
        }

        
    #endregion

    #region 方法

        /// <summary>
        /// 精灵进化： 
        /// 1.更改精灵模型
        /// 2.习得新技能
        /// </summary>
        public void Evolution()
        {
            name = nameList[nextName++];
            this.constHP += 20;
            this.constPhyAtk += 20;
            this.constPhyDef += 20;
            this.constMagAtk += 20;
            this.constMagDef += 20;
            this.constSpd += 20;
        }

        /// <summary>
        /// 精灵升级：
        /// 1.等级加一
        /// 2.修改等级相关的属性值
        /// </summary>
        public void Promotion(int amount){
            level += 1;
            curExp = 0;
            nextExp = (int)Math.Pow((level+1), 3);

            int preHP = maxHP;
            SetAttributes();
            curHP += (maxHP - preHP);

            // if(level == 5 && allList.Count > 5)
            // {
            //     LearnSkill(allList[nextSkill++]);
            // }
            // else if(level == 7)
            // {
            //     Evolution();
            //     LearnSkill(allList[nextSkill++]);
            // }
        }

        /// <summary>
        /// 根据等级设置精灵属性值
        /// </summary>
        public void SetAttributes()
        {
            maxHP  = (int)(constHP    *((double)level/maxLevel)*2 + level*10 + 10);
            phyAtk = (int)(constPhyAtk*((double)level/maxLevel)*2 + 5);
            phyDef = (int)(constPhyDef*((double)level/maxLevel)*2 + 5);
            magAtk = (int)(constMagAtk*((double)level/maxLevel)*2 + 5);
            magDef = (int)(constMagDef*((double)level/maxLevel)*2 + 5);
            spd    = (int)(constSpd   *((double)level/maxLevel)*2 + 5);
        }

        /// <summary>
        /// 习得新技能
        /// </summary>
        /// <param name="newSkill">新习得的技能</param>
        public string[] LearnSkill()
        {  
            string[] skillNames = new string[2];
            skillNames[0] = curList[replaceSkill].name;
            curList[replaceSkill] = allList[nextSkill++];
            skillNames[1] = curList[replaceSkill].name;
            return skillNames;
        }

        /// <summary>
        /// 获取的经验值
        /// </summary>
        /// <param name="amount">战斗结束后获得的经验值</param>
        public bool GainExp(ref int amount)
        {
            if(level < maxLevel)
            {
                int difExp = nextExp-curExp;
                if(amount >= difExp){
                    Promotion(amount-difExp);
                    amount -= difExp;
                    return true;
                }
                else{
                    curExp += amount;
                    amount = 0;
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// 中毒掉血
        /// </summary>
        /// <returns>是否被毒死</returns>
        public bool checkPoison()
        {
            if(state == StateEnum.POISONED)
            {
                curHP -= maxHP/16;
            }
            curHP = Math.Min(0, curHP);
            return curHP == 0;
        }

        /// <summary>
        /// 返回技能
        /// </summary>
        /// <returns>返回指定技能</returns>
        public SkillDemo1 getSkill(int n){
            return curList[n];
        }

        /// <summary>
        /// 返回技能表总数
        /// </summary>
        /// <returns>返回指定技能</returns>
        public int getAllCount(){
            return this.allList.Count;
        }

        /// <summary>
        /// 返回nameList长度
        /// </summary>
        /// <returns>返回指定技能</returns>
        public int getNameList(){
            return this.nameList.Count;
        }

    #endregion
}
