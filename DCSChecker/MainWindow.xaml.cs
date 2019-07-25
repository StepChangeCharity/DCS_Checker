using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;


namespace DCSChecker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        { 
            var progToCheck = "DCS";

            //CheckIfProcessRunning("Notepad");
            CheckIfProcessRunning(progToCheck);
                       
            Close(); //Close this program!
        }

        private static void CheckIfProcessRunning(string programName)
        {
            Process[] pname = Process.GetProcessesByName(programName);
            if (pname.Length != 0)
                if (CloseProcessWithWait(programName, pname)) //Closed?     //Check it's been terminated
                {
                    pname = Process.GetProcessesByName(programName);
                    if (pname.Length != 0)
                    { 
                        MessageBox.Show(programName + " - did not close, please retry.");
                    }
                    else
                    {
                        StartProcess(progToCheck);
                    }
                }
                else
                {
                    MessageBox.Show("Cancelled! Please rerun to open " + programName);
                }
            else
            { 
                StartProcess(progToCheck);
            }
        }

        private static bool CloseProcessWithWait(string programName, Process[] pname)
        { 
            if (MessageBox.Show("Restart: " + Environment.NewLine  + programName + " ", programName + " Restart", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                var process = Process.Start("cmd.exe", "/c taskkill /F /IM " + programName + ".exe /T");//Might need Admin to run?      
                process.WaitForExit();
                process = Process.Start("cmd.exe", "/c taskkill /F /IM DCSLogin.exe /T");//Might need Admin to run?
                process.WaitForExit();
                Thread.Sleep(2000); //Despite the WaitForExist the memory can lag behind giving a false positive! 
                return true;
            }
            else
            {
                return false;
            }
        }

        private static void StartProcess(string programName)
        {
            //if (programName == "Notepad")
            //{
            //    System.Diagnostics.Process.Start(@"C:\Windows\System32\" + programName + ".exe");
            //}

            if (programName == "DCS")
            {
                System.Diagnostics.Process.Start(@"C:\Program Files (x86)\DCS\DCS.exe"); 
            }
        }
    }
}

