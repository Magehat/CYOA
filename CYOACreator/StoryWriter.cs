using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.IO;

namespace CYOACreator
{
    static class StoryWriter
    {
        static string m_target;
        public static string separator = "#";

        static StoryWriter()
        {
            m_target = ConfigurationManager.AppSettings["writeFile"];
        }

        public static void Write(IEnumerable<Page> pages)
        {
            List<string> story = new List<string>();
            
            foreach(Page page in pages) {
                string toWrite = EncodePage(page);
                story.Add(toWrite);
            }

            File.WriteAllLines(m_target, story);
        }

        private static string EncodePage(Page page)
        {
            string ret;
            ret = "" + page.PageNum + separator
                + page.Text + separator;

            foreach (Transition trans in page.Transitions)
            {
                ret += EncodeTransition(trans);
            }

            return ret + "END";
        }

        private static string EncodeTransition(Transition trans)
        {
            string ret;
            ret = "" + trans.PageNum + separator
                + trans.Text + separator;

            return ret;
        }
    }
}
