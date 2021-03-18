using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.IO;
using System.Configuration;
using static Projet_PPE_GM.gestionmatos1DataSet;
using static System.Convert;

namespace GestionMatos
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void hash(string nom, int id)
        {
            SHA256 hach = SHA256.Create();
            byte[] hashValue = hach.ComputeHash(Encoding.ASCII.GetBytes(nom));
            var hashedString = Convert.ToBase64String(hashValue);

            string strcon = ConfigurationManager.ConnectionStrings["GM"].ToString();
            MySqlConnection cnSQL = null;
            MySqlCommand cmSQL = null;
            cnSQL = new MySqlConnection(strcon);
            cnSQL.Open();
            string sql = "update user set password = '" + hashedString + "' where id = " + id;
            cmSQL = new MySqlCommand(sql, cnSQL);
            cmSQL.ExecuteNonQuery();
            cnSQL.Close();
        }



        private void FormMain_Load(object sender, EventArgs e)            
        {
            // TODO: cette ligne de code charge les données dans la table 'gestionmatos1DataSet.intervention'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.interventionTableAdapter.Fill(this.gestionmatos1DataSet.intervention);
            // TODO: cette ligne de code charge les données dans la table 'gestionmatos1DataSet.marque'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.marqueTableAdapter.Fill(this.gestionmatos1DataSet.marque);
            // TODO: cette ligne de code charge les données dans la table 'gestionmatos1DataSet.client'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.clientTableAdapter.Fill(this.gestionmatos1DataSet.client);
            // TODO: cette ligne de code charge les données dans la table 'gestionmatos1DataSet.site'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.siteTableAdapter.Fill(this.gestionmatos1DataSet.site);
            // TODO: cette ligne de code charge les données dans la table 'gestionmatos1DataSet.materiel'. Vous pouvez la déplacer ou la supprimer selon les besoins.
            this.materielTableAdapter.Fill(this.gestionmatos1DataSet.materiel);

            // Connexion base
            string strcon = "Server=localhost;Database=gestionmatos1;Uid=root;Pwd=;";
            MySqlConnection cnSQL = null;
            MySqlCommand cmSQL = null;
            MySqlDataReader drSQL = null;
            string strSQL;

            cnSQL = new MySqlConnection(strcon);
            cnSQL.Open();

            bool vapas;
            do
            {
                vapas = false;
                FormConnection dlg = new FormConnection();
                DialogResult res = dlg.ShowDialog();
                if (res == DialogResult.OK)
                {
                    // hachage du password
                    SHA256 hach = SHA256.Create();
                    byte[] hashValue = hach.ComputeHash(Encoding.ASCII.GetBytes(dlg.password));
                    var hashedPassword = Convert.ToBase64String(hashValue);

                    strSQL = "select count(*) from user where login = '" + dlg.login + "' and password = '" + hashedPassword + "'";
                    cmSQL = new MySqlCommand(strSQL, cnSQL);
                    drSQL = cmSQL.ExecuteReader();
                    drSQL.Read();
                    int compteur = Convert.ToInt32(drSQL[0]);
                    drSQL.Close();
                    drSQL = null;

                    if (compteur == 0)
                    {
                        MessageBox.Show("Identifiants incorrect", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Exclamation); 
                        vapas = true;
                    }
                }
                else
                {
                    Application.Exit();
                }
            }
            while (vapas == true);

            if (drSQL != null)
                drSQL.Close();
            cnSQL.Close();
     
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            materielTableAdapter.Update(this.gestionmatos1DataSet.materiel);
            siteTableAdapter.Update(this.gestionmatos1DataSet.site);
            clientTableAdapter.Update(this.gestionmatos1DataSet.client);
            marqueTableAdapter.Update(this.gestionmatos1DataSet.marque);
            interventionTableAdapter.Update(this.gestionmatos1DataSet.intervention);
        }

       // ZONE BOUTON ADD

        public string name, serie, site, client, marque;
        public decimal mtbf;
        public DateTime date;
        private void buttonAddMateriel_Click(object sender, EventArgs e)
        {
            this.name = textBoxName.Text;
            this.serie = textBoxSerie.Text;
            string ladate = dateTimePickerInstall.Value.ToString("yyyy-MM-dd");
            this.mtbf = numericUpDownMTBF.Value;

            name = name.Replace("'", "''");

            int dsite = (int)(((ListControl)(comboBoxSite.DataBindings.Control)).SelectedValue);
            int dclient = (int)(((ListControl)(comboBoxClient.DataBindings.Control)).SelectedValue);
            int dmarque = (int)(((ListControl)(comboBoxMarque.DataBindings.Control)).SelectedValue);

            string strcon = "Server=localhost;Database=gestionmatos1;Uid=root;Pwd=;";
            MySqlConnection cnSQL = null;
            MySqlCommand cmSQL = null;
            string strSQL;

            cnSQL = new MySqlConnection(strcon);
            cnSQL.Open();
            strSQL = "INSERT INTO `materiel` (`Nom`, `NoSerie`, `Date_Installation`, `MTBF`, `ID_Site`, `ID_Client`, `ID_Marque`) " +
                "VALUES ('"+ name + "', '" + serie + "', '" + ladate + "', " + mtbf + ", " + dsite.ToString() + ", " + dclient.ToString() + ", " + dmarque.ToString() + ")";

            cmSQL = new MySqlCommand(strSQL, cnSQL);
            cmSQL.ExecuteNonQuery();
            cnSQL.Close();
            this.materielTableAdapter.Fill(this.gestionmatos1DataSet.materiel);
        }

        public string nameSite, adresseSite;
        private void buttonAddSite_Click(object sender, EventArgs e)
        {
            nameSite = textBoxNameSite.Text;
            adresseSite = textBoxAdresseSite.Text;

            string strcon = "Server=localhost;Database=gestionmatos1;Uid=root;Pwd=;";
            MySqlConnection cnSQL = null;
            MySqlCommand cmSQL = null;
            string strSQL;

            nameSite = nameSite.Replace("'", "''");
            adresseSite = adresseSite.Replace("'", "''");

            cnSQL = new MySqlConnection(strcon);
            cnSQL.Open();
             strSQL = "INSERT INTO `site` (`Nom`, `Adresse`) VALUES ('" + nameSite + "', '" + adresseSite + "')";

            cmSQL = new MySqlCommand(strSQL, cnSQL);
            cmSQL.ExecuteNonQuery();
            cnSQL.Close();
            this.siteTableAdapter.Fill(this.gestionmatos1DataSet.site);
        }

        public string nameClient, adresseClient, telClient, emailClient;
        private void buttonAddClient_Click(object sender, EventArgs e)
        {
            nameClient = textBoxNameClient.Text;
            adresseClient = textBoxAdresseClient.Text;
            telClient = textBoxTelClient.Text;
            emailClient = textBoxMailClient.Text;

            string strcon = "Server=localhost;Database=gestionmatos1;Uid=root;Pwd=;";
            MySqlConnection cnSQL = null;
            MySqlCommand cmSQL = null;
            string strSQL;

            nameClient = nameClient.Replace("'", "''");
            adresseClient = adresseClient.Replace("'", "''");
            emailClient = emailClient.Replace("'", "''");


            cnSQL = new MySqlConnection(strcon);
            cnSQL.Open();
            strSQL = "INSERT INTO `client` (`Nom`, `Adresse`, `Telephone`, `mail`) VALUES ('" + nameClient + "', '" + adresseClient + "', '" + telClient + "', '" + emailClient + "')";

            cmSQL = new MySqlCommand(strSQL, cnSQL);
            cmSQL.ExecuteNonQuery();
            cnSQL.Close();
            this.clientTableAdapter.Fill(this.gestionmatos1DataSet.client);
        }

        public string nameMarque, descMarque;
        private void buttonAddMarque_Click(object sender, EventArgs e)
        {
            nameMarque = textBoxNameMarque.Text;
            descMarque = textBoxDescMarque.Text;

            string strcon = "Server=localhost;Database=gestionmatos1;Uid=root;Pwd=;";
            MySqlConnection cnSQL = null;
            MySqlCommand cmSQL = null;
            string strSQL;

            nameMarque = nameMarque.Replace("'", "''");
            descMarque = descMarque.Replace("'", "''");


            cnSQL = new MySqlConnection(strcon);
            cnSQL.Open();
            strSQL = "INSERT INTO `marque` (`Nom`, `Description`) VALUES ('" + nameMarque + "', '" + descMarque + "')";

            cmSQL = new MySqlCommand(strSQL, cnSQL);
            cmSQL.ExecuteNonQuery();
            cnSQL.Close();
            this.marqueTableAdapter.Fill(this.gestionmatos1DataSet.marque);
        }

        

        public string commentaireInter;
        public DateTime dateInter;

        private void buttonAddInter_Click(object sender, EventArgs e)
        {
            int interMateriel = (int)(((ListControl)(comboBoxMateriel.DataBindings.Control)).SelectedValue);
            commentaireInter = textBoxInterCommentaire.Text;
            string ladateInter = dateTimePickerInstall.Value.ToString("yyyy-MM-dd");


            string strcon = "Server=localhost;Database=gestionmatos1;Uid=root;Pwd=;";
            MySqlConnection cnSQL = null;
            MySqlCommand cmSQL = null;
            string strSQL;

            cnSQL = new MySqlConnection(strcon);
            cnSQL.Open();

            commentaireInter = commentaireInter.Replace("'", "''");

            strSQL = "INSERT INTO `intervention` (`Materiel`, `Date_planifiee`, `Commentaire`) " +
                "VALUES (" + interMateriel.ToString() + ", '" + ladateInter + "', '" + commentaireInter + "')";

            cmSQL = new MySqlCommand(strSQL, cnSQL);
            cmSQL.ExecuteNonQuery();
            cnSQL.Close();
            this.interventionTableAdapter.Fill(this.gestionmatos1DataSet.intervention);
        }

        // ZONE BOUTON DELETE

        private void buttonDelMateriel_Click(object sender, EventArgs e)
        {
            if (dataGridViewMateriel.SelectedRows.Count == 1)
            {
                int iddelmateriel = (int)(dataGridViewMateriel.SelectedRows[0].Cells[0].Value);

                string strcon = "Server=localhost;Database=gestionmatos1;Uid=root;Pwd=;";
                MySqlConnection cnSQL = null;
                MySqlCommand cmSQL = null;
                string strSQL;

                cnSQL = new MySqlConnection(strcon);
                cnSQL.Open();
                strSQL = "delete from materiel where id = " + iddelmateriel.ToString();

                cmSQL = new MySqlCommand(strSQL, cnSQL);
                cmSQL.ExecuteNonQuery();
                cnSQL.Close();
                this.materielTableAdapter.Fill(this.gestionmatos1DataSet.materiel);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une ligne en cliquant sur la case tout à gauche", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonDelSite_Click(object sender, EventArgs e)
        {
            if (dataGridViewSite.SelectedRows.Count == 1)
            {
                string strcon = "Server=localhost;Database=gestionmatos1;Uid=root;Pwd=;";
                MySqlConnection cnSQL = null;
                MySqlCommand cmSQL = null;
                cnSQL = new MySqlConnection(strcon);
                try
                {
                    int iddelsite = (int)(dataGridViewSite.SelectedRows[0].Cells[0].Value);
                    string strSQL;
                    cnSQL.Open();
                    strSQL = "delete from site where id = " + iddelsite.ToString();
                    cmSQL = new MySqlCommand(strSQL, cnSQL);
                    cmSQL.ExecuteNonQuery();
                }
                catch (MySqlException error)
                {
                    MessageBox.Show("Suppression impossible, ce site est utilisé comme argument pour la section Matériels", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    cnSQL.Close();
                    this.siteTableAdapter.Fill(this.gestionmatos1DataSet.site);
                }

            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une ligne en cliquant sur la case tout à gauche", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonDelClient_Click(object sender, EventArgs e)
        {            
            if (dataGridViewClient.SelectedRows.Count == 1)
            {
                string strcon = "Server=localhost;Database=gestionmatos1;Uid=root;Pwd=;";
                MySqlConnection cnSQL = null;
                MySqlCommand cmSQL = null;
                cnSQL = new MySqlConnection(strcon);
                try
                {
                    int iddelclient = (int)(dataGridViewClient.SelectedRows[0].Cells[0].Value);
                    string strSQL;
                    cnSQL.Open();
                    strSQL = "delete from client where id = " + iddelclient.ToString();
                    cmSQL = new MySqlCommand(strSQL, cnSQL);
                    cmSQL.ExecuteNonQuery();
                }
                catch (MySqlException error)
                {
                    MessageBox.Show("Suppression impossible, ce site est utilisé comme argument pour la section Matériels", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    cnSQL.Close();
                    this.clientTableAdapter.Fill(this.gestionmatos1DataSet.client);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une ligne en cliquant sur la case tout à gauche", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonDelMarque_Click(object sender, EventArgs e)
        {            
            if (dataGridViewMarque.SelectedRows.Count == 1)
            {
                string strcon = "Server=localhost;Database=gestionmatos1;Uid=root;Pwd=;";
                MySqlConnection cnSQL = null;
                MySqlCommand cmSQL = null;
                cnSQL = new MySqlConnection(strcon);
                try
                {
                    int iddelmarque = (int)(dataGridViewMarque.SelectedRows[0].Cells[0].Value);
                    string strSQL;
                    cnSQL.Open();
                    strSQL = "delete from marque where id = " + iddelmarque.ToString();
                    cmSQL = new MySqlCommand(strSQL, cnSQL);
                    cmSQL.ExecuteNonQuery();
                }
                catch (MySqlException error)
                {
                    MessageBox.Show("Suppression impossible, cette marque est utilisé comme argument pour la section Matériels", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    cnSQL.Close();
                    this.marqueTableAdapter.Fill(this.gestionmatos1DataSet.marque);
                }
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une ligne en cliquant sur la case tout à gauche", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonDelInter_Click(object sender, EventArgs e)
        {
            if (dataGridViewIntervention.SelectedRows.Count == 1)
            {
                int iddelInter = (int)(dataGridViewIntervention.SelectedRows[0].Cells[0].Value);

                string strcon = "Server=localhost;Database=gestionmatos1;Uid=root;Pwd=;";
                MySqlConnection cnSQL = null;
                MySqlCommand cmSQL = null;
                string strSQL;

                cnSQL = new MySqlConnection(strcon);
                cnSQL.Open();
                strSQL = "delete from intervention where id = " + iddelInter.ToString();

                cmSQL = new MySqlCommand(strSQL, cnSQL);
                cmSQL.ExecuteNonQuery();
                cnSQL.Close();
                this.interventionTableAdapter.Fill(this.gestionmatos1DataSet.intervention);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une ligne en cliquant sur la case tout à gauche", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // ZONE BOUTON MODIFIER

        private void dataGridViewMateriel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int idMatos = Convert.ToInt32(dataGridViewMateriel.CurrentRow.Cells[0].Value);

            string strcon = "Server=localhost;Database=gestionmatos1;Uid=root;Pwd=;";
            MySqlConnection cnSQL = null;
            MySqlCommand cmSQL = null;
            string strSQL;

            cnSQL = new MySqlConnection(strcon);
            cnSQL.Open();
            strSQL = "SELECT * FROM materiel WHERE id = " + idMatos;

            cmSQL = new MySqlCommand(strSQL, cnSQL);
            cmSQL.ExecuteNonQuery();
            cnSQL.Close();

            textBoxName.Text = dataGridViewMateriel.CurrentRow.Cells[1].Value.ToString();
            textBoxSerie.Text = dataGridViewMateriel.CurrentRow.Cells[2].Value.ToString();
            dateTimePickerInstall.Value = Convert.ToDateTime(dataGridViewMateriel.CurrentRow.Cells[3].Value);
            numericUpDownMTBF.Text = dataGridViewMateriel.CurrentRow.Cells[4].Value.ToString();
            int a = Convert.ToInt32(dataGridViewMateriel.CurrentRow.Cells[5].Value);
            
            // comboBoxSite.SelectedValue = dataGridViewMateriel.CurrentRow.Cells[5].Value.ToString();
            // comboBoxClient.SelectedValue = dataGridViewMateriel.CurrentRow.Cells[6].Value.ToString();
            // comboBoxMarque.SelectedValue = dataGridViewMateriel.CurrentRow.Cells[7].Value.ToString();
            // Comment recuperer des valeurs liees a d'autres tables ?

        }

        private void buttonUpdateMateriel_Click(object sender, EventArgs e)
        {
            /*materielRow row = this.gestionmatos1DataSet.materiel.FindByID(10);

            row.Nom = textBoxName.Text;
                row.MTBF = 

                row. */


            this.materielTableAdapter.Fill(this.gestionmatos1DataSet.materiel);
        }
    }
}
