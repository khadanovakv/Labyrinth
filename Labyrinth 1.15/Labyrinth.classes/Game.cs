using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labyrinth.classes
{
    /// <summary>
    /// Игра
    /// </summary>
    public class CGame
    {
        #region constructor
        /// <summary>
        /// Конструктор
        /// </summary>
        /*
         * Поле
         * Указатель (текущее положение)
         */
        public CGame()
        {
            Field = new CField();
            Cursor = new CCursor();
        }
        #endregion
        /// <summary>
        /// Указатель
        /// </summary>
        public CCursor Cursor
        {
            get;
            private set;
        }
        /// <summary>
        /// Поле
        /// </summary>
        public CField Field
        {
            get;
            private set;
        }
        public Bitmap Curs;
        public void DrawingCursor()
        {
            Curs = new Bitmap(400, 400);
            using (Graphics g = Graphics.FromImage(Curs))
            {
                g.Clear(Color.Transparent);
                Bitmap crs = new Bitmap(@"cursor.png");
                g.DrawImage(crs, new Rectangle((Cursor.coordx)*40, (Cursor.coordy)*40, 40, 40));
            }
        }
        public void Start()
        {
            Field.newfield(); //создание и заполнение поля
            Cursor.coordx = 0;
            Cursor.coordy = 0;
            DrawingCursor();            
        }
        public void port()
        {
            {
                Random rnd = new Random();
                int r = rnd.Next(0, Field.PortalList.Count);
                if ((Field.PortalList[r].ycoord==Cursor.coordx)&&(Field.PortalList[r].xcoord== Cursor.coordy))
                {
                    port();
                }
                else
                {
                    Cursor.coordx = Field.PortalList[r].ycoord;
                    Cursor.coordy = Field.PortalList[r].xcoord;
                }
            }
            DrawingCursor();
        }
        public void bh()
        {
            Random rnd = new Random();
            Cursor.coordx = rnd.Next(0, Field.Width - 1);
            Cursor.coordy = rnd.Next(0, Field.Height - 1);
            DrawingCursor();
        }
        public void fin()
        {
            Cursor.coordx = Field.Width - 1;
            Cursor.coordy = Field.Height - 1;
            DrawingCursor();
        }
        public void strt()
        {
            Cursor.coordx = 0;
            Cursor.coordy = 0;
            DrawingCursor();
        }
        public void bottom()
        {
            
            if (!(Field.CellList[Field.GetIndex(Cursor.coordy,Cursor.coordx)].bborder)) //нет нижней границы
            {
                Cursor.coordy = Cursor.coordy + 1; //увел.коорд.Y (вниз)
                DrawingCursor();
            }
        }
        public void top()
        {

            if (!(Field.CellList[Field.GetIndex(Cursor.coordy, Cursor.coordx)].tborder)) //нет верхней границы
            {
                Cursor.coordy = Cursor.coordy - 1; //уменьш.коорд.Y (вверх)
                DrawingCursor();
            }
        }
        public void left()
        {
            if (!(Field.CellList[Field.GetIndex(Cursor.coordy, Cursor.coordx)].lborder)) //нет левой границы
            {
                Cursor.coordx = Cursor.coordx - 1; //уменьш.коорд.X (влево)
                DrawingCursor();
            }
        }
        public void right()
        {
            if (!(Field.CellList[Field.GetIndex(Cursor.coordy, Cursor.coordx)].rborder)) //нет правой границы
            {
                Cursor.coordx = Cursor.coordx + 1; //увел.коорд.X (вправо)
                DrawingCursor();
            }
        }
                
    }

}
