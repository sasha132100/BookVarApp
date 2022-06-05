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
    public class UnitTestAddAdmin
    {
        [TestMethod()]
        public void TestMethod1()
        {
            AddUserWindow addUserWindow = new AddUserWindow();
            string expected = "Администратор успешно зарегистрирован";
            string login = "TestLogin1";
            string email = "TestEmail1";
            string password = "TestPassword1";
            string role = "Admin";
            string actual = addUserWindow.AddNewUserTest(login, email, password, role);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestMethod2()
        {
            AddUserWindow addUserWindow = new AddUserWindow();
            string expected = "Введены некорректные данные для регистрации";
            string login = "Test";
            string email = "TestEmail1";
            string password = "TestPassword1";
            string role = "Admin";
            string actual = addUserWindow.AddNewUserTest(login, email, password, role);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestMethod3()
        {
            AddUserWindow addUserWindow = new AddUserWindow();
            string expected = "Неправильно введена роль пользователя";
            string login = "TestLogin1";
            string email = "TestEmail1";
            string password = "TestPassword1";
            string role = "Adm";
            string actual = addUserWindow.AddNewUserTest(login, email, password, role);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestMethod4()
        {
            AddUserWindow addUserWindow = new AddUserWindow();
            string expected = "Данный пользователь уже зарегистрирован";
            string login = "TestLogin1";
            string email = "TestEmail1";
            string password = "TestPassword1";
            string role = "Admin";
            string actual = addUserWindow.AddNewUserTest(login, email, password, role);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestMethod5()
        {
            AddUserWindow addUserWindow = new AddUserWindow();
            string expected = "Администратор успешно зарегистрирован";
            string login = "TestLogin2";
            string email = "TestEmail2";
            string password = "TestPassword2";
            string role = "Admin";
            string actual = addUserWindow.AddNewUserTest(login, email, password, role);
            Assert.AreEqual(expected, actual);
        }
    }
}
