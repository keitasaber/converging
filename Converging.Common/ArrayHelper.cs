using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converging.Common
{
    public class ArrayHelper
    {
        public static bool IsDuplicateTag(string strTag )
        {
            if (string.IsNullOrEmpty(strTag))
            {
                return false;
            }
            string[] tags = strTag.Split(',');
            for (int i = 0; i < tags.Length; i++)
            {

                tags[i] = StringHelper.ToUnsignString(tags[i].Trim());
            }
            if (tags.Length == tags.Distinct().ToArray().Length)
                return false;
            else
                return true;
        }

        public static IEnumerable<IEnumerable<T>> SplitList<T>(IEnumerable<T> locations, int nSize = 30)
        {
            var list = locations.ToList();
            for (int i = 0; i < list.Count; i += nSize)
            {
                yield return list.GetRange(i, Math.Min(nSize, list.Count - i));
            }
        }
    }
}
