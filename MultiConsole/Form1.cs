using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiConsole.classes;

namespace MultiConsole
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCleanAll_Click(object sender, EventArgs e)
        {
            this.txtDelimiter.Text = String.Empty;
            this.txtInput.Text = String.Empty;
            this.txtOutput.Text = String.Empty;
            this.nudRepetitionNumber.Value = this.nudRepetitionNumber.Minimum;
        }

        //https://stackoverflow.com/questions/12566166/how-to-asynchronously-read-the-standard-output-stream-and-standard-error-stream
        private void btnExecute_Click(object sender, EventArgs e)
        {
            int Repetition = (int)this.nudRepetitionNumber.Value;
            String LineDelimiter = this.txtDelimiter.Text;
            String Commands = this.txtInput.Text;

            if (String.IsNullOrEmpty(LineDelimiter))
                LineDelimiter = "\n";

            if (!String.IsNullOrEmpty(Commands))
            {
                classes.MultiConsole console = new classes.MultiConsole(Repetition, LineDelimiter);
                classes.MultiConsole.ConsoleResult result = console.Execute(Commands);
                //console.ErrorDataReceived += (o, args) =>
                //{
                    this.txtOutput.AppendText(result.Output, Color.LawnGreen);
                //};
                //console.OutputDataReceived += (o, args) =>
                //{
                    this.txtOutput.AppendText(result.Error, Color.Red);
                //};
                    if (String.IsNullOrEmpty(result.Error))
                        MessageBox.Show("MultiConsole has finished");
                    else
                        MessageBox.Show("MultiConsole has finished with errors");

            }
            else
            {
                MessageBox.Show("Please add console commands to input");
            }
        }

        //private void ConsoleOnErrorDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        //{
        //    throw new NotImplementedException();
        //}

        //private void ConsoleOnDataReceived(object sender, DataReceivedEventArgs dataReceivedEventArgs)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
