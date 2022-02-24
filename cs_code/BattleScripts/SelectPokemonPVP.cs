using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPokemonPVP : MonoBehaviour
{
    public int n;
    public GameObject PokemonNow;
    public PVPSystem pvpSystem;

    private GameObject[] m_Desk;   //����һ��ȫ����Ϸ���GameObject���͵�����

    public void Click()
    {   
        if(pvpSystem.state==PVPBattleState.CHANGING){
            Debug.Log("select normal");
            if(pvpSystem.changeUnit(n)){
                pvpSystem.waitForNextTurn();
            }
            return;
        }
        else if(pvpSystem.state == PVPBattleState.DEAD){
            Debug.Log("select dead");
            if (pvpSystem.deadChanging(n)){
                pvpSystem.waitForNewTurn();
            }
            return;
        }
        Debug.Log("NO SELECT");
    }
}
