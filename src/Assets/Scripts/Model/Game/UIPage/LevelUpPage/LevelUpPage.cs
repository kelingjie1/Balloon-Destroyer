using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelUpPage : MonoBehaviour 
{
	public static LevelUpPage Instance;
	void Awake () 
	{
		Instance = this;
	}
	
	void Start()
	{
		Dictionary<CANNON_TYPE,  CannonData> nDtAllCannon = CannonManger.GetInstance().GetAllCannon ();
		
	
		foreach (var item in nDtAllCannon)
		{
			CannonItem nCannonItem = CannonItem.Create ();
			nCannonItem.m_stCannonData = item.Value;
			this.gameObject.FindChild("UIGrid").AddChild(nCannonItem.gameObject);
		}
	}
	void Update () 
	{

		
		
	}
}