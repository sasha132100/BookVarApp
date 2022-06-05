﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookVarApp.Windows

{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            txtLogin.Text = "Damirka";
            txtPassword.Password = "qqqqwwww";
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            var register = new RegisterWindow();
            this.Close();
            register.ShowDialog();
        }

        public string LoginUser(string login, string password)
        {
            if (login != "")
            {
                if (login.Length >= 5)
                {
                    if (password != "")
                    {
                        using (var db = new Book_StoreEntities())
                        {
                            Users user = (from u in db.Users where u.UserLogin == login select u).FirstOrDefault();
                            if (user != null)
                            {
                                if (user.UserPassword == password)
                                {
                                    if (user.UserRole == "Admin")
                                    {
                                        return "Админ успешно вошел";
                                    }
                                    else if (user.UserRole == "Employee")
                                    {
                                        return "Сотрудник успешно вошел";
                                    }
                                    else if (user.UserRole == "User")
                                    {
                                        return "Пользователь успешно вошел";
                                    }
                                    else
                                    {
                                        return "Пошел пользователь с неопредленной ролью"; 
                                    }   
                                }
                                else
                                {
                                    return "Неверный пароль!";
                                }
                            }
                            else
                            {
                                return "Пользователя с таким логином не существует!";
                            }
                        }
                    }
                    else
                    {
                        return "Попытка входа без ввода пароля";
                    }
                }
                else
                {
                    return "Логин должен быть больше 5";
                }
            }
            else
            {
                return "Введите логин";
            }
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            string result = LoginMethod(txtLogin.Text, txtPassword.Password);
            if (result == $"Добро пожаловать, {txtLogin.Text}!")
            {
                using (var db = new Book_StoreEntities())
                {
                    Users user = (from u in db.Users where u.UserLogin == txtLogin.Text select u).FirstOrDefault();
                    if (user.UserRole == "User")
                    {
                        SystemContext.typeWindow = "Каталог";
                        ClientMainWindow cmw = new ClientMainWindow();
                        this.Close();
                        cmw.ShowDialog();
                    }
                    else if (user.UserRole == "Employee")
                    {
                        EmployeeMainWindow emw = new EmployeeMainWindow();
                        this.Close();
                        emw.ShowDialog();
                    }
                    else if (user.UserRole == "Admin")
                    {
                        AdminWindow aw = new AdminWindow();
                        this.Close();
                        aw.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Произошла ошибка с определением роли пользователя", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(result, "Результат", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private string LoginMethod(string login, string password)
        {
            if (login.Length == 0 || password.Length == 0)
                return "Не все поля заполнены!";
            using (var db = new Book_StoreEntities())
            {
                Users user = (from u in db.Users where u.UserLogin == login select u).FirstOrDefault();
                if (user == null)
                    return "Пользователя с таким логином не существует!";
                if (user.UserPassword != password)
                    return "Неверный пароль!";
                if (user.UserRole == "User")
                    SystemContext.Client = (from c in db.Client where c.UserID == user.UserID select c).FirstOrDefault();
                SystemContext.User = user;
            }
            return $"Добро пожаловать, {login}!";
        }
    }
}
