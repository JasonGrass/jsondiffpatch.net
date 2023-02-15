using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonDiffPatchDotNet
{
	public abstract class ItemMatch
	{
		internal Func<JToken, int, object> ObjectHash;

		protected ItemMatch()
		{

		}

		protected ItemMatch(Func<JToken, int, object> objectHash)
		{
			ObjectHash = objectHash;
		}

		public virtual bool Match(JToken object1, int index1, JToken object2, int index2)
		{
			return Match(object1, index1, object2, index2, ObjectHash);
		}

		public virtual bool Match(JToken object1, int index1, JToken object2, int index2, Func<JToken, int, object> objectHash)
		{
			if (objectHash == null || object1.Type != JTokenType.Object)
			{
				return JToken.DeepEquals(object1, object2);
			}

			var hash1 = objectHash.Invoke(object1, index1);
			if (hash1 == null)
			{
				return false;
			}

			var hash2 = objectHash.Invoke(object2, index2);
			if (hash2 == null)
			{
				return false;
			}
			return hash1.Equals(hash2);
		}
	}
}
