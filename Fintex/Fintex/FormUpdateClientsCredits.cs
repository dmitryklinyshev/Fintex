using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fintex
{
    public partial class FormUpdateClientsCredits : Form
    {

        private SqlConnection sqlConnection = null;
        private int id;


        public FormUpdateClientsCredits(SqlConnection connection, int id)
        {
            InitializeComponent();

            sqlConnection = connection;
            this.id = id;
        }

        private async void FormUpdateClientsCredits_Load(object sender, EventArgs e)
        {
            SqlCommand getCreditsInfoCommand = new SqlCommand("SELECT [Title], [Duration], [Sum], [Percent_Value], [Period_of_Payment], [State] FROM [Credit] WHERE [Id]=@Id", sqlConnection);
            getCreditsInfoCommand.Parameters.AddWithValue("Id", id);

            SqlDataReader sqlDataReader = null;

            try
            {
                sqlDataReader = await getCreditsInfoCommand.ExecuteReaderAsync();

                while(await sqlDataReader.ReadAsync())
                {
                    textBox1.Text = Convert.ToString(sqlDataReader["Title"]);
                    textBox2.Text = Convert.ToString(sqlDataReader["Duration"]);
                    textBox3.Text = Convert.ToString(sqlDataReader["Sum"]);
                    textBox4.Text = Convert.ToString(sqlDataReader["Percent_Value"]);
                    textBox5.Text = Convert.ToString(sqlDataReader["Period_of_Payment"]);
                    textBox6.Text = Convert.ToString(sqlDataReader["State"]);
                    
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if(sqlDataReader != null && !sqlDataReader.IsClosed)
                {
                    sqlDataReader.Close();
                }
            }

        }

        private async void buttonAdd_Click(object sender, EventArgs e)
        {
            SqlCommand update = new SqlCommand("UPDATE [Credit] SET [Title]=@Title, [Duration]=@Duration, [Sum]=@Sum, [Percent_Value]=@Percent_Value, [Period_of_Payment]=@Period_of_Payment, [State]=@State WHERE [Id]=@Id", sqlConnection);

            update.Parameters.AddWithValue("Id", id);
            update.Parameters.AddWithValue("Title", textBox1.Text);
            update.Parameters.AddWithValue("Duration", textBox2.Text);
            update.Parameters.AddWithValue("Sum", textBox3.Text);
            update.Parameters.AddWithValue("Percent_Value", textBox4.Text);
            update.Parameters.AddWithValue("Period_of_Payment", textBox5.Text);
            update.Parameters.AddWithValue("State", textBox6.Text);

            try
            {
                await update.ExecuteNonQueryAsync();

                Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void buttonCansel_Click(object sender, EventArgs e)
        {
            Close();
        }

       
    }
}
