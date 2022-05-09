using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Poprijenok
{
    /// <summary>
    /// Менеджер для удалённого управления объектами в программе
    /// </summary>
    static class Manager
    {
        /// <summary>
        /// Фрейм на главной странице
        /// </summary>
        public static Frame frame { get; set; }
        /// <summary>
        /// Название на главной странице
        /// </summary>
        public static TextBlock PageTitle { get; set; }
        /// <summary>
        /// Кнопка изменения приоритета
        /// </summary>
        public static Button EditPriority { get; set; }
    }
}
