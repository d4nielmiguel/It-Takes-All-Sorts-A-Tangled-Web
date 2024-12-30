using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

/**
 * I, Daniel Miguel, 000869785 certify that this material is my original work. No other person's work has been used without due acknowledgement.
 * 
 * File date: 7/12/2024
 * 
 * The program has a GUI interface that allows the user to load an html file and process through the file to check if all opening tags have 
 * closing tags which means it is a balanced file otherwise it's not a balanced file.
 **/

namespace Lab4b
{
    public partial class Form1 : Form
    {

        private string currentFilePath = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        /**
         * When the loadFile tool strip from the menu item is clicked it will run the FileDialog method.
         */
        private void loadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileDialog();
        }

        /**
         * FileDialog method will filter the OpenFileDialog to make sure only html files are loaded.
         */
        private void FileDialog()
        {
            // Create a new OpenFileDialog with only html files filter.
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "HTML Files|*.html"
            };

            // If statement checks if the DialogResult is good between the openFileDialog.
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                checkTagsToolStripMenuItem.Enabled = true; // Allow the user to check tags.
                currentFilePath = openFileDialog.FileName; // Loaded file path.
                fileLabel.Text = "Loaded: " + openFileDialog.SafeFileName; // Let the user know which file was loaded.
                fileListBox.Items.Clear(); // Clear the fileListBox.
            }
        }

        /**
         * Tags method is the main method where the checking is made.
         * I made use of Regex and Match objects for the checking.
         * 
         * Sources:
         * - https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.regex.match?view=net-8.0
         * - https://learn.microsoft.com/en-us/dotnet/api/system.text.regularexpressions.matchcollection?view=net-8.0
         */
        private bool Tags(string html) { 
            Stack<string> tagRead = new Stack<string>(); // Stack list of strings.
            Regex tagFormat = new Regex(@"<\s*(/?)\s*([^>\s]+)[^>]*>", RegexOptions.IgnoreCase); // Regex takes care of tag format.

            MatchCollection matches = tagFormat.Matches(html); // MatchCollection finds the matches between tagFormat and html parameter.

            /**
             * foreach loop will go over the matches and check for the opening and closing tags.
             */
            foreach (Match match in matches) {
                string tagName = match.Groups[2].Value.ToLower(); // tagName accesses the second capturing group in the regular expression.

                // If statement checks if "img", "hr" or "br" are contained between tagName.
                if (new[] { "img", "hr", "br" }.Contains(tagName))
                {
                    continue;
                }

                // If statement checks if match at group one value is not "/".
                if (match.Groups[1].Value != "/")
                {
                    tagRead.Push(tagName); // Push the tagName to the list.
                }
                else {
                    // If statement checks if the list is empty or the tagName isn't in the list.
                    if (tagRead.Count == 0 || tagRead.Peek() != tagName) {
                        return false;
                    }
                    tagRead.Pop(); // Removes the element at the top of the tagRead list.
                }
            }

            return tagRead.Count == 0; // Check if any unclosed tags remain
        }

        /**
         * The method will print the file into the fileListBox so the user can see the file in the program.
         * The method will tell the user if the tags are balanced or not.
         */
        private void checkTagsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If statement checks if the currentFilePath is not null or empty.
            if (!string.IsNullOrEmpty(currentFilePath))
            {
                string content = ""; // content will be used as the parameter for the main method. (content will contain all the information from the file)

                using (var file = new StreamReader(currentFilePath))
                {
                    string line;

                    // While loop will go through the file untill is not null
                    while ((line = file.ReadLine()) != null)
                    {
                        fileListBox.Items.Add(line); // Add the line from the file to the fileListBox
                        content += line; // Increment the lines from the file to the content.
                    }
                }

                bool balancedCheck = Tags(content); // balancedCheck is used to check if the content passed in is balanced or not
                fileLabel.Text = balancedCheck ? "Tags are balanced" : "Tags are not balanced";
            }
            // Else statement makes the checkTags disabled untill the user loads a file.
            else
            {
                checkTagsToolStripMenuItem.Enabled = false;
            }
        }

        /**
         * This method closes the application.
         */
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}