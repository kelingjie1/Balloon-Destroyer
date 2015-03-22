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

	public void CalculateAttr() //计算技能附加到塔属性
	{
		foreach (var item in m_DtComSkillLv)
		{
			//SkillData 
			//m_DtAttribute[item.Key]
		}
	}
}

[Serializable]
public class BasicCannonData: CannonData
{
	public BasicCannonData()
	{
		m_strCannonName = "自动炮塔";
		m_strPicPath = "./1.pic";
		//基础属性初始化
		m_DtAttribute [ATTRIBUTE.ATT_DAMAGE] = 10;
		m_DtComSkillLv [SKILL_TYPE.SKILL_HDMG] = 1;
		
	}
}

[Serializable]
public class OtherCannonData: CannonData
{
	public OtherCannonData()
	{
		m_strCannonName = "其它炮塔";
		m_strPicPath = "./2.pic";
		//基础属性初始化
		m_DtAttribute [ATTRIBUTE.ATT_DAMAGE] = 20;
		m_DtComSkillLv [SKILL_TYPE.SKILL_HSPED] = 1;
		
	}
}

public class CannonManger
{	
	private static CannonManger instance;
	public static string m_strConfigFile = @"d:/test.data";
	
	public Dictionary<CANNON_TYPE,  CannonData> m_DtAllCannon; //所有的塔
	
	
	//获取出战的塔
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
	
	//获取想到的塔
	public CannonData GetCannonByType(CANNON_TYPE CannonType)
	{
		return m_DtAllCannon.ContainsKey (CannonType) ? m_DtAllCannon [CannonType] : null;
		
	}
	
	public void CreateAllCannon()
	{
		//生成所有的canno
		if (m_DtAllCannon == null) 
		{
			FileInfo fi = new FileInfo(USER_DEFINE.USER_CANNONDATA_PATH); 
			if (!fi.Exists)
			{
				//初始化一些数据
				m_DtAllCannon = new Dictionary<CANNON_TYPE, CannonData>();
				m_DtAllCannon [CANNON_TYPE.BASIC_CANNNON] = new BasicCannonData ();
				m_DtAllCannon [CANNON_TYPE.OTHER_CANNON] = new OtherCannonData ();
				SaveData2Db();

			}
			LoadDb2Data();

			foreach(var item  in m_DtAllCannon)
			{
				string outstream = item.Key.ToString();
				Debug.Log(outstream);
			}
		}
	}
	public int LoadDb2Data()
	{
		BinarySerialize<Dictionary<CANNON_TYPE,  CannonData>> serializeIn
			= new BinarySerialize<Dictionary<CANNON_TYPE,  CannonData>>(); 

		m_DtAllCannon = serializeIn.DeSerialize(USER_DEFINE.USER_CANNONDATA_PATH); 
		
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
			serializeOut.Serialize(m_DtAllCannon, USER_DEFINE.USER_CANNONDATA_PATH);
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
		SaveData2Db();
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