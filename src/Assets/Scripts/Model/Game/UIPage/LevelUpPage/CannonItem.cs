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
       
        int i = 0;

        foreach (var item in m_stCannonData.m_DtComSkillLv)
        {
            SkillItem nSkillItem = SkillItem.Create();
            nSkillItem.m_stParentCannon = m_stCannonData;
            nSkillItem.m_iSkillType = item.Key;

			nSkillItem.gameObject.name = "SkillItem_" + item.Key.ToString();
			nSkillItem.gameObject.GetComponent<UISprite>().spriteName 
				= SkillDataManger.GetInstance().GetSkillBySkillType(item.Key).m_strSkillPic;
            nGrid.AddChild(nSkillItem.gameObject);
            float nfXpostion = i % 2 == 0 ? -110 : 110;
            nSkillItem.gameObject.transform.localPosition = new Vector3(nfXpostion, -(i / 2) * 55, 0);  
            i++;
        }
        

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

