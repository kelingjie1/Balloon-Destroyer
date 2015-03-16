using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using game_proto;
using ProtoBuf;
using System;
public enum FakeDataManagerMode
{
    UseOnlyFakeData,
    UseFakeDataIfExist,
}

public delegate NetworkManager.NetPacket HandleReq(IExtensible req);
public class FakeDataManager 
{
    static FakeDataManager m_instance;
    public static FakeDataManager Instance
    {
        get
        {
            if (m_instance==null)
            {
                m_instance = new FakeDataManager();
            }
            return m_instance;
        }
    }
    public FakeDataManagerMode mode;
    public Dictionary<Type, HandleReq> packetDic = new Dictionary<Type, HandleReq>();
    public bool IsHandlerExist(IExtensible packet)
    {
        Type type = packet.GetType();
        if (packetDic.ContainsKey(type))
        {
            return true;
        }
        return false;
    }
    public bool SendFakeDataAsync(IExtensible packet)
    {
        if (IsHandlerExist(packet))
        {
            Type type = packet.GetType();
            NetworkManager.Instance.AddPacketToQueue(packetDic[type](packet));
            return true;
        }
        if (mode==FakeDataManagerMode.UseFakeDataIfExist)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void RegisterRspHandler(Type ReqType, HandleReq func)
    {
        packetDic.Add(ReqType, func);
    }

    public void UnRegisterRspHandler(Type ReqType)
    {
        packetDic.Remove(ReqType);
    }

}
