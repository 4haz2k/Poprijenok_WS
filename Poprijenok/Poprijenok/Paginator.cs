using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poprijenok
{
    /// <summary>
    /// Статический класс для хранения данных пагинации
    /// </summary>
    class Paginator
    {
        /// <summary>
        /// Текущая страница
        /// </summary>
        public static int CurrentPage { get; set; }

        /// <summary>
        /// Общее кол-во данных
        /// </summary>
        public static int DataCount { get; set; }

        ///Подсчет данных о кол-во страниц
        public static int TotalPages
        {
            get
            {
                return (DataCount % 10 == 0) ? DataCount / 10 : DataCount / 10 + 1;
            }
        }
    }
}
