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
        TxtKeyValuePair kvp = null;

        public Form2(ref TxtKeyValuePair kvp1)
        {
            InitializeComponent();
            kvp = kvp1;
        }

        private void btnAddSnippet_Click(object sender, EventArgs e)
        {
            if (tbName.Text == "" || rtbCode.Text == "")
            {
                MessageBox.Show("Enter all fields");
                return;
            }

            kvp.add(tbName.Text.Trim(), rtbCode.Text.Trim());

            tbName.Text = "";
            rtbCode.Text = "";
            MessageBox.Show("Entered!");
        }
    }
}
