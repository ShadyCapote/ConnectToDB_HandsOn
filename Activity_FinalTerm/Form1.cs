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
    }
}
