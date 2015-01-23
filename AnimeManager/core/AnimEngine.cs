using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ConsoleLogger.Core;


namespace AnimeManager.core
{
    public sealed class AnimEngine
    {

        public static string[] GetParts(string filename)
        {
#if TEST_0xA1
            Cns.InitConsole();
#endif
            string temp = filename.Replace("_", " ")
                                  .ToLower();
            List<string> results = new List<string>();
            bool flag = false;
            char vs = '\n';

            for (int i = 0; i < temp.Length; i++)
            {
                char c = temp.ElementAt(i);
                if (flag && c == vs)
                {
                    flag = false;
                }
                else
                {
                    if (c >= 'a' && c <= 'z')
                    {

                    }
                    else
                    {
                        flag = true;
                        switch (c)
                        {
                            case '(':
                                vs = ')';
                                break;
                            case '[':
                                vs = ']';
                                break;
                            case '{':
                                vs = '}';
                                break;
                            case '-':
                                vs = c;
                                break;
                            default:
                                //TODO something
                                break;
                        }
                    }
                }
            }
            return results.ToArray();
        }
    }
}
