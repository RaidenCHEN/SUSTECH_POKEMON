using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// 战斗阶段枚举类
/// </summary>
public enum BattleState
{
    START, PLAYERTURN, ENEMYTURN, CHECKINGPLAYER, CHECKINGENEMY, TURNENDING, WIN, LOST, CHANGING, ESCAPE, DEAD, WORLD, CAUGHT
}


/// <summary>
/// 战斗系统类
/// </summary>
public class BattleSystem : MonoBehaviour
{
    public GameObject Pboom;
    private void setPboom(){
        Pboom.SetActive(false);
    }
    public GameObject Eboom;
    private void setEboom(){
        Eboom.SetActive(false);
    }
   /// <summary>
   /// 回合内总死亡精灵数
   /// </summary>
   int deadNumInTurn;

   /// <summary>
   /// 我方与敌方存活精灵数
   /// </summary>
   int PlayerPokNum;
   int EnemyPokNum;

   /// <summary>
   /// 战斗描述文本
   /// </summary>
   public Text battleDescriptionText;

   /// <summary>
   /// 玩家精灵背包
   /// </summary>
   public PokBag playerPokBag;

   /// <summary>
   /// 敌方精灵背包
   /// </summary>
   public PokBag enemyPokBag;

   /// <summary>
   /// 玩家场上实例单位
   /// </summary>
   PokemonDemo1 playerUnit;

   /// <summary>
   /// 敌方场上实例单位
   /// </summary>
   PokemonDemo1 enemyUnit;

   /// <summary>
   /// 玩家实例单位列表
   /// </summary>
   List<PokemonDemo1> playerList;

   /// <summary>
   /// 敌方实例单位列表
   /// </summary>
   List<PokemonDemo1> enemyList;

   /// <summary>
   /// 交换按钮
   /// </summary>
   public Button changebutton;

   /// <summary>
   /// 技能按钮UI
   /// </summary>
   public ButtonText button1Hud;
   public ButtonText button2Hud;
   public ButtonText button3Hud;
   public ButtonText button4Hud;

   /// <summary>
   /// 玩家HUD
   /// </summary>
   public BattleHud playerHud;

   /// <summary>
   /// 敌方HUD
   /// </summary>
   public BattleHud enemyHud;

   /// <summary>
   /// 工厂
   /// </summary>
   PokFactory pokFactory;

   /// <summary>
   /// 当前战斗阶段
   /// </summary>
   public BattleState state;

   /// <summary>
   /// 面板切换
   /// </summary>
   public GameObject mainPanel;
   public GameObject pokPanel;

   /// <summary>
   /// 模型
   /// </summary>
    GameObject playerModel;
    GameObject enemyModel;

    /// <summary>
    /// 退出战斗系统
    /// </summary>
    public GameObject player;
    public GameObject battle;
    private void act()
    {
        state = BattleState.WORLD;
      player.SetActive(true);
      battle.SetActive(false);

    }

    /// <summary>
    /// 启动战斗系统
    /// </summary>
    public void startBat(int type) {
      if(state == BattleState.WORLD){
        state = BattleState.START;
        StartCoroutine(setUpBattle(type));
      }else{
          return;
      }
   }

