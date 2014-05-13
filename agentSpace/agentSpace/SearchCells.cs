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
    //enum CellState  {Undiscovered, Discovered};

    public class SearchCells
    {
        //private Dictionary<int, CellState> field;
        private HashSet<int> field;
        private Int16 width;
        private Int16 height;

        private static Random randGen = new Random();

        public SearchCells() { }

        public SearchCells(float radius)
        {
            field = new HashSet<int>();
            calculateSize(radius);
        }

        private void calculateSize (float radius) {
            float cellSize = 0.9f * radius;
            //float cellSize = 0.1f;
            Int16 numberOfCells = (Int16)(1f/cellSize);
            width = numberOfCells;
            height = numberOfCells;
            setCell(36,5);
        }

        public bool isDiscovered(Int16 x, Int16 y)
        {
            if (x >= width || y >= height)
            {
                return false;
            }
            int pos = (x  + y*width);
            return field.Contains(pos);
        }

        public bool setCell(Int16 x, Int16 y)
        {//Returns true, if something in matrix was modified
            if (x >= width || y >= height)
            {
                return false;
            }
            int pos = (x  + y*width);
            if (field.Contains(pos))
            {
                return false;
            }
            else
            {
                field.Add(pos);
                return true;
            }
        }


        public bool setCell(Coordinates coord)
        {
            Int16 x = (Int16)(coord.x * width);
            Int16 y = (Int16)(coord.y * height);
            return setCell(x, y);
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
                ICollection<int> col = field;
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
            foreach (int pos in field)
            {
                x1 = (int)((float)(pos % width) * cellW);
                y1 = (int)((float)(pos / width) * cellH);
                e.Graphics.FillRectangle(new SolidBrush(Color.LightCoral), x1, y1, (int)cellW, (int)cellH);
            }
        }

        public HashSet<int> getCollection()
        {
            return field;
        }

        public void uniteWith(SearchCells obj)
        {
            field.UnionWith(obj.getCollection());
        }
    }
}
