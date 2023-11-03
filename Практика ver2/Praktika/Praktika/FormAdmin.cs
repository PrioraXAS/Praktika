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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace Praktika
{
    public partial class FormAdmin : Form
    {
        private SQLiteConnection DB;
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form formCreateTicket = new FormAdmin1();
            formCreateTicket.Show();
            formCreateTicket.FormClosed += new FormClosedEventHandler(Form_FormClosed);
            Hide();
        }

        private async void LoadingDB()
        {
            dataGridViewProduct.Rows.Clear();
            SQLiteDataReader sqlReader = null;
            SQLiteCommand command = new SQLiteCommand($"SELECT * FROM [{table_Product.main}]", DB);
            List<string[]> data = new List<string[]>();
            try
            {
                sqlReader = (SQLiteDataReader)await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    data.Add(new string[9]);

                    data[data.Count - 1][0] = Convert.ToString($"{sqlReader[$"{table_Product.ID}"]}");
                    data[data.Count - 1][1] = Convert.ToString($"{sqlReader[$"{table_Product.Name}"]}");
                    data[data.Count - 1][2] = Convert.ToString($"{sqlReader[$"{table_Product.Type}"]}");
                    data[data.Count - 1][3] = Convert.ToString($"{sqlReader[$"{table_Product.Company}"]}");
                    data[data.Count - 1][4] = Convert.ToString($"{sqlReader[$"{table_Product.Date}"]}");
                    data[data.Count - 1][5] = Convert.ToString($"{sqlReader[$"{table_Product.Count}"]}");
                    data[data.Count - 1][6] = Convert.ToString($"{sqlReader[$"{table_Product.License}"]}");
                    data[data.Count - 1][7] = Convert.ToString($"{sqlReader[$"{table_Product.Price}"]}");
                    data[data.Count - 1][8] = Convert.ToString($"{sqlReader[$"{table_Product.Scope}"]}");
                }

                foreach (string[] s in data)
                {
                    _ = dataGridViewProduct.Rows.Add(s);
                }
                dataGridViewProduct.ClearSelection();

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
        private async void FormAdmin_Load(object sender, EventArgs e)
        {
            DB = new SQLiteConnection(DataBase.connectionString);
            await DB.OpenAsync();
            LoadingDB();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            // Очистить существующие строки в таблице
            dataGridViewProduct.Rows.Clear();

            SQLiteCommand command = new SQLiteCommand($"SELECT * FROM [{table_Product.main}] ORDER BY Scope DESC", DB);
            SQLiteDataReader sqlReader = null;

            List<string[]> data = new List<string[]>();
            try
            {
                sqlReader = (SQLiteDataReader)await command.ExecuteReaderAsync();
                while (await sqlReader.ReadAsync())
                {
                    data.Add(new string[9]);

                    data[data.Count - 1][0] = Convert.ToString($"{sqlReader[$"{table_Product.ID}"]}");
                    data[data.Count - 1][1] = Convert.ToString($"{sqlReader[$"{table_Product.Name}"]}");
                    data[data.Count - 1][2] = Convert.ToString($"{sqlReader[$"{table_Product.Type}"]}");
                    data[data.Count - 1][3] = Convert.ToString($"{sqlReader[$"{table_Product.Company}"]}");
                    data[data.Count - 1][4] = Convert.ToString($"{sqlReader[$"{table_Product.Date}"]}");
                    data[data.Count - 1][5] = Convert.ToString($"{sqlReader[$"{table_Product.Count}"]}");
                    data[data.Count - 1][6] = Convert.ToString($"{sqlReader[$"{table_Product.License}"]}");
                    data[data.Count - 1][7] = Convert.ToString($"{sqlReader[$"{table_Product.Price}"]}");
                    data[data.Count - 1][8] = Convert.ToString($"{sqlReader[$"{table_Product.Scope}"]}");
                }

                foreach (string[] s in data)
                {
                    _ = dataGridViewProduct.Rows.Add(s);
                }
                dataGridViewProduct.ClearSelection();
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

        private void button3_Click(object sender, EventArgs e)
        {
            Form formCreateTicket = new FormTable();
            formCreateTicket.Show();
            formCreateTicket.FormClosed += new FormClosedEventHandler(Form_FormClosed);
            Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form formCreateTicket = new FormAdmin2();
            formCreateTicket.Show();
            formCreateTicket.FormClosed += new FormClosedEventHandler(Form_FormClosed);
            Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form formCreateTicket = new FormLogin();
            formCreateTicket.Show();
            formCreateTicket.FormClosed += new FormClosedEventHandler(Form_FormClosed);
            Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form formCreateTicket = new FormOrder();
            formCreateTicket.Show();
            formCreateTicket.FormClosed += new FormClosedEventHandler(Form_FormClosed);
            Hide();
        }
    }
}
