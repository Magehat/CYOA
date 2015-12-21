using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CYOACreator
{
    public class Transition
    {
        public string Text { get; set; }
        public long PageNum { get; set; }

        public Transition(string text, long pageNum)
        {
            Text = text;
            PageNum = pageNum;
        }

    }
}