   /// <summary>
   /// 初始化对战信息
   /// </summary>
   IEnumerator setUpBattle(int type){
    //    Debug.Log("start Set-up");
       pokFactory = new PokFactory();
       PlayerPokNum = 0;
       EnemyPokNum = 0;
       playerList = new List<PokemonDemo1>();
       enemyList = new List<PokemonDemo1>();
       enemyPokBag.putBag(type);
       playerPokBag.putBag(0);
       for (int i = 0; i < playerPokBag.poknum(); i++)
       {
           playerList.Add(pokFactory.create(playerPokBag.realList[i]));
           if(playerList[i].curHP==0||playerList[i].dead){
           }else{
               PlayerPokNum++;
           }
       }
       for (int i = 0; i < enemyPokBag.poknum(); i++)
       {
           enemyList.Add(pokFactory.create(enemyPokBag.realList[i]));
           if(enemyList[i].curHP==0||enemyList[i].dead){
           }else{
               EnemyPokNum++;
           }
       }
       for (int i = 0; i < playerList.Count; i++)
       {
           if(playerList[i].curHP>0 && !playerList[i].dead){
               playerUnit = playerList[i];
               playerModel = playerPokBag.realList[i].getModel();
               break;
           }
       }
       for (int i = 0; i < enemyList.Count; i++)
       {
           if(enemyList[i].curHP>0 && !enemyList[i].dead){
               enemyUnit = enemyList[i];
               enemyModel = enemyPokBag.realList[i].getModel();
               break;
           }
       }
       deadNumInTurn = 0;
       
       if(PlayerPokNum<=0){
           state = BattleState.WORLD;
           act();
       }


       Vector3 ap = new Vector3(1177, 3349, -490); 
       Quaternion bp = Quaternion.identity;
       bp.eulerAngles = new Vector3(0,-69,0);
       GameObject playerSphere = GameObject.Instantiate(playerModel, ap, bp) as GameObject;
       playerSphere.tag = "PlayerPokemonNow";
       playerSphere.transform.localScale = new Vector3(2.8f, 2.8f, 2.8f);
       playerSphere.SetActive(true);
       Vector3 ae = new Vector3(185, 3343, -478); 
       Quaternion be = Quaternion.identity;
       be.eulerAngles = new Vector3(0,86,0);
       GameObject enemySphere = GameObject.Instantiate(enemyModel, ae, be) as GameObject;
       enemySphere.tag = "EnemyPokemonNow";
       enemySphere.transform.localScale = new Vector3(2.8f, 2.8f, 2.8f);
       enemySphere.SetActive(true);

       playerHud.setHud(playerUnit);
       enemyHud.setHud(enemyUnit);
       button1Hud.setHud(playerUnit, 1);
       button2Hud.setHud(playerUnit, 2);
       button3Hud.setHud(playerUnit, 3);
       button4Hud.setHud(playerUnit, 4);
       if(type==1){
           battleDescriptionText.text = "A wild "+enemyUnit.name+" approaches!";
           yield return new WaitForSeconds(2f);
       }else{
           battleDescriptionText.text = "A trainer starts a battle!";
           yield return new WaitForSeconds(1f);
           battleDescriptionText.text = "The trainer chooses "+enemyUnit.name+" !";
           yield return new WaitForSeconds(2f);
       }

       state = BattleState.PLAYERTURN;
    //    Debug.Log("end Set-up");
       PlayerTurn();
   }

   /// <summary>
   /// 玩家选择阶段
   /// </summary>
   void PlayerTurn(){
    //    Debug.Log("start PlayerTurn");
       if(state != BattleState.PLAYERTURN){
           return;
       }
    //    Debug.Log("wait for choosing");
       battleDescriptionText.text = "Choose your action...";
   }

   /// <summary>
   /// 敌方选择阶段
   /// </summary>
   public void EnemyTurn(){
    //    Debug.Log("start enemy Turn");
       if(state != BattleState.ENEMYTURN){
           return;
       }
       System.Random rand = new System.Random();
       state = BattleState.CHECKINGENEMY;
       StartCoroutine(enemySkillActivate(rand.Next(1,4)));
   }
   
   /// <summary>
   /// 战斗胜利阶段
   /// </summary>
   IEnumerator playerWin(){
    //    Debug.Log("start win phase");
       if(state != BattleState.WIN){
           yield break;
       }
       battleDescriptionText.text = "战斗胜利！";
       yield return new WaitForSeconds(2f);
       playerPokBag.updateBag(playerList);
        act();
   }

   /// <summary>
   /// 成功捕捉阶段
   /// </summary>
   IEnumerator playerCaught(){
    //    Debug.Log("start caught phase");
       if(state != BattleState.CAUGHT){
           yield break;
       }
       battleDescriptionText.text = "好耶！抓到" + enemyUnit.name + "了！";
       yield return new WaitForSeconds(2f);
       playerPokBag.cUpdateBag(playerList, enemyUnit, enemyPokBag.realList[0]);
        playerPokBag.putBag(0);
        act();
        //    playerPokBag.cUpdateBag(playerList, enemyList[0]);
        //退出战斗场景实现
        //*************************************
    }

   /// <summary>
   /// 战斗失败阶段
   /// </summary>
   IEnumerator playerLost(){
    //    Debug.Log("start lose phase");
       if(state != BattleState.LOST){
           yield break;
       }
       battleDescriptionText.text = "战斗失败...";
       yield return new WaitForSeconds(2f);
       playerPokBag.updateBag(playerList);
        act();
        //    playerPokBag.updateBag(playerList);
        //退出战斗场景实现
        //*************************************
    }

   /// <summary>
   /// 逃跑阶段
   /// </summary>
   IEnumerator escape(){
    //    Debug.Log("escape from battle");
       if(state != BattleState.ESCAPE){
           yield break;
       }
       battleDescriptionText.text = "溜了溜了~~~~~";
       yield return new WaitForSeconds(2f);
       playerPokBag.updateBag(playerList);
        act();
        //退出战斗场景实现
        //*************************************
    }

