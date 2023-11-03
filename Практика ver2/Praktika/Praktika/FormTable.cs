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
    public partial class FormTable : Form
    {
        private SQLiteConnection DB;
        public FormTable()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string NameUser = textBox1.Text;
            // Создаем команду SQL для выполнения запроса
            SQLiteCommand command = new SQLiteCommand($"SELECT * FROM [{table_Order.main}] " +
                $"WHERE {table_Order.NameUser} = @NameUser ", DB);


            // Добавляем параметр для фильтрации по полю NameUser
            command.Parameters.AddWithValue("@NameUser", NameUser);

            // Создаем адаптер данных и выполняем запрос
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);

            // Если у вас уже есть привязанный источник данных (DataSource) к dataGridViewOrder
            if (dataGridView1.DataSource != null)
            {
                // Очищаем существующие данные в источнике данных
                ((DataTable)dataGridView1.DataSource).Clear();
                // Заполняем источник данных новыми данными
                dataAdapter.Fill((DataTable)dataGridView1.DataSource);
            }
            else
            {
                // Если у вас нет привязанного источника данных, создаем новый DataTable и связываем его с dataGridViewOrder
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }

        private async void FormTable_Load(object sender, EventArgs e)
        {
            DB = new SQLiteConnection(DataBase.connectionString);
            await DB.OpenAsync();
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
