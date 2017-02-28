using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace State
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form = new MainForm();
            InitialState state = new InitialState();
            form.State = state;
            
            Application.Run(form);
        }
    }

    public interface IState
    {
        IState SwitchState(MainForm f);
        void CloseHandler(object sender, FormClosingEventArgs e);
        void Save(string fileName, string fileContent);
    }

    class InitialState: IState
    {
        public IState SwitchState(MainForm f)
        {
            if (f.isFileNameEmpty())
                return new NoNameState();
            else if (f.isFileContentEmpty())
                return new EmptyState();
            return new ChangedState();
        }

        public void CloseHandler(object sender, FormClosingEventArgs e)
        {
            var form = (MainForm)sender;
            form.Close();
        }

        public void Save(string fileName, string fileContent)
        {
            MessageBox.Show("Can not save changes as no file name entered.", "Warning", MessageBoxButtons.OK);
        }
    }

    class EmptyState: IState
    {
        public IState SwitchState(MainForm f)
        {
            if (f.isFileNameEmpty())
                return new NoNameState();
            else if (f.isFileContentEmpty())
                return this;
            return new ChangedState();
        }

        public void CloseHandler(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Changes might not been saved. Save changes?", "Warning", MessageBoxButtons.YesNoCancel);
            var form = (MainForm)sender;
            switch (result)
            {
                case DialogResult.Yes:
                    Save(form.GetFileName(), form.GetFileContent());
                    break;
                case DialogResult.No:
                    break;
                default:
                    e.Cancel = true;
                    break;
            }
        }

        public void Save(string fileName, string fileContent)
        {
            var result = MessageBox.Show("Are you sure you want to save an empty file?", "Warning", MessageBoxButtons.YesNo);

            switch (result)
            {
                case DialogResult.Yes:
                    System.IO.File.WriteAllText(fileName, fileContent);
                    break;
                default:
                    break;
            }
        }
    }

    class ChangedState: IState
    {
        public IState SwitchState(MainForm f)
        {
            if (f.isFileNameEmpty())
                return new NoNameState();
            else if (f.isFileContentEmpty())
                return new EmptyState();
            else
                return this;
        }

        public void CloseHandler(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Changes might not been saved. Save changes?", "Warning", MessageBoxButtons.YesNoCancel);
            var form = (MainForm)sender;
            switch (result)
            {
                case DialogResult.Yes:
                    Save(form.GetFileName(), form.GetFileContent());
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }

        public void Save(string fileName, string fileContent)
        {
            System.IO.File.WriteAllText(fileName, fileContent);
        }
    }

    class NoNameState: IState
    {
        public IState SwitchState(MainForm f)
        {
            if (!f.isFileNameEmpty())
                return new ChangedState();
            else
                return this;
        }

        public void CloseHandler(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("Closing will not save changes as no file name entered. Continue closing?", "Warning", MessageBoxButtons.YesNo);
            var form = (MainForm)sender;
            switch (result)
            {
                case DialogResult.Yes:
                    break;
                default:
                    e.Cancel = true;
                    break;
            }
        }

        public void Save(string fileName, string fileContent)
        {
            MessageBox.Show("Can not save changes as no file name entered.", "Warning", MessageBoxButtons.OK);
        }
    }
}
