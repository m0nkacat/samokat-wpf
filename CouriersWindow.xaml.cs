using System;
using System.Linq;
using System.Windows;

namespace SamokatAppClean
{
    public partial class CouriersWindow : Window
    {
        private SamokatEntities context = new SamokatEntities();
        public CouriersWindow() { InitializeComponent(); LoadData(); }
        private void LoadData() => dgCouriers.ItemsSource = context.couriers.ToList();
        private void Search()
        {
            string term = txtSearch.Text.Trim();
            dgCouriers.ItemsSource = string.IsNullOrEmpty(term)
                ? context.couriers.ToList()
                : context.couriers.Where(c => c.full_name.Contains(term) || c.phone.Contains(term)).ToList();
        }
        private void btnSearch_Click(object sender, RoutedEventArgs e) => Search();
        private void dgCouriers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) { }
        private void btnAdd_Click(object sender, RoutedEventArgs e) { if (new CourierEditDialog(context).ShowDialog() == true) LoadData(); }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgCouriers.SelectedItem as couriers;
            if (selected == null) { MessageBox.Show("Выберите курьера"); return; }
            if (new CourierEditDialog(context, selected).ShowDialog() == true) LoadData();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgCouriers.SelectedItem as couriers;
            if (selected == null) { MessageBox.Show("Выберите курьера"); return; }
            if (context.orders.Any(o => o.courier_id == selected.id))
                if (MessageBox.Show("Есть заказы, удалить каскадно?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning) != MessageBoxResult.Yes)
                    return;
            context.couriers.Remove(selected);
            context.SaveChanges();
            LoadData();
            txtStatus.Text = "Удалено";
        }
        private void btnBack_Click(object sender, RoutedEventArgs e) { new MainWindow().Show(); Close(); }
    }
}