using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillItem : MonoBehaviour {


    public KeyValuePair<SKILL_TYPE, int> m_kvComSkillAndLevel;
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
