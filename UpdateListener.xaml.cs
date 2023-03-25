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
        public UpdateListener(int id)
        {
            InitializeComponent();
            SqlConnection connection = new SqlConnection("server=WIN-NHF22QP2E4K\\SQLEXPRESS; Trusted_Connection=YES;DataBase=bot;");
            connection.Open();
            string cmd = "select Courses.id as 'Код курса', Competence.name_competce as 'Компетенция',timetable.[day] as 'День проведения занятия', timetable.time_1 as 'Время начала', timetable.time_2 as 'Время окончания',Courses.date_start as 'Дата начала курса', Courses.date_end as 'Дата окончания курса' from Courses,Competence,timetable where Courses.id_competence = Competence.id and Courses.id_time = timetable.id \r\n";
            string cmd1 = "select id as 'Код начального образования', name as 'Наименование образования' from Educations";
            string cmd2 = $"select Listeners.id as 'Код', Persons.surname as 'Фамилия', Persons.[name] as 'Имя', Persons.patronymic as 'Отчество', Sex.id as 'Код пола', Listeners.birthday as 'Дата рождения',  Courses.id as 'Код курса',Competence.id as 'Код компетенции',Competence.name_competce as 'Компетенция', Listeners.email as 'Почта', Listeners.phone_number as 'Телефон',Listeners.series_passport as 'Серия паспорта', Listeners.number_passport as 'Номер паспорта',Educations.id as 'Код образования',Educations.[name] as 'Образование', Listeners.login_listener as 'Логин',Listeners.password_listener as 'Пароль' from Listeners,Courses,Persons,Educations,Competence,sex where Listeners.id_education = Educations.id and Listeners.id_person = Persons.id and Listeners.id_course = Courses.id and Courses.id_competence = Competence.id and Persons.id_sex = sex.id and Listeners.id = {id}";
            string cmd3 = "select id as 'Код пола',sex_name as 'Наименование пола' from sex";
            SqlCommand createcommand = new SqlCommand(cmd, connection);
            SqlCommand createcommand1 = new SqlCommand(cmd1, connection);
            SqlCommand createcommand2 = new SqlCommand(cmd2, connection);
            SqlCommand createcommand3 = new SqlCommand(cmd3, connection);
            createcommand.ExecuteNonQuery();
            createcommand1.ExecuteNonQuery();
            createcommand2.ExecuteNonQuery();
            createcommand3.ExecuteNonQuery();
            SqlDataAdapter sql = new SqlDataAdapter(createcommand);
            SqlDataAdapter sql1 = new SqlDataAdapter(createcommand1);
            SqlDataAdapter sql2 = new SqlDataAdapter(createcommand2);
            SqlDataAdapter sql3 = new SqlDataAdapter(createcommand3);
            DataTable dt_courses = new DataTable("courses");
            DataTable dt_educations = new DataTable("educations");
            DataTable dt_listeners = new DataTable("listeners");
            DataTable dt_sex = new DataTable("sex");
            sql.Fill(dt_courses);
            sql1.Fill(dt_educations);
            sql2.Fill(dt_listeners);
            sql3.Fill(dt_sex);
            datagrid_courses.ItemsSource = dt_courses.DefaultView;
            datagrid_educations.ItemsSource = dt_educations.DefaultView;
            datagrid_sex.ItemsSource = dt_sex.DefaultView;
            DataRow row = dt_listeners.Rows[0];
            Listeners listeners = new Listeners();
            listeners.id = Convert.ToInt32(row[0]);
            listeners.fname = Convert.ToString((string)row[1]);
            listeners.mname = Convert.ToString((string)row[2]);
            listeners.lname = Convert.ToString((string)row[3]);
            listeners.id_sex= Convert.ToInt32(row[4]);
            listeners.birthday = Convert.ToDateTime(row[5]);
            listeners.id_course = Convert.ToInt32(row[6]);
            listeners.id_competence = Convert.ToInt32(row[7]);
            listeners.competence = Convert.ToString((string)(row[8]));
            listeners.email = Convert.ToString((string)(row[9]));
            listeners.number = Convert.ToString((string)(row[10]));
            listeners.series = Convert.ToString((string)(row[11]));
            listeners.number_pass = Convert.ToString((string)(row[12]));
            listeners.id_education = Convert.ToInt32(row[13]);
            listeners.education = Convert.ToString((string)(row[14]));
            listeners.login = Convert.ToString((string)(row[15]));
            listeners.password = Convert.ToString((string)(row[16]));
            id_tb.Text = listeners.id.ToString();
            surname_tb.Text = listeners.fname.ToString();
            name_tb.Text = listeners.mname.ToString();
            patronymic_tb.Text = listeners.lname.ToString();
            birthday_tb.Text = listeners.birthday.ToString();
            id_course_tb.Text = listeners.id_course.ToString();
            email_tb.Text = listeners.email.ToString();
            phone_tb.Text = listeners.number.ToString();
            series_passport_tb.Text = listeners.series.ToString();
            number_passport_tb.Text = listeners.number_pass.ToString();
            id_education_tb.Text = listeners.id_education.ToString();
            login_tb.Text = listeners.login.ToString();
            password_tb.Password = listeners.password.ToString();
            password1_tb.Password = listeners.password.ToString();
            sex_tb.Text = listeners.id_sex.ToString();
            connection.Close();
        }
        public UpdateListener()
        {
            InitializeComponent();
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
            if (sex_tb.Text.Length != 0 && role_tb.Text.Length != 0 && id_tb.Text.Length != 0 && surname_tb.Text.Length != 0 && name_tb.Text.Length != 0 && patronymic_tb.Text.Length != 0
               && id_course_tb.Text.Length != 0 && birthday_tb.Text.Length != 0 && email_tb.Text.Length != 0 && phone_tb.Text.Length != 0
               && series_passport_tb.Text.Length != 0 && number_passport_tb.Text.Length != 0 && id_education_tb.Text.Length != 0 && login_tb.Text.Length != 0 && password_tb.Password.Length != 0 && password1_tb.Password.Length != 0)
            {
                bool sex = false, role = false, id = false, surname = false, name = false, patronymic = false, id_course = false, birthday = false, email = false, phone = false, series = false, number_passport = false, id_education = false, login = false, password = false, password1 = false;
                for (int i = 0; i < id_tb.Text.Length; i++)
                {
                    if (id)
                    {
                        id = false;
                    }
                    if (id_tb.Text[i] >= '1' && id_tb.Text[i] <= '9' && id_tb.Text[0] == '2')
                    {
                        id = true;
                        break;
                    }
                    if (id == false)
                    {
                        MessageBox.Show("Поле код должно начинаться с цифры 2");
                        break;
                    }
                }
                for (int i = 0; i < sex_tb.Text.Length; i++)
                {
                    if (sex)
                    {
                        sex = false;
                    }
                    if (sex_tb.Text[i] == '1' || sex_tb.Text[i] == '2')
                    {
                        sex = true;
                        break;
                    }
                    if (sex == false)
                    {
                        MessageBox.Show("Поле пол должно содердать 1- мужской 2- женский");
                        break;
                    }
                }
                for (int i = 0; i < role_tb.Text.Length; i++)
                {
                    if (role)
                    {
                        role = false;
                    }
                    if (role_tb.Text[i] >= '1' && role_tb.Text[i] <= '9' && role_tb.Text == "2")
                    {
                        role = true;
                        break;
                    }
                    if (role == false)
                    {
                        MessageBox.Show("Поле роль должно содержать цифру 2");
                        break;
                    }
                }
                for (int i = 0; i < surname_tb.Text.Length; i++)
                {
                    if (surname)
                    {
                        surname = false;
                    }
                    for (int j = 0; j < Alphabetrus.Count; j++)
                    {
                        if (Convert.ToString(surname_tb.Text[i]).Contains(Alphabetrus[j]))
                        {
                            surname = true;
                            break;
                        }
                    }
                    if (surname == false)
                    {
                        MessageBox.Show("Поле фамилия должно содержать только руссике символы");
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
                        MessageBox.Show("Поле имя должно содержать только русские символы");
                        break;
                    }
                }
                for (int i = 0; i < patronymic_tb.Text.Length; i++)
                {
                    if (patronymic)
                    {
                        patronymic = false;
                    }
                    for (int j = 0; j < Alphabetrus.Count; j++)
                    {
                        if (Convert.ToString(patronymic_tb.Text[i]).Contains(Alphabetrus[j]))
                        {
                            patronymic = true;
                            break;
                        }
                    }
                    if (patronymic == false)
                    {
                        MessageBox.Show("Поле отчество должно содержать только русские символы");
                        break;
                    }
                }
                DataTable data = Select("select * from Courses where id = " + id_course_tb.Text);
                for (int i = 0; i < id_course_tb.Text.Length; i++)
                {
                    if (id_course)
                    {
                        id_course = false;
                    }
                    if (id_tb.Text[i] >= '1' && id_tb.Text[i] <= '9')
                    {
                        id_course = true;
                        break;
                    }
                    if (id_course == false)
                    {
                        MessageBox.Show("Поле код курса должно содержать только цифры");
                        break;
                    }
                }
                if (data.Rows.Count > 0)
                {
                    id_course = true;
                }
                else if (data.Rows.Count == 0)
                {
                    id_course = false;
                    MessageBox.Show("Курса, который вы ввели, не существует");
                }
                for (int i = 0; i < birthday_tb.Text.Length; i++)
                {
                    if (birthday)
                    {
                        birthday = false;
                    }
                    if (birthday_tb.Text.Length != 0)
                    {
                        birthday = true;
                        break;
                    }
                    if (birthday == false)
                    {
                        MessageBox.Show("Поле дата рождения должно содержать только цифры");
                        break;
                    }
                }
                for (int i = 0; i < email_tb.Text.Length; i++)
                {
                    if (email)
                    {
                        email = false;
                    }
                    if (email_tb.Text.Length != 0 && email_tb.Text.Contains("@"))
                    {
                        email = true;
                        break;
                    }
                    if (email == false)
                    {
                        MessageBox.Show("Поле почта должно содержать @");
                        break;
                    }
                }
                for (int i = 0; i < phone_tb.Text.Length; i++)
                {
                    if (phone)
                    {
                        phone = false;
                    }
                    if (phone_tb.Text.Length == 12 && phone_tb.Text.Contains("+"))
                    {
                        phone = true;
                    }
                    if (phone == false)
                    {
                        MessageBox.Show("Поле номер телефона должна начинаться с + и иметь 12 символов");
                        break;
                    }
                }
                for (int i = 0; i < series_passport_tb.Text.Length; i++)
                {
                    if (series)
                    {
                        series = false;
                    }
                    if (series_passport_tb.Text[i] >= '0' && series_passport_tb.Text[i] <= '9' && series_passport_tb.Text.Length == 4)
                    {
                        series = true;
                        break;
                    }
                    if (series == false)
                    {
                        MessageBox.Show("Поле серия паспорта должно содержать только цифры");
                        break;
                    }
                }
                for (int i = 0; i < number_passport_tb.Text.Length; i++)
                {
                    if (number_passport)
                    {
                        number_passport = false;
                    }
                    if (number_passport_tb.Text.Length == 6 && number_passport_tb.Text[i] >= '0' && number_passport_tb.Text[i] <= '9')
                    {
                        number_passport = true;
                        break;
                    }
                    if (number_passport == false)
                    {
                        MessageBox.Show("Поле номер паспорта должно содержать только цифры");
                        break;
                    }
                }
                DataTable data1 = Select("select * from Educations where id = " + id_education_tb.Text);
                for (int i = 0; i < id_education_tb.Text.Length; i++)
                {
                    if (id_education)
                    {
                        id_education = false;
                    }
                    if (id_education_tb.Text[i] >= '0' && id_education_tb.Text[i] <= '9')
                    {
                        id_education = true;
                        break;
                    }
                    if (id_education == false)
                    {
                        MessageBox.Show("Поле код образования должно содержать только цифры");
                        break;
                    }
                }
                if (data1.Rows.Count > 0)
                {

                }
                else if (data1.Rows.Count == 0)
                {
                    MessageBox.Show("Код образования, который вы ввели, не существует");
                }

                DataTable loginData = Select("select * from Listeners where login_listener = '" + login_tb.Text + "'");
                for (int i = 0; i < login_tb.Text.Length; i++)
                {
                    if (login)
                    {
                        login = false;
                    }
                    if (login_tb.Text[i] >= 'A' && login_tb.Text[i] <= 'Z' || login_tb.Text[i] >= 'a' && login_tb.Text[i] <= 'z' && login_tb.Text.Length <= 6)
                    {
                        login = true;
                        break;
                    }
                    if (login == false)
                    {
                        MessageBox.Show("Поле логин должно содержать только английские символы и иметь длину не более 6");
                        break;
                    }
                }
                if (loginData.Rows.Count > 0)
                {
                    login = true;
                }
                else if (loginData.Rows.Count == 0)
                {
                    MessageBox.Show("В системе уже есть такой логин");
                }
                DataTable pass = Select("select * from Listeners where password_listener = '" + password_tb.Password + "'");
                for (int i = 0; i < password_tb.Password.Length; i++)
                {
                    if (password)
                    {
                        password = false;
                    }
                    if (password_tb.Password[i] >= 'A' && password_tb.Password[i] <= 'Z' || password_tb.Password[i] >= 'a' && password_tb.Password[i] <= 'z' && password_tb.Password.Length <= 10)
                    {
                        password = true;
                        break;
                    }
                    if (password == false)
                    {
                        MessageBox.Show("Поле пароль должно содержать только английские символы и имет длину не более 10 символов");
                        break;
                    }
                }
                if (pass.Rows.Count > 0)
                {
                    password = true;
                }
                else if (pass.Rows.Count == 0)
                {
                    MessageBox.Show("Такой пароль уже есть в системе");
                }
                for (int i = 0; i < password1_tb.Password.Length; i++)
                {
                    if (password1)
                    {
                        password1 = false;
                    }
                    if (password1_tb.Password == password_tb.Password)
                    {
                        password1 = true;
                        break;
                    }
                    if (password1 == false)
                    {
                        MessageBox.Show("Пароли не совпадают");
                        break;
                    }
                }
                if (id && sex && role && surname && name && patronymic && id_course && birthday && email && phone && series && number_passport && id_education && login && password && password1)
                {
                    DataTable listeners_persons_update = Select("update Persons set surname = N'" + surname_tb.Text + "', name = N'" + name_tb.Text + "', patronymic = N'" + patronymic_tb.Text + "', id_sex =" + sex_tb.Text + " where id = " + id_tb.Text);
                }
                if (id && sex && role && surname && name && patronymic && id_course && birthday && email && phone && series && number_passport && id_education && login && password && password1)
                {
                    DataTable listeners_update = Select("update Listeners set id_course =" + id_course_tb.Text + ", birthday = '" + birthday_tb.Text + "', email = '" + email_tb.Text + "', phone_number = '" + phone_tb.Text + "', series_passport = '" + series_passport_tb.Text + "', number_passport = '" + number_passport_tb.Text + "', id_education =" + id_education_tb.Text + ", login_listener= '" + login_tb.Text + "', password_listener = '" + password_tb.Password + "' where id = " + id_tb.Text);
                    MessageBox.Show($"Слушатель c кодом {id_tb.Text} изменен");
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
                MessageBox.Show("Все поля должны быть введены");
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
