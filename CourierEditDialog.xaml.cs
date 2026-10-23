using System;
using System.Linq;
using System.Windows;

namespace SamokatAppClean
{
    public partial class CourierEditDialog : Window
    {
        private SamokatEntities context;
        private couriers editingCourier;
        private bool isEdit;

        public CourierEditDialog(SamokatEntities ctx, couriers courier = null)
        {
            InitializeComponent();
            context = ctx;
            cbTransport.ItemsSource = context.transport.ToList();
            if (courier != null)
            {
                editingCourier = courier;
                isEdit = true;
                txtFullName.Text = editingCourier.full_name;
                txtPhone.Text = editingCourier.phone;
                cbTransport.SelectedValue = editingCourier.transport_id;
            }
        }

        private bool Validate()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                txtError.Text = "Введите ФИО";
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                txtError.Text = "Введите телефон";
                return false;
            }
            bool phoneExists = context.couriers.Any(c => c.phone == txtPhone.Text.Trim() && (!isEdit || c.id != editingCourier.id));
            if (phoneExists)
            {
                txtError.Text = "Телефон уже существует";
                return false;
            }
            txtError.Text = "";
            return true;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate()) return;
            try
            {
                if (!isEdit)
                {
                    couriers newCourier = new couriers
                    {
                        full_name = txtFullName.Text.Trim(),
                        phone = txtPhone.Text.Trim(),
                        transport_id = cbTransport.SelectedValue as int?
                    };
                    context.couriers.Add(newCourier);
                }
                else
                {
                    editingCourier.full_name = txtFullName.Text.Trim();
                    editingCourier.phone = txtPhone.Text.Trim();
                    editingCourier.transport_id = cbTransport.SelectedValue as int?;
                }
                context.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}