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

        /// <summary>
        /// Первая инициализация
        /// </summary>
        private void FirstInit()
        {
            var FilterParms = poprijenokEntities.GetEntities().Agents_type.ToList();
            FilterParms.Insert(0, new Agents_type { title = "Все типы" });

            var agents = poprijenokEntities.GetEntities().Agents.ToList();

            ShowAgentData(agents);
            Paginator.DataCount = agents.Count;
            Paginator.CurrentPage = 1;
            FilterFiled.SelectedIndex = 0;
            SearchField.Text = "";
            PagesCount.Text = "Страница " + Paginator.CurrentPage + " из " + Paginator.TotalPages;

            FilterFiled.ItemsSource = FilterParms;
        }

        /// <summary>
        /// Изменение отображения нумерации страниц
        /// </summary>
        /// <param name="flag"></param>
        private void PageChange(int flag)
        {
            var agents = poprijenokEntities.GetEntities().Agents.ToList();

            if (flag == 0)
            {
                if(Paginator.TotalPages == Paginator.CurrentPage)
                {
                    MessageBox.Show("Достигнуто максимальное кол-во страниц!");
                    return;
                }

                Paginator.CurrentPage += 1;        
                ShowAgentData(agents, 10, Paginator.CurrentPage * 10);
                PagesCount.Text = "Страница " + Paginator.CurrentPage + " из " + Paginator.TotalPages;
            }
            else
            {
                if(Paginator.CurrentPage != 1)
                {
                    Paginator.CurrentPage -= 1;
                    ShowAgentData(agents, 10, Paginator.CurrentPage * 10);
                    PagesCount.Text = "Страница " + Paginator.CurrentPage + " из " + Paginator.TotalPages;
                }
                else
                {
                    MessageBox.Show("Достигнуто минимальное кол-во страниц!");
                    return;
                }
            }
        }

        /// <summary>
        /// Генерация данных для отображения в списке
        /// </summary>
        /// <param name="PageSize">Размер страницы</param>
        private void ShowAgentData(List<Agents> agents, int PageSize = 10, int ToSkip = 0)
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

            LViewAgents.ItemsSource = AgentsList.Skip(ToSkip).Take(PageSize);
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

        /// <summary>
        /// нажатие кнопки предыдущая страница
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonPrev_Click(object sender, RoutedEventArgs e)
        {
            PageChange(1);
        }

        /// <summary>
        /// Нажатие кнопки следующая страница
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            PageChange(0);
        }

        /// <summary>
        /// Фильтрация данных при срабатывании события изменения combobox или textbox
        /// </summary>
        private void FilterData()
        {
            var agents = poprijenokEntities.GetEntities().Agents.ToList();

            if (FilterFiled.SelectedIndex > 0)
            {
                var type_id = (FilterFiled.SelectedItem as Agents_type).type_ID;

                agents = poprijenokEntities.GetEntities().Agents.Where(p => p.Agents_type.type_ID == type_id).ToList();   
            }

            var TextToSearch = SearchField.Text.ToLower();

            agents = agents.Where(p => p.title.ToLower().Contains(TextToSearch) || p.phone.ToLower().Contains(TextToSearch) || p.email.ToLower().Contains(TextToSearch)).ToList();

            if (agents.Count == 0)
            {
                MessageBox.Show("Данные по указанным фильтрам не найдены");
                SearchField.Text = "";

                var agentNew = poprijenokEntities.GetEntities().Agents.ToList();

                ShowAgentData(agentNew);

                Paginator.DataCount = agentNew.Count;
                Paginator.CurrentPage = 1;
                PagesCount.Text = "Страница " + Paginator.CurrentPage + " из " + Paginator.TotalPages;
                return;
            }

            ShowAgentData(agents);

            Paginator.DataCount = agents.Count;
            Paginator.CurrentPage = 1;
            PagesCount.Text = "Страница " + Paginator.CurrentPage + " из " + Paginator.TotalPages;
        }

        /// <summary>
        /// Срабатывания изменения выбора типа агента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterFiled_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterData();
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

        /// <summary>
        /// Добавление агента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddAgent_Click(object sender, RoutedEventArgs e)
        {
            Manager.frame.Navigate(new AddAgentPage());
        }

        /// <summary>
        /// Срабатывание изменения поля поиска 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchField_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterData();
        }

        /// <summary>
        /// Удаление агента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteAgent_Click(object sender, RoutedEventArgs e)
        {
            var selectedItems = LViewAgents.SelectedItems[0];

            if (selectedItems != null)
            {
                var agentToDelete = selectedItems as AgentsNew;

                var agent = poprijenokEntities.GetEntities().Agents.Where(p => p.agent_ID == agentToDelete.ID).FirstOrDefault();
                var agentAddress = poprijenokEntities.GetEntities().Agent_address.Where(p => p.Agent_ID == agentToDelete.ID).FirstOrDefault();

                if (agent.Agent_release_history.Count != 0)
                {
                    MessageBox.Show("У данного агента имеется история реализации продукта, его удаление запрещено!");
                    return;
                }

                if(agentAddress != null)
                    poprijenokEntities.GetEntities().Agent_address.Remove(agentAddress);

                if (agent != null)
                    poprijenokEntities.GetEntities().Agents.Remove(agent);

                try
                {
                    poprijenokEntities.GetEntities().SaveChanges();
                    MessageBox.Show("Данные удалены!");
                    FirstInit();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                MessageBox.Show("Для удаления сначала необходимо выбрать агента!");
                return;
            }
        }
    }
}
