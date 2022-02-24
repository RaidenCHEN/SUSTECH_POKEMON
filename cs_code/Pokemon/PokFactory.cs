using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PokFactory
{
    public PokemonDemo1 create(PokItem item){
        if(string.Equals(item.name,"喷火龙")||string.Equals(item.name,"小火龙")){
            return new Charizard(item.level, item.name, item.state, item.curHP, item.curexp, item.isDead, item.iswild);
        }else if(string.Equals(item.name,"雷丘")||string.Equals(item.name,"皮卡丘")){
            return new Pikachu(item.level, item.name, item.state, item.curHP, item.curexp, item.isDead, item.iswild);
        }else if(string.Equals(item.name,"水箭龟")||string.Equals(item.name,"杰尼龟")){
            return new Squirtle(item.level, item.name, item.state, item.curHP, item.curexp, item.isDead, item.iswild);
        }else if(string.Equals(item.name,"穿山王")||string.Equals(item.name,"穿山鼠")){
            return new Sandshrew(item.level, item.name, item.state, item.curHP, item.curexp, item.isDead, item.iswild);
        }else if(string.Equals(item.name,"妙蛙花")||string.Equals(item.name,"妙蛙种子")){
            return new Bulbasaur(item.level, item.name, item.state, item.curHP, item.curexp, item.isDead, item.iswild);
        }else if(string.Equals(item.name,"臭臭泥")||string.Equals(item.name,"臭泥")){
            return new Grimer(item.level, item.name, item.state, item.curHP, item.curexp, item.isDead, item.iswild);
        }else if(string.Equals(item.name,"比雕")||string.Equals(item.name,"比比鸟")){
            return new Pidgeotto(item.level, item.name, item.state, item.curHP, item.curexp, item.isDead, item.iswild);
        }
        else {
            return null;
        }
    }
}
