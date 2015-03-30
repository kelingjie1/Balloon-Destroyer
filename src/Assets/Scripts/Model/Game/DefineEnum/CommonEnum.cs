using System.Collections;
using System.Collections.Generic;
using System.IO;

public enum CannonAttrbute
{
    Attack = 1,
    Speed,              //子弹飞行速度
    Puncture,           //穿透
    ArmorBreak,         //破甲
    ShotSpeed,          //攻击速度
    Investigation,      //侦查力
}


public enum CANNON_TYPE
{
    BASIC_CANNNON = 1,
    OTHER_CANNON,
}


//可能有很多种
//技能SKILL_TYPE 有多种属性构成SKILL_ATT 每种属性都有该属性的表述SKILL_DESC_TYPE
public enum SKILL_TYPE
{
    SKILL_HDMG = 1,
    SKILL_HSPED,
    SKILL_FIRE,
}

//可能有很多种
public enum SKILL_DESC_TYPE
{
    INCREATE_ADD = 1,
    INCREATE_MULT,
}
   