using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace agentSpace
{
    enum CellState  {Undiscovered, Discovered};

    class SearchCells
    {
        private Dictionary<int, CellState> field;
        private Int16 width;
        private Int16 height;

        private static Random randGen = new Random();

        public SearchCells(float radius)
        {
            field = new Dictionary<int, CellState>();
            calculateSize(radius);
        }

        private void calculateSize (float radius) {
            float cellSize = 0.9f * radius;
            //float cellSize = 0.1f;
            Int16 numberOfCells = (Int16)(1f/cellSize);
            width = numberOfCells;
            height = numberOfCells;
            setCell(36,5, CellState.Discovered);
        }

        public CellState getCell(Int16 x, Int16 y)
        {
            if (x >= width || y >= height)
            {
                return CellState.Undiscovered;
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
            int pos = (x  + y*width);
            exclusiveAdd(pos, state);
        }

        private void exclusiveAdd(int pos, CellState state)
        {
            if (pos < 0)
            {
                Console.WriteLine("NEGATIVE POS! :" + pos.ToString());
            }
            try
            {
                field.Add(pos, state);
            }
            catch (ArgumentException )
            {
                field.Remove(pos);
                field.Add(pos, state);
            }
        }

        public void setCell(Coordinates coord, CellState state)
        {
            Int16 x = (Int16)(coord.x * width);
            Int16 y = (Int16)(coord.y * height);
            setCell(x, y, state);
        }

        private Coordinates getCellPos(Int16 x, Int16 y)
        {
            float x1 = ((float)x / width) + (float)(0.5 / width);
            float y1 = ((float)y / height) + (float)(0.5 / height); ;
            return new Coordinates(x1, y1);
        }

        private Coordinates getCellPos(int pos)
        {
            Int16 x = (Int16)(pos %width);
            Int16 y = (Int16)((pos - x) / width);
            return getCellPos(x,y);
        }

        public bool isFull()
        {
            return field.Count >= width * height;
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

        public void drawMatrix(PaintEventArgs e)
        {
            float cellW = (float)e.ClipRectangle.Width / (float)width;
            float cellH = (float)e.ClipRectangle.Height / (float)height;
            int x1 = 0, y1 = 0;
            foreach (int pos in field.Keys)
            {
                x1 = (int)((float)(pos % width) * cellW);
                y1 = (int)((float)(pos / width) * cellH);
                e.Graphics.FillRectangle(new SolidBrush(Color.LightCoral), x1, y1, (int)cellW, (int)cellH);
            }
        }


        //public void uniteWith(SearchCells obj) {
          //  foreach(int pos in 
    }
}
