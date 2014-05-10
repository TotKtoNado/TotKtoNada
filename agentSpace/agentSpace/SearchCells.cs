using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace agentSpace
{
    enum CellState  {Undiscovered, Discovered, Delayed, Out_Of_Range};

    class SearchCells
    {
        private Dictionary<int, CellState> field;
        private Int16 width;
        private Int16 height;

        private static Random randGen = new Random(1020);

        public SearchCells()
        {
            field = new Dictionary<int, CellState>();
        }

        private void calculateSize (float radius) {
            float cellSize = 0.3f * radius;
            Int16 numberOfCells = (Int16)(1f/cellSize);
            width = numberOfCells;
            height = numberOfCells;
        }

        public CellState getCell(Int16 x, Int16 y)
        {
            if (x >= width || y >= height)
            {
                return CellState.Out_Of_Range;
            }
            CellState rez;
            Int16 pos = (Int16)(x  + y*height);
            if (field.TryGetValue(pos, out rez))
            {
                return rez;
            }
            else
            {
                return CellState.Undiscovered;
            }
        }

        public void setCell(Int16 x, Int16 y, CellState state)
        {
            if (x >= width || y >= height)
            {
                return;
            }
            Int16 pos = (Int16)(x  + y*height);
            field.Add(pos, state);
        }

        private Coordinates getCellPos(Int16 x, Int16 y)
        {
            float x1 = (float)(x / width) + (float) (0.5/width);
            float y1 = (float)(y / height) +(float) (0.5/height);;
            return new Coordinates(x1, y1);
        }

        private Coordinates getCellPos(int pos)
        {
            Int16 x = (Int16)(pos %height);
            Int16 y = (Int16)((pos - x) / height);
            return getCellPos(x,y);
        }

        public bool isFull()
        {
            return field.Count < width * height;
        }

        public Coordinates getUndiscoveredCell()
        {
            int pos = randGen.Next(0, width * height - 1);
            if (isFull())
            {
                return getCellPos(pos);
            }
            else
            {
                int temp;
                ICollection<int> col = field.Keys;
                for (int i = 0; i < width * height - 1; i++)
                {
                    temp = (pos + i) % (width * height - 1);
                    if (!col.Contains(temp))
                    {
                        return getCellPos(temp);
                    }
                }
                return getCellPos(pos);
            }
        }
    }
}
