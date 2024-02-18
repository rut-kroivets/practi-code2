using Html_Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Html_Serializer
{
    internal class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; } = new List<string>();
        public Selector Parent { get; set; }
        public Selector Child { get; set; }

        public static Selector ParseQuery(string queryString)
        {
            string[] levels = queryString.Split(' ');

            Selector rootSelector = new Selector();
            Selector currentSelector = rootSelector;

            foreach (string level in levels)
            {
                string[] parts = Regex.Split(level, @"(?=[.#])");
                Selector newSelector = new Selector
                {
                    Parent = currentSelector
                };

                foreach (string part in parts)
                {
                    if (part.StartsWith("#"))
                    {
                        newSelector.Id = part.Substring(1);
                    }
                    else if (part.StartsWith("."))
                    {
                        newSelector.Classes.Add(part.Substring(1));
                    }
                    else
                    {
                        if (IsValidTagName(part))
                        {
                            newSelector.TagName = part;
                        }
                    }

                }
                currentSelector.Child = newSelector;
                currentSelector = newSelector;
            }

            return rootSelector.Child;
        }

        private static bool IsValidTagName(string tagName)
        {
            return HtmlHelper.Instance.AllTags.Contains(tagName) || HtmlHelper.Instance.SelfClosingTags.Contains(tagName);
        }
    }
}

