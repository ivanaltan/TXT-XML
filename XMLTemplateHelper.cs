using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TXT_XML
{
    static class XMLTemplateHelper
    {
        public static void Do(string filein)
        {
            string content = File.ReadAllText(filein);

            string output=
                System.Environment.NewLine+
                "DOES NOT MAP ATTRIBUTES!" +
                System.Environment.NewLine +
                System.Environment.NewLine +
                "XmlDocument xml = new XmlDocument();" +
                System.Environment.NewLine +
                System.Environment.NewLine;

            //stack for tags
            Stack<string> stack = new Stack<string>();

            //stack for how many children a tag has
            Stack<int> flag = new Stack<int>();
            int flagUp;

            int i = 0;

            while ( i < content.Length)
            {
                if (content[i] == '<')
                {
                    //close tag
                    if (content[i + 1] == '/')
                    {
                        i++;
                        string end = stack.Pop();

                        if (flag.Pop() > 0)
                        {
                            output += "}" + System.Environment.NewLine;
                            output += "#endregion " + end + System.Environment.NewLine;
                        }

                        //skip to next tag
                        while (i < content.Length && content[i] != '<')
                            i++;
                    }

                    //skip the first xml tag
                    else if (content[i + 1] == '?')
                    {
                        i++;
                        i++;
                        while (content[i] != '?')
                            i++;
                        while (content[i] != '>')
                        i++;
                        i++;
                    }

                    //open tag
                    else
                    {
                        i++;
                        string name = "";
                        while (content[i] != '>' && !Char.IsWhiteSpace(content[i]))
                        {
                            name += content[i];
                            i++;
                        }
                        //skip attributes and go to next tag
                        if (Char.IsWhiteSpace(content[i]))
                            while (i < content.Length && content[i] != '>')
                                i++;

                        if (flag.Count != 0)
                        {
                            flagUp = flag.Pop() + 1;
                            flag.Push(flagUp);
                        }

                        if (flag.Count != 0 && flag.Peek() == 1)
                        {
                            //remove value and add region for the parent because it has a child
                            output = output.Substring(0, output.Length-(stack.Peek() + ".InnerText=\"\";" + System.Environment.NewLine).Length);
                            output += "#region " + stack.Peek() + System.Environment.NewLine;
                            output += "{" + System.Environment.NewLine;
                        }

                        //if it's the first element add xml as parent
                        if (stack.Count == 0)
                            output += "XmlElement " + name + " = (XmlElement)xml.AppendChild(xml.CreateElement(\"" + name + "\"));" + System.Environment.NewLine;
                        //else add prevoius element as parent
                        else
                            output += "XmlElement " + name + " = (XmlElement)" + stack.Peek() + ".AppendChild(xml.CreateElement(\"" + name + "\"));" + System.Environment.NewLine;

                        output += name + ".InnerText=\"\";" + System.Environment.NewLine;

                        stack.Push(name);

                        flag.Push(0);

                    }

                }
                else i++;
            }

            Console.Write(output);

            Console.ReadLine();


        }
    }
}
