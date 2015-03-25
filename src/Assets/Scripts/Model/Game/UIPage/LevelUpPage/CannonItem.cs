using UnityEngine;
using System.Collections;

public class CannonItem : MonoBehaviour 
{
	CannonData m_stCannonData; //one 塔数据
	public static CannonItem Create()
	{
		Debug.Log ("Create");
		return ResourceManager.LoadGameObject("prefab/Game/LevelUpPage/CannonItem").AddComponent<CannonItem>();
	}

	public void OnClickForShowCannonDetail(GameObject button)
	{
		Debug.Log ("on click");
	}
	void Awake()
	{
		Debug.Log ("bind clikc");
		UIEventListener.Get (gameObject.FindChild("CannonItemButton")).onClick = OnClickForShowCannonDetail;
	}

	// Use this for initialization
	void Start () {
		Debug.Log ("Start");
	}
	// Update is called once per frame
	void Update () {
	
	}
}

