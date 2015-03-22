using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;
using System;
using System.Linq;  
using System.Text;  
using System.IO;  
using System.Runtime.Serialization.Formatters.Binary; 


public class BinarySerialize<T>  
{  
	private string _strFilePath = string.Empty;  
	
	public void Serialize(T obj, string strFilePath)  
	{  
		_strFilePath = strFilePath;  
		FileInfo fi = new FileInfo(_strFilePath);  
		  
		using (FileStream fs = new FileStream(_strFilePath, FileMode.Create))  
		{  
			BinaryFormatter formatter = new BinaryFormatter();  
			formatter.Serialize(fs, obj);  
		}  
	}  

	public T DeSerialize(string filePath)  
	{  
		FileInfo fi = new FileInfo(filePath);  
		//if (!fi.Exists)  
		//	throw new ArgumentException("File specified is not exist!");  
		T t;  
		using (FileStream fs = new FileStream(filePath, FileMode.Open))  
		{  
			BinaryFormatter formatter = new BinaryFormatter();  
			try  
			{  
				t = (T)formatter.Deserialize(fs);  
			}  
			catch (Exception ex)  
			{  
				throw ex;  
			}  
		}  
		return t;  
	}  
}  

public enum ATTRIBUTE
{
	ATT_DAMAGE = 1,
	ATT_SHOTSPEED,
	ATT_HP,
}

//可能有很多种
public enum SKILL_TYPE
{
	INCREATE_DAMAGE = 1,
	INCREATE_SHOTSPEED,
}
public enum CANNON_TYPE
{
	BASIC_CANNNON = 1,
	OTHER_CANNON,
}


public class SkillData
{
	public int i;
}

[Serializable]
public class CannonData
{
	//基础数据
	public string m_strCannonName;
	public string m_strPicPath;
	public int m_iInitGold = 100; //初始为100
	public int m_iLevel = 1;	  //等级
	public Dictionary<ATTRIBUTE, float> m_DtAttribute= new Dictionary<ATTRIBUTE, float>();//基础属性

	public bool  m_bOnFight; //是否在出战状态
	public bool  m_bUnLock;  //是否解锁

	public Dictionary<SKILL_TYPE,  int> m_DtComSkillLv = new Dictionary<SKILL_TYPE,  int>(); //普通技能对应的等级
	public Dictionary<SKILL_TYPE,  int> m_DtAdvSkillLv = new Dictionary<SKILL_TYPE,  int>();//特殊技能对应的等级


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
		m_DtComSkillLv [SKILL_TYPE.INCREATE_DAMAGE] = 1;

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
		m_DtComSkillLv [SKILL_TYPE.INCREATE_DAMAGE] = 1;
		
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
				m_DtAllCannon = new Dictionary<CANNON_TYPE, CannonData>();
				m_DtAllCannon [CANNON_TYPE.BASIC_CANNNON] = new BasicCannonData ();
				m_DtAllCannon [CANNON_TYPE.OTHER_CANNON] = new OtherCannonData ();

				BinarySerialize<Dictionary<CANNON_TYPE,  CannonData>> serializeOut
					= new BinarySerialize<Dictionary<CANNON_TYPE,  CannonData>>();   
				serializeOut.Serialize(m_DtAllCannon, USER_DEFINE.USER_CANNONDATA_PATH); 
			}


			BinarySerialize<Dictionary<CANNON_TYPE,  CannonData>> serializeIn 
				= new BinarySerialize<Dictionary<CANNON_TYPE,  CannonData>>();  
			m_DtAllCannon = serializeIn.DeSerialize(USER_DEFINE.USER_CANNONDATA_PATH);
		
		
		}
	}
	
	//初始化塔
	public void InitAllCannon()
	{
		CreateAllCannon ();
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