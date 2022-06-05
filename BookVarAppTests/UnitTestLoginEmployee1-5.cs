using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookVarApp.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookVarApp.Windows.Tests
{
    [TestClass()]
    public class UnitTestLoginEmployee
    {
        [TestMethod()]
        public void TestMethod1()
        {
            MainWindow mainWindow = new MainWindow();
            string expected = "Сотрудник успешно вошел";
            string login = "Employee1";
            string password = "qqqqwwww";
            string actual = mainWindow.LoginUser(login, password);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestMethod2()
        {
            MainWindow mainWindow = new MainWindow();
            string expected = "Неверный пароль!";
            string login = "Employee2";
            string password = "qqqqwwwww";
            string actual = mainWindow.LoginUser(login, password);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestMethod3()
        {
            MainWindow mainWindow = new MainWindow();
            string expected = "Пользователя с таким логином не существует!";
            string login = "Employe";
            string password = "qqqqwwww";
            string actual = mainWindow.LoginUser(login, password);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestMethod4()
        {
            MainWindow mainWindow = new MainWindow();
            string expected = "Попытка входа без ввода пароля";
            string login = "Employee4";
            string password = "";
            string actual = mainWindow.LoginUser(login, password);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestMethod5()
        {
            MainWindow mainWindow = new MainWindow();
            string expected = "Логин должен быть больше 5";
            string login = "Empl";
            string password = "qqqqwwww";
            string actual = mainWindow.LoginUser(login, password);
            Assert.AreEqual(expected, actual);
        }
    }
}
