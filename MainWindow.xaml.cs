using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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
namespace TestProga
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text.Length > 0)
            {
                if (pass1.Password.Length > 0)
                {
                    DataTable dt_infoadmins = Select("select * from [dbo].[Admins] where [login_admin] = '" + Login.Text + "' and [password_admin] = '" + pass1.Password + "'");
                    if (dt_infoadmins.Rows.Count > 0)
                    {
                        MessageBox.Show("Авторизация пройдена успешно");
                        AdminWindow adminWindow = new();
                        adminWindow.Show();
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Пользователь не найден");
                    }
                }
                else
                {
                    MessageBox.Show("Введите пароль");
                }
            }
            else
            {
                MessageBox.Show("Введите логин");
            }
        }
        private void Button_Click_Smena(object sender, RoutedEventArgs e)
        {
            ChangePassword password = new ChangePassword();
            password.Show();
            Close();
        }
        public static DataTable Select(string selectSQL)
        {
            DataTable dataTable = new("dataBase");
            SqlConnection sqlConnection = new("server=localhost\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSQL;
            SqlDataAdapter sqlDataAdapter = new(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
    }
        
}
