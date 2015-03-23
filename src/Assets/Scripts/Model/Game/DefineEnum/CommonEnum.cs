using System.Collections;
using System.Collections.Generic;
using System.IO;

public enum ATTRIBUTE
{
	ATT_DAMAGE = 1,
	ATT_SHOTSPEED,
	ATT_HP,
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

public enum SKILL_ATT
{
	INCREATE_DAMAGE = 1,
	INCREATE_SHOTSPEED,
}

//可能有很多种
public enum SKILL_DESC_TYPE
{
	INCREATE_ADD = 1,
	INCREATE_MULT,
}