   /// <summary>
   /// 主动更换精灵
   /// </summary>
   public bool changePlayerUnit(int n){
    //    Debug.Log("start active changing phase");
       if(state != BattleState.CHANGING){
                        //  Debug.Log("changing false 1");
           return false;
       }
       if(playerList[n].dead){
           StartCoroutine(showDialog(playerList[n].name+"已经没有战斗能力了..."));
        //    pokPanel.SetActive(false);
        //    mainPanel.SetActive(true);
        //    battleDescriptionText.text = playerList[n].name+"已经没有战斗能力了...";
        //    System.Threading.Thread.Sleep(1000);
        //    mainPanel.SetActive(false);
        //    pokPanel.SetActive(true);
                    //   Debug.Log("changing false 2");
           return false;
       }else{       
           playerUnit = playerList[n];
        //    StartCoroutine(showDialog("我决定换上"+playerList[n].name+"！"));
           mainPanel.SetActive(true);
           battleDescriptionText.text = "我决定换上"+playerList[n].name+"！";
           mainPanel.SetActive(false);
        //    System.Threading.Thread.Sleep(1000);
        //    backMainPanel();
           playerHud.setHud(playerUnit);
           enemyHud.setHud(enemyUnit);
           button1Hud.setHud(playerUnit, 1);
           button2Hud.setHud(playerUnit, 2);
           button3Hud.setHud(playerUnit, 3);
           button4Hud.setHud(playerUnit, 4);
        //    state = BattleState.ENEMYTURN;
                        //  Debug.Log("changing true");
           return true;
       }
        // Debug.Log("changing false 3");
       return false;

    //    return;
   }

   /// <summary>
   /// 死亡后更换精灵
   /// </summary>
   public bool deadChanging(int n){
    //    Debug.Log("start dead changing phase");
       if(state != BattleState.DEAD){
           return false;
       }
       if(playerList[n].dead){
           StartCoroutine(showDialog(playerList[n].name+"已经没有战斗能力了..."));
           return false;
       }else{       
           playerUnit = playerList[n];
           mainPanel.SetActive(true);
           battleDescriptionText.text = "就决定是你了！ "+playerList[n].name+"！";
           mainPanel.SetActive(false);
           playerHud.setHud(playerUnit);
           enemyHud.setHud(enemyUnit);
           button1Hud.setHud(playerUnit, 1);
           button2Hud.setHud(playerUnit, 2);
           button3Hud.setHud(playerUnit, 3);
           button4Hud.setHud(playerUnit, 4);
           return true;
       }
    //    Debug.Log("changing finish");
       return false;
   }


   /// <summary>
   /// 敌方死亡后更换精灵
   /// </summary>
   IEnumerator deadChangingEnemy(){
    //    Debug.Log("start enemy dead changing phase");
       if(state != BattleState.DEAD){
           yield break;
       }
       PokItem x = enemyPokBag.realList[0];
       for (int i = 0; i < enemyList.Count; i++)
       {
           if(!enemyList[i].dead){
               enemyUnit = enemyList[i];
               x = enemyPokBag.realList[i];
               break;
           }
       }
       battleDescriptionText.text = "敌人换上了 "+enemyUnit.name+"！";
       yield return new WaitForSeconds(1.5f);
       
       GameObject[] m_Desk = GameObject.FindGameObjectsWithTag("EnemyPokemonNow");
       GameObject PokemonNow = m_Desk[0];
       Destroy(PokemonNow);
       GameObject model = x.getModel();
       Vector3 a = new Vector3(185, 3343, -478); //ʵ����Ԥ�����position�����Զ��   
       Quaternion b = Quaternion.identity;
       b.eulerAngles = new Vector3(0,86,0);
       GameObject Sphere = GameObject.Instantiate(model, a, b) as GameObject;
       Sphere.tag = "EnemyPokemonNow";
       Sphere.transform.localScale = new Vector3(2.8f, 2.8f, 2.8f);
       Sphere.SetActive(true);

        //    Vector3 ae = new Vector3(185, 3343, -478); 
        //    Quaternion be = Quaternion.identity;
        //    be.eulerAngles = new Vector3(0,86,0);
        //    GameObject enemySphere = GameObject.Instantiate(enemyModel, ae, be) as GameObject;
        //    enemySphere.tag = "EnemyPokemonNow";
        //    enemySphere.transform.localScale = new Vector3(2.8f, 2.8f, 2.8f);
        enemyHud.setHud(enemyUnit);
       state = BattleState.PLAYERTURN;
       PlayerTurn();
   }


