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
        
        GameObject nGrid = this.gameObject.FindChild("UIGrid");
        UIGrid nUIGrid = nGrid.GetComponent<UIGrid>();
		foreach (var data in nDtAllCannon)
		{
			CannonItem nCannonItem = CannonItem.Create ();
            nCannonItem.m_stCannonData = data.Value;
           
            nGrid.AddChild(nCannonItem.gameObject);
    
		}
        nUIGrid.repositionNow = true;
	}
	void Update () 
	{

		
		
	}
}