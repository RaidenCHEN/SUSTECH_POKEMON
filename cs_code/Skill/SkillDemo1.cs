using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


public struct SkillDemo1
{	
	public string name;
    /// <summary>
    /// 技能威力。buff或debuff类技能威力为0
    /// </summary>
    public int skillStrength;

    /// <summary>
    /// 技能属性
    /// </summary>
    public RaceEnum skillRace;

    /// <summary>
    /// 该技能可造成的状态
    /// </summary>
    public StateEnum stateCause;

    /// <summary>
    /// 技能类型
    /// </summary>
    public int skillType;

    /// <summary>
    /// 加buff或debuff的级数
    /// </summary>
    public int buffLevl;

    /// <summary>
    /// 技能结构体初始化方法
    /// </summary>
    /// <param name="skillStrength">技能威力</param>
    /// <param name="skillRace">技能属性,0=火，1=水，2=草，3=毒，4=飞，5=地，6=电,7=普通</param>
    /// <param name="stateCause">造成状态，0=正常，1=麻痹，2=中毒</param>
    /// <param name="skillType">技能类型，0=状态，1=物攻，2=特攻</param> 
    /// <param name="buffLevl">buff量</param>
    public SkillDemo1(string name,int strength,int race, int state, int type, int buff){
		this.name=name;
        this.skillStrength = strength;
        this.skillRace = (RaceEnum) race;       // int强转enum
        this.stateCause = (StateEnum) state;    // int强转enum
        this.skillType = type;
        this.buffLevl = buff;
    }

