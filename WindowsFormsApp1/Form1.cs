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
            try
            {
                connection.Open();
                AdaugaRadacinaTreeView();
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        public void AdaugaRadacinaTreeView()
        {
            //treeView1.Nodes.Clear();
            //  SqlCommand cmd1 = new SqlCommand("select valuare from View_blocuri where id_master=1 and id_tip=2",connection);
            AdRamura(treeView1.Nodes, 0);
            // INCHIDEREA CONEXIUNII SE FACE LA INCHIDEREA FERESTREI 
            //            connection.Close();
        }
        // INPLEMENTEZ AFTERSELECT CARE SE APELEAZA DUPA CE SE DA CLICK PE UN NOD 
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // IN e SE PRIMESTE REFERINTA CATRE NODUL SELECTAT
            // EXTRAG VALOAREA DIN TAG PENTRU A PUTEA SELECTA NODURILE SUBORDONATE
            int nId = System.Convert.ToInt16(e.Node.Tag);
            // APELEZ ADAUGAREA RAMURII DEFINITA DE nId
            AdRamura(e.Node.Nodes, nId);

        }
        // ADAUGA O RAMURA LA UN NOD
        private void AdRamura(TreeNodeCollection nodes,int nId)
        {
            
            try
            {
                nodes.Clear();
                SqlDataReader dr = SqlQueryNoduri(nId);
                while (dr.Read())
                {
                    TreeNode node = new TreeNode(dr["valoare"].ToString());
                    // SALVEZ VALOAREA ID-ULUI IN PROPRIETATEA TAG A NODULUI PENTRU A PUTEA APELA MAI DEPARTE QUERY PENTRU NODURILE ACESTEI RAMURI
                    node.Tag = dr["id"];
                    nodes.Add(node);
                }
                dr.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw ex;
            }
            
        }

        private SqlDataReader SqlQueryNoduri(int nIdMaster)
        {
            SqlDataReader dr = null;
            SqlCommand cmd = new SqlCommand("select * from vOrganizatii where id_master=" + nIdMaster, connection);
            try
            {
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return dr;

        }


        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connection.Close();
        }
    }
}
