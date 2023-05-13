using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using MySql.Data.MySqlClient;
using System.Linq.Expressions;
using System.Data.SqlClient;

namespace Login
{
    public partial class Form1 : Form
    {
        //public int attemptsLeft = 0;
        
        private const string connectionString = "Data Source=PIPO\\SQLEXPRESS;Initial Catalog=Login;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            ////INSERT///
            //this imports the table from sql 
            SqlConnection con = new SqlConnection("Data Source=PIPO\\SQLEXPRESS;Initial Catalog=Login;Integrated Security=True");
            con.Open();
            //this takes the info from the database, to prepare them to insert the new values
            SqlCommand cmd = new SqlCommand("insert into ut values(@id,@user,@password)", con);
            //this brings the user and password from the form
            cmd.Parameters.AddWithValue("@id", txtid.Text);
            cmd.Parameters.AddWithValue("@user", txtUsername.Text);
            cmd.Parameters.AddWithValue("@password", txtPassword.Text);
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Correctamente agregado");
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=PIPO\\SQLEXPRESS;Initial Catalog=Login;Integrated Security=True");
            con.Open();
            //this updates the info from the db comparing the values individually, with the help of the index (id)
            SqlCommand cmd = new SqlCommand("update ut set user=@user, password=@password where id=@id", con);
            //this brings the user and password from the form
            cmd.Parameters.AddWithValue("@id", txtid.Text);
            cmd.Parameters.AddWithValue("@user", txtUsername.Text);
            cmd.Parameters.AddWithValue("@password", txtPassword.Text);
            cmd.ExecuteNonQuery();
            con.Close();

            MessageBox.Show("Correctamente actualizado");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnaccess_Click(object sender, EventArgs e)
        {
            {
                // obtains the username and password
                string user = txtUsername.Text;
                string password = txtPassword.Text;

                // establishes the connection with the SQL server
                string connectionString = "Data Source=PIPO\\SQLEXPRESS;Initial Catalog=Login;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // creates an sql query to obtain the login data from the user
                        string query = "SELECT COUNT(*) FROM ut WHERE user=@user AND password=@password";
                        SqlCommand command = new SqlCommand(query, connection);
                        command.Parameters.AddWithValue("@user", user);
                        command.Parameters.AddWithValue("@password", password);

                        // executes the sql query and obtains the number of affected rows 
                        int result = (int)command.ExecuteScalar();

                        // if the amount of affected rows is greater tan 0, the login is sucessfull 
                        if (result > 0)
                        {
                            MessageBox.Show ("Inicio de sesión correcto");
                        }
                        // If the number of affected rows is cero, the credentials are incorrect 
                        else
                        {
                            // Decrease the count
                            int remainingAttempts = int.Parse(lblattemptsremaining.Text) - 1;
                            lblattemptsremaining.Text = remainingAttempts.ToString();

                            // shows a message when the counter reaches zero
                            if (remainingAttempts == 0)
                            {
                                MessageBox.Show ("El inicio de sesión falló y no quedan más intentos.");
                                btnaccess.Enabled = false;
                            }
                            // if it's not zero, shows a warning error.
                            else
                            {
                                MessageBox.Show ("El inicio de sesión falló. Quedan " + remainingAttempts.ToString() + " intentos.");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show ("Error al intentar iniciar sesión: " + ex.Message);
                    }
                }
            }

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

                   


        
    

