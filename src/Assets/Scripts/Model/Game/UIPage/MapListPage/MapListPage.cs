using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Collections.Generic;

public class MapListPage : MonoBehaviour
{
    List<SectionData> m_LtSectionData;
    public static MapListPage Instance;
    void Awake()
    {
        Instance = this;
    }

    public void CreateItemMapList()
    {
        m_LtSectionData = MapListManger.GetInstance().m_LtSectionData;
        if (m_LtSectionData.Count <= 0)
            return;
        GameObject nGrid = this.gameObject.FindChild("ScrollDragArea");
        UIGrid nUIGrid = nGrid.GetComponent<UIGrid>();

        List<MapData> nMapData = m_LtSectionData[0].m_LtMapData;
        int Xpostion = -225;
        for (int i = 0; i < nMapData.Count; ++i)
        {
            MapItem nMapItem = MapItem.Create();
            nMapItem.gameObject.name = "MapItem" + nMapData[i].m_iIndex.ToString();
            nGrid.AddChild(nMapItem.gameObject);

            GameObject nMapItemOB = gameObject.FindChild(nMapItem.gameObject.name);
            //显示lable
            nMapItemOB.FindChild("MapLabel").GetComponent<UILabel>().text = "Map " + (nMapData[i].m_iIndex + 1).ToString();
           
            //解锁图片是否显示
            if (nMapData[i].m_bLock == false)
            {      
                GameObject nMapLockOB = nMapItemOB.FindChild("MapLock");
                nMapLockOB.SetActive(false);
    
            }
            //特殊图片
            if ((i + 1) % 10 == 0 )
            {
                gameObject.FindChild(nMapItem.gameObject.name).GetComponent<UISprite>().spriteName = nMapData[i].m_strPic;
            }
            //位置设置
            if (i % 5 == 0)
            {
                Xpostion = -225;
            }
            nMapItem.gameObject.transform.localPosition = new Vector3(Xpostion, 700 - (i / 5) * 70, 0);
            Xpostion += 110;

        }
    }
    void Start()
    {
        CreateItemMapList();//初始化关卡列表
    }
    void Update()
    {

    }
}
