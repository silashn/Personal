using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pages.Tests.Tools
{
    public static class Text
    {
        public static string FormatHtml(this string text)
        {
            while(text.IndexOf("<") >= 0)
                text = text.Replace(text.Substring(text.IndexOf("<"), text.IndexOf(">")), "\n");
            text = text.Replace(">", "").Replace("br /", "").Replace("/p", "").Replace("<", "");
            return text;
        }

        public static void Output(this string text, TestContext context)
        {
            context.WriteLine(text);
        }
    }
}
