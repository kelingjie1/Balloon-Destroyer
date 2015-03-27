using UnityEngine;
using System.Collections;

public class CannonItem : MonoBehaviour 
{
	public CannonData m_stCannonData; //one 塔数据
	public static CannonItem Create()
	{
		Debug.Log ("Create CannonItem");
		return ResourceManager.LoadGameObject("prefab/Game/LevelUpPage/CannonItem").AddComponent<CannonItem>();
	}

	public void OnClickForShowCannonDetail(GameObject button)
	{
		Debug.Log ("on click CannonItem");
	}
	void Awake()
	{
		UIEventListener.Get (gameObject.FindChild("CannonItemButton")).onClick = OnClickForShowCannonDetail;

	}

	// Use this for initialization
	void Start () 
	{
		foreach ( var item in m_stCannonData.m_DtComSkillLv)
		{
			SkillItem nSkillItem = SkillItem.Create(); 
			LevelUpPage.Instance.gameObject.FindChild("SkillUIGrid").AddChild(nSkillItem.gameObject);

		}

	}
	// Update is called once per frame
	void Update () {
	
	}
}

