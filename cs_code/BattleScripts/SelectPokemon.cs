using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPokemon : MonoBehaviour
{
    public int n;
    public PokBag bag;
    public GameObject PokemonNow;
    public BattleSystem battleSystem;

    private GameObject[] m_Desk;   //����һ��ȫ����Ϸ���GameObject���͵�����

    public bool notifyBattleSystem(int n){
        if (battleSystem.state==BattleState.CHANGING){
            Debug.Log("go change player unit");
            return battleSystem.changePlayerUnit(n);
        }else if(battleSystem.state ==BattleState.DEAD){
            return battleSystem.deadChanging(n);
        }
        return false;
    }
    public void Click()
    {   
        if(notifyBattleSystem(n)){
            
            m_Desk = GameObject.FindGameObjectsWithTag("PlayerPokemonNow");
            PokemonNow = m_Desk[0];
            Destroy(PokemonNow);


            PokItem x = bag.realList[n];
            GameObject model = x.getModel();
            Vector3 a = new Vector3(1177, 3349, -490); //ʵ����Ԥ�����position�����Զ���

            Quaternion b = Quaternion.identity;
            b.eulerAngles = new Vector3(0,-69,0);
            GameObject Sphere = GameObject.Instantiate(model, a, b) as GameObject;
            Sphere.tag = "PlayerPokemonNow";
            Sphere.transform.localScale = new Vector3(2.8f, 2.8f, 2.8f);
            Sphere.SetActive(true);
            if (battleSystem.state==BattleState.CHANGING){
                battleSystem.waitForEnemyTurn();
            }else if(battleSystem.state ==BattleState.DEAD){
                battleSystem.waitForPlayerTurn();
            }
            
        }
    }

    
}
