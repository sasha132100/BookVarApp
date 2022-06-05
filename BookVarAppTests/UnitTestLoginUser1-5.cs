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
    public class UnitTestLoginUser
    {
        [TestMethod()]
        public void TestMethod1()
        {
            MainWindow mainWindow = new MainWindow();
            string expected = "Пользователь успешно вошел";
            string login = "User1";
            string password = "qqqqwwww";
            string actual = mainWindow.LoginUser(login, password);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestMethod2()
        {
            MainWindow mainWindow = new MainWindow();
            string expected = "Неверный пароль!";
            string login = "User2";
            string password = "qqqqwwwww";
            string actual = mainWindow.LoginUser(login, password);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestMethod3()
        {
            MainWindow mainWindow = new MainWindow();
            string expected = "Пользователя с таким логином не существует!";
            string login = "User0";
            string password = "qqqqwwww";
            string actual = mainWindow.LoginUser(login, password);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestMethod4()
        {
            MainWindow mainWindow = new MainWindow();
            string expected = "Попытка входа без ввода пароля";
            string login = "User4";
            string password = "";
            string actual = mainWindow.LoginUser(login, password);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestMethod5()
        {
            MainWindow mainWindow = new MainWindow();
            string expected = "Логин должен быть больше 5";
            string login = "User";
            string password = "qqqqwwww";
            string actual = mainWindow.LoginUser(login, password);
            Assert.AreEqual(expected, actual);
        }
    }
}
