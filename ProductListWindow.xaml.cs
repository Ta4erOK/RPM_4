using Lab3.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Lab3
{
    /// <summary>
    /// Логика взаимодействия для ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        public User? User { get; init; }

        public bool CanFilterProducts => User != null;

        public ObservableCollection<Product> Products { get; } = new();

        public ICollectionView ProductsView { get; }

        private string _searchText = "";

        public ProductListWindow() : this(null)
        {
        }

        public ProductListWindow(User? user)
        {
            InitializeComponent();
            User = user;
            DataContext = this;

            ProductsView = CollectionViewSource.GetDefaultView(Products);
            ProductsView.Filter = FilterProduct;

            ReloadProducts();
        }

        private void ReloadProducts()
        {
            Products.Clear();

            using var context = new Lab3Context();

            var dbProducts = context.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.Supplier)
                .Include(p => p.ProductName);

            foreach (var prod in dbProducts)
                Products.Add(prod);
        }

        /// Фильтр поиска:
        private bool FilterProduct(object obj)
        {
            if (obj is not Product p)
                return false;

            if (string.IsNullOrWhiteSpace(_searchText))
                return true;

            var haystack = string.Join(" ",
                p.Article, p.Name,
                p.Category?.Name, p.Description,
                p.Manufacturer?.Name, p.Supplier?.Name,
                p.Unit);
            haystack = haystack.ToLowerInvariant();

var tokens = _searchText.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            foreach (var token in tokens)
            {
                if (!haystack.Contains(token.ToLowerInvariant()))
                    return false;
            }
            return true;
        }

        private void SearchTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            _searchText = SearchTextBox.Text ?? "";
            ProductsView.Refresh();
        }

        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            var win = new MainWindow();
            win.Show();
            this.Close();
        }
    }
}