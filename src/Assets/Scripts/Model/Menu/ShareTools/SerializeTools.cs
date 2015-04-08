using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;
using System.Linq;  
using System.Text;  
using System.IO;  

using System.Runtime.Serialization.Formatters.Binary; 
public class BinarySerialize<T>  
{  
	private string _strFilePath = string.Empty;  
	
	public void Serialize(T obj, string strFilePath)  
	{  
		_strFilePath = strFilePath;  
		FileInfo fi = new FileInfo(_strFilePath);   
		
		using (FileStream fs = new FileStream(_strFilePath, FileMode.Create))  
		{  
			BinaryFormatter formatter = new BinaryFormatter();  
			formatter.Serialize(fs, obj);  
		}  
	}  
	
	public T DeSerialize(string filePath)  
	{  
		FileInfo fi = new FileInfo(filePath);  
		if (!fi.Exists)  
			throw new ArgumentException("File specified is not exist!");  
		T t;  
		using (FileStream fs = new FileStream(filePath, FileMode.Open))  
		{  
			BinaryFormatter formatter = new BinaryFormatter();  
			try  
			{  
				t = (T)formatter.Deserialize(fs);  
			}  
			catch (Exception ex)  
			{  
				throw ex;  
			}  
		}  
		return t;  
	}  
}  