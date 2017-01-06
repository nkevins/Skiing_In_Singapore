using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiingInSingapore
{
    public class Result
    {
        public int maxDepth { get; set; }
        public int maxDrop { get; set; }

        public Result(int maxDepth, int maxDrop)
        {
            this.maxDepth = maxDepth;
            this.maxDrop = maxDrop;
        }
    }
}
