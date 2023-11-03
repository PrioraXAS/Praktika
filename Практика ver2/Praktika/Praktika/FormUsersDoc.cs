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
    public partial class FormUsersDoc : Form
    {
        private SQLiteConnection DB;
        public FormUsersDoc()
        {
            InitializeComponent();
        }

        private async void Form3_Load(object sender, EventArgs e)
        {
            DB = new SQLiteConnection(DataBase.connectionString);
            await DB.OpenAsync();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form formCreateTicket = new FormUsers();
            formCreateTicket.Show();
            formCreateTicket.FormClosed += new FormClosedEventHandler(Form_FormClosed);
            Hide();
        }
        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form formCreateTicket = new FormUsers();
            formCreateTicket.Show();
            formCreateTicket.FormClosed += new FormClosedEventHandler(Form_FormClosed);
            Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string selectedName = comboBox1.SelectedItem.ToString();
            int newScope;

            if (int.TryParse(textBox1.Text, out newScope))
            {
                // Получение текущего Scope из базы данных
                int currentScope = 0;

                using (SQLiteCommand selectCommand = new SQLiteCommand($"SELECT [{table_Order.Scope}] FROM [{table_Order.main}] WHERE [{table_Order.Name}] = @Name AND [{table_Order.NameUser}] = @NameUser", DB))
                {
                    selectCommand.Parameters.AddWithValue("@Name", selectedName);
                    selectCommand.Parameters.AddWithValue("@NameUser", $"{DataUser.name}");
                    currentScope = Convert.ToInt32(selectCommand.ExecuteScalar());
                }

                // Сложение текущего и нового значения Scope
                int updatedScope = currentScope + newScope;

                // Обновление базы данных с новым значением Scope
                using (SQLiteCommand updateCommand = new SQLiteCommand($"UPDATE [{table_Order.main}] SET [{table_Order.Scope}] = @UpdatedScope WHERE [{table_Order.Name}] = @Name AND [{table_Order.NameUser}] = @NameUser", DB))
                {
                    updateCommand.Parameters.AddWithValue("@UpdatedScope", updatedScope);
                    updateCommand.Parameters.AddWithValue("@Name", selectedName);
                    updateCommand.Parameters.AddWithValue("@NameUser", DataUser.name);

                    int rowsAffected = updateCommand.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Значение Scope успешно обновлено.");
                    }
                    else
                    {
                        MessageBox.Show("Запись с выбранным Name не найдена.");
                    }
                }
            }
        }   
    }
}