   /// <summary>
   /// 更换失败提示信息
   /// </summary>
   IEnumerator showDialog(string tip){
    //    Debug.Log("start changing phase");
       pokPanel.SetActive(false);
       mainPanel.SetActive(true);
       battleDescriptionText.text = tip;
       yield return new WaitForSeconds(2f);
       mainPanel.SetActive(false);
       pokPanel.SetActive(true);
    //    Debug.Log("changing finish");
   }

   /// <summary>
   /// 更换成功提示信息
   /// </summary>
   public void waitForEnemyTurn(){
    //    Debug.Log("wait For Enemy Turn");
       StartCoroutine(changingSuccess());
   }
   IEnumerator changingSuccess(){
       mainPanel.SetActive(true);
       pokPanel.SetActive(false);
       yield return new WaitForSeconds(2f);
       state = BattleState.ENEMYTURN;
       EnemyTurn();
   }

   /// <summary>
   /// 更换成功提示信息(死)
   /// </summary>
   public void waitForPlayerTurn(){
       StartCoroutine(changingSuccessD());
   }
   IEnumerator changingSuccessD(){
       mainPanel.SetActive(true);
       pokPanel.SetActive(false);
       yield return new WaitForSeconds(2f);
       state = BattleState.PLAYERTURN;
       PlayerTurn();
   }

   /// <summary>
   /// 回合结束阶段，结算中毒效果
   /// </summary>
   /// <param name="dead">对阵双方死亡情况</param>
   /// <returns></returns>
   IEnumerator TurnEnding(int dead){
    //    Debug.Log("start turn ending phase");
       if(state != BattleState.TURNENDING){
        //    Debug.Log("no Turnending state");
           yield break;
        }
       switch(dead){// 0 == 无人阵亡 1==己方阵亡 2==敌方阵亡 3==双方阵亡
           case 0:
               if(playerUnit.state==StateEnum.POISONED){
                   battleDescriptionText.text = playerUnit.name + "受到中毒伤害...";
                   yield return new WaitForSeconds(1f);
                   if(playerUnit.checkPoison()){
                       playerHud.updateHP(playerUnit);
                       playerHud.updateState(playerUnit);
                       battleDescriptionText.text = playerUnit.name + "倒下了...";
                       PlayerPokNum--;
                       deadNumInTurn++;
                       yield return new WaitForSeconds(2f);
                   }else{
                       playerHud.updateHP(playerUnit);
                       playerHud.updateState(playerUnit);;
                   }
               }
               if(enemyUnit.state==StateEnum.POISONED){
                   battleDescriptionText.text = enemyUnit.name + "受到中毒伤害...";
                   yield return new WaitForSeconds(1f);
                   if(enemyUnit.checkPoison()){
                       enemyHud.updateHP(enemyUnit);
                       enemyHud.updateState(enemyUnit);
                       battleDescriptionText.text = enemyUnit.name + "倒下了...";
                       EnemyPokNum--;
                       deadNumInTurn++;
                       yield return new WaitForSeconds(2f);
                   }else{
                       enemyHud.updateHP(enemyUnit);
                       enemyHud.updateState(enemyUnit);
                   }
               }
               break;
           case 1:
               if(enemyUnit.state==StateEnum.POISONED){
                   battleDescriptionText.text = enemyUnit.name + "受到中毒伤害...";
                   yield return new WaitForSeconds(1f);
                   if(enemyUnit.checkPoison()){
                       enemyHud.updateHP(enemyUnit);
                       enemyHud.updateState(enemyUnit);
                       battleDescriptionText.text = enemyUnit.name + "倒下了...";
                       EnemyPokNum--;
                       deadNumInTurn++;
                       yield return new WaitForSeconds(2f);
                   }else{
                       enemyHud.updateHP(enemyUnit);
                       enemyHud.updateState(enemyUnit);;
                   }
               }
               break;
           case 2:
               if(playerUnit.state==StateEnum.POISONED){
                   battleDescriptionText.text = playerUnit.name + "受到中毒伤害...";
                   
                   yield return new WaitForSeconds(1f);
                   if(playerUnit.checkPoison()){
                       playerHud.updateHP(playerUnit);
                       playerHud.updateState(playerUnit);
                       battleDescriptionText.text = playerUnit.name + "倒下了...";
                       PlayerPokNum--;
                       deadNumInTurn++;
                       yield return new WaitForSeconds(2f);

                   }else{
                       playerHud.updateHP(playerUnit);
                       playerHud.updateState(playerUnit);;
                   }
               }
               break;
           default :
               break;
       }
       if (deadNumInTurn>0){
           if(PlayerPokNum==0){
               deadNumInTurn=0;
               state = BattleState.LOST;
               StartCoroutine(playerLost());
           }else if(EnemyPokNum==0){
               deadNumInTurn=0;
               state = BattleState.WIN;
               StartCoroutine(GainEXP());
           }else{
               if(dead==1){
                   deadNumInTurn=0;
                   state = BattleState.DEAD;
                   OnExchangeButton();
                   changebutton.onClick.Invoke();
               }
               if(dead==2){
                   deadNumInTurn=0;
                   state = BattleState.DEAD;
                   StartCoroutine(GainEXP());
               }
           }
       }
       else{
           state = BattleState.PLAYERTURN;
           PlayerTurn();
       }
   }

