using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiingInSingapore
{
    public class SkiiMap
    {
        private int[,] map;
        private int sizeX;
        private int sizeY;

        public SkiiMap(string[] lines)
        {
            // Initialize map values
            string[] size = lines[0].Trim().Split();
            sizeX = int.Parse(size[0]);
            sizeY = int.Parse(size[1]);

            map = new int[sizeX, sizeY];

            for (int i = 1; i < lines.Length; i++)
            {
                string[] grids = lines[i].Trim().Split();
                
                for (int j = 0; j < grids.Length; j++)
                {
                    map[j, i - 1] = int.Parse(grids[j]);
                }
            }
        } 

        public Result Run(int startX, int startY)
        {
            List<Result> results = new List<Result>();

            // Run through all possible starting points in map
            for (int y = startY; y < sizeY; y++)
            {
                for (int x = startX; x < sizeX; x++)
                {
                    List<Hill> visitedHill = new List<Hill>();
                    Hill startHill = new Hill(x, y, map[x, y]);
                    visitedHill.Add(startHill);
                    Traverse(ref startHill, ref visitedHill);

                    Result r = new Result(startHill.depthCount, startHill.maxSlope);
                    results.Add(r);
                }
            }

            return results.OrderByDescending(x => x.maxDepth).ThenByDescending(x => x.maxDrop).First();
        }

        private void Traverse(ref Hill parentHill, ref List<Hill> visitedHill)
        {
            // Populate all possible next directions
            for (int direction = 1; direction <= 4; direction++)
            {
                Hill neighbourHill = GetAdjacentHill(parentHill.xCoord, parentHill.yCoord, direction);

                if (neighbourHill != null && neighbourHill.height < map[parentHill.xCoord, parentHill.yCoord] && !visitedHill.Contains(neighbourHill))
                {
                    visitedHill.Add(neighbourHill);
                    neighbourHill.parent = new Stack<Hill>(parentHill.parent.Reverse());
                    neighbourHill.parent.Push(parentHill);
                    parentHill.AddChild(neighbourHill);
                }
            }

            // Recursive traversal for all subsequent possible next directions
            for (int i = 0; i < parentHill.GetChild().Count; i++)
            {
                Hill childHill = parentHill.GetChild()[i];
                Traverse(ref childHill, ref visitedHill);
            }
        }

        private Hill GetAdjacentHill(int x, int y, int direction)
        {
            switch (direction)
            {
                // North
                case 1:
                    if (y == 0)
                    {
                        return null;
                    }
                    return new Hill(x, y - 1, map[x, y - 1]);
                // East
                case 2:
                    if (x + 1 == sizeX)
                    {
                        return null;
                    }
                    return new Hill(x + 1, y, map[x + 1, y]);
                // South
                case 3:
                    if (y + 1 == sizeY)
                    {
                        return null;
                    }
                    return new Hill(x, y + 1, map[x, y + 1]);
                // West
                case 4:
                    if (x == 0)
                    {
                        return null;
                    }
                    return new Hill(x - 1, y, map[x - 1, y]);
                default:
                    return null;
            }
        }
    }
}
