using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poprijenok
{

    /// <summary>
    /// Класс переопределения агентов
    /// </summary>
    public class AgentsNew
    {
        public int ID { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public int Sales { get; set; }
        public string Phone { get; set; }
        public int Priority { get; set; }
        public int Discount { get; set; }
        public string AgentColor { get; set; }
    }
}
