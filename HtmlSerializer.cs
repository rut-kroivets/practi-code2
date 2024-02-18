using Html_Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Html_Serializer
{
    internal class HtmlSerializer
    {
        public  HtmlElement Serializer(string[] htmlLines)
        {
            HtmlElement root = new HtmlElement();  // Assuming HtmlElement is your class representing HTML elements
            HtmlElement currentElement = root;  // Start with the root as the current element

            foreach (string line in htmlLines)
            {
                string firstWord = line.Split(' ').FirstOrDefault();  // Get the first word in the line

                // Check if it's "/html"
                if (firstWord == "/html")
                {
                    // You've reached the end of HTML, handle accordingly
                    break;
                }

                // Check if it starts with "/"
                if (firstWord.StartsWith("/"))
                {
                    // It's a closing tag, move back to the parent element
                    currentElement = currentElement.Parent;
                }
                else
                {
                    if (HtmlHelper.Instance.AllTags.Contains(firstWord) || HtmlHelper.Instance.SelfClosingTags.Contains(firstWord))
                    {
                        // It's a tag name, create a new element and add it to the current element's children
                        HtmlElement newElement = new HtmlElement();
                        newElement.Name = firstWord;
                        newElement.Parent = currentElement;
                        currentElement.Children.Add(newElement);
                        // Process attributes and classes
                        ProcessAttributesAndClasses(newElement, line);

                        // Update the current element to the newly created element
                        currentElement = newElement;

                    }
                    else
                        currentElement.InnerHtml = line;

                }
            }
            root = root.Children[0];
            return root;
        }


        private void ProcessAttributesAndClasses(HtmlElement element, string htmlElement)
        {
            var attributes = new Regex("([^\\s]*?)=\"(.*?)\"").Matches(htmlElement);

            foreach (Match attributeMatch in attributes)
            {
                string attributeName = attributeMatch.Groups[1].Value;
                string attributeValue = attributeMatch.Groups[2].Value;

                if (attributeName.ToLower() == "class")
                {
                    string[] classes = attributeValue.Split(' ');
                    element.Classes.AddRange(classes);
                }
                else if (attributeName.ToLower() == "id")
                {
                    element.Id = attributeValue;
                }
            }

           
        }

    }
}
