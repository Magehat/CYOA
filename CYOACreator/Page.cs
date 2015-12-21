using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYOACreator
{
    public class Page
    {
        static long PageCount = 1;

        public long PageNum { get; set; }
        public string Text { get; set; }
        public IEnumerable<Transition> Transitions { get; set; }

        public Page(string text, IEnumerable<Transition> transitions)
        {
            PageNum = PageCount;
            PageCount++;

            Text = text;
            Transitions = transitions;
        }

        public Page(long pageNum, string text, IEnumerable<Transition> transitions)
        {
            PageNum = pageNum;

            Text = text;
            Transitions = transitions;
        }

        public Page()
        {
            PageNum = PageCount;
            PageCount++;

            Transitions = new List<Transition>();
        }

        public static void SetPageCount(long pageCount){
            PageCount = pageCount;
        }
    }
}
