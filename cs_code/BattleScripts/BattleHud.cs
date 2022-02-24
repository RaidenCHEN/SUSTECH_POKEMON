using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;
    public Text stateText;

    public void setHud(PokemonDemo1 pokInBattle){
        this.nameText.text = pokInBattle.name;
        this.levelText.text = "Lv.  " + pokInBattle.level;
        this.hpSlider.maxValue = pokInBattle.maxHP;
        this.hpSlider.value = pokInBattle.curHP;
        if(pokInBattle.state == StateEnum.NORMAL){
            this.stateText.text = "Healthy";
        }else if(pokInBattle.state == StateEnum.PARASITE){
            this.stateText.text = "Parasite";
        }else if(pokInBattle.state == StateEnum.POISONED){
            this.stateText.text = "Poisoned";
        }
    }

    public void updateHP(PokemonDemo1 pokInBattle){
        hpSlider.value = pokInBattle.curHP;
    }
    public void updateState(PokemonDemo1 pokInBattle){
        if(pokInBattle.state == StateEnum.NORMAL){
            this.stateText.text = "Healthy";
        }else if(pokInBattle.state == StateEnum.PARASITE){
            this.stateText.text = "Parasite";
        }else if(pokInBattle.state == StateEnum.POISONED){
            this.stateText.text = "Poisoned";
        }
    }
    
}
