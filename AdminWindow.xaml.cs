﻿using System;
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
using System.Diagnostics;

namespace TestProga
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow()
        {
            InitializeComponent();
            SqlConnection connection = new SqlConnection("server=WIN-NHF22QP2E4K\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
            connection.Open();
            string cmd = "select Listeners.id as 'Код', Persons.surname as 'Фамилия', Persons.[name] as 'Имя', Persons.patronymic as 'Отчество', Listeners.birthday as 'Дата рождения',  Courses.id as 'Код курса',Competence.id as 'Код компетенции',Competence.name_competce as 'Компетенция', Listeners.email as 'Почта', Listeners.phone_number as 'Телефон',Listeners.series_passport as 'Серия паспорта', Listeners.number_passport as 'Номер паспорта',Educations.id as 'Код образования',Educations.[name] as 'Образование', Listeners.login_listener as 'Логин',Listeners.password_listener as 'Пароль' from Listeners,Courses,Persons,Educations,Competence where Listeners.id_education = Educations.id and Listeners.id_person = Persons.id and Listeners.id_course = Courses.id and Courses.id_competence = Competence.id\r\n";
            SqlCommand createcommand = new SqlCommand(cmd, connection);
            createcommand.ExecuteNonQuery();
            SqlDataAdapter sql = new SqlDataAdapter(createcommand);
            DataTable dt_listeners = new DataTable("listeners");
            sql.Fill(dt_listeners);
            List<Listeners> listeners_list = new List<Listeners>();
            foreach (DataRow row in dt_listeners.Rows)
            {
                Listeners listeners = new Listeners();
                listeners.id = Convert.ToInt32(row[0]);
                listeners.fname = Convert.ToString((string)row[1]);
                listeners.mname = Convert.ToString((string)row[2]);
                listeners.lname = Convert.ToString((string)row[3]);
                listeners.birthday = Convert.ToDateTime(row[4]);
                listeners.id_course = Convert.ToInt32(row[5]);
                listeners.id_competence = Convert.ToInt32(row[6]);
                listeners.competence = Convert.ToString((string)(row[7]));
                listeners.email = Convert.ToString((string)(row[8]));
                listeners.number = Convert.ToString((string)(row[9]));
                listeners.series = Convert.ToString((string)(row[10]));
                listeners.number_pass = Convert.ToString((string)(row[11]));
                listeners.id_education = Convert.ToInt32(row[12]);
                listeners.education = Convert.ToString((string)(row[13]));
                listeners.login = Convert.ToString((string)(row[14]));
                listeners.password = Convert.ToString((string)(row[15]));
                listeners_list.Add(listeners);
            }
            data_grid_listeners.ItemsSource = listeners_list.ToList();
            //data_grid_listeners.ItemsSource = dt.DefaultView;
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

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click_Add_Listener(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Update_Listener(object sender, RoutedEventArgs e)
        {

        }

        private DataGrid GetData_grid_listeners()
        {
            return data_grid_listeners;
        }

     
        private void Button_Click_Excel_Listener(object sender, RoutedEventArgs e)
        {
            Process.Start(@"");// доделать
        }

        private void Button_Click_Diagram_Listener(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти ?", "Выход", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }

        }
        private void Button_Click_Update_Table(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            SqlConnection connection = new SqlConnection("server=WIN-NHF22QP2E4K\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
            connection.Open();
            string cmd = "select Listeners.id as 'Код', Persons.surname as 'Фамилия', Persons.[name] as 'Имя', Persons.patronymic as 'Отчество', Listeners.birthday as 'Дата рождения',  Courses.id as 'Код курса',Competence.id as 'Код компетенции',Competence.name_competce as 'Компетенция', Listeners.email as 'Почта', Listeners.phone_number as 'Телефон',Listeners.series_passport as 'Серия паспорта', Listeners.number_passport as 'Номер паспорта',Educations.id as 'Код образования',Educations.[name] as 'Образование', Listeners.login_listener as 'Логин',Listeners.password_listener as 'Пароль' from Listeners,Courses,Persons,Educations,Competence where Listeners.id_education = Educations.id and Listeners.id_person = Persons.id and Listeners.id_course = Courses.id and Courses.id_competence = Competence.id\r\n";
            SqlCommand createcommand = new SqlCommand(cmd, connection);
            createcommand.ExecuteNonQuery();
            SqlDataAdapter sql = new SqlDataAdapter(createcommand);
            DataTable dt_listeners = new DataTable("listeners");
            sql.Fill(dt_listeners);
            List<Listeners> listeners_list = new List<Listeners>();
            foreach (DataRow row in dt_listeners.Rows)
            {
                Listeners listeners = new Listeners();
                listeners.id = Convert.ToInt32(row[0]);
                listeners.fname = Convert.ToString((string)row[1]);
                listeners.mname = Convert.ToString((string)row[2]);
                listeners.lname = Convert.ToString((string)row[3]);
                listeners.birthday = Convert.ToDateTime(row[4]);
                listeners.id_course = Convert.ToInt32(row[5]);
                listeners.id_competence = Convert.ToInt32(row[6]);
                listeners.competence = Convert.ToString((string)(row[7]));
                listeners.email = Convert.ToString((string)(row[8]));
                listeners.number = Convert.ToString((string)(row[9]));
                listeners.series = Convert.ToString((string)(row[10]));
                listeners.number_pass = Convert.ToString((string)(row[11]));
                listeners.id_education = Convert.ToInt32(row[12]);
                listeners.education = Convert.ToString((string)(row[13]));
                listeners.login = Convert.ToString((string)(row[14]));
                listeners.password = Convert.ToString((string)(row[15]));
                listeners_list.Add(listeners);
            }
            data_grid_listeners.ItemsSource = listeners_list.ToList();
            //data_grid_listeners.ItemsSource = dt.DefaultView;
            connection.Close();
        }
        private void Button_Click_Delete_Listener(object sender, RoutedEventArgs e)
        {
            if (data_grid_listeners.SelectedItem != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить  данные?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Listeners listeners = data_grid_listeners.SelectedItem as Listeners;
                    DataTable sel = Select("Delete Listeners where id =" + listeners.id);
                    MessageBox.Show("Слушатель с кодом " + listeners.id + " удален");
                }
            }
            else if (data_grid_listeners.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали строку для удаления");
            }
        }
    }

}