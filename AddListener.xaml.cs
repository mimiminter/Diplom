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
    /// Логика взаимодействия для AddListener.xaml
    /// </summary>
    public partial class AddListener : Window
    {
        public AddListener()
        {
            InitializeComponent();
            SqlConnection connection = new SqlConnection("server=WIN-NHF22QP2E4K\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
            connection.Open();
            string cmd = "select Courses.id as 'Код', Competence.name_competce as 'Компетенция',timetable.[day] as 'День проведения занятия', timetable.time_1 as 'Время начала', timetable.time_2 as 'Время окончания',Courses.date_start as 'Дата начала курса', Courses.date_end as 'Дата окончания курса' from Courses,Competence,timetable where Courses.id_competence = Competence.id and Courses.id_time = timetable.id \r\n";
            SqlCommand createcommand = new SqlCommand(cmd, connection);
            createcommand.ExecuteNonQuery();
            SqlDataAdapter sql = new SqlDataAdapter(createcommand);
            DataTable dt_courses = new DataTable("courses");
            sql.Fill(dt_courses);
            datagrid_courses.ItemsSource = dt_courses.DefaultView;
            connection.Close();
        }
    }
}
