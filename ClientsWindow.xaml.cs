using System;
using System.Linq;
using System.Windows;

namespace SamokatAppClean
{
    public partial class ClientsWindow : Window
    {
        private SamokatEntities context = new SamokatEntities();
        public ClientsWindow() { InitializeComponent(); LoadData(); }
        private void LoadData() => dgClients.ItemsSource = context.clients.ToList();
        private void Search()
        {
            string term = txtSearch.Text.Trim();
            dgClients.ItemsSource = string.IsNullOrEmpty(term)
                ? context.clients.ToList()
                : context.clients.Where(c => c.full_name.Contains(term) || c.phone.Contains(term)).ToList();
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e) => Search();
        private void dgClients_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) { }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (new ClientEditDialog(context).ShowDialog() == true) LoadData();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgClients.SelectedItem as clients;
            if (selected == null) { MessageBox.Show("Выберите клиента"); return; }
            if (new ClientEditDialog(context, selected).ShowDialog() == true) LoadData();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgClients.SelectedItem as clients;
            if (selected == null) { MessageBox.Show("Выберите клиента"); return; }
            if (context.orders.Any(o => o.client_id == selected.id))
                if (MessageBox.Show("Есть заказы, удалить каскадно?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                    return;
            context.clients.Remove(selected);
            context.SaveChanges();
            LoadData();
            txtStatus.Text = "Удалено";
        }
        private void btnBack_Click(object sender, RoutedEventArgs e) { new MainWindow().Show(); Close(); }
    }
}