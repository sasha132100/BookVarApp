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
using System.Windows.Shapes;

namespace BookVarApp.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        public AddUserWindow()
        {
            InitializeComponent();
            try
            {
                btnProfileText.Text = SystemContext.User.UserLogin;
                btnProfile.Click += ButtonMyProfile_Click;
            }
            catch 
            {
                
            }
        }

        public string AddNewUserTest(string login, string email, string password, string role)
        {
            try
            {
                if (login != "" && login.Length >= 5 && email != "" && password != "" && password.Length >= 8 && role != "")
                {
                    if (role == "Admin")
                    {
                        using (var db = new Book_StoreEntities())
                        {
                            Users user = new Users
                            {
                                UserLogin = login,
                                UserEmail = email,
                                UserPassword = password,
                                UserRole = "Admin"
                            };
                            db.Users.Add(user);
                            db.SaveChanges();
                        }
                        return "Администратор успешно зарегистрирован";
                    }
                    else if (role == "Employee")
                    {
                        using (var db = new Book_StoreEntities())
                        {
                            Users user = new Users
                            {
                                UserLogin = login,
                                UserEmail = email,
                                UserPassword = password,
                                UserRole = "Employee"
                            };
                            db.Users.Add(user);
                            db.SaveChanges();
                        }
                        return "Сотрудник успешно зарегистрирован";
                    }
                    else if (role == "User")
                    {
                        using (var db = new Book_StoreEntities())
                        {
                            Users user = new Users
                            {
                                UserLogin = login,
                                UserEmail = email,
                                UserPassword = password,
                                UserRole = "User"
                            };
                            db.Users.Add(user);
                            db.SaveChanges();
                        }
                        return "Пользователь успешно зарегистрирован";
                    }
                    else
                    {
                        return "Неправильно введена роль пользователя";
                    }
                }
                else
                {
                    return "Введены некорректные данные для регистрации";
                }
            }
            catch
            {
                return "Данный пользователь уже зарегистрирован";
            }
        }

        private void ButtonMyProfile_Click(object sender, RoutedEventArgs e)
        {
            MyProfileAdminWindow mpaw = new MyProfileAdminWindow();
            this.Close();
            mpaw.ShowDialog();
        }

        private void AdminUserListClick_Button(object sender, MouseButtonEventArgs e)
        {
            AdminWindow aw = new AdminWindow();
            this.Close();
            aw.ShowDialog();
        }

        private void AdminUserAddClick_Button(object sender, MouseButtonEventArgs e)
        {
            InitializeComponent();
        }

        private void CancelButton_Click(object sender, MouseButtonEventArgs e)
        {
            AdminWindow aw = new AdminWindow();
            this.Close();
            aw.ShowDialog();
        }

        private void AddNewUserButton_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (txtLogin.Text != "")
                {
                    if (txtLogin.Text.Length >= 5)
                    {
                        if (txtEmail.Text != "")
                        {
                            if (txtPassword.Password != "")
                            {
                                if (txtPassword.Password.Length >= 8)
                                {
                                    if (RadioBtnAdmin.IsChecked == true)
                                    {
                                        string login, email, password;
                                        login = txtLogin.Text;
                                        email = txtEmail.Text;
                                        password = txtPassword.Password;

                                        using (var db = new Book_StoreEntities())
                                        {
                                            Users user = new Users
                                            {
                                                UserLogin = login,
                                                UserEmail = email,
                                                UserPassword = password,
                                                UserRole = "Admin"
                                            };
                                            db.Users.Add(user);
                                            db.SaveChanges();
                                        }

                                        MessageBox.Show("Пользователь успешно зарегистрирован");
                                    }
                                    else if (RadioBtnEmployee.IsChecked == true)
                                    {
                                        string login, email, password;
                                        login = txtLogin.Text;
                                        email = txtEmail.Text;
                                        password = txtPassword.Password;

                                        using (var db = new Book_StoreEntities())
                                        {
                                            Users user = new Users
                                            {
                                                UserLogin = login,
                                                UserEmail = email,
                                                UserPassword = password,
                                                UserRole = "Employee"
                                            };
                                            db.Users.Add(user);
                                            db.SaveChanges();
                                        }

                                        MessageBox.Show("Пользователь успешно зарегистрирован");
                                    }
                                    else if (RadioBtnClient.IsChecked == true)
                                    {
                                        string login, email, password;
                                        login = txtLogin.Text;
                                        email = txtEmail.Text;
                                        password = txtPassword.Password;

                                        using (var db = new Book_StoreEntities())
                                        {
                                            Users user = new Users
                                            {
                                                UserLogin = login,
                                                UserEmail = email,
                                                UserPassword = password,
                                                UserRole = "User"
                                            };
                                            var user2 = db.Users.Add(user);
                                            db.Client.Add(new Client() { UserID = user2.UserID, ClientFIO = null, ClientNumber = null, ClientPreferredAddress = null });
                                            db.SaveChanges();
                                        }

                                        MessageBox.Show("Пользователь успешно зарегистрирован");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Выберите роль пользователя");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Пароль должен быть больше 7 символов");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Введите пароль!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Введите email!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Логин пользователя должен быть больше 5 символов");
                    }
                }
                else
                {
                    MessageBox.Show("Введите логин!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
