using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        SqlConnection connection = new SqlConnection(@"Data Source=82.208.137.149\sqlexpress, 8833;Initial Catalog=proba_transare;Persist Security Info=True;User ID=sa;Password=pro");
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            AdaugaRadacinaTreeView();
        }
        public void AdaugaRadacinaTreeView()
        {
            treeView1.Nodes.Clear();
            try
            {
                connection.Open();
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SqlCommand cmd = new SqlCommand("select * from Vasociatii ", connection);
            //  SqlCommand cmd1 = new SqlCommand("select valuare from View_blocuri where id_master=1 and id_tip=2",connection);
            try
            {
                SqlDataReader dr = cmd.ExecuteReader();
                //      SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    TreeNode node = new TreeNode(dr["valoare"].ToString());
                    node.Nodes.Add(dr["id"].ToString());
                    treeView1.Nodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
            connection.Close();
        }

    }
}