   /// <summary>
   /// 玩家所选技能执行阶段
   /// </summary>
   /// <param name="skillNum">所选技能编号</param>
   /// <returns></returns>
   IEnumerator playerSkillActivate(int skillNum){
    //    Debug.Log("activate player skill");
       if(state != BattleState.CHECKINGPLAYER){
           yield break;
       }
       battleDescriptionText.text ="我方的" + playerUnit.name + "使用了" + playerUnit.getSkill(skillNum-1).name + " !";
       yield return new WaitForSeconds(2f);
       int[] result = playerUnit.curList[skillNum-1].combat(ref playerUnit, ref enemyUnit);//[0]==麻痹是否触发,[1]==是否命中,[2]==克制情况,[3]==状态变化,[4]==是否死亡
    //    Debug.Log("result:"+ result[0]+result[1]+result[2]+result[3]+result[4]);
       switch(result[0]){
           case 1 :
               battleDescriptionText.text = "我方的" + playerUnit.name + "麻痹了\n无法行动！";
               yield return new WaitForSeconds(2f);
               break;
           default:
               break;
       }
       switch(result[1]){
           case 0 :
               battleDescriptionText.text = "我方的" + playerUnit.name + "没有击中对手！";
               yield return new WaitForSeconds(2f);
               break;
           default:
               break;
       }
       switch(result[2]){
           case 1 :
               battleDescriptionText.text = "效果拔群！";
               yield return new WaitForSeconds(1f);
               break;
           case 2 :
               battleDescriptionText.text = "效果一般...";
               yield return new WaitForSeconds(1f);
               break;
           case 3 :
               battleDescriptionText.text = "对" + "敌方的" + enemyUnit.name + "没有效果...";
               yield return new WaitForSeconds(1f);
               break;
           default:
               break;
       }
       enemyHud.updateHP(enemyUnit);
       switch(result[4]){
           case 1 :
               battleDescriptionText.text = "敌方的" + enemyUnit.name + "倒下了...";
               enemyUnit.dead = true;
               yield return new WaitForSeconds(2f);
               EnemyPokNum--;
               deadNumInTurn++;
               state = BattleState.TURNENDING;
               StartCoroutine(TurnEnding(2));
               yield break;
           default:
               break;
       }
       if(result[4]==0){
           switch(result[3]){
               case 1 :
                   battleDescriptionText.text = "敌方的" + enemyUnit.name + "麻痹了...\n可能无法行动！";
                   yield return new WaitForSeconds(1f);
                   break;
               case 2 :
                   battleDescriptionText.text = "敌方的" + enemyUnit.name + "中毒了...";
                   yield return new WaitForSeconds(1f);
                   break;
               case 3 :
                   battleDescriptionText.text = "我方的" + playerUnit.name + "的物攻和特攻提升了！！";
                   yield return new WaitForSeconds(1f);
                   break;
               case 4 :
                   battleDescriptionText.text = "敌方的" + enemyUnit.name + "的物攻和特攻下降了...";
                   yield return new WaitForSeconds(1f);
                   break;
               case 5:
                   battleDescriptionText.text = "敌方的" + enemyUnit.name + "已经麻痹了...";
                   yield return new WaitForSeconds(1f);
                   break;
               case 6:
                   battleDescriptionText.text = "敌方的" + enemyUnit.name + "已经中毒了...";
                   yield return new WaitForSeconds(1f);
                   break;
               case 7:
                   battleDescriptionText.text = "我方的" + playerUnit.name + "的物攻和特攻不能再上升了...";
                   yield return new WaitForSeconds(1f);
                   break;
               case 8:
                   battleDescriptionText.text = "敌方的" + enemyUnit.name + "的物攻和特攻不能再下降了...";
                   yield return new WaitForSeconds(1f);
                   break;
               default:
                   break;
           }
       }
       enemyHud.updateState(enemyUnit);
       state = BattleState.ENEMYTURN;
       EnemyTurn();
       if(GameObject.FindGameObjectsWithTag("PlayerPokemonNow")[0].GetComponent<Animator>()!=null)
        GameObject.FindGameObjectsWithTag("PlayerPokemonNow")[0].GetComponent<Animator>().SetInteger("state", 2);
        if(GameObject.FindGameObjectsWithTag("EnemyPokemonNow")[0].GetComponent<Animator>()!=null)
        GameObject.FindGameObjectsWithTag("EnemyPokemonNow")[0].GetComponent<Animator>().SetInteger("state", 1);
        Eboom.SetActive(true);
            Invoke("setEboom", 2.5f);
    }

