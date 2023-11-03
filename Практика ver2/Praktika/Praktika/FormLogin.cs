using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktika
{
    public partial class FormLogin : Form
    {
        private SQLiteConnection DB;
        public FormLogin()
        {
            InitializeComponent();
        }

        private async void FormLogin_Load(object sender, EventArgs e)
        {
            DB = new SQLiteConnection(DataBase.connectionString);
            await DB.OpenAsync();
        }
        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string login = textBox1.Text;
            string Password = textBox2.Text;

            if (login != "" && Password != "")
            {
                SQLiteCommand command = new SQLiteCommand($"SELECT * FROM [{table_User.main}] " +
                    $"WHERE {table_User.Login}=@Login " +
                    $"AND {table_User.Password}=@Password", DB);
                _ = command.Parameters.AddWithValue("Login", textBox1.Text);
                _ = command.Parameters.AddWithValue("Password", textBox2.Text);
                SQLiteDataReader sqlReader = (SQLiteDataReader)await command.ExecuteReaderAsync();

                if (await sqlReader.ReadAsync())
                {
                    DataUser.family = sqlReader[$"{table_User.Region}"].ToString();
                    DataUser.name = sqlReader[$"{table_User.Name}"].ToString();
                    DataUser.number = sqlReader[$"{table_User.Scope}"].ToString();
                    DataUser.login = sqlReader[$"{table_User.Login}"].ToString();
                    DataUser.password = sqlReader[$"{table_User.Password}"].ToString();

                    if (sqlReader[$"{table_User.Role}"].ToString() == "Admin")
                    {
                        Form formlogin = new FormAdmin();
                        _ = MessageBox.Show("Вход прошел успешно", "Успех", MessageBoxButtons.OK);
                        formlogin.Show();
                        formlogin.FormClosed += new FormClosedEventHandler(Form_FormClosed);
                        Hide();

                    }
                    else if (sqlReader[$"{table_User.Role}"].ToString() == "User")
                    {
                        Form formlogin = new FormUsers();
                        _ = MessageBox.Show("Вход прошел успешно", "Успех", MessageBoxButtons.OK);
                        formlogin.Show();
                        formlogin.FormClosed += new FormClosedEventHandler(Form_FormClosed);
                        Hide();
                    }
                    sqlReader.Close();
                }
                else
                {
                    _ = MessageBox.Show("Вход не выполнен, введены неправильно данные", "Ошибка входа", MessageBoxButtons.OK);
                    return;
                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}