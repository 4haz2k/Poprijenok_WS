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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Poprijenok
{
    /// <summary>
    /// Логика взаимодействия для AgentsPage.xaml
    /// </summary>
    public partial class AgentsPage : Page
    {
        public AgentsPage()
        {
            InitializeComponent();      
            FirstInit();
        }

        private void FirstInit()
        {
            var FilterParms = poprijenokEntities.GetEntities().Agents_type.ToList();
            FilterParms.Insert(0, new Agents_type { title = "Все типы" });

            ShowAgentData(poprijenokEntities.GetEntities().Agents.ToList());

            FilterFiled.ItemsSource = FilterParms;
        }

        /// <summary>
        /// Генерация данных для отображения в списке
        /// </summary>
        /// <param name="PageSize">Размер страницы</param>
        private void ShowAgentData(List<Agents> agents, int PageSize = 10)
        {
            List<AgentsNew> AgentsList = new List<AgentsNew>();
            

            foreach(var agent in agents)
            {
                var sales = poprijenokEntities.GetEntities().Agent_release_history.Where(p => p.agent_ID == agent.agent_ID).ToList();
                int SalesCount = 0;            

                if (sales.Any())
                {                  
                    foreach(var sale in sales)
                    {
                        SalesCount += sale.qty;
                    }
                }

                int discount = CalculateDiscount(SalesCount);

                AgentsList.Add(new AgentsNew() { 
                    ID = agent.agent_ID,
                    Discount = discount,
                    Image = agent.newLogo,
                    Phone = agent.phone,
                    Priority = agent.priority,
                    Sales = SalesCount,
                    Title = agent.Agents_type.title + " | " + agent.title,
                    AgentColor = discount == 25 ? "LightGreen" : "Transparent"
                });
            }

            LViewAgents.ItemsSource = AgentsList.Take(PageSize);
        }

        /// <summary>
        /// Подсчет скидки агента
        /// </summary>
        /// <param name="SalesCount">Кол-во продаж</param>
        /// <returns></returns>
        private int CalculateDiscount(int SalesCount)
        {
            if(SalesCount >= 10000 && SalesCount < 50000)
            {
                return 5;
            }
            else if (SalesCount >= 50000 && SalesCount < 150000)
            {
                return 10;
            }
            else if (SalesCount >= 150000 && SalesCount < 500000)
            {
                return 20;
            }
            else if (SalesCount >= 500000)
            {
                return 25;
            }
            else
            {
                return 0;
            }
        }

        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FilterFiled_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(FilterFiled.SelectedIndex > 0)
            {
                var type_id = (FilterFiled.SelectedItem as Agents_type).type_ID;
                var agents = poprijenokEntities.GetEntities().Agents.Where(p => p.Agents_type.type_ID == type_id).ToList();

                if(agents == null)
                {
                    MessageBox.Show("Данные по указанным фильтрам не найдены");
                    ShowAgentData(poprijenokEntities.GetEntities().Agents.ToList());
                    return;
                }

                ShowAgentData(agents);
            }
            else
            {
                ShowAgentData(poprijenokEntities.GetEntities().Agents.ToList());
            }
        }

        private void ChangePriority_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = LViewAgents.SelectedItems;      
            
            if(selectedItems != null)
            {
                Manager.EditPriority = EditPriority;
                Manager.EditPriority.IsEnabled = false;
                List<AgentsNew> agentsTransport = new List<AgentsNew>(selectedItems.OfType<AgentsNew>());
                EditAgentsPriority editAgentsPriority = new EditAgentsPriority(agentsTransport);
                editAgentsPriority.Show();
            }
            else
            {
                MessageBox.Show("Для изменения приоритета сначала необходимо выбрать агентов!");
                return;
            }
        }
    }
}