   /// <summary>
   /// 敌方所选技能执行阶段
   /// </summary>
   /// <param name="skillNum">所选技能编号</param>
   /// <returns></returns>
   IEnumerator enemySkillActivate(int skillNum){
    //    Debug.Log("activate enemy skill");
       if(state != BattleState.CHECKINGENEMY){
           yield break;
        }
       battleDescriptionText.text ="敌方的" + enemyUnit.name + "使用了" + enemyUnit.getSkill(skillNum-1).name + " !";
       yield return new WaitForSeconds(2f);
       int[] result = enemyUnit.curList[skillNum-1].combat(ref enemyUnit, ref playerUnit);//[0]==麻痹是否触发,[1]==是否命中,[2]==克制情况,[3]==状态变化,[4]==是否死亡
    //    Debug.Log("result:"+ result[0]+result[1]+result[2]+result[3]+result[4]);
       switch(result[0]){
           case 1 :
               battleDescriptionText.text = "敌方的" + enemyUnit.name + "麻痹了\n无法行动！";
               yield return new WaitForSeconds(2f);
               break;
           default:
               break;
       }
       switch(result[1]){
           case 0 :
               battleDescriptionText.text = "敌方的" + enemyUnit.name + "没有击中对手！";
               yield return new WaitForSeconds(2f);
               break;
           default:
               break;
       }
       switch(result[2]){
           case 1 :
               battleDescriptionText.text = "效果拔群！";
               yield return new WaitForSeconds(1f);
               break;
           case 2 :
               battleDescriptionText.text = "效果一般...";
               yield return new WaitForSeconds(1f);
               break;
           case 3 :
               battleDescriptionText.text = "对" + "我方的" + playerUnit.name + "没有效果...";
               yield return new WaitForSeconds(1f);
               break;
           default:
               break;
       }
       playerHud.updateHP(playerUnit);
       switch(result[4]){
           case 1 :
               battleDescriptionText.text = playerUnit.name + "倒下了...";
               yield return new WaitForSeconds(2f);
               PlayerPokNum--;
               deadNumInTurn++;
               state = BattleState.TURNENDING;
               StartCoroutine(TurnEnding(1));
               yield break;
           default:
               break;
       }
       
       if(result[4]==0){
           switch(result[3]){
               case 1 :
                   battleDescriptionText.text = "我方的" + playerUnit.name + "麻痹了...\n可能无法行动！";
                   yield return new WaitForSeconds(1f);
                   break;
               case 2 :
                   battleDescriptionText.text = "我方的" + playerUnit.name + "中毒了...";
                   yield return new WaitForSeconds(1f);
                   break;
               case 3 :
                   battleDescriptionText.text = "敌方的" + enemyUnit.name + "的物攻和特攻提升了！！";
                   yield return new WaitForSeconds(1f);
                   break;
               case 4 :
                   battleDescriptionText.text = "我方的" + playerUnit.name + "的物攻和特攻下降了...";
                   yield return new WaitForSeconds(1f);
                   break;
               case 5:
                   battleDescriptionText.text = "我方的" + playerUnit.name + "已经麻痹了...";
                   yield return new WaitForSeconds(1f);
                   break;
               case 6:
                   battleDescriptionText.text = "我方的" + playerUnit.name + "已经中毒了...";
                   yield return new WaitForSeconds(1f);
                   break;
               case 7:
                   battleDescriptionText.text = "敌方的" + enemyUnit.name + "的物攻和特攻不能再上升了...";
                   yield return new WaitForSeconds(1f);
                   break;
               case 8:
                   battleDescriptionText.text = "我方的" + playerUnit.name + "的物攻和特攻不能再下降了...";
                   yield return new WaitForSeconds(1f);
                   break;
               default:
                   break;
           }
       }

       playerHud.updateState(playerUnit);
       state = BattleState.TURNENDING;
       StartCoroutine(TurnEnding(0));

        if(GameObject.FindGameObjectsWithTag("PlayerPokemonNow")[0].GetComponent<Animator>()!=null)
        GameObject.FindGameObjectsWithTag("PlayerPokemonNow")[0].GetComponent<Animator>().SetInteger("state", 1);
        if(GameObject.FindGameObjectsWithTag("EnemyPokemonNow")[0].GetComponent<Animator>()!=null)
        GameObject.FindGameObjectsWithTag("EnemyPokemonNow")[0].GetComponent<Animator>().SetInteger("state", 2);
                    Pboom.SetActive(true);
            Invoke("setPboom", 2.5f);
    }

