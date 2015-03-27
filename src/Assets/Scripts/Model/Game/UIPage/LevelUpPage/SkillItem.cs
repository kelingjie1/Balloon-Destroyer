using UnityEngine;
using System.Collections;

public class SkillItem : MonoBehaviour {

	SkillItem m_stCannonData; //一个技能
	public static SkillItem Create()
	{
		return ResourceManager.LoadGameObject("prefab/Game/LevelUpPage/SkillItem").AddComponent<SkillItem>();
	}
	

	void Awake()
	{


	}
	
	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {
		
	}
}
