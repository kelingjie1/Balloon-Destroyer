using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System;
using System.Collections.Generic;

public class SectionData
{
    public int  m_iIndex;
    public string m_strDesc = "Section ";
    public float m_fBaseDifficult;
    public List<MapData> m_LtMapData;
}
public class MapData
{
    public float m_fDifficult;
    public int   m_iIndex;
}

public class MapListManger
{
    public static MapListManger instance = null;

    public List<SectionData> m_LtSectionData;
    public static MapListManger GetInstance()
    {
        if (instance == null)
        {
            Debug.Log("Init MapListManger");
            instance = new MapListManger();
        }
        return instance;
    }

    MapListManger()
    {
        m_LtSectionData = new List<SectionData>();
        for (int i = 1; i < COMM_DEFINE.MAX_SECTION_NUM; ++i)
        {
            SectionData stSectionData = new SectionData();
            stSectionData.m_iIndex = i;         
            stSectionData.m_fBaseDifficult = i * 1;
            stSectionData.m_strDesc += stSectionData.m_iIndex.ToString();

            stSectionData.m_LtMapData = new List<MapData>();

            for(int j = 1 ;j < COMM_DEFINE.MAX_MAPS_IN_SECTION; ++j)
            {
                MapData stMapData = new MapData();
                stMapData.m_iIndex = j;
                stMapData.m_fDifficult = (float)((j % 10 == 0) ? (j * 0.1 + 0.2) : j * 0.1);
                stSectionData.m_LtMapData.Add(stMapData);
            }
            m_LtSectionData.Add(stSectionData);
        }
    }
}
