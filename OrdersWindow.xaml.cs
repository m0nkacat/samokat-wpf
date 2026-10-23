using System;
using System.Linq;
using System.Windows;

namespace SamokatAppClean
{
    public partial class OrdersWindow : Window
    {
        private SamokatEntities context = new SamokatEntities();
        public OrdersWindow() { InitializeComponent(); LoadData(); }
        private void LoadData() => dgOrders.ItemsSource = context.orders.ToList();
        private void Search()
        {
            string term = txtSearch.Text.Trim();
            dgOrders.ItemsSource = string.IsNullOrEmpty(term)
                ? context.orders.ToList()
                : context.orders.Where(o => o.delivery_address.Contains(term) || o.status.Contains(term) || o.clients.full_name.Contains(term)).ToList();
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e) => Search();
        private void dgOrders_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) { }
        private void btnAdd_Click(object sender, RoutedEventArgs e) { if (new OrderEditDialog(context).ShowDialog() == true) LoadData(); }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgOrders.SelectedItem as orders;
            if (selected == null) { MessageBox.Show("Выберите заказ"); return; }
            if (new OrderEditDialog(context, selected).ShowDialog() == true) LoadData();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgOrders.SelectedItem as orders;
            if (selected == null) { MessageBox.Show("Выберите заказ"); return; }
            context.orders.Remove(selected);
            context.SaveChanges();
            LoadData();
            txtStatus.Text = "Удалено";
        }
        private void btnBack_Click(object sender, RoutedEventArgs e) { new MainWindow().Show(); Close(); }
    }
}