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
    /// Логика взаимодействия для ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        public ChangePassword()
        {
            InitializeComponent();
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
        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }

        private void Button_Click_Change_Password(object sender, RoutedEventArgs e)
        {
            if (Login.Text.Length > 0 && pass1.Password.Length > 0 && pass2.Password.Length > 0)
            {
                bool log = false, pas1 = false, pas2 = false;
                DataTable dt_infoadmins = Select("select * from [dbo].[Admins] where [login_admin] = '" + Login.Text+ "'");
                if (dt_infoadmins.Rows.Count > 0)
                {
                    log = true;
                }
                else { log = false; MessageBox.Show("Такого логина нет в системе"); }
                for (int i = 0; i < pass1.Password.Length; i++)
                {
                    if (pas1)
                    {
                        pas1 = false;
                    }
                    if (pass1.Password[i] >= 'a' && pass1.Password[i] <= 'z' || pass1.Password[i] >= 'A' && pass1.Password[i] <=
'Z' || pass1.Password[i] >= '0' && pass1.Password[i] <= '9' && pass1.Password.Length <=10)
                    {
                        pas1 = true;
                        break;
                    }
                    if (pas1 == false)
                    {
                        MessageBox.Show("Поле пароль должно содержатm английские символы,цифры и иметь длину не более 10 символов");
                        break;
                    }
                }
                for (int i = 0; i < pass2.Password.Length; i++)//проверка подтверждения пароля
                {
                    if (pas2)
                    {
                        pas2 = false;
                    }
                    if (pass2.Password[i] == pass1.Password[i])
                    {
                        pas2 = true;
                        break;
                    }
                    if (pas2 == false)
                    {
                        MessageBox.Show("Пароль не совпадает или вы не ввели его");
                        break;
                    }
                }
                if (log && pas1 && pas2)
                {
                    DataTable uppass = Select("update [dbo].[Admins] set password_admin = '" + pass1.Password + "' where login_admin = '" + Login.Text + "'");
                    MessageBox.Show("Смена пароля для логина " + Login.Text + " выполнена успешно");
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
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
    }
}
