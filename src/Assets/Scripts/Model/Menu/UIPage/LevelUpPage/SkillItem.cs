using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillItem : MonoBehaviour {

    public CannonData m_stParentCannon;
    public SKILL_TYPE  m_iSkillType;

	public static SkillItem Create()
	{
		return ResourceManager.LoadGameObject("prefab/Game/LevelUpPage/SkillItem").AddComponent<SkillItem>();
	}

    public  void OnClickForShowSkillDetail(GameObject SkillItem)
    {
        UpDateSkillDetail();
    }

    public void UpDateSkillDetail()
    {
        GameObject nSkillDetail = LevelUpPage.Instance.gameObject.FindChild("SkillDetail");
        SkillData nSkillData = SkillDataManger.GetInstance().GetSkillBySkillType(m_iSkillType);
        nSkillDetail.FindChild("NameLabelContent").GetComponent<UILabel>().text
            = nSkillData.m_strSkillName;
        nSkillDetail.FindChild("DescLabelContent").GetComponent<UILabel>().text
            = nSkillData.m_strSkillDesc;
        nSkillDetail.FindChild("LevelLabelContent").GetComponent<UILabel>().text
           = m_stParentCannon.m_DtComSkillLv[m_iSkillType].ToString();
        nSkillDetail.FindChild("GoldLabel").GetComponent<UILabel>().text
          = (nSkillData.m_fLevelUpGold * (1 << (m_stParentCannon.m_DtComSkillLv[m_iSkillType] - 1))).ToString();
    }

    public void OnClickForUpGradeButton(GameObject UpGradeButton)
    {
        m_stParentCannon.m_DtComSkillLv[m_iSkillType] += 1;
        UpDateSkillDetail();
        CannonManger.GetInstance().SaveData2Db();
    }
	void Awake()
	{
        UIEventListener.Get(this.gameObject.FindChild("SkillItemButton")).onClick = OnClickForShowSkillDetail;
        UIEventListener.Get(LevelUpPage.Instance.gameObject.FindChild("UpGradeButton")).onClick = OnClickForUpGradeButton;

	}
	
	// Use this for initialization
	void Start () {

	}
	// Update is called once per frame
	void Update () {
		
	}
}
