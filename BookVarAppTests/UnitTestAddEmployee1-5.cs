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
    public class UnitTestAddEmployee
    {
        [TestMethod()]
        public void TestMethod1()
        {
            AddUserWindow addUserWindow = new AddUserWindow();
            string expected = "Сотрудник успешно зарегистрирован";
            string login = "TestLogin3";
            string email = "TestEmail3";
            string password = "TestPassword3";
            string role = "Employee";
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
            string role = "Employee";
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
            string role = "Empl";
            string actual = addUserWindow.AddNewUserTest(login, email, password, role);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestMethod4()
        {
            AddUserWindow addUserWindow = new AddUserWindow();
            string expected = "Данный пользователь уже зарегистрирован";
            string login = "TestLogin3";
            string email = "TestEmail3";
            string password = "TestPassword3";
            string role = "Employee";
            string actual = addUserWindow.AddNewUserTest(login, email, password, role);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void TestMethod5()
        {
            AddUserWindow addUserWindow = new AddUserWindow();
            string expected = "Сотрудник успешно зарегистрирован";
            string login = "TestLogin4";
            string email = "TestEmail4";
            string password = "TestPassword4";
            string role = "Employee";
            string actual = addUserWindow.AddNewUserTest(login, email, password, role);
            Assert.AreEqual(expected, actual);
        }
    }
}
