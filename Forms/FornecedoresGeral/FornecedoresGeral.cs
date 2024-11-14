using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using FazendaUrbanaDesktop.Database;

namespace FazendaUrbanaDesktop
{
    public partial class FornecedoresGeral : Form
    {
        public FornecedoresGeral()
        {
            InitializeComponent();
        }

        private void FornecedoresGeral_Load(object sender, EventArgs e)
        {
            fornecedoresListView.Columns.Add("Fornecedor", 100);
            fornecedoresListView.Columns.Add("CPF/CNPJ", 100);
            fornecedoresListView.Columns.Add("E-mail", 100);
            fornecedoresListView.Columns.Add("Telefone", 100);
            fornecedoresListView.Columns.Add("Produtos", 150);

            LoadFornecedoresFromDatabase();
        }

        private void LoadFornecedoresFromDatabase()
        {
            string query = "SELECT Fornecedor, CpfCnpj, Email, Telefone, Produtos FROM Fornecedores";
            using (var reader = DatabaseConnection.ExecuteReader(query))
            {
                fornecedoresListView.Items.Clear();

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["Fornecedor"].ToString());

                    item.SubItems.Add(reader["CpfCnpj"].ToString());
                    item.SubItems.Add(reader["Email"].ToString());
                    item.SubItems.Add(reader["Telefone"].ToString());
                    item.SubItems.Add(reader["Produtos"].ToString());

                    fornecedoresListView.Items.Add(item);
                }
            }
        }

        private void addFornecedorButton_Click(object sender, EventArgs e)
        {
            string fornecedor = fornecedorBox.Text;
            string cpfCnpj = cpfCnpjBox.Text;
            string email = emailBox.Text;
            string telefone = telefoneBox.Text;
            string produtos = produtosBox.Text;

            if (string.IsNullOrEmpty(fornecedor) || string.IsNullOrEmpty(cpfCnpj) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(telefone) || string.IsNullOrEmpty(produtos))
            {
                MessageBox.Show("Por favor, preencha todos os campos.");
                return;
            }

            string insertQuery = "INSERT INTO Fornecedores (Fornecedor, CpfCnpj, Email, Telefone, Produtos) " +
                                 "VALUES (@fornecedor, @cpfCnpj, @email, @telefone, @produtos)";

            using (var connection = DatabaseConnection.GetConnection())
            {
                using (var command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@fornecedor", fornecedor);
                    command.Parameters.AddWithValue("@cpfCnpj", cpfCnpj);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@telefone", telefone);
                    command.Parameters.AddWithValue("@produtos", produtos);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao adicionar o fornecedor: {ex.Message}");
                    }
                }
            }

            fornecedorBox.Clear();
            cpfCnpjBox.Clear();
            emailBox.Clear();
            telefoneBox.Clear();
            produtosBox.Clear();

            LoadFornecedoresFromDatabase();
        }

        private void remover_Click(object sender, EventArgs e)
        {
            if (fornecedoresListView.SelectedItems.Count > 0)
            {
                var selectedItem = fornecedoresListView.SelectedItems[0];
                string fornecedor = selectedItem.SubItems[0].Text;
                string cpfCnpj = selectedItem.SubItems[1].Text;

                string deleteQuery = "DELETE FROM Fornecedores WHERE Fornecedor = @fornecedor AND CpfCnpj = @cpfCnpj";

                using (var connection = DatabaseConnection.GetConnection())
                {
                    using (var command = new SqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@fornecedor", fornecedor);
                        command.Parameters.AddWithValue("@cpfCnpj", cpfCnpj);

                        try
                        {
                            connection.Open();
                            command.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Erro ao remover o fornecedor: {ex.Message}");
                        }
                    }
                }

                fornecedoresListView.Items.Remove(selectedItem);
            }
            else
            {
                MessageBox.Show("Por favor, selecione um item para remover.");
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Menu menu = new Menu();
            menu.Show();

            this.Hide();
        }
    }
}
