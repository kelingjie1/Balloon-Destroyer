using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.Text;  
using System.IO;  
using UnityEngine;


public class SkillDataManger
{
    public static SkillDataManger instance;
	public  static SkillDataManger GetInstance () 
	{
		if (instance == null) 
		{
			Debug.Log("Init SkillDataManger");
			instance = new SkillDataManger();
		}
		return instance;
	}

    public Dictionary<SKILL_TYPE, SkillData> m_DtAllSkill = new Dictionary<SKILL_TYPE, SkillData>();

    public SkillData  GetSkillBySkillType(SKILL_TYPE iSkill_type)
    {
        return m_DtAllSkill[iSkill_type];
    }
	public SkillDataManger()
	{
		m_DtAllSkill [SKILL_TYPE.SKILL_HDMG] = new SkillHDmgData ();
		m_DtAllSkill [SKILL_TYPE.SKILL_HSPED] = new SkillHSpedData ();
        m_DtAllSkill [SKILL_TYPE.SKILL_FIRE] = new SkillFireData();
	}
}

public class SkillData
{
    public float  m_fLevelUpGold = 100f;
	public string m_strSkillName;
	public string m_strSkillPic;
    public string m_strSkillDesc;
    public Dictionary<CannonAttribute, EffectsDesc> m_DtSkillEffect = new Dictionary<CannonAttribute, EffectsDesc>(); //技能生成的效果
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
        m_strSkillName = "Single Stun";
        m_strSkillDesc = "This skill is very userfull,its base attack 10 and 5 attack added when levelup";
		m_strSkillPic = "1_skill";
		EffectsDesc nEffectsDesc = new EffectsDesc (10, 5, SKILL_DESC_TYPE.INCREATE_ADD);
        m_DtSkillEffect[CannonAttribute.Attack] = new EffectsDesc(10, 5, SKILL_DESC_TYPE.INCREATE_ADD);
	}
}


public class SkillHSpedData : SkillData
{
	public SkillHSpedData()
	{
        m_strSkillName = "Battle Sown";
        m_strSkillDesc = "Sown skill can help you contorl some powerfull master,its base chance 2% and 0.1% added when levelup";
		m_strSkillPic = "2_skill";
		EffectsDesc nEffectsDesc = new EffectsDesc (0.2f, 0.01f, SKILL_DESC_TYPE.INCREATE_MULT);
        m_DtSkillEffect[CannonAttribute.ShotSpeed] = nEffectsDesc;
	}
}

public class SkillFireData : SkillData
{
	public SkillFireData()
	{
        m_strSkillName = "Deadly Strike";
        m_strSkillDesc = "Deadly Deadly can kill master easily,it raise speed 10% and 0.2% added when levelup";
		m_strSkillPic = "3_skill";
		EffectsDesc nEffectsDesc = new EffectsDesc (0.1f, 0.02f, SKILL_DESC_TYPE.INCREATE_MULT);
        m_DtSkillEffect[CannonAttribute.ShotSpeed] = nEffectsDesc;
	}
}