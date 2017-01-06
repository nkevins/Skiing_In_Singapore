using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiingInSingapore
{
    public class Hill
    {
        private List<Hill> child;
        public Stack<Hill> parent;

        public int xCoord;
        public int yCoord;
        public int height;

        public int depthCount;
        public int maxSlope;

        public Hill(int x, int y, int height)
        {
            parent = new Stack<Hill>();
            child = new List<Hill>();
            xCoord = x;
            yCoord = y;
            this.height = height;

            depthCount = 0;
            maxSlope = 0;
        }

        public Hill GetRootHill()
        {
            if (this.parent.Count == 0)
            {
                return this;
            }

            return this.parent.Peek().GetRootHill();
        }

        public void AddChild(Hill h)
        {
            child.Add(h);

            int totalDepth = h.parent.Count + 1;
            int slopeDiff = this.GetRootHill().height - h.height;

            if (totalDepth >= h.GetRootHill().depthCount && slopeDiff >= h.GetRootHill().maxSlope)
            {
                h.GetRootHill().depthCount = totalDepth;
                h.GetRootHill().maxSlope = slopeDiff;               
            }
        }

        public List<Hill> GetChild()
        {
            return child;
        }
    }
}
