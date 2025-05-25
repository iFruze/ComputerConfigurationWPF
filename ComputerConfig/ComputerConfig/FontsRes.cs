using PdfSharp.Fonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ComputerConfig
{
    internal class FontsRes : IFontResolver
    {
        public FontResolverInfo ResolveTypeface(string familyName, bool bold, bool italic)
        {
            if (familyName == "Arial")
            {
                return new FontResolverInfo("Arial");
            }
            return null;
        }

        public byte[] GetFont(string faceName)
        {
            if (faceName == "Arial")
            {
                return File.ReadAllBytes(@"C:\Windows\Fonts\arial.ttf");
            }
            return null;
        }
    }
}
