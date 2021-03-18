using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace GestionMatos
{
    public partial class FormConnection : Form
    {
        public string login, password;
        public FormConnection()
        {
            InitializeComponent();
        }

        private void annuler_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void valider_Click(object sender, EventArgs e)
        {
            this.login = id_field.Text;
            this.password = pass_field.Text;
            Close();
        }

    }
}
