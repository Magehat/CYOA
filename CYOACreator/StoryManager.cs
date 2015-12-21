using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.IO;

namespace CYOACreator
{
    class StoryManager
    {
        private Dictionary<long, Page> m_pages;
        private string m_target;
        private string m_separator;

        public StoryManager()
        {
            m_pages = new Dictionary<long,Page>();
            m_target = ConfigurationManager.AppSettings["writeFile"];
            m_separator = StoryWriter.separator;

            long maxPage = 0;

            if (File.Exists(m_target))
            {
                string[] lines = File.ReadAllLines(m_target);
                foreach (string line in lines)
                {
                    string[] values = line.Split(m_separator.ToCharArray());
                    
                    long pageNum = long.Parse(values[0]);
                    maxPage = maxPage > pageNum ? maxPage : pageNum;
                    string text = values[1];

                    List<Transition> transitions = new List<Transition>();

                    for (int i = 2; ; i+=2)
                    {
                        if (values[i] == "END")
                        {
                            break;
                        }

                        transitions.Add(new Transition(values[i + 1], long.Parse(values[i])));
                    }

                    UpdatePage(pageNum, text, transitions);
                }
                Page.SetPageCount(maxPage+1);
            }
            else
            {
                CreatePage();
            }
        }

        public IEnumerable<Page> GetPages()
        {
            return m_pages.Values;
        }

        public Page GetPage(long PageNum)
        {
            return FetchPage(PageNum);
        }

        public long CreatePage()
        {
            return CreatePage("");
        }

        public long CreatePage(string pageText)
        {
            Page newPage = new Page(pageText, new List<Transition>());
            m_pages.Add(newPage.PageNum, newPage);
            return newPage.PageNum;
        }

        public void UpdatePage(long pageNum, string text, IEnumerable<Transition> transitions)
        {
            Page updatedPage = new Page(pageNum, text, transitions);
            m_pages[pageNum] = updatedPage;
        }

        public void UpdateTransitionsForPage(long pageNum, IEnumerable<Transition> transitions) 
        {
            Page page = FetchPage(pageNum);

            page.Transitions = transitions;
        }

        public void AddTransition(long pageNum, string transitionText, long transitionTarget)
        {
            Page page = FetchPage(pageNum);

            Transition[] trans = { new Transition(transitionText, transitionTarget) };
            page.Transitions = page.Transitions.Concat(trans);
        }

        private Page FetchPage(long pageNum)
        {
            Page page;

            if (!m_pages.TryGetValue(pageNum, out page))
            {
                throw new Exception("Page Not Found!");
            }

            return page;
        }
    }
}