   /// <summary>
   /// 捕捉精灵
   /// </summary>
   /// <returns></returns>
   IEnumerator catchPok(){
    //    Debug.Log("catch pokemon");
        if(state != BattleState.CHECKINGPLAYER){
           yield break;
        }
        if(!enemyUnit.wild){
            battleDescriptionText.text = "这只精灵有主人，\n我不能做小偷...";
            yield return new WaitForSeconds(2f);
            state = BattleState.ENEMYTURN;
            EnemyTurn();
            yield break;
        }
        if(playerList.Count>=6){
            battleDescriptionText.text = "我已经有六只精灵了，还是不要再抓了...";
            yield return new WaitForSeconds(2f);
            state = BattleState.PLAYERTURN;
            PlayerTurn();
            yield break;
        }
        for (int i = 0; i < playerPokBag.realList.Count; i++)
        {
            if(string.Equals(enemyUnit.name, playerPokBag.realList[i].name)){
                battleDescriptionText.text = "我已经有这只精灵了，还是不要再抓了...";
                yield return new WaitForSeconds(2f);
                state = BattleState.PLAYERTURN;
                PlayerTurn();
                yield break;
            }
        }
        battleDescriptionText.text = "投出了精灵球（南科大限定）！";
        yield return new WaitForSeconds(1.5f);
        System.Random rand = new System.Random();
        if(rand.Next(1,10)<=5){
            battleDescriptionText.text = "啊，精灵跑出来了！\n我的南科大限定精灵球......";
            yield return new WaitForSeconds(2f);
            state = BattleState.ENEMYTURN;
            EnemyTurn();
            yield break;
        }


        state = BattleState.CAUGHT;
        StartCoroutine(playerCaught());
   }

   /// <summary>
   /// 按下一号技能按钮
   /// </summary>
   public void OnSkiillOneButton(){
    //    Debug.Log("sk1Butt");
       if(state != BattleState.PLAYERTURN){
           return;
       }
       state = BattleState.CHECKINGPLAYER;
       StartCoroutine(playerSkillActivate(1));
   }

   /// <summary>
   /// 按下二号技能按钮
   /// </summary>
   public void OnSkiillTwoButton(){
    //    Debug.Log("sk2Butt");
       if(state != BattleState.PLAYERTURN){
           return;
       }
       state = BattleState.CHECKINGPLAYER;
       StartCoroutine(playerSkillActivate(2));
   }

   /// <summary>
   /// 按下三号技能按钮
   /// </summary>
   public void OnSkiillThreeButton(){
    //    Debug.Log("sk3Butt");
       if(state != BattleState.PLAYERTURN){
           return;
       }
       state = BattleState.CHECKINGPLAYER;
       StartCoroutine(playerSkillActivate(3));
   }

   /// <summary>
   /// 按下四号技能按钮
   /// </summary>
   public void OnSkiillFourButton(){
    //    Debug.Log("sk4Butt");
       if(state != BattleState.PLAYERTURN){
           return;
       }
       state = BattleState.CHECKINGPLAYER;
       StartCoroutine(playerSkillActivate(4));
   }

   /// <summary>
   /// 按下捕捉精灵按钮
   /// </summary>
   public void OnCatchButton(){
    //    Debug.Log("catchButt");
       if(state != BattleState.PLAYERTURN){
           return;
       }
       state = BattleState.CHECKINGPLAYER;
       StartCoroutine(catchPok());
   }

