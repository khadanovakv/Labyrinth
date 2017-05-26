namespace Labyrinth.classes
{
    /// <summary>
    /// Класс ячейка
    /// </summary>
    public class CCell
    {
        /// <summary>
        /// тип ячейки
        /// </summary>
        public EType type
        {
            get;
            set;
        }
        /// <summary>
        /// Левая граница
        /// </summary>
        public bool lborder
        {
            get;
            set;
        }
        /// <summary>
        /// Правая граница
        /// </summary>
        public bool rborder
        {
            get;
            set;
        }
        /// <summary>
        /// Верхняя граница
        /// </summary>
        public bool tborder
        {
            get;
            set;
        }
        /// <summary>
        /// Нижняя граница
        /// </summary>
        public bool bborder
        {
            get;
            set;
        }
        /// <summary>
        /// Координата X
        /// </summary>
        public int xcoord
        {
            get;
            set;
        }
        /// <summary>
        /// Координата Y
        /// </summary>
        public int ycoord
        {
            get;
            set;
        }
    }
}