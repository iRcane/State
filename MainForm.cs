using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace State
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public IState State { get; set; }

        public bool isFileNameEmpty()
        {
            if (fileName.Text.Trim(' ', '\t') == "")
                return true;
            else
                return false;
        }

        public bool isFileContentEmpty()
        {
            if (fileContent.Text == "")
                return true;
            else
                return false;
        }

        public string GetFileName()
        {
            return fileName.Text;
        }

        public string GetFileContent()
        {
            return fileContent.Text;
        }

        private void fileName_TextChanged(object sender, EventArgs e)
        {
            State = State.SwitchState(this);
        }

        private void fileContent_TextChanged(object sender, EventArgs e)
        {
            State = State.SwitchState(this);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            State.CloseHandler(sender, e);
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            State.Save(this.GetFileName(), this.GetFileContent());
        }
    }
}
