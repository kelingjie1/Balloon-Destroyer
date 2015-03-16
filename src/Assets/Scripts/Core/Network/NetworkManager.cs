using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using ProtoBuf;
using game_proto;

public delegate void NetworkResponseDelegate(int opcode,byte[] data);
public sealed partial class NetworkManager
{
	public class NetPacket
	{
		public Boolean Cancelled { get; set; }
		public Exception Error { get; set; }
		public Int32 Opcode { get; set; }
		public Byte[] Data { get; set; }
		
		public NetPacket(Boolean cancelled, Exception error, Int32 opcode, Byte[] data)
		{
			this.Cancelled = cancelled;
			this.Error = error;
			this.Opcode = opcode;
			this.Data = data;
		}
	}

	private Uri m_ServerUri = new Uri("http://182.254.202.167:8080/app/service");
	private const String OPCODE_KEY = "opcode";
	private String m_SessionId = String.Empty;
	private WebClient m_WebClient = new WebClient();
	private Queue m_PacketQueue = Queue.Synchronized(new Queue());
    Dictionary<int,List<NetworkResponseDelegate>> m_delegateDic = new Dictionary<int,List<NetworkResponseDelegate>>();

	// single instance
	private static NetworkManager instance = null;
	public static NetworkManager Instance
	{
		get
		{
			if (null == instance)
			{
				instance = new NetworkManager();
			}
			
			return instance;
		}
	}
    public bool useFakeData;
	private NetworkManager ()
	{
		m_WebClient.UploadDataCompleted += new UploadDataCompletedEventHandler(OnUploadDataCompleted);
	}

	public void SetServerUrl(String url)
	{
		if (String.IsNullOrEmpty(url))
		{
			m_ServerUri = null;
			return;
		}

		m_ServerUri = new Uri(url);
	}
	public void Send(IExtensible msg)
	{
        if (!useFakeData || !FakeDataManager.Instance.SendFakeDataAsync(msg))
        {
			// header
			ReqHeader header = new ReqHeader ();
			header.mobile_info = SystemInfo.operatingSystem;

			// package
			ReqPackage package = new ReqPackage ();
			package.header = header;
			package.body = ProtoManager.Serialize (msg);
			Debug.Log("msg_type:" + msg.GetType());
			package.type = GetMsgType(msg.GetType().ToString());
		    Send(ProtoManager.Serialize(package));
        }
	}
	void Send(byte[] packet)
	{
		if (m_ServerUri == null)
		{
			Debug.LogError("Network: Server URI is not available!");
			return;
		}

		if (m_WebClient.IsBusy)
		{
			Debug.LogWarning("Network: WebClient is busy!");
			return;
		}

		try
		{
			Debug.Log ("Send packet:" + System.Text.Encoding.Default.GetString (packet));
            m_WebClient.UploadDataAsync(m_ServerUri, packet);
			
			//EventManager.Instance.DispatchEvent(Event.EventDefine.ConnectStart);
		}
		catch (Exception e)
		{
			Debug.LogWarning("Network: Upload data exception: " + e.Message);
		}
	}
	
	public void Update()
	{
		lock (m_PacketQueue)
		{
			if (m_PacketQueue.Count > 0)
			{
				// EventManager.Instance.DispatchEvent(EventDefine.ConnectFinish);
			}

			while (m_PacketQueue.Count > 0)
			{
				NetPacket netPacket = m_PacketQueue.Dequeue() as NetPacket;
				if (netPacket == null)
				{
					Debug.LogWarning("Network: A null-packet found!");
					continue;
				}

				if (netPacket.Cancelled)
				{
					Debug.LogWarning("Network: Upload data cancelled!");
					continue;
				}

				if (netPacket.Error != null)
				{
					Debug.LogWarning("Network: Upload data exception: " + netPacket.Error.Message);
					continue;
				}

				if (netPacket.Data == null)
				{
					Debug.LogWarning("Network: Invalid packet data!");
					continue;
				}

                if (!m_delegateDic.ContainsKey(netPacket.Opcode) || m_delegateDic[netPacket.Opcode].Count==0)
				{
					Debug.LogWarning("Network: A valid packet found (OPCODE=" + netPacket.Opcode + "), but there is no handler, you must forget RegisterHandler!");
					continue;
				}
                else
                {
                    Debug.Log("ret type:" + netPacket.Opcode);
                    DispatchPacket(netPacket.Opcode, netPacket.Data);
                }
				
			}
		}
	}
    public void DispatchPacket(int opcode,byte[] data)
    {
        foreach (NetworkResponseDelegate func in m_delegateDic[opcode])
        {
            func(opcode, data);
        }
    }
	private int GetMsgType(string msg_type)
	{
		int ret_type = 0;
	switch(msg_type)
		{
		case "game_proto.UpdateAppReq":
			ret_type = (int)MessageType.kMsgUpdateAppReq;
			break;
		case "game_proto.LoginReq":
			ret_type = (int)MessageType.kMsgLoginReq;
			break;
		case "game_proto.GetAllInfoReq":
			ret_type = (int)MessageType.kMsgGetAllInfoReq;
			break;
		default:
			ret_type = -1;
			break;
		}
			return ret_type;
	}

	public void RegisterHandler(int opcode,NetworkResponseDelegate func)
	{
        if (!m_delegateDic.ContainsKey(opcode))
		{
            m_delegateDic.Add(opcode, new List<NetworkResponseDelegate>());
		}
        List<NetworkResponseDelegate> delegateList = m_delegateDic[opcode];
        delegateList.Add(func);
		Debug.Log ("regiser opcode:" + opcode);
	}
    public void UnRegisterHandler(int opcode,NetworkResponseDelegate func)
    {
        List<NetworkResponseDelegate> delegateList = m_delegateDic[opcode];
        delegateList.Remove(func);
    }

	private void OnUploadDataCompleted(object sender, UploadDataCompletedEventArgs args)
	{
		Int32 opcode = 0;
		if (m_WebClient.ResponseHeaders != null)
		{
			String opcodeStr = m_WebClient.ResponseHeaders.Get(OPCODE_KEY);
			Int32.TryParse(opcodeStr, out opcode);
			Debug.Log ("return opcode:" + opcodeStr);
		}

        AddPacketToQueue(new NetPacket(args.Cancelled, args.Error, opcode, args.Result));
	}

    public void AddPacketToQueue(NetPacket packet)
    {
        lock (m_PacketQueue)
        {
            m_PacketQueue.Enqueue(packet);
        }
    }
	
}


