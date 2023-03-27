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
using System.Diagnostics;
using System.Xml;

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
            SqlConnection connection = new SqlConnection("server=localhost\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
            connection.Open();
            string cmd = "select Listeners.id as 'Код', Persons.surname as 'Фамилия', Persons.[name] as 'Имя', Persons.patronymic as 'Отчество', Persons.id_sex as 'Код пола', sex.sex_name as 'Пол',Listeners.birthday as 'Дата рождения',  Courses.id as 'Код курса',Competence.id as 'Код компетенции',Competence.name_competce as 'Компетенция', Listeners.email as 'Почта', Listeners.phone_number as 'Телефон',Listeners.series_passport as 'Серия паспорта', Listeners.number_passport as 'Номер паспорта',Educations.id as 'Код образования',Educations.[name] as 'Образование', Listeners.login_listener as 'Логин',Listeners.password_listener as 'Пароль' from Listeners,Courses,Persons,sex,Educations,Competence where Listeners.id_education = Educations.id and Listeners.id_person = Persons.id and Listeners.id_course = Courses.id and Courses.id_competence = Competence.id and Persons.id_sex = sex.id";
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
                listeners.id_sex = Convert.ToInt32(row[4]);
                listeners.sex = Convert.ToString((string)row[5]);
                listeners.birthday = Convert.ToDateTime(row[6]);
                listeners.id_course = Convert.ToInt32(row[7]);
                listeners.id_competence = Convert.ToInt32(row[8]);
                listeners.competence = Convert.ToString((string)(row[9]));
                listeners.email = Convert.ToString((string)(row[10]));
                listeners.number = Convert.ToString((string)(row[11]));
                listeners.series = Convert.ToString((string)(row[12]));
                listeners.number_pass = Convert.ToString((string)(row[13]));
                listeners.id_education = Convert.ToInt32(row[14]);
                listeners.education = Convert.ToString((string)(row[15]));
                listeners.login = Convert.ToString((string)(row[16]));
                listeners.password = Convert.ToString((string)(row[17]));
                listeners_list.Add(listeners);
            }
            data_grid_listeners.ItemsSource = listeners_list;
            //data_grid_listeners.ItemsSource = dt_listeners.DefaultView;

            string cmd1 = "select Courses.id as 'Код курса', Courses.id_competence as 'Код компетенции',Competence.name_competce as 'Название компетенции',Courses.id_time as 'Код времени',timetable.day as 'День проведения занятия', timetable.time_1 as 'Время начала',timetable.time_2 as 'Время окончания',Courses.date_start as 'Дата начала курса',Courses.date_end as 'Дата окончания курса' from Courses,Competence,timetable where Courses.id_competence = Competence.id and Courses.id_time = timetable.id\r\n";
            SqlCommand createcommand1 = new SqlCommand(cmd1, connection);
            createcommand1.ExecuteNonQuery();
            SqlDataAdapter sql1 = new SqlDataAdapter(createcommand1);
            DataTable dt_courses = new DataTable("courses");
            sql1.Fill(dt_courses);
            List<Courses> courses_list = new List<Courses>();
            foreach (DataRow row1 in dt_courses.Rows)
            {
                Courses course = new Courses();
                course.id = Convert.ToInt32(row1[0]);
                course.id_competence = Convert.ToInt32(row1[1]);
                course.competence = Convert.ToString((string)row1[2]);
                course.time = Convert.ToInt32(row1[3]);
                course.day = Convert.ToString((string)row1[4]);
                course.time_1 = Convert.ToString(row1[5]);
                course.time_2 = Convert.ToString(row1[6]);
                course.date_1 = Convert.ToDateTime(row1[7]);
                course.date_2 = Convert.ToDateTime(row1[8]);
                courses_list.Add(course);
            }
            data_grid_courses.ItemsSource = courses_list;
            //data_grid_courses.ItemsSource = dt_courses.DefaultView;

            string cmd2 = "select Competence.id as 'Код компетенции',Competence.name_competce as 'Наименование компетенции',TypeOfTraining.id as 'Код типа обучения', TypeOfTraining.name_type as 'Наименование типа обучения' from Competence,TypeOfTraining where Competence.id_type_of_training = TypeOfTraining.id";
            SqlCommand createcommand2 = new SqlCommand(cmd2, connection);
            createcommand2.ExecuteNonQuery();
            SqlDataAdapter sql2 = new SqlDataAdapter(createcommand2);
            DataTable dt_competence = new DataTable("competence");
            sql2.Fill(dt_competence);
            List<Competence> competence_list = new List<Competence>();
            foreach (DataRow row2 in dt_competence.Rows)
            {
                Competence competence = new Competence();
                competence.id = Convert.ToInt32(row2[0]);
                competence.name = Convert.ToString((string)(row2[1]));
                competence.id_type = Convert.ToInt32(row2[2]);
                competence.name_type = Convert.ToString((string)(row2[3]));
                competence_list.Add(competence);
            }
            data_grid_competence.ItemsSource = competence_list;
            connection.Close();
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

        private void Button_Click_Search(object sender, RoutedEventArgs e)
        {
            if (search_listener.Text != null)
            {
                SqlConnection connection = new SqlConnection("server=localhost\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
                connection.Open();
                string cmd = "select Listeners.id as 'Код', Persons.surname as 'Фамилия', Persons.[name] as 'Имя', Persons.patronymic as 'Отчество', Persons.id_sex as 'Код пола', sex.sex_name as 'Пол', Listeners.birthday as 'Дата рождения'," +
                    "Courses.id as 'Код курса',Competence.id as 'Код компетенции',Competence.name_competce as 'Компетенция', Listeners.email as 'Почта', Listeners.phone_number as 'Телефон'," +
                    "Listeners.series_passport as 'Серия паспорта', Listeners.number_passport as 'Номер паспорта',Educations.id as 'Код образования',Educations.[name] as 'Образование', " +
                    "Listeners.login_listener as 'Логин',Listeners.password_listener as 'Пароль' from Listeners,Courses,Persons,Educations,Competence,sex where Listeners.id_education = Educations.id" +
                    " and Listeners.id_person = Persons.id and Listeners.id_course = Courses.id and Courses.id_competence = Competence.id and Persons.id_sex = sex.id " +
                    "and (Listeners.id like '%" + search_listener.Text + "%' or Persons.surname like '%" + search_listener.Text + "%' or Persons.[name] like '%" + search_listener.Text + "%' or " +
                    "Persons.patronymic like '%" + search_listener.Text + "%' or Listeners.birthday like '%" + search_listener.Text + "%' or Courses.id like '%" + search_listener.Text + "%' or " +
                    "Competence.id like '%" + search_listener.Text + "%' or Competence.name_competce like '%" + search_listener.Text + "%' or Listeners.email like '%" + search_listener.Text + "%' or " +
                    "Listeners.phone_number like '%" + search_listener.Text + "%' or Listeners.series_passport like '%" + search_listener.Text + "%' or Listeners.number_passport like '%" + search_listener.Text + "%' or " +
                    "Educations.id like '%" + search_listener.Text + "%' or Educations.[name] like '%" + search_listener.Text + "%' or Listeners.login_listener like '%" + search_listener.Text + "%' or " +
                    "Listeners.password_listener like '%" + search_listener.Text + "%' or sex.id like '%" + search_listener.Text + "%' or sex.sex_name like '%" + search_listener.Text + "%' )";
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
                    listeners.id_sex = Convert.ToInt32(row[4]);
                    listeners.sex = Convert.ToString((string)row[5]);
                    listeners.birthday = Convert.ToDateTime(row[6]);
                    listeners.id_course = Convert.ToInt32(row[7]);
                    listeners.id_competence = Convert.ToInt32(row[8]);
                    listeners.competence = Convert.ToString((string)(row[9]));
                    listeners.email = Convert.ToString((string)(row[10]));
                    listeners.number = Convert.ToString((string)(row[11]));
                    listeners.series = Convert.ToString((string)(row[12]));
                    listeners.number_pass = Convert.ToString((string)(row[13]));
                    listeners.id_education = Convert.ToInt32(row[14]);
                    listeners.education = Convert.ToString((string)(row[15]));
                    listeners.login = Convert.ToString((string)(row[16]));
                    listeners.password = Convert.ToString((string)(row[17]));
                    listeners_list.Add(listeners);
                }
                data_grid_listeners.ItemsSource = listeners_list.ToList();
                //data_grid_listeners.ItemsSource = dt.DefaultView;
                connection.Close();
            }
            else if (search_listener.Text == null)
            {
                SqlConnection connection = new SqlConnection("server=localhost\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
                connection.Open();
                string cmd = "select Listeners.id as 'Код', Persons.surname as 'Фамилия', Persons.[name] as 'Имя', Persons.patronymic as 'Отчество', Persons.id_sex as 'Код пола', sex.sex_name as 'Пол', Listeners.birthday as 'Дата рождения',  Courses.id as 'Код курса',Competence.id as 'Код компетенции',Competence.name_competce as 'Компетенция', Listeners.email as 'Почта', Listeners.phone_number as 'Телефон',Listeners.series_passport as 'Серия паспорта', Listeners.number_passport as 'Номер паспорта',Educations.id as 'Код образования',Educations.[name] as 'Образование', Listeners.login_listener as 'Логин',Listeners.password_listener as 'Пароль' from Listeners,Courses,Persons,Educations,Competence,sex where Listeners.id_education = Educations.id and Listeners.id_person = Persons.id and Listeners.id_course = Courses.id and Courses.id_competence = Competence.id and Persons.id_sex = sex.id";
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
                    listeners.id_sex = Convert.ToInt32(row[4]);
                    listeners.sex = Convert.ToString((string)row[5]);
                    listeners.birthday = Convert.ToDateTime(row[6]);
                    listeners.id_course = Convert.ToInt32(row[7]);
                    listeners.id_competence = Convert.ToInt32(row[8]);
                    listeners.competence = Convert.ToString((string)(row[9]));
                    listeners.email = Convert.ToString((string)(row[10]));
                    listeners.number = Convert.ToString((string)(row[11]));
                    listeners.series = Convert.ToString((string)(row[12]));
                    listeners.number_pass = Convert.ToString((string)(row[13]));
                    listeners.id_education = Convert.ToInt32(row[14]);
                    listeners.education = Convert.ToString((string)(row[15]));
                    listeners.login = Convert.ToString((string)(row[16]));
                    listeners.password = Convert.ToString((string)(row[17]));
                    listeners_list.Add(listeners);
                }
                data_grid_listeners.ItemsSource = listeners_list.ToList();
                //data_grid_listeners.ItemsSource = dt.DefaultView;
                connection.Close();
            }
        }
        private void Button_Click_Add_Listener(object sender, RoutedEventArgs e)
        {
            AddListener add = new AddListener();
            add.Show();
            Close();
        }

        private void Button_Click_Update_Listener(object sender, RoutedEventArgs e)
        {
            if (data_grid_listeners.SelectedItem != null)
            {
                Listeners selectedListeners = data_grid_listeners.SelectedItem as Listeners;
                UpdateListener updateListener = new UpdateListener(selectedListeners.id);
                updateListener.Show();
                Close();
            }
            else if (data_grid_listeners.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали строку для изменения");
            }
        }
        private void Button_Click_Excel_Listener(object sender, RoutedEventArgs e)
        {
            //Process.Start(@"");// доделать
        }

        private void Button_Click_Sort_Listener(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            SqlConnection connection = new SqlConnection("server=localhost\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
            connection.Open();
            string cmd = "select count(*) as 'Количество слушателей' from Listeners";
            SqlCommand createcommand = new SqlCommand(cmd, connection);
            createcommand.ExecuteNonQuery();
            SqlDataAdapter sql = new SqlDataAdapter(createcommand);
            DataTable dt_sort = new DataTable("sort");
            sql.Fill(dt_sort);
            data_grid_listeners.ItemsSource = dt_sort.DefaultView;
            connection.Close();
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
            SqlConnection connection = new SqlConnection("server=localhost\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
            connection.Open();
            string cmd = "select Listeners.id as 'Код', Persons.surname as 'Фамилия', Persons.[name] as 'Имя', Persons.patronymic as 'Отчество', Persons.id_sex as 'Код пола', sex.sex_name as 'Пол', Listeners.birthday as 'Дата рождения',  Courses.id as 'Код курса',Competence.id as 'Код компетенции',Competence.name_competce as 'Компетенция', Listeners.email as 'Почта', Listeners.phone_number as 'Телефон',Listeners.series_passport as 'Серия паспорта', Listeners.number_passport as 'Номер паспорта',Educations.id as 'Код образования',Educations.[name] as 'Образование', Listeners.login_listener as 'Логин',Listeners.password_listener as 'Пароль' from Listeners,Courses,Persons,Educations,Competence,sex where Listeners.id_education = Educations.id and Listeners.id_person = Persons.id and Listeners.id_course = Courses.id and Courses.id_competence = Competence.id and Persons.id_sex = sex.id";
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
                listeners.id_sex = Convert.ToInt32(row[4]);
                listeners.sex = Convert.ToString((string)row[5]);
                listeners.birthday = Convert.ToDateTime(row[6]);
                listeners.id_course = Convert.ToInt32(row[7]);
                listeners.id_competence = Convert.ToInt32(row[8]);
                listeners.competence = Convert.ToString((string)(row[9]));
                listeners.email = Convert.ToString((string)(row[10]));
                listeners.number = Convert.ToString((string)(row[11]));
                listeners.series = Convert.ToString((string)(row[12]));
                listeners.number_pass = Convert.ToString((string)(row[13]));
                listeners.id_education = Convert.ToInt32(row[14]);
                listeners.education = Convert.ToString((string)(row[15]));
                listeners.login = Convert.ToString((string)(row[16]));
                listeners.password = Convert.ToString((string)(row[17]));
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
                    DataTable listeners_delete = Select("Delete Listeners where id =" + listeners.id);
                    DataTable persons_delete = Select("Delete Persons where id =" + listeners.id);
                    MessageBox.Show("Слушатель с кодом " + listeners.id + " удален");
                }
            }
            else if (data_grid_listeners.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали строку для удаления");
            }
        }

        private void Button_Click_Search_Courses(object sender, RoutedEventArgs e)
        {
            if(search_courses.Text !=null)
            {
                SqlConnection connection = new SqlConnection("server=localhost\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
                connection.Open();
                string cmd1 = "select Courses.id as 'Код курса', Courses.id_competence as 'Код компетенции',Competence.name_competce as 'Название компетенции',Courses.id_time as 'Код времени',timetable.day as 'День проведения занятия', timetable.time_1 as 'Время начала',timetable.time_2 as 'Время окончания',Courses.date_start as 'Дата начала курса',Courses.date_end as 'Дата окончания курса' from Courses,Competence,timetable where Courses.id_competence = Competence.id and Courses.id_time = timetable.id " + " and ( Courses.id like '%" + search_courses.Text + "%' or Courses.id_competence like '%" + search_courses.Text + "%' or Competence.name_competce like '%" + search_courses.Text + "%' or Courses.id_time like '%" + search_courses.Text + "%' or timetable.day like '%" + search_courses.Text + "%' or timetable.time_1 like '%" + search_courses.Text + "%' or timetable.time_2 like '%" + search_courses.Text + "%' or Courses.date_start like '%" + search_courses.Text  + "%' or Courses.date_end like '%" + search_courses.Text + "%')"; 
                SqlCommand createcommand1 = new SqlCommand(cmd1, connection);
                createcommand1.ExecuteNonQuery();
                SqlDataAdapter sql1 = new SqlDataAdapter(createcommand1);
                DataTable dt_courses = new DataTable("courses");
                sql1.Fill(dt_courses);
                List<Courses> courses_list = new List<Courses>();
                foreach (DataRow row1 in dt_courses.Rows)
                {
                    Courses course = new Courses();
                    course.id = Convert.ToInt32(row1[0]);
                    course.id_competence = Convert.ToInt32(row1[1]);
                    course.competence = Convert.ToString((string)row1[2]);
                    course.time = Convert.ToInt32(row1[3]);
                    course.day = Convert.ToString((string)row1[4]);
                    course.time_1 = Convert.ToString(row1[5]);
                    course.time_2 = Convert.ToString(row1[6]);
                    course.date_1 = Convert.ToDateTime(row1[7]);
                    course.date_2 = Convert.ToDateTime(row1[8]);
                    courses_list.Add(course);
                }
                data_grid_courses.ItemsSource = courses_list;
                //data_grid_courses.ItemsSource = dt_courses.DefaultView;
                connection.Close();
            }
            else if (search_courses.Text == null)
            {
                SqlConnection connection = new SqlConnection("server=localhost\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
                connection.Open();
                string cmd1 = "select Courses.id as 'Код курса', Courses.id_competence as 'Код компетенции',Competence.name_competce as 'Название компетенции',Courses.id_time as 'Код времени',timetable.day as 'День проведения занятия', timetable.time_1 as 'Время начала',timetable.time_2 as 'Время окончания',Courses.date_start as 'Дата начала курса',Courses.date_end as 'Дата окончания курса' from Courses,Competence,timetable where Courses.id_competence = Competence.id and Courses.id_time = timetable.id\r\n";
                SqlCommand createcommand1 = new SqlCommand(cmd1, connection);
                createcommand1.ExecuteNonQuery();
                SqlDataAdapter sql1 = new SqlDataAdapter(createcommand1);
                DataTable dt_courses = new DataTable("courses");
                sql1.Fill(dt_courses);
                List<Courses> courses_list = new List<Courses>();
                foreach (DataRow row1 in dt_courses.Rows)
                {
                    Courses course = new Courses();
                    course.id = Convert.ToInt32(row1[0]);
                    course.id_competence = Convert.ToInt32(row1[1]);
                    course.competence = Convert.ToString((string)row1[2]);
                    course.time = Convert.ToInt32(row1[3]);
                    course.day = Convert.ToString((string)row1[4]);
                    course.time_1 = Convert.ToString(row1[5]);
                    course.time_2 = Convert.ToString(row1[6]);
                    course.date_1 = Convert.ToDateTime(row1[7]);
                    course.date_2 = Convert.ToDateTime(row1[8]);
                    courses_list.Add(course);
                }
                data_grid_courses.ItemsSource = courses_list;
                //data_grid_courses.ItemsSource = dt_courses.DefaultView;
                connection.Close();
            }
        }

        private void Button_Click_Add_Courses(object sender, RoutedEventArgs e)
        {
            AddCourse addCourse = new AddCourse();
            addCourse.Show();
            Close();
        }

        private void Button_Click_Update_Courses(object sender, RoutedEventArgs e)
        {
            if (data_grid_courses.SelectedItem != null)
            {
                Courses courses = data_grid_courses.SelectedItem as Courses;
                UpdateCourses updatecourses = new UpdateCourses(courses.id);
                updatecourses.Show();
                Close();
            }
            else if (data_grid_listeners.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали строку для изменения");
            }
        }

        private void Button_Click_Delete_Courses(object sender, RoutedEventArgs e)
        {
            if (data_grid_courses.SelectedItem != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить  данные?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Courses courses = data_grid_courses.SelectedItem as Courses;
                    DataTable listeners_delete = Select("Delete Courses where id =" + courses.id);
                    MessageBox.Show("Курс с кодом " + courses.id + " удален");
                }
            }
            else if (data_grid_listeners.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали строку для удаления");
            }
        }

        private void Button_Click_Excel_Courses(object sender, RoutedEventArgs e)
        {
            //Process.Start(@"");
        }

        private void Button_Click_Sort_Courses(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            SqlConnection connection = new SqlConnection("server=localhost\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
            connection.Open();
            string cmd = "select id_course as 'Код курса',COUNT(*) as 'Количество человек на курсе' from Listeners group by id_course";
            SqlCommand createcommand = new SqlCommand(cmd, connection);
            createcommand.ExecuteNonQuery();
            SqlDataAdapter sql = new SqlDataAdapter(createcommand);
            DataTable dt_sort = new DataTable("sort");
            sql.Fill(dt_sort);
            data_grid_courses.ItemsSource = dt_sort.DefaultView;
            connection.Close();
        }

        private void Button_Click_Update_Table_Courses(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=localhost\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
            connection.Open();
            string cmd1 = "select Courses.id as 'Код курса', Courses.id_competence as 'Код компетенции',Competence.name_competce as 'Название компетенции',Courses.id_time as 'Код времени',timetable.day as 'День проведения занятия', timetable.time_1 as 'Время начала',timetable.time_2 as 'Время окончания',Courses.date_start as 'Дата начала курса',Courses.date_end as 'Дата окончания курса' from Courses,Competence,timetable where Courses.id_competence = Competence.id and Courses.id_time = timetable.id\r\n";
            SqlCommand createcommand1 = new SqlCommand(cmd1, connection);
            createcommand1.ExecuteNonQuery();
            SqlDataAdapter sql1 = new SqlDataAdapter(createcommand1);
            DataTable dt_courses = new DataTable("courses");
            sql1.Fill(dt_courses);
            List<Courses> courses_list = new List<Courses>();
            foreach (DataRow row1 in dt_courses.Rows)
            {
                Courses course = new Courses();
                course.id = Convert.ToInt32(row1[0]);
                course.id_competence = Convert.ToInt32(row1[1]);
                course.competence = Convert.ToString((string)row1[2]);
                course.time = Convert.ToInt32(row1[3]);
                course.day = Convert.ToString((string)row1[4]);
                course.time_1 = Convert.ToString(row1[5]);
                course.time_2 = Convert.ToString(row1[6]);
                course.date_1 = Convert.ToDateTime(row1[7]);
                course.date_2 = Convert.ToDateTime(row1[8]);
                courses_list.Add(course);
            }
            data_grid_courses.ItemsSource = courses_list;
            //data_grid_courses.ItemsSource = dt_courses.DefaultView;
            connection.Close();
        }

        private void Button_Click_Search_Competence(object sender, RoutedEventArgs e)
        {
            if(search_competence.Text !=null)
            {
                SqlConnection connection = new SqlConnection("server=localhost\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
                connection.Open();
                string cmd2 = $"select Competence.id as 'Код компетенции',Competence.name_competce as 'Наименование компетенции',TypeOfTraining.id as 'Код типа обучения', TypeOfTraining.name_type as 'Наименование типа обучения' from Competence,TypeOfTraining where Competence.id_type_of_training = TypeOfTraining.id and ( Competence.id like '%{search_competence.Text}%' or Competence.name_competce like '%{search_competence.Text}%' or TypeOfTraining.id like '%{search_competence.Text}%' or TypeOfTraining.name_type like '%{search_competence.Text}%' )";
                SqlCommand createcommand2 = new SqlCommand(cmd2, connection);
                createcommand2.ExecuteNonQuery();
                SqlDataAdapter sql2 = new SqlDataAdapter(createcommand2);
                DataTable dt_competence = new DataTable("competence");
                sql2.Fill(dt_competence);
                List<Competence> competence_list = new List<Competence>();
                foreach (DataRow row2 in dt_competence.Rows)
                {
                    Competence competence = new Competence();
                    competence.id = Convert.ToInt32(row2[0]);
                    competence.name = Convert.ToString((string)(row2[1]));
                    competence.id_type = Convert.ToInt32(row2[2]);
                    competence.name_type = Convert.ToString((string)(row2[3]));
                    competence_list.Add(competence);
                }
                data_grid_competence.ItemsSource = competence_list;
                connection.Close();
            }
            else if (search_competence.Text== null)
            {
                SqlConnection connection = new SqlConnection("server=localhost\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
                connection.Open();
                string cmd2 = "select Competence.id as 'Код компетенции',Competence.name_competce as 'Наименование компетенции',TypeOfTraining.id as 'Код типа обучения', TypeOfTraining.name_type as 'Наименование типа обучения' from Competence,TypeOfTraining where Competence.id_type_of_training = TypeOfTraining.id";
                SqlCommand createcommand2 = new SqlCommand(cmd2, connection);
                createcommand2.ExecuteNonQuery();
                SqlDataAdapter sql2 = new SqlDataAdapter(createcommand2);
                DataTable dt_competence = new DataTable("competence");
                sql2.Fill(dt_competence);
                List<Competence> competence_list = new List<Competence>();
                foreach (DataRow row2 in dt_competence.Rows)
                {
                    Competence competence = new Competence();
                    competence.id = Convert.ToInt32(row2[0]);
                    competence.name = Convert.ToString((string)(row2[1]));
                    competence.id_type = Convert.ToInt32(row2[2]);
                    competence.name_type = Convert.ToString((string)(row2[3]));
                    competence_list.Add(competence);
                }
                data_grid_competence.ItemsSource = competence_list;
                connection.Close();
            }
        }

        private void Button_Click_Add_Competence(object sender, RoutedEventArgs e)
        {
            AddCompetence addCompetence = new AddCompetence();
            addCompetence.Show();
            Close();
        }

        private void Button_Click_Update_Competence(object sender, RoutedEventArgs e)
        {
            if (data_grid_competence.SelectedItem != null)
            {
                Competence competence = data_grid_competence.SelectedItem as Competence;
                UpdateCompetence updateCompetence = new UpdateCompetence(competence.id);
                updateCompetence.Show();
                Close();
            }
            else if (data_grid_competence.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали строку для изменения");
            }
        }

        private void Button_Click_Delete_Competence(object sender, RoutedEventArgs e)
        {
            if (data_grid_competence.SelectedItem != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить  данные?", "Удаление", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Competence competence = data_grid_competence.SelectedItem as Competence;
                    DataTable competence_delete = Select($"Delete Competence where id = {competence.id} ");
                    MessageBox.Show("Компетенция с кодом " + competence.id + " удалена");
                }
            }
            else if (data_grid_competence.SelectedItem == null)
            {
                MessageBox.Show("Вы не выбрали строку для удаления");
            }
        }

        private void Button_Click_Excel_Competence(object sender, RoutedEventArgs e)
        {
            //Process.Start(@"");
        }

        private void Button_Click_Sort_Competence(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=localhost\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
            connection.Open();
            string cmd2 = "select TypeOfTraining.name_type as 'Тип обучения', COUNT(Competence.id_type_of_training) as 'Количество компетенций с данным типом обучения'  from TypeOfTraining,Competence where Competence.id_type_of_training = TypeOfTraining.id group by TypeOfTraining.name_type";
            SqlCommand createcommand2 = new SqlCommand(cmd2, connection);
            createcommand2.ExecuteNonQuery();
            SqlDataAdapter sql2 = new SqlDataAdapter(createcommand2);
            DataTable dt_competence = new DataTable("competence");
            sql2.Fill(dt_competence);
            data_grid_competence.ItemsSource = dt_competence.DefaultView;
            connection.Close();
        }

        private void Button_Click_Update_Table_Competence(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection("server=localhost\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
            connection.Open();
            string cmd2 = "select Competence.id as 'Код компетенции',Competence.name_competce as 'Наименование компетенции',TypeOfTraining.id as 'Код типа обучения', TypeOfTraining.name_type as 'Наименование типа обучения' from Competence,TypeOfTraining where Competence.id_type_of_training = TypeOfTraining.id";
            SqlCommand createcommand2 = new SqlCommand(cmd2, connection);
            createcommand2.ExecuteNonQuery();
            SqlDataAdapter sql2 = new SqlDataAdapter(createcommand2);
            DataTable dt_competence = new DataTable("competence");
            sql2.Fill(dt_competence);
            List<Competence> competence_list = new List<Competence>();
            foreach (DataRow row2 in dt_competence.Rows)
            {
                Competence competence = new Competence();
                competence.id = Convert.ToInt32(row2[0]);
                competence.name = Convert.ToString((string)(row2[1]));
                competence.id_type = Convert.ToInt32(row2[2]);
                competence.name_type = Convert.ToString((string)(row2[3]));
                competence_list.Add(competence);
            }
            data_grid_competence.ItemsSource = competence_list;
            connection.Close();
        }
    }
}
