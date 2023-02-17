using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonDiffPatchDotNet.Demo
{
	internal class Utils
	{

		/// <summary>
		/// 格式化 json 字符串
		/// </summary>
		public static string FormatJsonString(string jsonString)
		{
			if (jsonString == null || string.IsNullOrWhiteSpace(jsonString))
			{
				return "";
			}

			var result = jsonString;
			try
			{
				JsonSerializer jsonSerializer = new JsonSerializer();
				using TextReader textReader = new StringReader(jsonString);
				using JsonTextReader jsonTextReader = new JsonTextReader(textReader);
				object? obj = jsonSerializer.Deserialize(jsonTextReader);
				if (obj != null)
				{
					using StringWriter textWriter = new StringWriter();
					using JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
					{
						Formatting = Formatting.Indented,
						Indentation = 2,
						IndentChar = ' '
					};
					jsonSerializer.Serialize(jsonWriter, obj);
					result = textWriter.ToString();
				}
				return result;
			}
			catch (Exception)
			{
				return jsonString;
			}

		}
	}
}
