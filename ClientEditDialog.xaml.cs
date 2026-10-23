using System;
using System.Linq;
using System.Windows;

namespace SamokatAppClean
{
    public partial class ClientEditDialog : Window
    {
        private SamokatEntities context;
        private clients editingClient;
        private bool isEdit;
        public ClientEditDialog(SamokatEntities ctx, clients client = null)
        {
            InitializeComponent();
            context = ctx;
            if (client != null)
            {
                editingClient = client;
                isEdit = true;
                txtFullName.Text = editingClient.full_name;
                txtPhone.Text = editingClient.phone;
                txtAddress.Text = editingClient.address;
            }
        }
        private bool Validate()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text)) { txtError.Text = "Введите ФИО"; return false; }
            if (string.IsNullOrWhiteSpace(txtPhone.Text)) { txtError.Text = "Введите телефон"; return false; }
            if (string.IsNullOrWhiteSpace(txtAddress.Text)) { txtError.Text = "Введите адрес"; return false; }
            bool phoneExists = context.clients.Any(c => c.phone == txtPhone.Text.Trim() && (!isEdit || c.id != editingClient.id));
            if (phoneExists) { txtError.Text = "Телефон уже существует"; return false; }
            txtError.Text = "";
            return true;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!Validate()) return;
            try
            {
                if (!isEdit)
                    context.clients.Add(new clients { full_name = txtFullName.Text.Trim(), phone = txtPhone.Text.Trim(), address = txtAddress.Text.Trim() });
                else
                {
                    editingClient.full_name = txtFullName.Text.Trim();
                    editingClient.phone = txtPhone.Text.Trim();
                    editingClient.address = txtAddress.Text.Trim();
                }
                context.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch (Exception ex) { MessageBox.Show($"Ошибка: {ex.Message}"); }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e) { DialogResult = false; Close(); }
    }
}