using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Activity_FinalTerm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void lblIDNumber_Click(object sender, EventArgs e)
        {

           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            ConnectDB.getMaxID();
            lblIDNumber.Text = Instantiate.recordChecker.MaxID.ToString();

            ClearFields();
            LoadData();
        }

        private void LoadData(string search = "")
        {
            string query = "SELECT personal_id, firstname, middlename, lastname, birthdate " +
                           "FROM personal_info " +
                           "WHERE firstname LIKE @s OR middlename LIKE @s OR lastname LIKE @s " +
                           "ORDER BY personal_id";
            var parameters = new Dictionary<string, object> { { "@s", "%" + search + "%" } };
            dgvPersonal.DataSource = ConnectDB.getData(query, parameters);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO personal_info (personal_id, firstname,middlename,lastname, birthdate) VALUES (@id,@firstname, @middlename, @lastname, @birthdate)";

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                {"@id", lblIDNumber.Text},
                {"@firstname", txtFname.Text},
                {"@middlename", txtMname.Text},
                { "@lastname", txtLname.Text},
                {"@birthdate", dtpBdate.Text}
            };

            ConnectDB.saveUpdateDeleteData(query, parameters);
        }

        private bool FieldsAreValid()
        {
            if (string.IsNullOrWhiteSpace(txtFname.Text) || string.IsNullOrWhiteSpace(txtLname.Text))
            {
                MessageBox.Show("First name and last name are required.", "Missing data",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void ClearFields()
        {
            txtFname.Clear();
            txtMname.Clear();
            txtLname.Clear();
            dtpBdate.Value = DateTime.Today;
            ConnectDB.getMaxID();
            lblIDNumber.Text = Instantiate.recordChecker.MaxID.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show("Delete this record?", "Confirm delete",
        MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            string query = "DELETE FROM personal_info WHERE personal_id=@id";
            var parameters = new Dictionary<string, object> { { "@id", lblIDNumber.Text } };
            ConnectDB.saveUpdateDeleteData(query, parameters);

            MessageBox.Show("Record deleted.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            LoadData(txtSearch.Text);
            ClearFields();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dgvPersonal_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvPersonal.Rows[e.RowIndex];

            lblIDNumber.Text = row.Cells["personal_id"].Value.ToString();
            txtFname.Text = row.Cells["firstname"].Value.ToString();
            txtMname.Text = row.Cells["middlename"].Value.ToString();
            txtLname.Text = row.Cells["lastname"].Value.ToString();
            dtpBdate.Value = Convert.ToDateTime(row.Cells["birthdate"].Value);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadData(txtSearch.Text);

        }
    }
}
