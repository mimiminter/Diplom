using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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

namespace TestProga
{
    /// <summary>
    /// Логика взаимодействия для AddCompetence.xaml
    /// </summary>
    public partial class AddCompetence : Window
    {
        List<string> Alphabetrus = new List<string> {
            "А", "а", "Б", "б", "В", "в", "Г", "г", "Д", "д", "Е", "е", "Ё", "ё", "Ж", "ж", "З", "з", "И", "и", "Й", "й",
            "К", "к", "Л", "л", "М", "м", "Н", "н", "О", "о", "П", "п", "Р", "р", "С", "с", "Т", "т", "У", "у", "Ф", "ф",
            "Х", "х", "Ц", "ц", "Ч", "ч", "Ш", "ш", "Щ", "щ", "Ъ", "ъ", "Ы", "ы", "Ь", "ь", "Э", "э", "Ю", "ю", "Я", "я" };

        public AddCompetence()
        {
            InitializeComponent();
            SqlConnection connection = new SqlConnection("server=WIN-NHF22QP2E4K\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
            connection.Open();
            string cmd = "select id as 'Код обучения', name_type as 'Тип обучения' from TypeOfTraining";
            SqlCommand createcommand = new SqlCommand(cmd, connection);
            createcommand.ExecuteNonQuery();
            SqlDataAdapter sql = new SqlDataAdapter(createcommand);
            DataTable dt_competence = new DataTable("competence");
            sql.Fill(dt_competence);
            datagrid_types.ItemsSource = dt_competence.DefaultView;
            connection.Close();
        }
        public static DataTable Select(string selectSQL)
        {
            DataTable dataTable = new("dataBase");
            SqlConnection sqlConnection = new("server=WIN-NHF22QP2E4K\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSQL;
            SqlDataAdapter sqlDataAdapter = new(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (id_tb.Text.Length != 0 && id_type_tb.Text.Length != 0 && name_tb.Text.Length != 0)
            {
                bool id = false, id_type = false, name = false;
                for (int i = 0; i < id_tb.Text.Length; i++)
                {
                    if (id)
                    {
                        id = false;
                    }
                    if (id_tb.Text[i] >= '1' && id_tb.Text[i] <= '9')
                    {
                        id = true;
                        break;
                    }
                    if (id == false)
                    {
                        MessageBox.Show("Поле код должно содержать только цифры");
                        break;
                    }
                }
                for (int i = 0; i < name_tb.Text.Length; i++)
                {
                    if (name)
                    {
                        name = false;
                    }
                    for (int j = 0; j < Alphabetrus.Count; j++)
                    {
                        if (Convert.ToString(name_tb.Text[i]).Contains(Alphabetrus[j]))
                        {
                            name = true;
                            break;
                        }
                    }
                    if (name == false)
                    {
                        MessageBox.Show("Поле наименование должно содержать только руссике символы");
                        break;
                    }
                }
                DataTable sel1 = Select("select * from TypeOfTraining where id = " + id_type_tb.Text);
                for (int i = 0; i < id_type_tb.Text.Length; i++)
                {
                    if (id_type)
                    {
                        id_type = false;
                    }
                    if (id_tb.Text[i] >= '1' && id_tb.Text[i] <= '9')
                    {
                        id_type = true;
                        break;
                    }
                    if (id_type == false)
                    {
                        MessageBox.Show("Поле код типа обучения должно содержать только цифры");
                        break;
                    }
                }
                if(sel1.Rows.Count > 0)
                {
                    id_type = true;
                }
                else if(sel1.Rows.Count == 0) 
                {
                    id_type = false;
                    MessageBox.Show("Код типа обучения, который вы ввели, не существует");
                }
                if(id && id_type && name)
                {
                    DataTable dataTable = Select($"insert into Competence values ({id_tb.Text},'{name_tb.Text}',{id_type_tb.Text})" );
                    MessageBox.Show("Компетенция добавлена");
                    AdminWindow adminWindow = new AdminWindow();
                    adminWindow.Show();
                    Close();
                }
                else
                {
                    MessageBox.Show("Неправильный формат");
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены");
            }
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            Close();
        }
    }
}
