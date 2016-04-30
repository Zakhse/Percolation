using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace PercolationGUI
{
	public class Serialization
	{
		public static void Serialize<T>(T dataToSerialize,string filePath)
		{
			try
			{
				using (Stream stream = File.Open(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(T));
					XmlTextWriter writer = new XmlTextWriter(stream, Encoding.UTF8);
					writer.Formatting = Formatting.Indented;
					serializer.Serialize(writer, dataToSerialize);
					writer.Close();
				}
			}
			catch
			{
				throw new Exception("Serialization went wrong");
			}
		}//Serialize

		public static T Deserialize<T>(string filePath)
		{
			try
			{
				XmlSerializer serializer = new XmlSerializer(typeof(T));
				T serializedData;
				using (Stream stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
				{
					serializedData = (T)serializer.Deserialize(stream);
				}
				return serializedData;
			}
			catch
			{
				throw new Exception("Deserialization went wrong");
			}
		}//Deserialize
	}
}
