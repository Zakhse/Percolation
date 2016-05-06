using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace XMLSerializationLIB
{
	public class Serialization
	{
		/// <summary>
		/// Serializes serializable data
		/// </summary>
		/// <typeparam name="T">Type of serializable data</typeparam>
		/// <param name="dataToSerialize">Data to serialize</param>
		/// <param name="filePath">Path to data</param>
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
			catch(Exception e)
			{
				throw new Exception(e.Message);
			}
		}//Serialize

		/// <summary>
		/// Deserializes serializable data
		/// </summary>
		/// <typeparam name="T">Type of serializable data</typeparam>
		/// <param name="filePath">Path to data</param>
		/// <returns>Deserialized data</returns>
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
			catch (Exception e)
			{
				throw new Exception(e.Message);
			}
		}//Deserialize
	}
}
