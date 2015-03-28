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
			nCannonItem.gameObject.name = "CannonItem_" + data.Key.ToString();
			//nCannonItem.gameObject.GetComponent<UISprite>().spriteName = data.Value.m_strPicPath;
            nGrid.AddChild(nCannonItem.gameObject);
			gameObject.FindChild(nCannonItem.gameObject.name).GetComponent<UISprite>().spriteName = data.Value.m_strPicPath;
    
		}
        nUIGrid.repositionNow = true;
	}
	void Update () 
	{

		
		
	}
}