   /// <summary>
   /// 按下逃跑按钮
   /// </summary>
   public void OnEscapeButton(){
    //    Debug.Log("escapeButt");
       if(state != BattleState.PLAYERTURN){
           return;
       }
       state = BattleState.ESCAPE;
       StartCoroutine(escape());
   }

   /// <summary>
   /// 按下交换精灵按钮
   /// </summary>
   public void OnExchangeButton(){
    //    Debug.Log("exchangeButt");
       if(state != BattleState.PLAYERTURN && state != BattleState.DEAD){
           return;
       }
       mainPanel.SetActive(false);
       pokPanel.SetActive(true);
       state = state == BattleState.PLAYERTURN?BattleState.CHANGING:BattleState.DEAD;
   }

   /// <summary>
   /// 返回MainPanel
   /// </summary>
   public void backMainPanel(){
    //    Debug.Log("exchangeButt");
       if(state != BattleState.CHANGING && state!=BattleState.PLAYERTURN){
           return;
       }
       pokPanel.SetActive(false);
       mainPanel.SetActive(true);
       state = BattleState.PLAYERTURN;
   }

   IEnumerator GainEXP(){
       int exp = (int)Math.Pow(enemyUnit.level, 3) + 6 * (enemyUnit.level-playerUnit.level);
       int showexp = exp>=30?exp:30;
       battleDescriptionText.text = "我方的" + playerUnit.name + "获得了" + showexp + "点经验！";
       yield return new WaitForSeconds(1.5f);
       while (playerUnit.GainExp(ref showexp)){
           battleDescriptionText.text = playerUnit.name + "升到了" + (int)playerUnit.level + "级！";
           yield return new WaitForSeconds(2f);
           playerHud.setHud(playerUnit);
           button2Hud.setHud(playerUnit, 2);
           button3Hud.setHud(playerUnit, 3);
           button4Hud.setHud(playerUnit, 4);
           button1Hud.setHud(playerUnit, 1);
           if(playerUnit.level==5 && playerUnit.getAllCount() > 5){
               playerUnit.replaceSkill = 0;
               string[] a = playerUnit.LearnSkill();
               battleDescriptionText.text = playerUnit.name + "学会了" + a[1] + "并遗忘了" + a[0] + "!";
               yield return new WaitForSeconds(2f);
               button2Hud.setHud(playerUnit, 2);
               button3Hud.setHud(playerUnit, 3);
               button4Hud.setHud(playerUnit, 4);
               button1Hud.setHud(playerUnit, 1);
           }else if(playerUnit.level==7){
               string tmp = playerUnit.name;
               if(playerUnit.nextName<playerUnit.getNameList()){
                   playerUnit.Evolution();
                   playerUnit.SetAttributes();
                   playerPokBag.getItemByName(tmp).update(playerUnit);
                   battleDescriptionText.text = tmp + "进化成" + playerUnit.name + "了!";
                   playerHud.setHud(playerUnit);
                   GameObject[] m_Desk = GameObject.FindGameObjectsWithTag("PlayerPokemonNow");
                   GameObject PokemonNow = m_Desk[0];
                   Destroy(PokemonNow);
                   GameObject model = playerPokBag.getItemByName(playerUnit.name).getModel();
                   Vector3 a = new Vector3(1177, 3349, -490); //ʵ����Ԥ�����position�����Զ��   
                   Quaternion b = Quaternion.identity;
                   b.eulerAngles = new Vector3(0,-69,0);
                   GameObject Sphere = GameObject.Instantiate(model, a, b) as GameObject;
                   Sphere.tag = "PlayerPokemonNow";
                   Sphere.transform.localScale = new Vector3(2.8f, 2.8f, 2.8f);
                   Sphere.SetActive(true);
       
                   yield return new WaitForSeconds(2f);
                   playerUnit.replaceSkill = 1;
                   string[] s = playerUnit.LearnSkill();
                   battleDescriptionText.text = playerUnit.name + "学会了" + s[1] + "并遗忘了" + s[0] + "!";
                   yield return new WaitForSeconds(2f);
                   button2Hud.setHud(playerUnit, 2);
                   button3Hud.setHud(playerUnit, 3);
                   button4Hud.setHud(playerUnit, 4);
                   button1Hud.setHud(playerUnit, 1);
               }
           }
       }
       if(state == BattleState.DEAD){
           state = BattleState.DEAD;
           StartCoroutine(deadChangingEnemy());
       }else if(state == BattleState.WIN){
           StartCoroutine(playerWin());
       }
   }
}