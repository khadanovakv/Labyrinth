using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labyrinth.classes
{
    /// <summary>
    /// Класс поле
    /// </summary>
    public class CField
    {
       /*
        * Ячейки
        * Вход
        * Выход
        */
        public int Width; // ширина поля
        public int Height; // высота поля
        public CField()
        {
            CellList = new List<CCell>();
            Width = 10;
            Height = 10;
        }
        /// <summary>
        /// Массив ячеек
        /// </summary>
        public List<CCell> CellList
        {
            get;
            private set;
        }
        public List<CCell> PortalList
        {
            get;
            set;
        }
        public CCell Cell
        {
            get;
            set;
        }
        public Bitmap Img; //лабиринт
        public Bitmap BlackHoles; //"черные дыры"

        
        public CCell Get(int X, int Y) //ячейка с координатами X,Y
        {
            return CellList[GetIndex(X, Y)];
        }
        public int GetIndex(int X, int Y) //индекс ячейки с координатами X,Y
        {
            if (X >= Height) throw new Exception(string.Format("высота {0} больше, чем {1}", X, Height));
            if (Y >= Width) throw new Exception(string.Format("ширина {0} больше, чем {1}", Y, Width));
            int ii = X * (Width + 1) + Y;
            return ii;
            throw new Exception(string.Format("ячейка в позиции {0},{1} не найдена",X, Y));            
        }
        
        public void borders() //создание случайных границ
        {            
            Random rand = new Random();
            for (int i = 1; i <= (Width * Height /2); i++)
            {
                int ran = rand.Next(0, CellList.Count - Width - 3); //случайное число - номер ячейки
                if (CellList[ran].ycoord == Width)
                    ran = ran - 1;
                CellList[ran].tborder = true;
                if (CellList[ran].xcoord >= 1)
                {
                    CCell pXMinus1 = Get(CellList[ran].xcoord - 1, CellList[ran].ycoord); //соседняя ячейка сверху
                    pXMinus1.bborder = true; //создание парной границы
                }
                ran = rand.Next(0, CellList.Count - Width - 3); //случайное число - номер ячейки
                if (CellList[ran].ycoord == Width)
                    ran = ran - 1;
                CellList[ran].lborder = true;
                if (CellList[ran].ycoord >= 1)
                {
                    CCell pYMinus1 = Get(CellList[ran].xcoord, CellList[ran].ycoord - 1); //соседняя ячейка слева
                    pYMinus1.rborder = true; //создание парной границы
                }
            }
        }
        public void bholes() //создание "черных дыр"
        {
            Random rand = new Random();
            PortalList = new List<CCell>();
            for (int i = 1; i <= 5; i++)
            {
                int ran = rand.Next(1, CellList.Count - Width - 4); //случайное число - номер ячейки
                if (CellList[ran].ycoord == Width)
                    ran = ran - 1;
                int rantype = rand.Next(3, 7);
                if (rantype == 3)
                {
                    CellList[ran].type = EType.Portal;
                    PortalList.Add(CellList[ran]);
                    int ran2 = rand.Next(1, CellList.Count - Width - 4);
                    if (CellList[ran2].ycoord == Width)
                        ran2 = ran2 - 1;
                    CellList[ran2].type = EType.Portal;
                    PortalList.Add(CellList[ran2]);
                }
                if (rantype == 4)
                {
                    CellList[ran].type = EType.BlackHole;
                }
                if (rantype == 5)
                {
                    CellList[ran].type = EType.Fin;
                }
                if (rantype == 6)
                {
                    CellList[ran].type = EType.Strt;
                }
            }

        }
        public void DrawingField()
        {
            List<Bitmap> cells = new List<Bitmap>(); //список готовых ячеек
            for (int j = 0; j <= CellList.Count-1; j++)
            //int j = 0;
            {
                List<string> ListBorders = new List<string>(); //список границ одной ячейки
                if (CellList[j].lborder)
                    ListBorders.Add(@"left3.png");
                if (CellList[j].tborder)
                    ListBorders.Add(@"top3.png");
                if (CellList[j].type == EType.Exit)
                    ListBorders.Add(@"exit.png");
                int x = CellList[j].xcoord;
                int y = CellList[j].ycoord;
                if ((x > 0) & (y > 0))
                    if ((Get(x-1, y-1).rborder) & (Get(x-1, y-1).bborder))
                        ListBorders.Add(@"lt.png");
                ListBorders.Add(@"empty.png");
                Bitmap cell = CombineBitmap(ListBorders); //картинка одной ячейки со всеми ее границами
                cells.Add(cell);
            }
            Img = new Bitmap((Width+1)*40, (Height+1)*40);
            using (Graphics g = Graphics.FromImage(Img))
            {
                g.Clear(Color.Transparent);
                for(int i=0; i<=CellList.Count-1; i++)
                {
                    g.DrawImage(cells[i], (CellList[i].ycoord)*40, (CellList[i].xcoord)*40);
                }
            }
            Img.Save(@"img.png");           
        }

        public void DrawingBlackHoles()
        {
            List<Bitmap> holes = new List<Bitmap>(); //список
            for (int j = 0; j <= CellList.Count - Width - 4; j++)
            {
                Bitmap hole = new Bitmap(40, 40);
                switch (CellList[j].type)
                {
                    case EType.Portal:
                        hole = new Bitmap(Image.FromFile(@"blackhole1.png"), 40, 40);
                        break;
                    case EType.BlackHole:
                        hole = new Bitmap(Image.FromFile(@"blackhole2.png"), 40, 40);
                        break;
                    case EType.Fin:
                        hole = new Bitmap(Image.FromFile(@"blackhole3.png"), 40, 40);
                        break;
                    case EType.Strt:
                        hole = new Bitmap(Image.FromFile(@"blackhole4.png"), 40, 40);
                        break;
                    case EType.Ordinary:
                        hole = new Bitmap(Image.FromFile(@"empty.png"), 40, 40);
                        break;
                }
                holes.Add(hole);
            }
            BlackHoles = new Bitmap((Width+1) * 40, (Height+1) * 40); //картинка с черными дырами
            using (Graphics g = Graphics.FromImage(BlackHoles))
            {
                g.Clear(Color.Transparent);
                for (int i = 0; i <= CellList.Count - Width - 4; i++)
                {
                    g.DrawImage(holes[i], (CellList[i].ycoord) * 40, (CellList[i].xcoord) * 40);
                }
            }
            BlackHoles.Save(@"bh.png");
            //List<string> Pctrs = new List<string>();
            //Pctrs.Add(@"img.png");
            //Pctrs.Add(@"bh.png");
            //Pict = CombineBitmap(Pctrs); //изображение с "черными дырами" и лабиринтом
        }

        //public static Bitmap CombineBitmap(IEnumerable<string> files)
        public static Bitmap CombineBitmap(List<string> files)
        {
            int width = 0;
            int height = 0;
            List<Bitmap> images = new List<Bitmap>(); //создание списка изображений
            foreach (string image in files)
            {
                Bitmap bitmap = new Bitmap(image); //изображение из файла
                width = bitmap.Width;
                height = bitmap.Height;
                images.Add(bitmap); //добавление в список
            }
            Bitmap finalImage = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(finalImage))
            {
                g.Clear(Color.Transparent); //фон
                foreach (Bitmap image in images)
                {
                    g.DrawImage(image, new Rectangle(0, 0, width, height));
                }
            }
            return finalImage;
        }
        
        public void newfield()
        {
            CellList.Clear();
            for (int i = 0; i <= Height; i++)
            {
                for (int j = 0; j <= Width; j++)
                {
                    CCell Cell = new CCell()
                    {
                        xcoord = i,
                        ycoord = j,
                        bborder = false,
                        lborder = false,
                        rborder = false,
                        tborder = false,
                        type = EType.Ordinary
                    };
                    //создание внешних границ
                    if ((i == 0) || (i == Height))
                    {
                        Cell.tborder = true;
                    }
                    if ((j == 0) || (j == Width))
                    {
                        Cell.lborder = true;
                    }
                    if (i == (Height - 1))
                    {
                        Cell.bborder = true;
                    }
                    if (j == (Width - 1))
                    {
                        Cell.rborder = true;
                    }
                    CellList.Add(Cell);
                }
            }
            CellList[0].type = EType.Entry;
            CellList[CellList.Count-Width-3].type = EType.Exit;
            borders(); //создание случайных границ
            bholes(); //создание "черных дыр"
            DrawingField(); //рисование поля
            DrawingBlackHoles();//рисование черных дыр
        }

    }
}
