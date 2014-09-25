﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан инструментальным средством
//     В случае повторного создания кода изменения, внесенные в этот файл, будут потеряны.
// </auto-generated>
//------------------------------------------------------------------------------
namespace GeneralPackage.Visualizer
{
	using GeneralPackage.GameData;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
    using System.Windows.Forms;
    using System.Drawing;

	/// <summary>
	/// Отображает всё игровое поле
	/// </summary>
	public class CommonFormVisualizer : Visualizer
    {
        private PictureBox screen;
        public bool Active
        {
            get;
            set;
        }
        #region constructor 

        public CommonFormVisualizer(ref Board board, ref PictureBox screen, bool isActive = true)
            :base (ref board)
        {
            Active = isActive;
            this.screen = screen;
        }

        #endregion

        public void draw(PaintEventArgs e)
        {
            if (Active)
            {

            }
        }

        public void drawAgents(PaintEventArgs e) {
            foreach (Agent agent in board.Agents.getDictionary().Values)
            {
                double drawRad = 0.005;
                int x = (int)((agent.coord.x - drawRad / 2.0f) * e.ClipRectangle.Width);
                int y = (int)((agent.coord.y - drawRad / 2.0f) * e.ClipRectangle.Height);
                int w = (Int32)(drawRad * e.ClipRectangle.Width);
                int h = (Int32)(drawRad * e.ClipRectangle.Height);
                Rectangle rec = new Rectangle(x, y, w, h);
                Pen p = new Pen(Color.Black, 3);
                e.Graphics.DrawEllipse(p, rec);
                p.Dispose();
            }
        }

    }
}

