using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JsonDiffPatchDotNet.Demo
{
	internal class DiffHandler
	{
		private Options _options;

		public DiffHandler()
		{
			_options = new Options()
			{
				ArrayDiff = ArrayDiffMode.Efficient,
				ObjectHash = (obj, index) =>
				{
					var objString = obj.ToString(Formatting.Indented); // for debug view
					return $"$$index{index}";

					////var id = obj["Id"]?.Value<string>();
					////if (!string.IsNullOrWhiteSpace(id))
					////{
					////    return id;
					////}
					////else
					////{
					////    return $"$$index{index}";
					////}
				}
			};
		}

		public string Diff(string left, string right)
		{
			if (string.IsNullOrWhiteSpace(left) || string.IsNullOrWhiteSpace(right))
			{
				return "";
			}

			var jdp = new JsonDiffPatch(_options);

			var leftToken = JToken.Parse(left);
			var rightToken = JToken.Parse(right);

			JToken? patch = jdp.Diff(leftToken, rightToken);

			if (patch == null)
			{
				throw new InvalidOperationException("没有正确生成 patch 数据");
			}

			var result = patch.ToString(Formatting.Indented);

			return result;
		}


		public string Patch(string left, string patch)
		{
			var leftToken = JToken.Parse(left);
			var patchToken = JToken.Parse(patch);

			var jdp = new JsonDiffPatch(_options);
			var rightToken = jdp.Patch(leftToken, patchToken);
			return rightToken.ToString();

		}
	}
}
