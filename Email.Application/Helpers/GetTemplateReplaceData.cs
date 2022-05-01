using System.Collections.Generic;

namespace Email.Application.Helpers
{
    public static class CreateTemplate
    {
        public static string GetTemplateReplaceData(this string temp, Dictionary<string, string> dictData)
        {
            if (temp == null)
            {
                //
            }

            foreach (var item in dictData.Keys)
            {
                temp = temp.Replace("{" + item + "}", dictData[item]);
            }

            return temp;
        }
    }
}
