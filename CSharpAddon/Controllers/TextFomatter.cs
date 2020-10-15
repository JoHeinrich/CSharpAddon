using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VoiceControl
{
    class TextFomatter
    {
  
        public static  string FormatVariable(String input)
        {
            var word = FormatClass(input);
            return  word.Substring(0, 1).  ToLower() + word.Substring(1);

        }
        public static string FormatClass(String input)
        {
            var cleand=Regex.Replace(input, @"(\.|\?|!|,)","");
            var test = cleand.Split(' ');
            var caseCorrected=String.Empty;
            foreach (var word in test)
            {
                caseCorrected+= word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower();
            }
            return caseCorrected;
        }
    }
}
