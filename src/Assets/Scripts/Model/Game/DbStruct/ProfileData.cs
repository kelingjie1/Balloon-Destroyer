using UnityEngine;
using System.Collections;
using System.Xml.Serialization;
using System.IO;
using System;

[Serializable]
public class ProfileData
{
    public float m_fMoney = 0.2f;
    public int m_iUserLevel = 1; 

}
public class ProfileManger
{
    public static ProfileManger instance = null;
    public static string m_strConfigFile = USER_DEFINE.USER_PROFILEDATA_PATH;
    public ProfileData m_stProfileData;

    public static ProfileManger GetInstance()
    {
        if (instance == null)
        {
            Debug.Log("Init ProfileManger");
            instance = new ProfileManger();
        }
        return instance;
    }
    ProfileManger()
    {
        CreateProfile();
    }

    ~ProfileManger()
    {
        SaveData2Db();
    }
    public void CreateProfile()
    {
        try
        {
            FileInfo fi = new FileInfo(m_strConfigFile);
            if (!fi.Exists)
            {
                m_stProfileData = new ProfileData();
                SaveData2Db();
            }
            LoadDb2Data();
        }
        catch (Exception ex)
        {
            Debug.Log("error catch");
        }

    }
    public int LoadDb2Data()
    {
        BinarySerialize <ProfileData> serializeIn
            = new BinarySerialize<ProfileData>();

        m_stProfileData = serializeIn.DeSerialize(m_strConfigFile);

        if (m_stProfileData == null)
        {
            Debug.Log("LoadDb2Data profile data is empty");
            return -1;
        }
        return 0;

    }
    public int SaveData2Db()
    {
        if (m_stProfileData != null)
        {
            BinarySerialize<ProfileData> serializeOut
                = new BinarySerialize<ProfileData>();
            serializeOut.Serialize(m_stProfileData, m_strConfigFile);
        }
        else
        {
            Debug.Log("SaveData2Db profile data is empty");
            return -1;
        }
        return 0;
    }
}