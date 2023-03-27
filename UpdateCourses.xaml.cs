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
    /// Логика взаимодействия для UpdateCourses.xaml
    /// </summary>
    public partial class UpdateCourses : Window
    {
        public UpdateCourses()
        {
            InitializeComponent();
        }
        public UpdateCourses(int id)
        {
            InitializeComponent();
            SqlConnection connection = new("server = localhost\\SQLEXPRESS;Trusted_Connection=YES;DataBase=bot;");
            connection.Open();
            string cmd = "select Competence.id as 'Код компетенции', Competence.name_competce as 'Название',TypeOfTraining.name_type as 'Тип обучения' from Competence,TypeOfTraining where Competence.id_type_of_training = TypeOfTraining.id";
            string cmd1 = "select id as 'Код', [day] as 'День недели', time_1 as 'Время начала занятия',time_2 as 'Время окончания занятия' from timetable \r\n";
            SqlCommand createcommand = new SqlCommand(cmd, connection);
            SqlCommand createcommand1 = new SqlCommand(cmd1, connection);
            createcommand.ExecuteNonQuery();
            createcommand1.ExecuteNonQuery();
            SqlDataAdapter sql = new SqlDataAdapter(createcommand);
            SqlDataAdapter sql1 = new SqlDataAdapter(createcommand1);
            DataTable dt_competence = new DataTable("competence");
            DataTable dt_time = new DataTable("timetable");
            sql.Fill(dt_competence);
            sql1.Fill(dt_time);
            datagrid_competence.ItemsSource = dt_competence.DefaultView;
            datagrid_times.ItemsSource = dt_time.DefaultView;
            string cmd2 = $"select Courses.id as 'Код курса', Courses.id_competence as 'Код компетенции',Courses.id_time as 'Код времени',Courses.date_start as 'Дата начала курса',Courses.date_end as 'Дата окончания курса' from Courses,Competence,timetable where Courses.id_competence = Competence.id and Courses.id_time = timetable.id and Courses.id = {id}";
            SqlCommand createcommand2 = new SqlCommand(cmd2, connection);
            createcommand2.ExecuteNonQuery();
            SqlDataAdapter sql2 = new SqlDataAdapter(createcommand2);
            DataTable dt_courses = new DataTable("courses");
            sql2.Fill(dt_courses);
            DataRow row = dt_courses.Rows[0];
            Courses courses = new Courses();
            courses.id = Convert.ToInt32(row[0]);
            courses.id_competence = Convert.ToInt32(row[1]);
            courses.time= Convert.ToInt32(row[2]);
            courses.date_1 = Convert.ToDateTime(row[3]);
            courses.date_2 = Convert.ToDateTime(row[4]);
            id_tb.Text = courses.id.ToString();
            id_competence_tb.Text = courses.id_competence.ToString();
            id_time_tb.Text = courses.time.ToString();
            date_start_tb.Text = courses.date_1.ToString();
            date_end_tb.Text = courses.date_2.ToString();
            connection.Close();
        }
        public static DataTable Select(string selectSQL)
        {
            DataTable dataTable = new("dataBase");
            SqlConnection sqlConnection = new("server = localhost\\SQLEXPRESS;Trusted_Connection=YES;DataBase=bot;");
            sqlConnection.Open();
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandText = selectSQL;
            SqlDataAdapter sqlDataAdapter = new(sqlCommand);
            sqlDataAdapter.Fill(dataTable);
            sqlConnection.Close();
            return dataTable;
        }
        private void Button_Click_Update(object sender, RoutedEventArgs e)
        {
            if (id_tb.Text.Length != 0 && id_competence_tb.Text.Length != 0 && id_time_tb.Text.Length != 0 && date_start_tb.Text.Length != 0 && date_end_tb.Text.Length != 0)
            {
                bool id = false, id_competence = false, id_time = false, date1 = false, date2 = false;
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
                DataTable sel = Select("select * from Competence where id = " + id_competence_tb.Text);
                for (int i = 0; i < id_competence_tb.Text.Length; i++)
                {
                    if (id_competence)
                    {
                        id_competence = false;
                    }
                    if (id_competence_tb.Text[i] >= '1' && id_competence_tb.Text[i] <= '9')
                    {
                        id_competence = true;
                        break;
                    }
                    if (id_competence == false)
                    {
                        MessageBox.Show("Поле код компетенции должно содержать только цифры");
                        break;
                    }
                }
                if (sel.Rows.Count > 0)
                {
                    id_competence = true;
                }
                else if (sel.Rows.Count == 0)
                {
                    MessageBox.Show("Код компетенции, который вы ввели, нет в системе");
                }
                DataTable sel1 = Select("select * from timetable where id = " + id_time_tb.Text);
                for (int i = 0; i < id_time_tb.Text.Length; i++)
                {
                    if (id_time)
                    {
                        id_time = false;
                    }
                    if (id_time_tb.Text[i] >= '1' && id_time_tb.Text[i] <= '9')
                    {
                        id_time = true;
                        break;
                    }
                    if (id_time == false)
                    {
                        MessageBox.Show("Поле код времени занятия должно содержать только цифры");
                        break;
                    }
                }
                if (sel1.Rows.Count > 0)
                {
                    id_time = true;
                }
                else if (sel1.Rows.Count == 0)
                {
                    MessageBox.Show("Код времени занятия, который вы ввели, нет в системе");
                }
                for (int i = 0; i < date_start_tb.Text.Length; i++)
                {
                    if (date1)
                    {
                        date1 = false;
                    }
                    if (date_start_tb.Text.Length != 0)
                    {
                        date1 = true;
                        break;
                    }
                    if (date1 == false)
                    {
                        MessageBox.Show("Поле дата начала курса должно быть заполнено");
                        break;
                    }
                }
                for (int i = 0; i < date_end_tb.Text.Length; i++)
                {
                    if (date2)
                    {
                        date2 = false;
                    }
                    if (date_end_tb.Text.Length != 0)
                    {
                        date2 = true;
                        break;
                    }
                    if (date2 == false)
                    {
                        MessageBox.Show("Поле дата окончания курса должно быть заполнено");
                        break;
                    }
                }
                if (id && id_competence && id_time && date1 && date2)
                {
                    DataTable add_course = Select($"update Courses set id_competence = {id_competence_tb.Text}, id_time = {id_time_tb.Text},date_start = '{date_start_tb.Text}',date_end = '{date_end_tb.Text}' where id = {id_tb.Text}");
                    MessageBox.Show($"Курс c кодом {id_tb.Text} изменен");
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
