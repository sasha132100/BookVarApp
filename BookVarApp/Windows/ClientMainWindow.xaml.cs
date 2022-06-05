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
    /// Логика взаимодействия для ClientMainWindow.xaml
    /// </summary>
    public partial class ClientMainWindow : Window
    {
        Boolean isCatalog;
        Dictionary<int, TextBox> quantityList = new Dictionary<int, TextBox>();
        public ClientMainWindow()
        {
            InitializeComponent();
            isCatalog = true;
            LoadContent();
            try
            {
                btnProfile.Content = SystemContext.User.UserLogin;
                btnProfile.Click += ButtonMyProfile_Click;
            }
            catch { }

            if (SystemContext.typeWindow == "Каталог")
            {
                goOrderBtn.Visibility = Visibility.Hidden;
                isCatalog = true;
                SetButton();
                contentPanel.Children.Clear();
                LoadContent();
            }
            else if (SystemContext.typeWindow == "Корзина")
            {
                goOrderBtn.Visibility = Visibility.Visible;
                isCatalog = false;
                SetButton();
                contentPanel.Children.Clear();
                LoadContent();
            }
            else { MessageBox.Show("Произошла ошибка"); }
        }
        void LoadContent(string searchText = "")
        {
            using (var db = new Book_StoreEntities())
            {
                List<Product> products;
                try
                {
                    if (searchText == "")
                    {
                        if (isCatalog)
                            products = (from p in db.Product select p).ToList<Product>();
                        else
                            products = LoadShopCartProducts(db);
                    }
                    else
                    {
                        IEnumerable<Product> productsSet;
                        if (isCatalog)
                            productsSet = (from p in db.Product select p);
                        else
                            productsSet = LoadShopCartProducts(db);
                        products = productsSet.Where(product => product.ProductName.Contains($"{searchText}")).ToList<Product>();
                    }
                    int i = 0;
                    foreach (var product in products)
                    {
                        AddProductPanel(product, product.ProductName, product.ProductAuthor, product.ProductCount, product.ProductPrice);
                        i++;
                    }
                }
                catch { }
            }
        }

        private List<Product> LoadShopCartProducts(Book_StoreEntities db)
        {
            var shopCart = (from p in db.Product
                            join sc in db.Basket on p.ProductID equals sc.ProductID
                            where sc.ClientID == SystemContext.Client.ClientID
                            select p).ToList<Product>();
            return shopCart;
        }
        public BitmapSource ByteArrayToImage(byte[] buffer)
        {
            using (var stream = new System.IO.MemoryStream(buffer))
            {
                return BitmapFrame.Create(stream,
                    BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
            }
        }
        void AddProductPanel(Product product, string name, string purpose, int quantity, int price)
        {
            var borderPanel = new Border() { BorderBrush = Brushes.LightGray, BorderThickness = new Thickness(2), Style = (Style)contentPanel.Resources["contentBorderStyle"] };
            StackPanel sp = new StackPanel() { };
            sp.Tag = product;
            sp.MouseLeftButtonDown += Sp_MouseLeftButtonDown;
            Image img = new Image() { };
            if (product.ProductImage != null)
                img.Source = ByteArrayToImage(product.ProductImage);
            TextBlock nameUp = new TextBlock() { Margin = new Thickness(17, -28, 0, 0), FontWeight = FontWeights.Bold, Foreground = (Brush)(new BrushConverter().ConvertFrom("#A67B00")), FontSize = 16 };
            StackPanel.SetZIndex(nameUp, 1);
            TextBlock backNameUp = new TextBlock() { Foreground = Brushes.White, Margin = new Thickness(0, -25, 0, 0), FontSize = 14, Background = Brushes.White };
            backNameUp.Effect = new System.Windows.Media.Effects.BlurEffect() { Radius = 10, KernelType = System.Windows.Media.Effects.KernelType.Gaussian };
            TextBlock purposeTxt = new TextBlock() { Text = "Назначение: " };
            TextBlock availabilityTxt = new TextBlock() { Text = "Количество: ", Margin = new Thickness(12, 0, 3, 0) };
            TextBlock priceTxt = new TextBlock() { Text = "Цена: " };
            priceTxt.Inlines.Add(new TextBlock() { Text = $" {price} руб.", Foreground = (Brush)(new BrushConverter().ConvertFrom("#A67B00")), Margin = new Thickness(0) });

            //Bottom
            StackPanel bottomSp = new StackPanel() { Orientation = Orientation.Horizontal, Margin = new Thickness(12, 0, 0, 0) };
            Button reduceBtn = new Button() { Tag = product.ProductID, Width = 30, Height = 40, Background = Brushes.Transparent, Content = "-", FontWeight = FontWeights.Bold, FontSize = 20, BorderThickness = new Thickness(0) };
            reduceBtn.Click += ButtonReduce_Click;
            Border borderqTxt = new Border() { Width = 40, Height = 25, CornerRadius = new CornerRadius(5), BorderThickness = new Thickness(1), BorderBrush = Brushes.Gray };
            TextBox quantityTxtBox = new TextBox() { MaxLength = 3, Text = "1", BorderThickness = new Thickness(0), Background = Brushes.Transparent, FontWeight = FontWeights.Bold, FontSize = 14, TextAlignment = TextAlignment.Center };
            borderqTxt.Child = quantityTxtBox;
            if (!isCatalog)
            {
                using (var db = new Book_StoreEntities())
                {
                    var client = SystemContext.Client;
                    var productShopCart = (from sc in db.Basket where sc.ClientID == client.ClientID & sc.ProductID == product.ProductID select sc).FirstOrDefault<Basket>();
                    quantityTxtBox.Text = productShopCart.BasketProductCount.ToString();
                    quantityTxtBox.Tag = productShopCart;
                }
            }
            quantityTxtBox.TextChanged += QuantityTxtBox_TextChanged;
            quantityList[product.ProductID] = quantityTxtBox;
            Button increaseBtn = new Button() { Tag = product.ProductID, Width = 30, Height = 40, Background = Brushes.Transparent, Content = "+", FontWeight = FontWeights.Bold, FontSize = 20, BorderThickness = new Thickness(0) };
            increaseBtn.Click += ButtonIncrease_Click;
            Button addBtn = new Button() { Width = 80, Height = 30, Content = "Добавить", Foreground = Brushes.White, Margin = new Thickness(12, 0, 0, 0), Cursor = Cursors.Hand };

            if (isCatalog)
                addBtn.Click += ButtonAdd_Click;
            else
            {
                addBtn.Content = "Убрать";
                addBtn.Click += ButtonRemove_Click;
            }
            addBtn.Style = (Style)contentPanel.Resources["RoundedButtonStyle"];
            addBtn.Tag = product;
            bottomSp.Children.Add(reduceBtn);
            bottomSp.Children.Add(borderqTxt);
            bottomSp.Children.Add(increaseBtn);
            bottomSp.Children.Add(addBtn);
            var borderPanel2 = new Border() { CornerRadius = new CornerRadius(0, 0, 10, 10), Background = (Brush)(new BrushConverter().ConvertFrom("#f6f6f6")) };

            //добавление данных
            nameUp.Text += name;
            backNameUp.Text += name;
            purposeTxt.Text += purpose;
            availabilityTxt.Text += quantity.ToString();

            //Добавление элементов в контейнер
            var sp2 = new StackPanel() { MinHeight = 73, Background = (Brush)(new BrushConverter().ConvertFrom("#f6f6f6")) };
            sp.Children.Add(img);
            sp.Children.Add(nameUp);
            sp.Children.Add(backNameUp);
            sp2.Children.Add(purposeTxt);
            sp2.Children.Add(availabilityTxt);
            sp2.Children.Add(priceTxt);
            borderPanel2.Child = bottomSp;
            borderPanel.Child = sp;
            sp.Children.Add(sp2);
            sp.Children.Add(borderPanel2);
            contentPanel.Children.Add(borderPanel);
        }

        private void Sp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SystemContext.Product = (sender as StackPanel).Tag as Product;
            var piw = new ProductInfoWindow();
            this.Close();
            piw.ShowDialog();
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new Book_StoreEntities())
            {
                var client = SystemContext.Client;
                var product = ((Product)(sender as Button).Tag);
                var productShopCart = (from sc in db.Basket where sc.ClientID == client.ClientID & sc.ProductID == product.ProductID select sc).FirstOrDefault<Basket>();
                db.Entry(productShopCart).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            ButtonCart_Click(this, new RoutedEventArgs());
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new Book_StoreEntities())
            {
                var client = SystemContext.Client;
                var product = ((Product)(sender as Button).Tag);
                var productShopCart = (from sc in db.Basket where sc.ClientID == client.ClientID & sc.ProductID == product.ProductID select sc).FirstOrDefault<Basket>();
                if (productShopCart == null)
                    db.Basket.Add(new Basket() { ClientID = client.ClientID, ProductID = product.ProductID, BasketProductCount = Convert.ToInt32(quantityList[product.ProductID].Text) });
                else
                {
                    productShopCart.BasketProductCount += Convert.ToInt32(quantityList[product.ProductID].Text);
                    db.Entry(productShopCart).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();
            }
        }

        private void QuantityTxtBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32((sender as TextBox).Text) < 1)
                    (sender as TextBox).Text = "1";
                if (!isCatalog)
                {
                    using (var db = new Book_StoreEntities())
                    {
                        var productShopCart = ((Basket)(sender as TextBox).Tag);
                        productShopCart.BasketProductCount = Convert.ToInt32((sender as TextBox).Text);
                        db.Entry(productShopCart).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                if ((sender as TextBox).Text != "")
                    (sender as TextBox).Text = "1";
            }
        }
        private void ButtonIncrease_Click(object sender, RoutedEventArgs e)
        {
            int tag = Convert.ToInt32(((Button)sender).Tag);
            quantityList[tag].Text = (Convert.ToInt32(quantityList[tag].Text) + 1).ToString();
        }

        private void ButtonReduce_Click(object sender, RoutedEventArgs e)
        {
            int tag = Convert.ToInt32(((Button)sender).Tag);
            int num = Convert.ToInt32(quantityList[tag].Text);
            quantityList[tag].Text = (num - 1).ToString();
        }
        private void ButtonCatalog_Click(object sender, RoutedEventArgs e)
        {
            goOrderBtn.Visibility = Visibility.Hidden;
            isCatalog = true;
            SetButton();
            contentPanel.Children.Clear();
            LoadContent();
        }
        private void ButtonMyOrders_Click(object sender, RoutedEventArgs e)
        {
            OrderListWindow olw = new OrderListWindow();
            this.Close();
            olw.ShowDialog();
        }
        private void ButtonCart_Click(object sender, RoutedEventArgs e)
        {
            goOrderBtn.Visibility = Visibility.Visible;
            isCatalog = false;
            SetButton();
            contentPanel.Children.Clear();
            LoadContent();
        }

        private void SetButton()
        {
            if (isCatalog)
            {
                txtCatalog.TextDecorations = TextDecorations.Underline;
                txtShopCart.TextDecorations = null;
            }
            else
            {
                txtShopCart.TextDecorations = TextDecorations.Underline;
                txtCatalog.TextDecorations = null;
            }
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            contentPanel.Children.Clear();
            LoadContent(searchTxt.Text);
        }

        private void ButtonMyProfile_Click(object sender, RoutedEventArgs e)
        {
            UserProfileWindow upw = new UserProfileWindow();
            this.Close();
            upw.ShowDialog();
        }

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderingWindow ow = new OrderingWindow();
            this.Close();
            ow.ShowDialog();
        }
    }
}
