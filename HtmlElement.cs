using System;
using System.Collections.Generic;

namespace Html_Serializer
{
    internal class HtmlElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Attributes { get; set; }
        public List<string> Classes { get; set; }
        public string InnerHtml { get; set; }
        public HtmlElement Parent { get; set; }
        public List<HtmlElement> Children { get; set; }

        public HtmlElement()
        {
            Attributes = new List<string>();
            Classes = new List<string>();
            Children = new List<HtmlElement>();
        }

        public IEnumerable<HtmlElement> Descendants()
        {
            Queue<HtmlElement> queue = new Queue<HtmlElement>();
            queue.Enqueue(this); 

            while (queue.Count > 0)
            {
                HtmlElement current = queue.Dequeue();
                yield return current;

                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }
            
        }

        public IEnumerable<HtmlElement> Ancestors()
        {
            HtmlElement current = this;

            while (current != null)
            {
                yield return current;
                current = current.Parent;
            }
        }

        public  IEnumerable<HtmlElement> FindElementsBySelector(Selector selector)
        {
            HashSet<HtmlElement> result = new HashSet<HtmlElement>();
            FindElementsBySelectorRecorsive(this, selector, result);
            return result;

        }
        public static IEnumerable<HtmlElement> FindElementsBySelectorRecorsive(HtmlElement element, Selector selector, HashSet<HtmlElement> s)
        {
            if (selector == null)
            {
                return null;
            }
            IEnumerable<HtmlElement> descendants = element.Descendants();
            foreach (HtmlElement d in descendants)
            {
                if (selector.TagName != null && selector.TagName != "")
                {
                    if (selector.TagName != d.Name)
                        continue;

                }
                if (selector.Id != null)
                {
                    if (selector.Id != d.Id)
                        continue;

                }
                if (selector.Classes != null)
                {
                    if (d.Classes != null)
                    {
                        if (!selector.Classes.All(c => d.Classes.Contains(c)))
                            continue;
                    }
                    else
                        continue;

                }
                if (selector.Child == null)
                {
                    s.Add(d);
                }
                else
                {
                    FindElementsBySelectorRecorsive(d, selector.Child, s);
                }
            }           
            return s;
        }

































        //// Function to find elements in the tree based on a selector
        //public IEnumerable<HtmlElement> FindElementsBySelector(Selector selector)
        //{
        //    HashSet<HtmlElement> result = new HashSet<HtmlElement>();
        //    FindElementsRecursive(this, selector, result);
        //    if(selector.Child==null)
        //      return result;
        //    for (int i = 0; i < result.Count(); i++)
        //    {
        //        var r = result.First().Descendants(); ;
        //        result.Remove(result.First());
        //    }
        //}

        //// Recursive helper function to find elements based on selector
        //private void FindElementsRecursive(HtmlElement current, Selector selector, HashSet<HtmlElement> result)
        //{
        //    if (current == null)
        //        return;

        //    // Check if the current element matches the selector criteria
        //    if (MatchesSelector(current, selector))
        //    {
        //        result.Add(current);
        //    }

        //    // Recursively search in the descendants
        //    foreach (var child in current.Children)
        //    {
        //        FindElementsRecursive(child, selector, result);
        //    }
        //}

        //// Helper function to check if an element matches the selector criteria
        //private bool MatchesSelector(HtmlElement element, Selector selector)
        //{
        //    // Check tag name
        //    if (!string.IsNullOrEmpty(selector.TagName) && !string.Equals(element.Name, selector.TagName, StringComparison.OrdinalIgnoreCase))
        //        return false;

        //    // Check ID
        //    if (!string.IsNullOrEmpty(selector.Id) && !string.Equals(element.Id, selector.Id, StringComparison.OrdinalIgnoreCase))
        //        return false;

        //    // Check classes
        //    if (selector.Classes != null && selector.Classes.Any() && !selector.Classes.All(c => element.Classes.Contains(c)))
        //        return false;

        //    // Check other criteria as needed

        //    return true;
        //}
        // Function to find elements in the tree based on a selector
        //public IEnumerable<HtmlElement> FindElementsBySelector(Selector selector)
        //{
        //    // Start the search with the current element
        //    IEnumerable<HtmlElement> currentElements = new List<HtmlElement> { this };

        //    // Iterate through selector levels
        //    while (selector != null)
        //    {
        //        // Find elements that match the current selector level
        //        currentElements = FindElementsByCurrentLevel(currentElements, selector);

        //        // Move to the next selector level
        //        selector = selector.Child;
        //    }

        //    return currentElements;
        //}

        //// Helper function to find elements that match the current selector level
        //private IEnumerable<HtmlElement> FindElementsByCurrentLevel(IEnumerable<HtmlElement> elements, Selector selector)
        //{
        //    List<HtmlElement> result = new List<HtmlElement>();

        //    foreach (var element in elements)
        //    {
        //        // Check if the element matches the current selector level
        //        if (MatchesSelector(element, selector))
        //        {
        //            result.Add(element);
        //        }

        //        // Find descendants that match the current selector level
        //        var descendants = element.Descendants().Where(descendant => MatchesSelector(descendant, selector));
        //        result.AddRange(descendants);
        //    }

        //    return result;
        //}

        //// Helper function to check if an element matches the selector criteria
        //private bool MatchesSelector(HtmlElement element, Selector selector)
        //{
        //    // Implement the logic to check if the element matches the selector
        //    // This might involve checking tag name, id, classes, etc.
        //    // You can use the existing Selector properties like TagName, Id, Classes
        //    // and the helper functions IsValidTagName and MatchesClass
        //    // (these helper functions need to be implemented based on your requirements)

        //    bool matches = true;

        //    if (!string.IsNullOrEmpty(selector.TagName))
        //    {
        //        matches &= IsValidTagName(element.Name) && element.Name.Equals(selector.TagName, StringComparison.OrdinalIgnoreCase);
        //    }

        //    if (!string.IsNullOrEmpty(selector.Id))
        //    {
        //        matches &= element.Id == selector.Id;
        //    }

        //    if (selector.Classes.Count > 0)
        //    {
        //        matches &= selector.Classes.All(className => MatchesClass(element.Classes, className));
        //    }

        //    return matches;
        //}

        //// Helper function to check if a tag name is valid
        //private bool IsValidTagName(string tagName)
        //{
        //    return HtmlHelper.Instance.AllTags.Contains(tagName) || HtmlHelper.Instance.SelfClosingTags.Contains(tagName);
        //}

        //// Helper function to check if a class matches
        //private bool MatchesClass(List<string> classes, string className)
        //{
        //    return classes.Contains(className);
        //}
    }
}
