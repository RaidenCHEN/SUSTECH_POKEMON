using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// 战斗阶段枚举类
/// </summary>
public enum PVPBattleState
{
    START, PLAYERTURN, ENEMYTURN, CHECKINGSKILL, TURNENDING, WIN, LOST, CHANGING, DEAD
}


/// <summary>
/// 战斗系统类
/// </summary>
public class PVPSystem : MonoBehaviour
{
    public GameObject battle;
    public GameObject startUI;
    public GameObject cameral;


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
   /// 精灵按钮
   /// </summary>
    public List<Text> buttonText;
    public List<GameObject> button;
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
   public PVPBattleState state;

   /// <summary>
   /// 当前轮次
   /// </summary>
   private int turn;


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
   /// 双方技能选择
   /// </summary>
    int[] skillChoice;

 
    private void act()
    {
        state = PVPBattleState.START;
        GameObject[] m_DeskP = GameObject.FindGameObjectsWithTag("PlayerPokemonNow");
        GameObject PokemonNowP = m_DeskP[0];
        Destroy(PokemonNowP);
        GameObject[] m_DeskE = GameObject.FindGameObjectsWithTag("EnemyPokemonNow");
        GameObject PokemonNowE = m_DeskE[0];
        Destroy(PokemonNowE);
        battle.SetActive(false);
        cameral.SetActive(true);
        startUI.SetActive(true);

    }


    private void Start() {
   }
   /// <summary>
   /// 启动战斗系统
   /// </summary>
   public void startBat() {
        state = PVPBattleState.START;
        StartCoroutine(setUpBattle());
   }

   /// <summary>
   /// 初始化对战信息
   /// </summary>
   IEnumerator setUpBattle(){
       turn = 0;
       skillChoice = new int[2];
    //    Debug.Log("start Set-up");
       pokFactory = new PokFactory();
       PlayerPokNum = 0;
       EnemyPokNum = 0;
       playerList = new List<PokemonDemo1>();
       enemyList = new List<PokemonDemo1>();
       enemyPokBag.putBag(3);
       playerPokBag.putBag(3);
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
       
       battleDescriptionText.text = "战斗开始！";
       yield return new WaitForSeconds(1f);
       battleDescriptionText.text = "玩家1 派出了 "+playerUnit.name+" !";
       yield return new WaitForSeconds(2f);
       battleDescriptionText.text = "玩家2 派出了 "+enemyUnit.name+" !";
       yield return new WaitForSeconds(2f);

       state = PVPBattleState.PLAYERTURN;
    //    Debug.Log("end Set-up");
       PlayerTurn();
       yield break;
   }

   /// <summary>
   /// 玩家选择阶段
   /// </summary>
   public void PlayerTurn(){
    //    Debug.Log("start PlayerTurn");
       if(state != PVPBattleState.PLAYERTURN){
           return;
       }
       turn=0;
       button1Hud.setHud(playerUnit, 1);
       button2Hud.setHud(playerUnit, 2);
       button3Hud.setHud(playerUnit, 3);
       button4Hud.setHud(playerUnit, 4);
    //    Debug.Log("wait for choosing");
       battleDescriptionText.text = "玩家1 选择你的行动";
   }

   /// <summary>
   /// 敌方选择阶段
   /// </summary>
   public void EnemyTurn(){
    //    Debug.Log("start enemy Turn");
       if(state != PVPBattleState.ENEMYTURN){
           return;
       }
       turn=1;
       button1Hud.setHud(enemyUnit, 1);
       button2Hud.setHud(enemyUnit, 2);
       button3Hud.setHud(enemyUnit, 3);
       button4Hud.setHud(enemyUnit, 4);
    //    state = PVPBattleState.CHECKINGENEMY;
       battleDescriptionText.text = "玩家2 选择你的行动";
   }
   
   /// <summary>
   /// 战斗胜利阶段
   /// </summary>
   IEnumerator playerWin(){
    //    Debug.Log("start win phase");
       if(state != PVPBattleState.WIN){
           yield break;
       }
       battleDescriptionText.text = "玩家1 胜利！";
       //playerPokBag.updateBag(playerList);
       yield return new WaitForSeconds(2f);
       act();
   }

   /// <summary>
   /// 战斗失败阶段
   /// </summary>
   IEnumerator playerLost(){
    //    Debug.Log("start lose phase");
       if(state != PVPBattleState.LOST){
           yield break;
       }
       battleDescriptionText.text = "玩家2 胜利！";
       //playerPokBag.updateBag(playerList);
       yield return new WaitForSeconds(2f);
       act();
   }


