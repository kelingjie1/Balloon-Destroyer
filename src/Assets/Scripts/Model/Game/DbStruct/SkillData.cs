using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.Text;  
using System.IO;  
using UnityEngine;


public class SkillDataManger
{
	private static SkillDataManger instance;
	public  static SkillDataManger GetInstance () 
	{
		if (instance == null) 
		{
			Debug.Log("Init SkillDataManger");
			instance = new SkillDataManger();
		}
		return instance;
	}
	
	public Dictionary<SKILL_TYPE, SkillData> m_DtAllSkill;

    public SkillData  GetSkillBySkillType(SKILL_TYPE iSkill_type)
    {
        return m_DtAllSkill[iSkill_type];
    }
	SkillDataManger()
	{
		m_DtAllSkill [SKILL_TYPE.SKILL_HDMG] = new SkillHDmgData ();
		m_DtAllSkill [SKILL_TYPE.SKILL_HSPED] = new SkillHSpedData ();
	}
}

public class SkillData
{
	public Dictionary<SKILL_ATT, EffectsDesc> m_DtSkillEffect = new Dictionary<SKILL_ATT, EffectsDesc>(); //技能生成的效果
}


public class EffectsDesc
{
	public float m_iBaseEffect;
	public float m_iLevelUpEffect;
	public SKILL_DESC_TYPE m_iSkillDescType;

	public EffectsDesc(){;}
	public EffectsDesc(float iBaseEffect, float iLevelUpEffect, SKILL_DESC_TYPE iSkillDescType)
	{
		m_iBaseEffect = iBaseEffect;
		m_iLevelUpEffect = iLevelUpEffect;
		m_iSkillDescType = iSkillDescType;
	}
}



public class SkillHDmgData : SkillData
{
	public SkillHDmgData()
	{
		EffectsDesc nEffectsDesc = new EffectsDesc (10, 5, SKILL_DESC_TYPE.INCREATE_ADD);
		m_DtSkillEffect[SKILL_ATT.INCREATE_DAMAGE] = nEffectsDesc;
	}
}


public class SkillHSpedData : SkillData
{
	public SkillHSpedData()
	{
		EffectsDesc nEffectsDesc = new EffectsDesc (2, 0.1f, SKILL_DESC_TYPE.INCREATE_MULT);
		m_DtSkillEffect[SKILL_ATT.INCREATE_SHOTSPEED] = nEffectsDesc;
	}
}