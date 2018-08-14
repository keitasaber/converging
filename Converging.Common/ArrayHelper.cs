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
    }
}
