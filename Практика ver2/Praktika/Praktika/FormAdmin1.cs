using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktika
{
    public partial class FormAdmin1 : Form
    {
        private SQLiteConnection DB;
        public FormAdmin1()
        {
            InitializeComponent();
        }
        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form formCreateTicket = new FormAdmin2();
            formCreateTicket.Show();
            formCreateTicket.FormClosed += new FormClosedEventHandler(Form_FormClosed);
            Hide();
        }
        private async void LoadingDB()
        {
            dataGridViewOrder.Rows.Clear();
            SQLiteDataReader sqlReader = null;
            SQLiteCommand command = new SQLiteCommand($"SELECT * FROM [{table_Order.main}]", DB);
            List<string[]> data = new List<string[]>();
            try
            {
                sqlReader = (SQLiteDataReader)await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    data.Add(new string[7]);

                    data[data.Count - 1][0] = Convert.ToString($"{sqlReader[$"{table_Order.ID}"]}");
                    data[data.Count - 1][1] = Convert.ToString($"{sqlReader[$"{table_Order.Name}"]}");
                    data[data.Count - 1][2] = Convert.ToString($"{sqlReader[$"{table_Order.Scope}"]}");
                    data[data.Count - 1][3] = Convert.ToString($"{sqlReader[$"{table_Order.DateInst}"]}");
                    data[data.Count - 1][4] = Convert.ToString($"{sqlReader[$"{table_Order.DateDeinst}"]}");
                    data[data.Count - 1][5] = Convert.ToString($"{sqlReader[$"{table_Order.Price}"]}");
                    data[data.Count - 1][6] = Convert.ToString($"{sqlReader[$"{table_Order.NameUser}"]}");
                }

                foreach (string[] s in data)
                {
                    _ = dataGridViewOrder.Rows.Add(s);
                }
                dataGridViewOrder.ClearSelection();

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


        private async void FormAdmin1_Load(object sender, EventArgs e)
        {
            DB = new SQLiteConnection(DataBase.connectionString);
            await DB.OpenAsync();
            LoadingDB();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form formCreateTicket = new FormAdmin();
            formCreateTicket.Show();
            formCreateTicket.FormClosed += new FormClosedEventHandler(Form_FormClosed);
            Hide();
        }

    }
}


