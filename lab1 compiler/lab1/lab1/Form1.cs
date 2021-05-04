using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab1
{
    public partial class Form1 : Form
    {
        private string fileContent = string.Empty;
        private string fileName = string.Empty;
        private string startText = string.Empty;
        List<string> LastData = new List<string>();
        int undoCount = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Save()
        {
            if (fileName == string.Empty)
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (save.ShowDialog() != DialogResult.Cancel)
                {
                    File.WriteAllText(save.FileName, richTextBox1.Text);
                    fileName = save.FileName;
                }
            }
            else
            {
                StreamWriter sw = new StreamWriter(fileName);
                sw.WriteLine(richTextBox1.Text);
                sw.Close();
            }
        }

        private void Create()
        {
            richTextBox1.Text = string.Empty;
            richTextBox2.Text = string.Empty;
            fileName = string.Empty;
            fileContent = string.Empty;
        }

        private void Open()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.InitialDirectory = "c:\\";
            open.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (open.ShowDialog() != DialogResult.Cancel)
            {
                fileName = open.FileName;
                var fileStream = open.OpenFile();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                    startText = reader.ReadToEnd();
                }
                richTextBox1.Text = fileContent;
            }
        }
        private void create_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Сохранить текущий файл?",
            "Сообщение",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
                Save();
            else if (result == DialogResult.No)
            {
                LastData.Clear();
                Create();
                undoCount = 0;
            }
        }

        private void Cut()
        {
            Clipboard.SetText(richTextBox1.SelectedText, TextDataFormat.UnicodeText);
            richTextBox1.SelectedText = "";
        }
        private void Copy()
        {
            Clipboard.SetText(richTextBox1.SelectedText, TextDataFormat.UnicodeText);
        }

        private void Paste()
        {
            richTextBox1.Paste();
        }
        private void Delete()
        {
            richTextBox1.SelectedText = "";
        }
        private void SelectAll()
        {
            richTextBox1.SelectAll();
        }

        private void open_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Сохранить текущий файл?",
            "Сообщение",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
                Save();
            else if (result == DialogResult.No)
            {
                LastData.Clear();
                Open();
                undoCount = 0;
            }
        }
        private void Undo()
        {
            if (undoCount < LastData.Count)
            {
                richTextBox1.Text = LastData[LastData.Count - undoCount - 1];
                ++undoCount;
            }
        }

        private void redo()
        {
            if (undoCount > 0)
            {
                --undoCount;
                richTextBox1.Text = LastData[LastData.Count - undoCount - 1];
            }
        }

        private void save_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void saveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            save.FileName = fileName;
            if (save.ShowDialog() != DialogResult.Cancel)
            {
                File.WriteAllText(save.FileName, richTextBox1.Text);
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void cut_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void copy_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void paste_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void highlight_Click(object sender, EventArgs e)
        {
            SelectAll();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Сохранить текущий файл?",
            "Сообщение",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
                Save();
            else if(result == DialogResult.No)
            Create();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Сохранить текущий файл?",
            "Сообщение",
            MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
                Save();
            else if(result == DialogResult.No)
            Open();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Cut();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Copy();
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void about_Click(object sender, EventArgs e)
        {
             MessageBox.Show(
            "Данная программа представляет собой текстовый редактор, с реализованными в нем всеми необходимыми для работы с тектом функциями. " + "\n" +
            "" + "\n" +
            "Version 1.0" + "\n" +

            "Copyright ©2021, NSTU, Novosibirsk",
            "О программе",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information,
            MessageBoxDefaultButton.Button1,
            MessageBoxOptions.DefaultDesktopOnly);

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (startText != fileContent)
            {
                DialogResult result = MessageBox.Show(
                "Сохранить файл перед выходом?",
                "Сообщение",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
                if (result == DialogResult.Cancel)
                    e.Cancel = true;
                if (result == DialogResult.Yes)
                    Save();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LastData.Add(richTextBox1.Text);
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space || e.KeyData==Keys.Enter)
            {
                LastData.Add(richTextBox1.Text);
                undoCount = 0;
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            redo();
        }

        private void repeat_Click(object sender, EventArgs e)
        {
            redo();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            fileContent = richTextBox1.Text;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = string.Empty;
            richTextBox2.Text = string.Empty;
        }

        private void start_Click(object sender, EventArgs e)
        {
            richTextBox2.Text = string.Empty;
            string pattern1 = @"((\b[А-ЯЁ][а-яё]+(\-[А-ЯЁ][а-яё]*)?\s+[А-ЯЁ]{1}?\.+[А-ЯЁ]{1}?\.?))";
            string pattern2 = @"((\b[А-ЯЁ]{1}?\.+[А-ЯЁ]{1}?\.\s*[А-ЯЁ][а-яё]+(\-[А-ЯЁ][а-яё]*)?))";
            Regex regex1 = new Regex(pattern1);
            Regex regex2 = new Regex(pattern2);
            MatchCollection match1 = regex1.Matches(richTextBox1.Text);
            MatchCollection match2 = regex2.Matches(richTextBox1.Text);
            //for (int i = 0; i < match1.Count; i++)
            //{
            //    richTextBox2.Text += match1[i] + "(" + match1[i].Index.ToString() + ")\n";
            //}
            //for (int i = 0; i < match2.Count; i++)
            //{
            //    richTextBox2.Text += match2[i] + "(" + match2[i].Index.ToString() + ")\n";
            //}
            DetermStateMachine stateMachine = new DetermStateMachine();
            string str = richTextBox1.Text;
            char[] text = str.ToCharArray();
            for (int i = 0; i< text.Length;i++)
            {
                if (stateMachine.SymbolChecking(text[i]) == false || stateMachine.str==string.Empty)
                {
                    stateMachine = new DetermStateMachine();
                }
                if (stateMachine.getState().ToString() == "q5")
                {
                    richTextBox2.Text += stateMachine.states + "\n";
                    //stateMachine.states = "q1->";
                    richTextBox2.Text += stateMachine.getStr() + "\n";
                    stateMachine = new DetermStateMachine();
                }
            }
            Scaner scaner = new Scaner(richTextBox1.Text);
            richTextBox2.Text = scaner.result;
        }

        private void callHelp_Click(object sender, EventArgs e)
        {
            File.WriteAllText("newFile.html", Properties.Resources.spravka);
            System.Diagnostics.Process.Start("newFile.html");
        }
    }
}
