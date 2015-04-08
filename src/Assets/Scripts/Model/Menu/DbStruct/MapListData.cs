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
    public float    m_fDifficult;
    public int      m_iIndex;
    public string   m_strPic;
    public bool     m_bLock = true;
}

public class MapListManger
{
    public static MapListManger instance = null;
    public static int m_iCurrSectionIndex = 1;
    public static int m_iCurrMapIndex = 1;

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
    public void GetCurrSectionMapById(SectionData sectionData, MapData mapData)
    {
        if(sectionData == null)
            sectionData = new SectionData();
        sectionData = m_LtSectionData[m_iCurrSectionIndex];
        if (mapData == null)
            mapData = new MapData();
        mapData = m_LtSectionData[m_iCurrSectionIndex].m_LtMapData[m_iCurrMapIndex];
    }
   

    MapListManger()
    {
        m_LtSectionData = new List<SectionData>();
        ProfileData nProfileData =  ProfileManger.GetInstance().m_stProfileData;
        //当前通关关卡
        m_iCurrSectionIndex = nProfileData.m_iMapIndex / 100;
        m_iCurrMapIndex = nProfileData.m_iMapIndex % 100;

        for (int i = 0; i < COMM_DEFINE.MAX_SECTION_NUM; ++i)
        {
            SectionData stSectionData = new SectionData();
            stSectionData.m_iIndex = i;         
            stSectionData.m_fBaseDifficult = i * 1;
            stSectionData.m_strDesc += stSectionData.m_iIndex.ToString();

            stSectionData.m_LtMapData = new List<MapData>();

            for(int j = 0 ;j < COMM_DEFINE.MAX_MAPS_IN_SECTION; ++j)
            {
                MapData stMapData = new MapData();
                stMapData.m_iIndex = j;
                stMapData.m_fDifficult = (float)((j % 10 == 0) ? (j * 0.1 + 0.2) : j * 0.1);
                stMapData.m_strPic = (((j+1) % 10 == 0) ? "supermapicon" : "mapicon");

                if ((i * COMM_DEFINE.MAX_MAPS_IN_SECTION + j) <= nProfileData.m_iMapIndex)
                {
                    stMapData.m_bLock = false;
                }
                stSectionData.m_LtMapData.Add(stMapData);
            }
           
            m_LtSectionData.Add(stSectionData);
        }
    }
}