	public int[] combat(ref PokemonDemo1 user,ref PokemonDemo1 target){
		int[] result=new int[5];
	//0：是否麻痹 	0：否；1：是
	//1：是否命中 	0：否；1：是
	//2：效果		0：效果普通 1：效果拔群 2：效果较差 3：无效 
	//3：是否附加状态 	0：否；1：敌方麻痹；2：敌方中毒；3：己方攻击提升；4：敌方攻击下降; 5:敌方已经麻痹; 6:敌方已经中毒；7：buff不可增加；8：buff不可降低
	//4：敌方是否死亡	0：否；1：是

	//是否麻痹
		Debug.Log(target.race+"  "+skillRace);
		if(user.state==(StateEnum)1){
			System.Random sta = new System.Random();
			int pra = sta.Next(0, 1000);
			double s= (double)pra/1000;
			if(s<=0.25) {result[0]=1; return result;}
		}
	//是否命中
		double hitRate;
		if(user.spd>=target.spd) hitRate=1;
		else hitRate= (user.spd/target.spd)/2+0.5;

		System.Random r = new System.Random();
		int i = r.Next(0, 1000);
		double x= (double)i/1000;
		
		if(x>hitRate&&buffLevl<=0) {result[1]=0; return result;}
		else result[1]=1;

	//种族克制关系
		double rate=1;
		result[2] = 0;
		if(skillStrength!=0){
			if(skillRace==target.race && target.race!=(RaceEnum)7 && target.race!=(RaceEnum)4) {rate = 0.5; result[2]=2;}

			if(skillRace==(RaceEnum)5 && target.race==(RaceEnum)4) {rate=0; result[2]=3;}

			if(skillRace==(RaceEnum)6 && target.race==(RaceEnum)5) {rate=0; result[2]=3;}
			if(skillRace==(RaceEnum)5 && target.race==(RaceEnum)6) {rate=2; result[2]=1;}

			if(skillRace==(RaceEnum)0 && target.race==(RaceEnum)1) {rate=0.5; result[2]=2;}
			if(skillRace==(RaceEnum)1 && target.race==(RaceEnum)0) {rate=2; result[2]=1;}

			if(skillRace==(RaceEnum)0 && target.race==(RaceEnum)2) {rate=2; result[2]=1;}
			if(skillRace==(RaceEnum)2 && target.race==(RaceEnum)0) {rate=0.5; result[2]=2;}

			if(skillRace==(RaceEnum)1 && target.race==(RaceEnum)2) {rate=0.5; result[2]=2;}
			if(skillRace==(RaceEnum)2 && target.race==(RaceEnum)1) {rate=2; result[2]=1;}

			if(skillRace==(RaceEnum)3 && target.race==(RaceEnum)2) {rate=2; result[2]=1;}
			if(skillRace==(RaceEnum)2 && target.race==(RaceEnum)3) {rate=0.5; result[2]=2;}

			if(skillRace==(RaceEnum)6 && target.race==(RaceEnum)4) {rate=2; result[2]=1;}
			if(skillRace==(RaceEnum)4 && target.race==(RaceEnum)6) {rate=0.5; result[2]=2;}

			if(skillRace==(RaceEnum)3 && target.race==(RaceEnum)3) {rate=0.5; result[2]=2;}

			if(skillRace==(RaceEnum)5 && target.race==(RaceEnum)3) {rate=2; result[2]=1;}
			if(skillRace==(RaceEnum)3 && target.race==(RaceEnum)5) {rate=0.5; result[2]=2;}

			if(skillRace==(RaceEnum)1 && target.race==(RaceEnum)5) {rate=2; result[2]=1;}

			if(skillRace==(RaceEnum)5 && target.race==(RaceEnum)0) {rate=2; result[2]=1;}
			if(skillRace==(RaceEnum)0 && target.race==(RaceEnum)5) {rate=0.5; result[2]=2;}

			if(skillRace==(RaceEnum)2 && target.race==(RaceEnum)4) {rate=0.5; result[2]=2;}
			if(skillRace==(RaceEnum)4 && target.race==(RaceEnum)2) {rate=2; result[2]=1;}

			if(skillRace==(RaceEnum)6 && target.race==(RaceEnum)1) {rate=2; result[2]=1;}
		}else{
			result[2]=0;
		}
	//buff等级已到上限或敌方已有效果
		

	//攻击力buff	
		float buff = 0;
		if(user.ATKbuff>0){
			buff=(user.ATKbuff+2)/2;
		}else{
			buff = -buff;
			buff=2/(user.ATKbuff+2);
		}
	//技能效果
		int trueDamage=0;
		if (skillType==2 && result[2] != 3){
			trueDamage=(int)(buff*rate*(((user.level*2.0/5+2)*skillStrength*user.magAtk/target.magDef)/50)+2);
			target.curHP-=trueDamage;
			if(buffLevl>0&&user.ATKbuff==6) result[3]=7;
			else if(buffLevl<0&&target.ATKbuff==-6) result[3]=8;
			else if(target.state==(StateEnum)1&&(stateCause==(StateEnum)1||stateCause==(StateEnum)2)) result[3]=5;
			else if(target.state==(StateEnum)2&&(stateCause==(StateEnum)1||stateCause==(StateEnum)2)) result[3]=6;
			else if(stateCause==(StateEnum)1){
				target.state=(StateEnum)1;
				result[3]=1;
			}else if(stateCause==(StateEnum)2){
				target.state=(StateEnum)2;
				result[3]=2;
			}else if(buffLevl>0){
				user.ATKbuff+=1;
				result[3]=3;
			}else if(buffLevl<0){
				target.ATKbuff-=1;
				result[3]=4;
			}
		}else if (skillType== 1 && result[2] != 3)
		{
			trueDamage=(int)(buff*rate*(((user.level*2/5+2)*skillStrength*user.phyAtk/target.phyDef)/50)+2);
			target.curHP -= trueDamage;
			if(buffLevl>0&&user.ATKbuff==6) result[3]=7;
			else if(buffLevl<0&&target.ATKbuff==-6) result[3]=8;
			else if(target.state==(StateEnum)1&&(stateCause==(StateEnum)1||stateCause==(StateEnum)2)) result[3]=5;
			else if(target.state==(StateEnum)2&&(stateCause==(StateEnum)1||stateCause==(StateEnum)2)) result[3]=6;
			else if(stateCause==(StateEnum)1){
				target.state=(StateEnum)1;
				result[3]=1;
			}else if(stateCause==(StateEnum)2){
				target.state=(StateEnum)2;
				result[3]=2;
			}else if(buffLevl>0){
				user.ATKbuff+=1;
				result[3]=3;
			}else if(buffLevl<0){
				target.ATKbuff-=1;
				result[3]=4;
			}
		}else{
			if(buffLevl>0&&user.ATKbuff==6) result[3]=7;
			else if(buffLevl<0&&target.ATKbuff==-6) result[3]=8;
			else if(target.state==(StateEnum)1&&(stateCause==(StateEnum)1||stateCause==(StateEnum)2)) result[3]=5;
			else if(target.state==(StateEnum)2&&(stateCause==(StateEnum)1||stateCause==(StateEnum)2)) result[3]=6;
			else if(stateCause==(StateEnum)1){
				target.state=(StateEnum)1;
				result[3]=1;
			}else if(stateCause==(StateEnum)2){
				target.state=(StateEnum)2;
				result[3]=2;
			}else if(buffLevl>0){
				user.ATKbuff+=1;
				result[3]=3;
			}else if(buffLevl<0){
				target.ATKbuff-=1;
				result[3]=4;
			}
		}

	//技能结束判断
		if(target.curHP<=0)	{result[4]=1;target.curHP=0;target.dead=true;}	//判断敌方是否死亡

		return result;
	}
}
