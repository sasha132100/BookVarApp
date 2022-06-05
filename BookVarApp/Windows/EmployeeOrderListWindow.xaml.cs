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
    /// Логика взаимодействия для EmployeeOrderListWindow.xaml
    /// </summary>
    public partial class EmployeeOrderListWindow : Window
    {
        public EmployeeOrderListWindow()
        {
            InitializeComponent();
            LoadContent();
            try
            {
                btnProfileText.Text = SystemContext.User.UserLogin;
                btnProfile.Click += ButtonMyProfile_Click;
            }
            catch { }
        }

        void LoadContent()
        {
            using (var db = new Book_StoreEntities())
            {
                List<Orders> orders;
                try
                {
                    orders = (from o in db.Orders orderby o.OrderData descending select o).ToList<Orders>();
                    int i = 0;
                    foreach (var order in orders)
                    {
                        AddNewOrder(order.OrderID.ToString(), order.OrderData.ToString(), order.ProductID.ToString(), order.OrderProductCount, order.ShopID.ToString(), order.OrderStatus, order.OrderTotalPrice, order.ClientID.ToString());
                        i++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void AddNewOrder(string OrderID, string OrderData, string ProductID, int OrderProductCount, string ShopID, string OrderStatus, int OrderTotalPrice, string ClientID)
        {
            using (var db = new Book_StoreEntities())
            {
                string[] data = OrderData.Split(' ');
                SystemContext.Client = (from c in db.Client where c.ClientID.ToString() == ClientID select c).FirstOrDefault();
                string userLogin = (from u in db.Users where u.UserID == SystemContext.Client.UserID select u.UserLogin).FirstOrDefault();
                SystemContext.Shop = (from s in db.Shop where s.ShopID.ToString() == ShopID select s).FirstOrDefault();
                SystemContext.Product = (from p in db.Product where p.ProductID.ToString() == ProductID select p).FirstOrDefault();

                var borderPanel = new Border() { BorderBrush = Brushes.LightGray, BorderThickness = new Thickness(2), Style = (Style)OrdersView.Resources["contentBorderStyle"] };
                var mainGrid = new Grid() { };
                mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30, GridUnitType.Star) });
                mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(37, GridUnitType.Star) });
                mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(33, GridUnitType.Star) });

                StackPanel sp = new StackPanel() { };
                TextBlock Order = new TextBlock() { Text = "Заказ от ", Style = (Style)OrdersView.Resources["Lbl"], Margin = new Thickness(5, 0, 0, 0) };
                TextBlock FIO = new TextBlock() { Text = "ФИО: ", Style = (Style)OrdersView.Resources["Lbl"], Margin = new Thickness(5, 5, 0, 0), Foreground = Brushes.Black };
                TextBlock Number = new TextBlock() { Text = "Номер телефона: ", Style = (Style)OrdersView.Resources["Lbl"], Margin = new Thickness(5, 5, 0, 0), Foreground = Brushes.Black };
                TextBlock OrderProductText = new TextBlock() { Text = "Покупка: ", Style = (Style)OrdersView.Resources["Lbl"], Margin = new Thickness(5, 5, 0, 0) };
                TextBlock OrderProduct = new TextBlock() { Text = " - ", Style = (Style)OrdersView.Resources["Lbl"], Margin = new Thickness(5, 5, 0, 0), Foreground = Brushes.Black };
                TextBlock OrderAddressText = new TextBlock() { Text = "Адрес доставки:", Style = (Style)OrdersView.Resources["Lbl"], Margin = new Thickness(0, 0, 0, 0), HorizontalAlignment = HorizontalAlignment.Center };
                TextBlock OrderAddress = new TextBlock() { Text = $"{SystemContext.Shop.ShopAddress}", Style = (Style)OrdersView.Resources["Lbl"], Margin = new Thickness(0, 15, 0, 0), Foreground = Brushes.Black, HorizontalAlignment = HorizontalAlignment.Center };
                TextBlock OrderStatusCheck = new TextBlock() { Text = "Статус: ", Style = (Style)OrdersView.Resources["Lbl"], Margin = new Thickness(0, 0, 0, 0), HorizontalAlignment = HorizontalAlignment.Right };
                TextBlock OrderTotalPriceCheck = new TextBlock() { Text = "Общая стоимость: ", Style = (Style)OrdersView.Resources["Lbl"], Margin = new Thickness(0, 80, 0, 0), HorizontalAlignment = HorizontalAlignment.Right, Foreground = Brushes.Black, FontSize = 17 };
                Order.Inlines.Add(new TextBlock() { Text = $" {data[0]}", Margin = new Thickness(0) });
                FIO.Inlines.Add(new TextBlock() { Text = $" {SystemContext.Client.ClientFIO}", Foreground = (Brush)(new BrushConverter().ConvertFrom("Black")), Margin = new Thickness(0) });
                Number.Inlines.Add(new TextBlock() { Text = $" {SystemContext.Client.ClientNumber}", Foreground = (Brush)(new BrushConverter().ConvertFrom("Black")), Margin = new Thickness(0) });
                OrderProduct.Inlines.Add(new TextBlock() { Text = $" {SystemContext.Product.ProductName}.  Кол-во: {OrderProductCount}", Foreground = (Brush)(new BrushConverter().ConvertFrom("Black")), Margin = new Thickness(0) });
                OrderStatusCheck.Inlines.Add(new TextBlock() { Text = $" {OrderStatus}", Foreground = (Brush)(new BrushConverter().ConvertFrom("Black")), Margin = new Thickness(0) });
                OrderTotalPriceCheck.Inlines.Add(new TextBlock() { Text = $" {OrderTotalPrice}", Foreground = (Brush)(new BrushConverter().ConvertFrom("#A67B00")), Margin = new Thickness(0) });
                Button cancelBtn = new Button() { Width = 81, Height = 23, Content = "Отменить", Foreground = Brushes.White, FontWeight = FontWeights.Bold, Cursor = Cursors.Hand, Margin = new Thickness(0, 105, 0, 0), HorizontalAlignment = HorizontalAlignment.Right };
                cancelBtn.Style = (Style)OrdersView.Resources["RoundedButtonStyle"];
                cancelBtn.Name = "btn_" + OrderID;
                cancelBtn.Click += DeleteButtonOnClick;

                Grid.SetColumn(sp, 0);
                Grid.SetColumn(Order, 0);
                Grid.SetColumn(FIO, 0);
                Grid.SetColumn(Number, 0);
                Grid.SetColumn(OrderProductText, 0);
                Grid.SetColumn(OrderProduct, 0);
                Grid.SetColumn(OrderAddressText, 1);
                Grid.SetColumn(OrderAddress, 1);
                Grid.SetColumn(OrderStatusCheck, 2);
                Grid.SetColumn(OrderTotalPriceCheck, 2);
                Grid.SetColumn(cancelBtn, 2);

                sp.Children.Add(Order);
                sp.Children.Add(FIO);
                sp.Children.Add(Number);
                sp.Children.Add(OrderProductText);
                sp.Children.Add(OrderProduct);
                mainGrid.Children.Add(OrderAddressText);
                mainGrid.Children.Add(OrderAddress);
                mainGrid.Children.Add(OrderStatusCheck);
                mainGrid.Children.Add(OrderTotalPriceCheck);
                mainGrid.Children.Add(cancelBtn);
                mainGrid.Children.Add(sp);
                borderPanel.Child = mainGrid;
                OrdersView.Children.Add(borderPanel);
            }
        }

        private void ButtonMyProfile_Click(object sender, RoutedEventArgs e)
        {
            EmployeeProfileWindow epw = new EmployeeProfileWindow();
            this.Close();
            epw.ShowDialog();
        }

        private void ButtonCatalog_Click(object sender, RoutedEventArgs e)
        {
            EmployeeMainWindow emw = new EmployeeMainWindow();
            this.Close();
            emw.ShowDialog();
        }

        private void ButtonOrders_Click(object sender, RoutedEventArgs e)
        {
            EmployeeOrderListWindow eolw = new EmployeeOrderListWindow();
            this.Close();
            eolw.ShowDialog();
        }

        private void DeleteButtonOnClick(object sender, EventArgs eventArgs)
        {
            var button = (Button)sender;
            if (button != null)
            {
                using (var db = new Book_StoreEntities())
                {
                    string[] OrderID = button.Name.Split('_');
                    string OrderIDNormal = OrderID[1];
                    var order = db.Orders.FirstOrDefault(o => o.OrderID.ToString() == OrderIDNormal);
                    if (order == null)
                    {
                        return;
                    }
                    SystemContext.Orders = (from o in db.Orders where o.OrderID.ToString() == OrderIDNormal select o).FirstOrDefault();
                    SystemContext.Orders.OrderStatus = "Отменен";
                    db.Entry(SystemContext.Orders).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    OrdersView.Children.Clear();
                    LoadContent();
                }
            }
        }
    }
}
