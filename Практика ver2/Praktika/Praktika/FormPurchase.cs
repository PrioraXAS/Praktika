using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktika
{
    public partial class FormPurchase : Form
    {
        private SQLiteConnection DB;
        public FormPurchase()
        {
            InitializeComponent();
        }

        private async void Form2_Load(object sender, EventArgs e)
        {
            DB = new SQLiteConnection(DataBase.connectionString);
            await DB.OpenAsync();
            Text = $"{DataUser.name}";
        }
        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            Close();
        }
        private async void button2_Click(object sender, EventArgs e)
        {
            Dictionary<string, List<decimal>> prices = new Dictionary<string, List<decimal>>
            {
                { "Ноябрь 2023", new List<decimal> { 1000.00M, 10000.00M, 5000.00M } },
                { "Декабрь 2023", new List<decimal> { 1000.00M } },
                { "Январь 2024", new List<decimal> { 5000.00M } },
                { "Июнь 2024", new List<decimal> { 10000.00M } },
                { "Июнь 2025", new List<decimal> { 10000.00M } },
            };

            string Name = comboBox1.Text;
            string Scope = textBox1.Text;
            DateTime selectedDateInst = dateTimePickerDateInst.Value;
            DateTime selectedDateDeinst = dateTimePickerDateDeinst.Value;

            // Определите месяц и год на основе выбранной даты
            string monthYearInst = selectedDateInst.ToString("MMMM yyyy"); // Используйте выбранную дату для формирования строки
            string monthYearDeinst = selectedDateDeinst.ToString("MMMM yyyy"); // Используйте выбранную дату для формирования строки

            decimal selectedPriceInst = -1; // Значение по умолчанию, если цена не найдена
            decimal selectedPriceDeinst = -1; // Значение по умолчанию, если цена не найдена

            // Получение цен и умножение на значение Scope
            if (prices.ContainsKey(monthYearInst))
            {
                List<decimal> priceList = prices[monthYearInst];
                if (priceList.Count > 0)
                {
                    selectedPriceInst = priceList[0] * (decimal.TryParse(Scope, out decimal scopeValue) ? scopeValue : 1);
                }
            }

            if (prices.ContainsKey(monthYearDeinst))
            {
                List<decimal> priceList = prices[monthYearDeinst];
                if (priceList.Count > 0)
                {
                    selectedPriceDeinst = priceList[0] * (decimal.TryParse(Scope, out decimal scopeValue) ? scopeValue : 1);
                }
            }

            if (selectedPriceInst != -1 && selectedPriceDeinst != -1)
            {
                // Используйте выбранные цены
                label5.Text = $"Цена начала: {selectedPriceInst:C}, Цена окончания: {selectedPriceDeinst:C}";

                // Остальной код для работы с базой данных
                try
                {
                    // Проверить, существует ли заявка
                    SQLiteCommand checkCommand = new SQLiteCommand(
                        $"SELECT * FROM [{table_Order.main}] " +
                        $"WHERE {table_Order.Name} = @Name " +
                        $"AND {table_Order.Scope} = @Scope " +
                        $"AND {table_Order.DateInst} = @DateInst " +
                        $"AND {table_Order.DateDeinst} = @DateDeinst " +
                        $"AND {table_Order.Price} = @Price " +
                        $"AND {table_Order.NameUser} = @NameUser", DB);

                    checkCommand.Parameters.AddWithValue("@Name", Name);
                    checkCommand.Parameters.AddWithValue("@Scope", Scope);
                    checkCommand.Parameters.AddWithValue("@DateInst", selectedDateInst.ToString("yyyy-MM-dd"));
                    checkCommand.Parameters.AddWithValue("@DateDeinst", selectedDateDeinst.ToString("yyyy-MM-dd"));
                    checkCommand.Parameters.AddWithValue("@Price", selectedPriceInst); // Выбранная цена начала
                    checkCommand.Parameters.AddWithValue("NameUser", $"{DataUser.name}");

                    SQLiteDataReader sqlReader = checkCommand.ExecuteReader();

                    if (sqlReader.HasRows)
                    {
                        MessageBox.Show("Такая заявка уже есть", "Ошибка создания заявки", MessageBoxButtons.OK);
                    }
                    else
                    {
                        // Вставить запись в базу данных
                        SQLiteCommand insertCommand = new SQLiteCommand(
                            $"INSERT INTO [{table_Order.main}] " +
                            $"({table_Order.Name}, {table_Order.Scope}, " +
                            $"{table_Order.DateInst}, {table_Order.DateDeinst}, {table_Order.Price}, {table_Order.NameUser}) " +
                            $"VALUES (@Name, @Scope, @DateInst, @DateDeinst, @Price, @NameUser)", DB);

                        insertCommand.Parameters.AddWithValue("@Name", Name);
                        insertCommand.Parameters.AddWithValue("@Scope", Scope);
                        insertCommand.Parameters.AddWithValue("@DateInst", selectedDateInst.ToString("yyyy-MM-dd"));
                        insertCommand.Parameters.AddWithValue("@DateDeinst", selectedDateDeinst.ToString("yyyy-MM-dd"));
                        insertCommand.Parameters.AddWithValue("@Price", selectedPriceInst); // Выбранная цена начала
                        insertCommand.Parameters.AddWithValue("NameUser", $"{DataUser.name}");

                        insertCommand.ExecuteNonQuery();

                        Form formCreateTicket = new FormUsers();
                        MessageBox.Show($"Заявка успешно зарегистрирована!\nНазвание: {Name} Количество: {Scope}", "Создание новой заявки прошла успешно ", MessageBoxButtons.OK);
                        formCreateTicket.Show();
                        formCreateTicket.FormClosed += new FormClosedEventHandler(Form_FormClosed);
                        Hide();
                    }
                }
                finally
                {

                }
            }
            else
            {
                label5.Text = "Цены не определены";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form formCreateTicket = new FormUsers();
            formCreateTicket.Show();
            formCreateTicket.FormClosed += new FormClosedEventHandler(Form_FormClosed);
            Hide();
        }
    }
}