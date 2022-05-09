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
    /// Логика взаимодействия для AddAgentPage.xaml
    /// </summary>
    public partial class AddAgentPage : Page
    {
        public AddAgentPage()
        {
            InitializeComponent();
            LoadAgentsType();
            Manager.PageTitle.Text = "Попрыженок: добавление агента";
        }

        /// <summary>
        /// Добавление агента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddAgent_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(AgentTitle.Text))
            {
                errors.AppendLine("Необходимо заполнить поле наименование;");
            }

            if (string.IsNullOrWhiteSpace(AgentPriority.Text))
            {
                errors.AppendLine("Необходимо заполнить поле приоритет;");
            }

            if (!int.TryParse(AgentPriority.Text, out int value))
            {
                errors.AppendLine("Поле Приоритет должно быть числом;");
            }            

            if (string.IsNullOrWhiteSpace(AgentAddress.Text))
            {
                errors.AppendLine("Необходимо заполнить поле адрес;");
            }

            if (string.IsNullOrWhiteSpace(AgentINN.Text))
            {
                errors.AppendLine("Необходимо заполнить поле ИНН;");
            }

            if (string.IsNullOrWhiteSpace(AgentKPP.Text))
            {
                errors.AppendLine("Необходимо заполнить поле КПП;");
            }

            if (string.IsNullOrWhiteSpace(AgentFIO.Text))
            {
                errors.AppendLine("Необходимо заполнить поле ФИО директора;");
            }

            if (string.IsNullOrWhiteSpace(AgentPhone.Text))
            {
                errors.AppendLine("Необходимо заполнить поле телефон;");
            }

            if (string.IsNullOrWhiteSpace(AgentEmail.Text))
            {
                errors.AppendLine("Необходимо заполнить поле Email;");
            }

            if(AgentType.SelectedItem == null)
            {
                errors.AppendLine("Необходимо указать тип агента;");
            }

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            var agent = new Agents()
            {
                title = AgentTitle.Text,
                email = AgentEmail.Text,
                phone = AgentPhone.Text,
                logo = AgentLogo.Text == "" ? null : AgentLogo.Text,
                priority = Convert.ToInt32(AgentPriority.Text),
                director_name = AgentFIO.Text.Split(' ')[1],
                director_surname = AgentFIO.Text.Split(' ')[0],
                director_patronymic = AgentFIO.Text.Split(' ')[2],
                INN = AgentINN.Text,
                KPP = AgentKPP.Text,
                agent_type_ID = (AgentType.SelectedItem as Agents_type).type_ID
            };

            poprijenokEntities.GetEntities().Agents.Add(agent);

            try
            {
                poprijenokEntities.GetEntities().SaveChanges();

                Agent_address agent_Address = new Agent_address()
                {
                    Agent_ID = agent.agent_ID,
                    index = AgentAddress.Text.Split(',')[0],
                    region = AgentAddress.Text.Split(',')[1],
                    city = AgentAddress.Text.Split(',')[2],
                    street = AgentAddress.Text.Split(',')[3],
                    frame = AgentAddress.Text.Split(',')[4],
                };

                poprijenokEntities.GetEntities().Agent_address.Add(agent_Address);

                try
                {
                    poprijenokEntities.GetEntities().SaveChanges();
                    MessageBox.Show("Агент сохранен.");
                    Manager.frame.Navigate(new AgentsPage());
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Загрузка типов агентов из БД
        /// </summary>
        private void LoadAgentsType()
        {
            AgentType.ItemsSource = poprijenokEntities.GetEntities().Agents_type.ToList();
        }
    }
}
