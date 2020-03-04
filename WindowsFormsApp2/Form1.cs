using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var parentNode1 = treeView1.Nodes.Add("Example data bound tree nodes 1");
            var parentNode2 = treeView1.Nodes.Add("Example data bound tree nodes 2");

            // Setup the TreeViewBinding 1.
            var treeNodeCollectionBinder1 = new TreeViewBinding(
                parentNode1,
                bindingSource1==AsociatiiDataSet,
                // Callback to cast the data item (from DataRowView to type DataRow)
                listItem => (DataSet1.DataTable1Row)((DataRowView)listItem).Row,
                // Callback for creating TreeNode objects.
                dataItem => new TreeNode(),
                // Callback to sync properties between TreeNode and data item.
                (dataItem, treeNode) => {
                    treeNode.Name = dataItem.ID.ToString();
                    treeNode.Text = dataItem.DisplayName;
                    treeNode.ToolTipText = dataItem.ID.ToString();
                });

            // Setup the TreeViewBinding 2.
            var treeNodeCollectionBinder2 = new TreeViewBinding(
                parentNode2,
                bindingSource2,
                listItem => (DataSet1.DataTable2Row)((DataRowView)listItem).Row,
                dataItem => new TreeNode(),
                (dataItem, treeNode) => {
                    treeNode.Name = dataItem.ID.ToString();
                    treeNode.Text = dataItem.FullName;
                    treeNode.ToolTipText = dataItem.ID.ToString();
                });
        }
    }
}
