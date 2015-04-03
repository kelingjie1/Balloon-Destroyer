using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Collections.Generic;

public class MapListPage : MonoBehaviour
{
    public static MapListPage Instance;
    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        List<SectionData> m_LtSectionData = MapListManger.GetInstance().m_LtSectionData;
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
            
            if (i % 5 == 0)
            {
                Xpostion = -225;
            }
            if ((i+1) % 10 == 0)
            {
                gameObject.FindChild(nMapItem.gameObject.name).GetComponent<UISprite>().spriteName = "supermapicon";
            }
            nMapItem.gameObject.transform.localPosition = new Vector3(Xpostion, 700 - (i / 5) * 70, 0); 
            Xpostion += 110;

        }
    }
    void Update()
    {

    }
}
