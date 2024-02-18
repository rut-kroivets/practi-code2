using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;


namespace Html_Serializer
{
    internal class HtmlHelper
    {
        private readonly static HtmlHelper _instance = new HtmlHelper();
        public static HtmlHelper Instance => _instance;
        public string[] AllTags { get; set; }
        public string[] SelfClosingTags { get; set; }
        
        private HtmlHelper()
        {
            var content1 = File.ReadAllText("seed/AllTags.json");
            AllTags = JsonSerializer.Deserialize<string[]>(content1);

            var content2 = File.ReadAllText("seed/SelfClosingTags.json");
            SelfClosingTags = JsonSerializer.Deserialize<string[]>(content2);

        }
        
    }
}
