using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Praktika
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLogin());
        }
    }
    static class DataBase // строка подключения к БД
    {
        public static string connectionString = @"Data Source=data.db; Integrated Security=False; MultipleActiveResultSets=True";
    }
    static class table_Order // описание таблиц из БД
    {
        public static string main = "Order";
        public static string ID = "ID";
        public static string Name = "Name";
        public static string Price = "Price";
        public static string DateInst = "DateInst";
        public static string DateDeinst = "DateDeinst";
        public static string Scope = "Scope";
        public static string NameUser = "NameUser";
    }
    static class table_Product
    {
        public static string main = "Product";
        public static string ID = "ID";
        public static string Name = "Name";
        public static string Type = "Type";
        public static string Company = "Company";
        public static string Date = "Date";
        public static string Scope = "Scope";
        public static string License = "License";
        public static string Price = "Price";
        public static string Count = "Count";
    }
    static class table_User
    {
        public static string main = "User";
        public static string ID = "ID";
        public static string Name = "Name";
        public static string Region = "Region";
        public static string Scope = "Scope";
        public static string Role = "Role";
        public static string Login = "Login";
        public static string Password = "Password";
    }
    static class DataUser
    {
        public static string family { get; set; }
        public static string name { get; set; }
        public static string number { get; set; }
        public static string login { get; set; }
        public static string password { get; set; }
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        // Другие свойства заказа...
    }
}
