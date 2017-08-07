using System;
using System.Diagnostics;

namespace MultiConsole.classes
{
  public  class MultiConsole
  {
      private int _repetition;
      private String _lineDelimiter;
      private const String CMD = "cmd.exe";

      public int Repetition
      {
          get { return _repetition; }
          set { _repetition = value; }
      }

      public String LineDelimiter
      {
          get { return _lineDelimiter; }
          set { _lineDelimiter = value; }
      }

      public MultiConsole()
      {
          this.Repetition = 1;
          this.LineDelimiter = "\r\n";
      }
      public MultiConsole(int Repetition, String LineDelimiter)
      {
          this.Repetition = Repetition;
          this.LineDelimiter = LineDelimiter;
      }

      public struct  ConsoleResult
      {
          public string Output;
          public string Error;
      }

      //delegate void DataReceivedEventHandlers(object sender, EventArgs e);
      //public event System.Diagnostics.DataReceivedEventHandler DataReceived;
      //public event System.Diagnostics.DataReceivedEventHandler ErrorDataReceived;

     

      public ConsoleResult Execute(String Commands)
      {
          ConsoleResult s = new ConsoleResult();

          ProcessStartInfo processStartInfo = new ProcessStartInfo(CMD);
          processStartInfo.RedirectStandardInput = true;
          processStartInfo.RedirectStandardOutput = true;
          processStartInfo.RedirectStandardError = true;
          processStartInfo.CreateNoWindow = false;
          processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
          processStartInfo.UseShellExecute = false;

          Process process = Process.Start(processStartInfo);

          for (int i = 0; i < Repetition; i++)
          {
              foreach (String command in Commands.Split(LineDelimiter.ToCharArray()))
              {
                      process.StandardInput.WriteLine(command);
                 
              }
          }
          process.StandardInput.Close();
          string outputString = process.StandardOutput.ReadToEnd();
          string outputError = process.StandardError.ReadToEnd();
          s.Error = outputError;
          s.Output = outputString;
          return s;
      }
    
  }
}
