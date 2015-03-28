using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using System;
using System.Linq;  
using System.Text;  
using System.IO;


[Serializable]
public class CannonData
{
	//基础数据
	public string m_strCannonName;
	public string m_strPicPath;
	public int m_iInitGold = 100; //初始为100
	public int m_iLevel = 1;	  //等级
	public bool  m_bOnFight = false; //是否在出战状态
	public bool  m_bUnLock = false;  //是否解锁

	public Dictionary<ATTRIBUTE, float> m_DtAttribute= new Dictionary<ATTRIBUTE, float>();//基础属性

	
	public Dictionary<SKILL_TYPE,  int> m_DtComSkillLv = new Dictionary<SKILL_TYPE,  int>(); //普通技能对应的等级
	public Dictionary<SKILL_TYPE,  int> m_DtAdvSkillLv = new Dictionary<SKILL_TYPE,  int>();//特殊技能对应的等级

    public CannonData()
    {
        m_DtAttribute[ATTRIBUTE.ATT_DAMAGE] = 1;
        m_DtAttribute[ATTRIBUTE.ATT_SHOTSPEED] = 1;
        m_DtAttribute[ATTRIBUTE.ATT_HP] = 1;
    }
    public void CalculatePriv(Dictionary<SKILL_TYPE, int> DtSkillLv, SKILL_DESC_TYPE nDescType)
    {
        foreach (var nSkllItem in DtSkillLv) //相加属性先做
        {
            int nSkillLevel = nSkllItem.Value;
            SkillData nSkillData = SkillDataManger.GetInstance().GetSkillBySkillType(nSkllItem.Key);
            //获取技能详细说明
            foreach (var nSkillDesc in nSkillData.m_DtSkillEffect)
            {
                EffectsDesc nEffectsDesc = nSkillDesc.Value;
                ATTRIBUTE nAttribute = nSkillDesc.Key;
                if (nSkillDesc.Value.m_iSkillDescType == nDescType && nDescType == SKILL_DESC_TYPE.INCREATE_ADD)
                {
                    m_DtAttribute[nAttribute]
                        += (nSkillDesc.Value.m_iBaseEffect + nSkillDesc.Value.m_iLevelUpEffect * nSkillLevel);
                }
                if (nSkillDesc.Value.m_iSkillDescType == nDescType && nDescType == SKILL_DESC_TYPE.INCREATE_MULT)
                {
                    m_DtAttribute[nAttribute]
                        = m_DtAttribute[nAttribute] + m_DtAttribute[nAttribute] * (nSkillDesc.Value.m_iBaseEffect + nSkillDesc.Value.m_iLevelUpEffect * nSkillLevel);
                }
            }
        }
    }

	public void CalculateAttr()
	{
        //先算加法
        CalculatePriv(m_DtComSkillLv, SKILL_DESC_TYPE.INCREATE_ADD);
        CalculatePriv(m_DtAdvSkillLv, SKILL_DESC_TYPE.INCREATE_ADD);
        //后算乘法
        CalculatePriv(m_DtComSkillLv, SKILL_DESC_TYPE.INCREATE_MULT);
        CalculatePriv(m_DtAdvSkillLv, SKILL_DESC_TYPE.INCREATE_MULT);
	}
}

[Serializable]
public class BasicCannonData: CannonData
{
	public BasicCannonData()
	{
		m_strCannonName = "自动炮塔";
		m_strPicPath = "ta1";
		//基础属性初始化
		m_DtAttribute [ATTRIBUTE.ATT_DAMAGE] = 10;
		m_DtComSkillLv [SKILL_TYPE.SKILL_HDMG] = 1;
        m_DtComSkillLv[SKILL_TYPE.SKILL_HSPED] = 2;
        m_DtComSkillLv[SKILL_TYPE.SKILL_FIRE] = 2;
		
	}
}

[Serializable]
public class OtherCannonData: CannonData
{
	public OtherCannonData()
	{
		m_strCannonName = "其它炮塔";
		m_strPicPath = "ta2";
		//基础属性初始化
		m_DtAttribute [ATTRIBUTE.ATT_DAMAGE] = 20;
		m_DtComSkillLv [SKILL_TYPE.SKILL_HSPED] = 1;
		
	}
}

public class CannonManger
{	
	public static CannonManger instance = null;
    public static string m_strConfigFile = USER_DEFINE.USER_CANNONDATA_PATH;
	
	public Dictionary<CANNON_TYPE,  CannonData> m_DtAllCannon;
	
	public Dictionary<CANNON_TYPE, CannonData> GetAllCannon()
	{
		return m_DtAllCannon;
	}

	public Dictionary<CANNON_TYPE, CannonData> GetOnFightCannon()
	{
		Dictionary<CANNON_TYPE, CannonData> nDtCannon = new Dictionary<CANNON_TYPE, CannonData>();
		foreach (var item in m_DtAllCannon)
		{	
			if(item.Value.m_bOnFight == true)
				nDtCannon.Add(item.Key, item.Value);
		}
		return nDtCannon;
	}
	

	public CannonData GetCannonByType(CANNON_TYPE CannonType)
	{
		return m_DtAllCannon.ContainsKey (CannonType) ? m_DtAllCannon [CannonType] : null;
		
	}
	
	public void CreateAllCannon()
	{
		try
		{
			FileInfo fi = new FileInfo(m_strConfigFile); 
			if (!fi.Exists)  
			{
				m_DtAllCannon = new Dictionary<CANNON_TYPE, CannonData>();
				m_DtAllCannon [CANNON_TYPE.BASIC_CANNNON] = new BasicCannonData ();
				m_DtAllCannon [CANNON_TYPE.OTHER_CANNON] = new OtherCannonData ();
				SaveData2Db();
			}

			LoadDb2Data();
			
			foreach(var item  in m_DtAllCannon)
			{
                item.Value.CalculateAttr();
				string outstream = item.Key.ToString();
				Debug.Log(outstream);
			}
		}
		catch(Exception ex)
		{
			Debug.Log ("error catch");
		}
	
	}
	public int LoadDb2Data()
	{
		BinarySerialize<Dictionary<CANNON_TYPE,  CannonData>> serializeIn
			= new BinarySerialize<Dictionary<CANNON_TYPE,  CannonData>>(); 

		m_DtAllCannon = serializeIn.DeSerialize(m_strConfigFile); 
		
		if(m_DtAllCannon == null)
		{
			Debug.Log("LoadDb2Data data is empty");
			return -1;
		}

		return 0;
		
	}
	public int SaveData2Db()
	{
		if(m_DtAllCannon != null )
		{
			BinarySerialize<Dictionary<CANNON_TYPE,  CannonData>> serializeOut
				= new BinarySerialize<Dictionary<CANNON_TYPE,  CannonData>>();   
			serializeOut.Serialize(m_DtAllCannon, m_strConfigFile);
		}
		else
		{
			Debug.Log("SaveData2Db data is empty");
			return -1;
		}
		return 0;
	}
	
	
	//初始化塔
	public CannonManger()
	{
		CreateAllCannon ();
	}
	
	~CannonManger()
	{
		//SaveData2Db();
	}
	
	
	public  static CannonManger GetInstance () 
	{
		if (instance == null) 
		{
			Debug.Log("Init CannonManger");
			instance = new CannonManger();
		}
		return instance;
	}
}