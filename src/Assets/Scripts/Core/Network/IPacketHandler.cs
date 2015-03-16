using System;
public interface IPacketHandler
{
	Int32 GetOpcode();
	void Handle(Byte[] data);
}
