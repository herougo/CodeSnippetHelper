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
        Database db = new Database();
        ResultsDataStructure ds = null;

        public Form1()
        {
            InitializeComponent();

            ds = new ResultsDataStructure(ref db);

            initTextboxes();
            selectSearchResult(0);

            tbInput.KeyDown += new KeyEventHandler(this.tbInput_KeyDown);

            tbInput.Select();
        }

        Panel panel = new Panel();
        TextBox[] search_results = new TextBox[5];
        int selected_search_result = 0;

        Dictionary<Keys, char> keyMap = new Dictionary<Keys, char>() {
            { Keys.A, 'a' },
            { Keys.B, 'b' },
            { Keys.C, 'c' },
            { Keys.D, 'd' },
            { Keys.E, 'e' },
            { Keys.F, 'f' },
            { Keys.G, 'g' },
            { Keys.H, 'h' },
            { Keys.I, 'i' },
            { Keys.J, 'j' },
            { Keys.K, 'k' },
            { Keys.L, 'l' },
            { Keys.M, 'm' },
            { Keys.N, 'n' },
            { Keys.O, 'o' },
            { Keys.P, 'p' },
            { Keys.Q, 'q' },
            { Keys.R, 'r' },
            { Keys.S, 's' },
            { Keys.T, 't' },
            { Keys.U, 'u' },
            { Keys.V, 'v' },
            { Keys.W, 'w' },
            { Keys.X, 'x' },
            { Keys.Y, 'y' },
            { Keys.Z, 'z' },
            { Keys.Space, ' ' }
        };

        char keyToChar(Keys keys)
        {
            char result = '\0';
            try
            {
                keyMap.TryGetValue(keys, out result);
            }
            catch
            {

            }

            return result;
        }

        void initTextboxes()
        {
            panel.Size = new Size(220, 160);
            panel.Location = new Point(30, 90);
            this.Controls.Add(panel);

            for (int i = 0; i < 5; i++)
            {
                search_results[i] = new TextBox();
                search_results[i].Text = "";
                search_results[i].Tag = i;
                search_results[i].Location = new Point(0, 10 + 30 * i);
                search_results[i].Size = new Size(220, 20);
                search_results[i].ReadOnly = true;
                search_results[i].BackColor = Color.LightGray;
                search_results[i].KeyDown += new KeyEventHandler(this.search_result_KeyDown);
                panel.Controls.Add(search_results[i]);
            }
        }

        private void search_result_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            int tag = Convert.ToInt32(((System.Windows.Forms.TextBox)sender).Tag.ToString());

            if (e.KeyCode == Keys.Enter)
            {
                exitWithSnippet();
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (tag == 0)
                {
                    tbInput.Select();
                }
                else
                {
                    selectSearchResult(tag - 1);
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (tag < 4)
                {
                    selectSearchResult(tag + 1);
                }
            }
            Application.DoEvents();
        }

        private void selectSearchResult(int i) {
            search_results[selected_search_result].BackColor = Color.LightGray;
            selected_search_result = i;
            search_results[i].BackColor = Color.LightBlue;
            search_results[i].Select();
            Application.DoEvents();
        }

        private void exitWithSnippet()
        {
            // string text = search_results[selected_search_result].Text;
            // if (text == null || text == "") { text = "(empty)"; }
            // Clipboard.SetText(text);
            if (search_results[selected_search_result].Text == "")
            {
                return;
            }
            Clipboard.SetText(ds.getCodeFromSorted(ds.min_index + selected_search_result).Replace("\r", String.Empty).Replace("\n", "\r\n"));
            Application.Exit();
        }

        private void tbInput_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                exitWithSnippet();
            }
            else if (e.KeyCode == Keys.Down)
            {
                selectSearchResult(0);
            }
            else if (e.KeyCode == Keys.Back)
            {
                ds.removeChar();
                updateTextboxes();
            }
            else
            {
                char key_char = keyToChar(e.KeyCode);
                if (key_char != '\0') {
                    ds.addChar(key_char);
                    updateTextboxes();
                }
            }
        }

        // ** TO DO EFFICIENCY - update textbox when necessary
        public void updateTextboxes()
        {
            int min = Math.Min(ds.max_index - ds.min_index, 4);

            for (int i = 0; i <= min; i++)
            {
                search_results[i].Text = ds.getNameFromSorted(ds.min_index + i);
            }

            for (int i = min + 1; i <= 4; i++)
            {
                search_results[i].Text = "";
            }
        }

        private void btnDisplaySnippet_Click(object sender, EventArgs e)
        {
            
        }

        private void btnAddSnippet_Click(object sender, EventArgs e)
        {
            Form2 new_snippets = new Form2(ref db);
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
