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
    /// Логика взаимодействия для UpdateListener.xaml
    /// </summary>
    public partial class UpdateListener : Window
    {
        List<string> Alphabetrus = new List<string> {
            "А", "а", "Б", "б", "В", "в", "Г", "г", "Д", "д", "Е", "е", "Ё", "ё", "Ж", "ж", "З", "з", "И", "и", "Й", "й",
            "К", "к", "Л", "л", "М", "м", "Н", "н", "О", "о", "П", "п", "Р", "р", "С", "с", "Т", "т", "У", "у", "Ф", "ф",
            "Х", "х", "Ц", "ц", "Ч", "ч", "Ш", "ш", "Щ", "щ", "Ъ", "ъ", "Ы", "ы", "Ь", "ь", "Э", "э", "Ю", "ю", "Я", "я" };
        public UpdateListener()
        {
            InitializeComponent();
            SqlConnection connection = new SqlConnection("server=WIN-NHF22QP2E4K\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
            connection.Open();
            string cmd = "select Courses.id as 'Код курса', Competence.name_competce as 'Компетенция',timetable.[day] as 'День проведения занятия', timetable.time_1 as 'Время начала', timetable.time_2 as 'Время окончания',Courses.date_start as 'Дата начала курса', Courses.date_end as 'Дата окончания курса' from Courses,Competence,timetable where Courses.id_competence = Competence.id and Courses.id_time = timetable.id \r\n";
            string cmd1 = "select id as 'Код начального образования', name as 'Наименование образования' from Educations";
            SqlCommand createcommand = new SqlCommand(cmd, connection);
            SqlCommand createcommand1 = new SqlCommand(cmd1, connection);
            createcommand.ExecuteNonQuery();
            createcommand1.ExecuteNonQuery();
            SqlDataAdapter sql = new SqlDataAdapter(createcommand);
            SqlDataAdapter sql1 = new SqlDataAdapter(createcommand1);
            DataTable dt_courses = new DataTable("courses");
            DataTable dt_educations = new DataTable("educations");
            sql.Fill(dt_courses);
            sql1.Fill(dt_educations);
            datagrid_courses.ItemsSource = dt_courses.DefaultView;
            datagrid_educations.ItemsSource = dt_educations.DefaultView;
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

        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            AdminWindow adminWindow = new AdminWindow();
            adminWindow.Show();
            Close();
        }
    }
}
