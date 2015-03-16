using UnityEngine;
using ProtoBuf;
using System.IO;
using System.Collections;

public class ProtoManager {
	public static byte[] Serialize(IExtensible msg)
	{
		byte[] result;
		using (var stream = new MemoryStream())
		{
			Serializer.Serialize(stream, msg);
			result = stream.ToArray();
		}
		return result;
	}

	public static IExtensible Deserialize<IExtensible>(byte[] message)
	{
		IExtensible result;
		using (var stream = new MemoryStream(message))
		{
			result = Serializer.Deserialize<IExtensible>(stream);
		}
		return result;
	}
}