   /// <summary>
   /// 主动更换精灵
   /// </summary>
   public bool changeUnit(int n){
    //    Debug.Log("start active changing phase");
       if(state != PVPBattleState.CHANGING){
        //    Debug.Log("changing false 1");
           return false;
       }
       if (turn % 2 ==0){
            if(playerList[n].dead){
                StartCoroutine(showDialog(playerList[n].name+"已经没有战斗能力了..."));

                            // Debug.Log("changing false 2");
                return false;
            }else{       
                skillChoice[0] = -(n+1);

                return true;
            }
       }else{
           if(enemyList[n].dead){
                StartCoroutine(showDialog(enemyList[n].name+"已经没有战斗能力了..."));
  
                // Debug.Log("changing false 2");
                return false;
            }else{       
                skillChoice[1] = -(n+1);

                return true;
            }
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
       if(state != PVPBattleState.DEAD){
           return false;
       }
       if (turn%2 ==0){
            if(playerList[n].dead){
                StartCoroutine(showDialog(playerList[n].name+"已经没有战斗能力了..."));
                return false;
            }else{       
                playerUnit = playerList[n];
                mainPanel.SetActive(true);
                battleDescriptionText.text = "玩家1 决定派出 "+playerList[n].name+"！";
                mainPanel.SetActive(false);
                playerHud.setHud(playerUnit);
                enemyHud.setHud(enemyUnit);
                GameObject[] m_Desk = GameObject.FindGameObjectsWithTag("PlayerPokemonNow");
                GameObject PokemonNow = m_Desk[0];
                Destroy(PokemonNow);

                PokItem x = playerPokBag.realList[n];
                GameObject model = x.getModel();
                Vector3 a = new Vector3(1177, 3349, -490); //ʵ����Ԥ�����position�����Զ���

                Quaternion b = Quaternion.identity;
                b.eulerAngles = new Vector3(0, -69, 0);
                GameObject Sphere = GameObject.Instantiate(model, a, b) as GameObject;
                Sphere.tag = "PlayerPokemonNow";
                Sphere.transform.localScale = new Vector3(2.8f, 2.8f, 2.8f);
                Sphere.SetActive(true);
                return true;
            }
       }else{
           if(enemyList[n].dead){
                StartCoroutine(showDialog(enemyList[n].name+"已经没有战斗能力了..."));
                return false;
            }else{       
                enemyUnit = enemyList[n];
                mainPanel.SetActive(true);
                battleDescriptionText.text = "玩家2 决定派出 " + enemyList[n].name+"！";
                mainPanel.SetActive(false);
                playerHud.setHud(playerUnit);
                enemyHud.setHud(enemyUnit);
                GameObject[] m_Desk = GameObject.FindGameObjectsWithTag("EnemyPokemonNow");
                GameObject PokemonNow = m_Desk[0];
                Destroy(PokemonNow);

                PokItem x = enemyPokBag.realList[n];
                GameObject model = x.getModel();
                Vector3 a = new Vector3(185, 3343, -478); //ʵ����Ԥ�����position�����Զ���

                Quaternion b = Quaternion.identity;
                b.eulerAngles = new Vector3(0,86,0);
                GameObject Sphere = GameObject.Instantiate(model, a, b) as GameObject;
                Sphere.tag = "EnemyPokemonNow";
                Sphere.transform.localScale = new Vector3(2.8f, 2.8f, 2.8f);
                Sphere.SetActive(true);
                return true;
            }
       }
    //    Debug.Log("changing finish");
       return false;
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
   public void waitForNextTurn(){
    //    Debug.Log("wait For Next Turn");
       StartCoroutine(changingSuccess());
   }
   IEnumerator changingSuccess(){
        // Debug.Log("normal Changing");
       mainPanel.SetActive(true);
       pokPanel.SetActive(false);
       if (turn % 2 == 0){
            // Debug.Log("Prepare E turn");
            state = PVPBattleState.ENEMYTURN;
            EnemyTurn();
       }else{
            // Debug.Log("turn = " + turn);
            // Debug.Log("Prepare activation");
            state = PVPBattleState.CHECKINGSKILL;
            StartCoroutine(skillActivate());
       }
        yield return new WaitForSeconds(0.1f);
    }

   /// <summary>
   /// 更换成功提示信息(死)
   /// </summary>
   public void waitForNewTurn(){
       StartCoroutine(changingSuccessD());
   }
   IEnumerator changingSuccessD(){
        // Debug.Log("Dead Changing");
        mainPanel.SetActive(true);
       pokPanel.SetActive(false);
       state = PVPBattleState.PLAYERTURN;
       PlayerTurn();
       yield return new WaitForSeconds(0.1f);
    }

    /// <summary>
    /// 回合结束阶段
    /// </summary>
    /// <param name="dead">对阵双方死亡情况</param>
    /// <returns></returns>
    IEnumerator TurnEnding(int dead){
       skillChoice[0] = 0;
       skillChoice[1] = 0;
    //    Debug.Log("start tu/rn ending phase");
       if(state != PVPBattleState.TURNENDING){
        //    Debug.Log("not Turnending state");
           yield break;
        }
       if (deadNumInTurn>0){
           if(PlayerPokNum==0){
               deadNumInTurn=0;
               state = PVPBattleState.LOST;
                StartCoroutine(playerLost());
           }else if(EnemyPokNum==0){
               deadNumInTurn=0;
               state = PVPBattleState.WIN;
               StartCoroutine(playerWin());
           }else{
               if(dead==1){
                   deadNumInTurn=0;
                   turn = 0;
                   state = PVPBattleState.DEAD;
                   battleDescriptionText.text = "玩家1 请更换精灵。";
                   yield return new WaitForSeconds(2f);
                   OnExchangeButton();
               }
               if(dead==2){
                   turn=1;
                   deadNumInTurn=0;
                   state = PVPBattleState.DEAD;
                   battleDescriptionText.text = "玩家2 请更换精灵。";
                   yield return new WaitForSeconds(2f);
                   OnExchangeButton();
               }
           }
       }
       else{
           state = PVPBattleState.PLAYERTURN;
           PlayerTurn();
       }
   }

   /// <summary>
   /// 所选行动执行阶段
   /// </summary>
   /// <param name="skillNum">所选技能编号</param>
   /// <returns></returns>
   IEnumerator skillActivate(){
    //    Debug.Log("activate skill");
       if(state != PVPBattleState.CHECKINGSKILL){
           yield break;
       }
       if (skillChoice[0]<0){
            // Debug.Log("start activation");
            playerUnit = playerList[-skillChoice[0]-1];
            mainPanel.SetActive(true);
            battleDescriptionText.text = "玩家1 决定换上"+playerList[-skillChoice[0]-1].name+"！";
            playerHud.setHud(playerUnit);
            enemyHud.setHud(enemyUnit);
            yield return new WaitForSeconds(1.5f);
            // Debug.Log("changing true");

            GameObject[] m_Desk = GameObject.FindGameObjectsWithTag("PlayerPokemonNow");
            GameObject PokemonNow = m_Desk[0];
            Destroy(PokemonNow);


            PokItem x = playerPokBag.realList[-skillChoice[0]-1];
            GameObject model = x.getModel();
            Vector3 a = new Vector3(1177, 3349, -490); //ʵ����Ԥ�����position�����Զ���

            Quaternion b = Quaternion.identity;
            b.eulerAngles = new Vector3(0,-69,0);
            GameObject Sphere = GameObject.Instantiate(model, a, b) as GameObject;
            Sphere.tag = "PlayerPokemonNow";
            Sphere.transform.localScale = new Vector3(2.8f, 2.8f, 2.8f);
            Sphere.SetActive(true);
            // if (battleSystem.state==PVPBattleState.CHANGING){
            //     battleSystem.waitForEnemyTurn();
            // }else if(battleSystem.state ==PVPBattleState.DEAD){
            //     battleSystem.waitForPlayerTurn();
            // }
       }
       if (skillChoice[1]<0){
            enemyUnit = enemyList[-skillChoice[1]-1];
            mainPanel.SetActive(true);
            battleDescriptionText.text = "玩家2 决定换上"+enemyList[-skillChoice[1]-1].name+"！";
            playerHud.setHud(playerUnit);
            enemyHud.setHud(enemyUnit);
            yield return new WaitForSeconds(1.5f);
            // Debug.Log("changing true");

            GameObject[] m_Desk = GameObject.FindGameObjectsWithTag("EnemyPokemonNow");
            GameObject PokemonNow = m_Desk[0];
            Destroy(PokemonNow);

            PokItem x = enemyPokBag.realList[-skillChoice[1]-1];
            GameObject model = x.getModel();
            Vector3 a = new Vector3(185, 3343, -478); //ʵ����Ԥ�����position�����Զ���

            Quaternion b = Quaternion.identity;
            b.eulerAngles = new Vector3(0,86,0);
            GameObject Sphere = GameObject.Instantiate(model, a, b) as GameObject;
            Sphere.tag = "EnemyPokemonNow";
            Sphere.transform.localScale = new Vector3(2.8f, 2.8f, 2.8f);
            Sphere.SetActive(true);
            // if (battleSystem.state==PVPBattleState.CHANGING){
            //     battleSystem.waitForEnemyTurn();
            // }else if(battleSystem.state ==PVPBattleState.DEAD){
            //     battleSystem.waitForPlayerTurn();
            // }
       }

       if(skillChoice[0]>0){
            battleDescriptionText.text ="玩家1 的" + playerUnit.name + "使用了" + playerUnit.getSkill(skillChoice[0]-1).name + " !";
            yield return new WaitForSeconds(2f);
            int[] result = playerUnit.curList[skillChoice[0]-1].combat(ref playerUnit, ref enemyUnit);//[0]==麻痹是否触发,[1]==是否命中,[2]==克制情况,[3]==状态变化,[4]==是否死亡
            // Debug.Log("result:"+ result[0]+result[1]+result[2]+result[3]+result[4]);
            switch(result[0]){
                case 1 :
                    battleDescriptionText.text = "玩家1 的" + playerUnit.name + "麻痹了\n无法行动！";
                    yield return new WaitForSeconds(2f);
                    break;
                default:
                    break;
            }
            switch(result[1]){
                case 0 :
                    battleDescriptionText.text = "玩家1 的" + playerUnit.name + "没有击中对手！";
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
                    battleDescriptionText.text = "对" + "玩家2 的" + enemyUnit.name + "没有效果...";
                    yield return new WaitForSeconds(1f);
                    break;
                default:
                    break;
            }
            enemyHud.updateHP(enemyUnit);
            switch(result[4]){
                case 1 :
                    battleDescriptionText.text = "玩家2 的" + enemyUnit.name + "倒下了...";
                    enemyUnit.dead = true;
                    yield return new WaitForSeconds(2f);
                    EnemyPokNum--;
                    deadNumInTurn++;
                    state = PVPBattleState.TURNENDING;
                    StartCoroutine(TurnEnding(2));
                    yield break;
                default:
                    break;
            }
            if(result[4]==0){
                switch(result[3]){
                    case 1 :
                        battleDescriptionText.text = "玩家2 的" + enemyUnit.name + "麻痹了...\n可能无法行动！";
                        yield return new WaitForSeconds(1f);
                        break;
                    case 2 :
                        battleDescriptionText.text = "玩家2 的" + enemyUnit.name + "中毒了...";
                        yield return new WaitForSeconds(1f);
                        break;
                    case 3 :
                        battleDescriptionText.text = "玩家1 的" + playerUnit.name + "的物攻和特攻提升了！！";
                        yield return new WaitForSeconds(1f);
                        break;
                    case 4 :
                        battleDescriptionText.text = "玩家2 的" + enemyUnit.name + "的物攻和特攻下降了...";
                        yield return new WaitForSeconds(1f);
                        break;
                    case 5:
                        battleDescriptionText.text = "玩家2 的" + enemyUnit.name + "已经麻痹了...";
                        yield return new WaitForSeconds(1f);
                        break;
                    case 6:
                        battleDescriptionText.text = "玩家2 的" + enemyUnit.name + "已经中毒了...";
                        yield return new WaitForSeconds(1f);
                        break;
                    case 7:
                        battleDescriptionText.text = "玩家1 的" + playerUnit.name + "的物攻和特攻不能再上升了...";
                        yield return new WaitForSeconds(1f);
                        break;
                    case 8:
                        battleDescriptionText.text = "玩家2 的" + enemyUnit.name + "的物攻和特攻不能再下降了...";
                        yield return new WaitForSeconds(1f);
                        break;
                    default:
                        break;
                }
            }
            enemyHud.updateState(enemyUnit);
             if(GameObject.FindGameObjectsWithTag("PlayerPokemonNow")[0].GetComponent<Animator>()!=null)
             GameObject.FindGameObjectsWithTag("PlayerPokemonNow")[0].GetComponent<Animator>().SetInteger("state", 2);
             if(GameObject.FindGameObjectsWithTag("EnemyPokemonNow")[0].GetComponent<Animator>()!=null)
            GameObject.FindGameObjectsWithTag("EnemyPokemonNow")[0].GetComponent<Animator>().SetInteger("state", 1);
            Eboom.SetActive(true);
            Invoke("setEboom", 2.5f);
        }

       if(skillChoice[1]>0){
            battleDescriptionText.text ="玩家2 的" + enemyUnit.name + "使用了" + enemyUnit.getSkill(skillChoice[1]-1).name + " !";
            yield return new WaitForSeconds(2f);
            int[] result = enemyUnit.curList[skillChoice[1]-1].combat(ref enemyUnit, ref playerUnit);//[0]==麻痹是否触发,[1]==是否命中,[2]==克制情况,[3]==状态变化,[4]==是否死亡
            // Debug.Log("result:"+ result[0]+result[1]+result[2]+result[3]+result[4]);
            switch(result[0]){
                case 1 :
                    battleDescriptionText.text = "玩家2 的" + enemyUnit.name + "麻痹了\n无法行动！";
                    yield return new WaitForSeconds(2f);
                    break;
                default:
                    break;
            }
            switch(result[1]){
                case 0 :
                    battleDescriptionText.text = "玩家2 的" + enemyUnit.name + "没有击中对手！";
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
                    battleDescriptionText.text = "对" + "玩家1 的" + playerUnit.name + "没有效果...";
                    yield return new WaitForSeconds(1f);
                    break;
                default:
                    break;
            }
            playerHud.updateHP(playerUnit);
            switch(result[4]){
                case 1 :
                    battleDescriptionText.text = "玩家1 的" + playerUnit.name + "倒下了...";
                    playerUnit.dead = true;
                    yield return new WaitForSeconds(2f);
                    PlayerPokNum--;
                    deadNumInTurn++;
                    state = PVPBattleState.TURNENDING;
                    StartCoroutine(TurnEnding(1));
                    yield break;
                default:
                    break;
            }
            if(result[4]==0){
                switch(result[3]){
                    case 1 :
                        battleDescriptionText.text = "玩家1 的" + playerUnit.name + "麻痹了...\n可能无法行动！";
                        yield return new WaitForSeconds(1f);
                        break;
                    case 2 :
                        battleDescriptionText.text = "玩家1 的" + playerUnit.name + "中毒了...";
                        yield return new WaitForSeconds(1f);
                        break;
                    case 3 :
                        battleDescriptionText.text = "玩家2 的" + enemyUnit.name + "的物攻和特攻提升了！！";
                        yield return new WaitForSeconds(1f);
                        break;
                    case 4 :
                        battleDescriptionText.text = "玩家1 的" + playerUnit.name + "的物攻和特攻下降了...";
                        yield return new WaitForSeconds(1f);
                        break;
                    case 5:
                        battleDescriptionText.text = "玩家1 的" + playerUnit.name + "已经麻痹了...";
                        yield return new WaitForSeconds(1f);
                        break;
                    case 6:
                        battleDescriptionText.text = "玩家1 的" + playerUnit.name + "已经中毒了...";
                        yield return new WaitForSeconds(1f);
                        break;
                    case 7:
                        battleDescriptionText.text = "玩家2 的" + enemyUnit.name + "的物攻和特攻不能再上升了...";
                        yield return new WaitForSeconds(1f);
                        break;
                    case 8:
                        battleDescriptionText.text = "玩家1 的" + playerUnit.name + "的物攻和特攻不能再下降了...";
                        yield return new WaitForSeconds(1f);
                        break;
                    default:
                        break;
                }
            }
            playerHud.updateState(playerUnit);
            if(GameObject.FindGameObjectsWithTag("PlayerPokemonNow")[0].GetComponent<Animator>()!=null)
            GameObject.FindGameObjectsWithTag("PlayerPokemonNow")[0].GetComponent<Animator>().SetInteger("state", 1);
            if(GameObject.FindGameObjectsWithTag("EnemyPokemonNow")[0].GetComponent<Animator>()!=null)
            GameObject.FindGameObjectsWithTag("EnemyPokemonNow")[0].GetComponent<Animator>().SetInteger("state", 2);
            Pboom.SetActive(true);
            Invoke("setPboom", 2.5f);
        } 
       state = PVPBattleState.TURNENDING;
       StartCoroutine(TurnEnding(0));
       
   }

   /// <summary>
   /// 按下一号技能按钮
   /// </summary>
   public void OnSkiillOneButton(){
    //    Debug.Log("sk1Butt");
       if(state != PVPBattleState.PLAYERTURN && state != PVPBattleState.ENEMYTURN){
           return;
       }
       if (state == PVPBattleState.PLAYERTURN){
           skillChoice[0] = 1;
           state = PVPBattleState.ENEMYTURN;
           EnemyTurn();
       }else{
           skillChoice[1] = 1;
           state = PVPBattleState.CHECKINGSKILL;
            StartCoroutine(skillActivate());
       }
   }

   /// <summary>
   /// 按下二号技能按钮
   /// </summary>
   public void OnSkiillTwoButton(){
    //    Debug.Log("sk2Butt");
       if(state != PVPBattleState.PLAYERTURN && state != PVPBattleState.ENEMYTURN){
           return;
       }
       if (state == PVPBattleState.PLAYERTURN){
           skillChoice[0] = 2;
           state = PVPBattleState.ENEMYTURN;
           EnemyTurn();
       }else{
           skillChoice[1] = 2;
           state = PVPBattleState.CHECKINGSKILL;
            StartCoroutine(skillActivate());
       }
   }

   /// <summary>
   /// 按下三号技能按钮
   /// </summary>
   public void OnSkiillThreeButton(){
    //    Debug.Log("sk3Butt");
       if(state != PVPBattleState.PLAYERTURN && state != PVPBattleState.ENEMYTURN){
           return;
       }
       if (state == PVPBattleState.PLAYERTURN){
           skillChoice[0] = 3;
           state = PVPBattleState.ENEMYTURN;
           EnemyTurn();
       }else{
           skillChoice[1] = 3;
           state = PVPBattleState.CHECKINGSKILL;
            StartCoroutine(skillActivate());
       }
   }

   /// <summary>
   /// 按下四号技能按钮
   /// </summary>
   public void OnSkiillFourButton(){
    //    Debug.Log("sk4Butt");
       if(state != PVPBattleState.PLAYERTURN && state != PVPBattleState.ENEMYTURN){
           return;
       }
       if (state == PVPBattleState.PLAYERTURN){
           skillChoice[0] = 4;
           state = PVPBattleState.ENEMYTURN;
           EnemyTurn();
       }else{
           skillChoice[1] = 4;
           state = PVPBattleState.CHECKINGSKILL;
            StartCoroutine(skillActivate());
       }
       
   }


   /// <summary>
   /// 按下交换精灵按钮
   /// </summary>
   public void OnExchangeButton(){
    //    Debug.Log("exchangeButt");
       if(state != PVPBattleState.PLAYERTURN && state != PVPBattleState.ENEMYTURN && state != PVPBattleState.DEAD){
           return;
       }
       mainPanel.SetActive(false);
       pokPanel.SetActive(true);
       state = (state == PVPBattleState.PLAYERTURN||state == PVPBattleState.ENEMYTURN)?PVPBattleState.CHANGING:PVPBattleState.DEAD;
       if (turn%2==0){
           for (int i = 0; i < playerPokBag.realList.Count; i++)
                    {
                        buttonText[i].text = playerPokBag.realList[i].name;
                        button[i].SetActive(true);
                    }
           for (int i = playerPokBag.realList.Count; i < 3; i++)
                    {
                        button[i].SetActive(false);
                    }
       }else{
           for (int i = 0; i < enemyPokBag.realList.Count; i++)
                    {
                        buttonText[i].text = enemyPokBag.realList[i].name;
                        button[i].SetActive(true);
                    }
           for (int i = enemyPokBag.realList.Count; i < 3; i++)
                    {
                        button[i].SetActive(false);
                    }
       }

   }

   /// <summary>
   /// 返回MainPanel
   /// </summary>
   public void backMainPanel(){
    //    Debug.Log("exchangeButt");
       if(state != PVPBattleState.CHANGING){//  && state != PVPBattleState.PLAYERTURN && state != PVPBattleState.ENEMYTURN
           return;
       }
       pokPanel.SetActive(false);
       mainPanel.SetActive(true);
       state = turn%2==0?PVPBattleState.PLAYERTURN:PVPBattleState.ENEMYTURN;
   }
}

