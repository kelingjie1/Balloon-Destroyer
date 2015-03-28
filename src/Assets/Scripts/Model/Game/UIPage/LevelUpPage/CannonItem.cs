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

	public void OnClickForShowCannonDetail(GameObject CannonItem)
	{
		Debug.Log ("on click CannonItem");

        GameObject nGrid = LevelUpPage.Instance.gameObject.FindChild("SkillUIGrid");
        UIGrid nUIGrid = nGrid.GetComponent<UIGrid>();
     
       for(int k = 0;k < nUIGrid.transform.childCount;k++)
       {
                GameObject ntemp = nUIGrid.transform.GetChild(k).gameObject;
                Destroy(ntemp);
           
        }
        foreach (var item in m_stCannonData.m_DtComSkillLv)
        {
            SkillItem nSkillItem = SkillItem.Create();
            nSkillItem.m_kvComSkillAndLevel = item;
            nGrid.AddChild(nSkillItem.gameObject);
        }
        nUIGrid.repositionNow = true;

	}
	void Awake()
	{
		UIEventListener.Get (gameObject.FindChild("CannonItemButton")).onClick = OnClickForShowCannonDetail;

	}

	// Use this for initialization
	void Start () 
	{

	}
	// Update is called once per frame
	void Update () {
	
	}
}

