using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Poprijenok
{
    /// <summary>
    /// Логика взаимодействия для EditAgentsPriority.xaml
    /// </summary>
    public partial class EditAgentsPriority : Window
    {
        private List<AgentsNew> agents;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="_agents"></param>
        public EditAgentsPriority(List<AgentsNew> _agents)
        {
            InitializeComponent();
            this.agents = _agents;
            var maxPriority = poprijenokEntities.GetEntities().Agents.OrderByDescending(p => p.priority).FirstOrDefault();
            DataContext = maxPriority;
        }

        /// <summary>
        /// Изменение приоритета агентов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(PriorityToChange.Text, out int value))
            {
                MessageBox.Show("Ввёденое значение должно быть числом!");
                return;
            }

            int priority = Convert.ToInt32(PriorityToChange.Text);

            foreach (var agent in agents)
            {
                var AgentToChange = poprijenokEntities.GetEntities().Agents.Where(p => p.agent_ID == agent.ID).FirstOrDefault();

                AgentToChange.priority = priority;

                try
                {
                    poprijenokEntities.GetEntities().SaveChanges();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Не удалось изменить приоритеты");
                }
            }

            MessageBox.Show("Данные изменены!");
            Manager.EditPriority.IsEnabled = true;      
            this.Close();
        }
    }
}
