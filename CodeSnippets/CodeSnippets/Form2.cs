using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeSnippets
{
    public partial class Form2 : Form
    {
        Database db = null;

        public Form2(ref Database db1)
        {
            InitializeComponent();
            db = db1;
        }

        private void btnAddSnippet_Click(object sender, EventArgs e)
        {
            if (tbName.Text == "" || rtbCode.Text == "")
            {
                MessageBox.Show("Enter all fields");
                return;
            }
            
            db.upsert("tblSnippets", Database.table_structure["tblSnippets"], 
                new string[2] { tbName.Text, rtbCode.Text },
                new string[1] { "name" },
                new string[1] { tbName.Text });

            tbName.Text = "";
            rtbCode.Text = "";
        }
    }
}
