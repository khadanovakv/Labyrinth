using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth.classes
{
    /// <summary>
    /// Тип ячейки (обычная/вход/выход)
    /// </summary>
    public enum EType
    {
        /// <summary>
        /// Обычная
        /// </summary>
        Ordinary = 0,
        /// <summary>
        /// Вход
        /// </summary>
        Entry = 1,
        /// <summary>
        /// Выход
        /// </summary>
        Exit = 2,
        /// <summary>
        /// Портал
        /// </summary>
        Portal = 3,
        /// <summary>
        /// Черная дыра
        /// </summary>
        BlackHole = 4,
        /// <summary>
        /// Финиш
        /// </summary>
        Fin = 5,
        /// <summary>
        /// Старт
        /// </summary>
        Strt = 6
    }
}
