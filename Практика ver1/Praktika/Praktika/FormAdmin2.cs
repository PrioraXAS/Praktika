using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktika
{
    public partial class FormAdmin2 : Form
    {
        private SQLiteConnection DB;
        public FormAdmin2()
        {
            InitializeComponent();
        }

        private async void Form3_Load(object sender, EventArgs e)
        {
            DB = new SQLiteConnection(DataBase.connectionString);
            await DB.OpenAsync();
            LoadingDB();
        }
        private async void LoadingDB()
        {
            dataGridViewUser.Rows.Clear();
            SQLiteDataReader sqlReader = null;
            SQLiteCommand command = new SQLiteCommand($"SELECT * FROM [{table_User.main}]", DB);
            List<string[]> data = new List<string[]>();
            try
            {
                sqlReader = (SQLiteDataReader)await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    data.Add(new string[7]);

                    data[data.Count - 1][0] = Convert.ToString($"{sqlReader[$"{table_User.ID}"]}");
                    data[data.Count - 1][1] = Convert.ToString($"{sqlReader[$"{table_User.Name}"]}");
                    data[data.Count - 1][2] = Convert.ToString($"{sqlReader[$"{table_User.Region}"]}");
                    data[data.Count - 1][3] = Convert.ToString($"{sqlReader[$"{table_User.Scope}"]}");
                    data[data.Count - 1][4] = Convert.ToString($"{sqlReader[$"{table_User.Login}"]}");
                    data[data.Count - 1][5] = Convert.ToString($"{sqlReader[$"{table_User.Password}"]}");
                    data[data.Count - 1][6] = Convert.ToString($"{sqlReader[$"{table_User.Role}"]}");
                }

                foreach (string[] s in data)
                {
                    _ = dataGridViewUser.Rows.Add(s);
                }
                dataGridViewUser.ClearSelection();

            }
            catch (Exception ex)
            {
                _ = MessageBox.Show($"{ex.Message}", $"{ex.Source}");
            }
            finally
            {
                sqlReader?.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form formCreateTicket = new FormAdmin();
            formCreateTicket.Show();
            formCreateTicket.FormClosed += new FormClosedEventHandler(Form_FormClosed);
            Hide();
        }
        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }
    }
}
