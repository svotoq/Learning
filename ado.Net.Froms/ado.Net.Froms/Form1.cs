using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ado.Net.Froms
{
    public partial class Form1 : Form
    {
        string ConnectionString;
        DataSet dataSet;
        SqlDataAdapter adapter;
        SqlCommandBuilder commandBuilder;
        string SQLExpression = "Select * From ComputersView";
        public Form1()
        {
            InitializeComponent();
            DataGridViewElement.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewElement.AllowUserToAddRows = false;
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow elem in DataGridViewElement.SelectedRows)
                {
                    DataGridViewElement.Rows.Remove(elem);
                }
            }
            catch {
                Console.WriteLine("Error! Row/rows cannot be deleted");
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                adapter = new SqlDataAdapter(SQLExpression, connection);
                dataSet= new DataSet();
                adapter.Fill(dataSet);
                DataGridViewElement.DataSource = dataSet.Tables[0];
                DataGridViewElement.Columns["ID"].ReadOnly = true;
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                adapter = new SqlDataAdapter(SQLExpression, connection);
                commandBuilder = new SqlCommandBuilder(adapter);
                adapter.InsertCommand = new SqlCommand("SP_INSERT_STUDENT", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add("@GROUPID", SqlDbType.Int, 0, "GROUPID");
                adapter.InsertCommand.Parameters.Add("@NAME", SqlDbType.NVarChar, 100, "NAME");
                adapter.InsertCommand.Parameters.Add("@BDAY", SqlDbType.Date, 0, "BDAY");
                SqlParameter ID = adapter.InsertCommand.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
                ID.Direction = ParameterDirection.Output;
                adapter.Update(dataSet);
            }
        }

        private void AddRowButton_Click(object sender, EventArgs e)
        {
            try
            {
                DataRow newRow = dataSet.Tables[0].NewRow();
                dataSet.Tables[0].Rows.Add(newRow);
            }
            catch
            {
                Console.WriteLine("Error! Row cannot be added!");
            }
        }
    }
}
