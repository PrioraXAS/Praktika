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
    public partial class FormOrder : Form
    {
        private SQLiteConnection DB;
        public FormOrder()
        {
            InitializeComponent();
        }

        private async void FormOrder_Load(object sender, EventArgs e)
        {
            DB = new SQLiteConnection(DataBase.connectionString);
            await DB.OpenAsync();
        }

        DataTable dataTable = new DataTable();
        private void button1_Click(object sender, EventArgs e)
        {
            string NameUser = textBox1.Text;
            SQLiteCommand command = new SQLiteCommand($"SELECT * FROM [{table_Order.main}] " +
                $"WHERE {table_Order.Name} = @Name ", DB);

            command.Parameters.AddWithValue("@Name", NameUser);
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);

            dataTable.Clear();

            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            {
                string nameToSearch = textBox1.Text; 

                SQLiteCommand command = new SQLiteCommand($"SELECT * FROM [{table_Order.main}] " +
                    $"WHERE {table_Order.Name} = @Name " + 
                    $"ORDER BY {table_Order.Scope} DESC", DB); 

                command.Parameters.AddWithValue("@Name", nameToSearch);

                SQLiteDataReader sqlReader = null;
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

                    dataTable.Rows.Clear();

                    foreach (string[] s in data)
                    {
                        dataTable.Rows.Add(s);
                    }

                    dataGridView1.ClearSelection();
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
        }

        private void button4_Click(object sender, EventArgs e)
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
