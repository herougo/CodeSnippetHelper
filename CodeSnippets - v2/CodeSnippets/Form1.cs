/* 
This is a simple program I wrote to archive code snippets. This includes database interaction, UI data helpers,
and anything else I can think of that can be reused in future programs. This is intended to:
 * write programs in minimal time using as many reused "snippets" as possible
 * be accessed via keyboard shortcuts

I wanted to create my own since I wanted to improve upon Visual Studio's version to fit my coding styles.
Also, this allows program allows me to use Code Snippets for things outside of Visual Studio

To use this program as intended
 * create a shortcut to the .exe file. Right click on that shortcut and click properties. Under, the Shortcut tab, change 
   "shortcut key" to the desired keyboard shortcut
 * start typing in the text box to get search results, when the top result matches what you want, press enter
 * the code snippet will be saved to your clipbaord and the program will close

 * to add snippets, click the "Add Snippet" button to load form with that funtionality
 * to view existing snippets, click the "Existing Snippets" button
*/

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
    public partial class Form1 : Form
    {
        TxtKeyValuePair kvp = new TxtKeyValuePair();

        public Form1()
        {
            InitializeComponent();

            var source = new AutoCompleteStringCollection();
            source.AddRange(kvp.file_names.ToArray()); // ** range is not consistent

            tbInput.AutoCompleteCustomSource = source;
            tbInput.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            tbInput.AutoCompleteSource = AutoCompleteSource.CustomSource;

            tbInput.KeyDown += new KeyEventHandler(this.tbInput_KeyDown);
        }

        private void exitWithSnippet()
        {
            // string text = search_results[selected_search_result].Text;
            // if (text == null || text == "") { text = "(empty)"; }
            // Clipboard.SetText(text);
            // MessageBox.Show(tbInput.Text);
            Clipboard.SetText(kvp.getValue(tbInput.Text).Replace("\r", String.Empty).Replace("\n", "\r\n"));
            Application.Exit();
        }

        private void tbInput_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                exitWithSnippet();
            }
        }

        private void btnDisplaySnippet_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAddSnippet_Click(object sender, EventArgs e)
        {
            Form2 new_snippets = new Form2(ref kvp);
            new_snippets.Show();
        }


        /*
        Application.ApplicationExit += new EventHandler(this.OnApplicationExit);
        
        private void OnApplicationExit(object sender, EventArgs e)
        {
            string TO_EDIT = "";
        }
         */

    }
}
