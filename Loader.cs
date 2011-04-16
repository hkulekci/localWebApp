using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace loader
{
    public partial class Loader : Form
    {
        Process apache = new Process();
        Process mysql = new Process();
        Process prism = new Process();
        static string pathh = Application.StartupPath;
		
		//Apache Settings
        static string apachePath = pathh + "\\apache\\";
		string apacheClosePath = apachePath + "bin\\pv.exe";
		string apacheCloseParameters = "-f -k httpd.exe -q";
		
		// Mysql Settings
		string mysqlPath = pathh + "\\mysql\\";
        string mysql_parameters = "--defaults-file=mysql\\bin\\my.ini --standalone --console";
		string mysqlClosePath = apachePath + "bin\\pv.exe";
		string mysqlCloseParameters = "-f -k mysqld.exe -q";
		
		//Prism Settings
		string prismPath = pathh + "\\loader\\prism\\prism.exe";
		string prism_parameters = "-override \"" + pathh + "\\loader\\WebApps\\loader@prism.app\\override.ini\" -webapp loader@prism.app";
		string prism_working_directory = pathh + "\\loader\\WebApps";
		/*
			In this part, you must create a configure file for prism.
			Can you get help this page => http://prism.mozillalabs.com/
		*/

		// Logs Settings
		string log_directory = pathh + "\\loader\\hlogs";


        public Loader()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Replaces text in a file.
        /// </summary>
        /// <param name="filePath">Path of the text file.</param>
        /// <param name="searchText">Text to search for.</param>
        /// <param name="replaceText">Text to replace the search text.</param>
        static public void replace_from_file(string filePath, string searchText, string replaceText)
        {
            StreamReader reader = new StreamReader(filePath);
            string content = reader.ReadToEnd();
            reader.Close();

            content = Regex.Replace(content, searchText, replaceText);

            StreamWriter writer = new StreamWriter(filePath);
            writer.Write(content);
            writer.Close();
        }
        /// <summary>
        /// Show message box for logs
        /// </summary>
        private void messaged( string s ) {
            MessageBox.Show( s );
            logged(s);
        }
        /// <summary>
        /// Save logs to listbox
        /// </summary>
        private void logged( string s ) {
			// logging operation in a listbox 
			// Maybe this part can be change
            log.Items.Add(DateTime.Now.ToString() + " | " + s);
        }

        /// <summary>
        /// Save logs to log file
        /// </summary>
        private void save_log() {
            if (!Directory.Exists(log_directory))
                Directory.CreateDirectory(log_directory);
            try
            {
                System.IO.StreamWriter logFile = new System.IO.StreamWriter(log_directory + "\\log_" +
                        DateTime.Now.Year + "_" + DateTime.Now.Month + ".txt", true);
                foreach (string lines in log.Items)
                    logFile.WriteLine(lines.ToString());
                logFile.Close();
            }catch(Exception a){
                messaged("Program error code : 1005");
            }


            Application.Exit();
        }

        private void run_Click(object sender, EventArgs e)
        {
            this.Hide();

            #region replace_part
            // I dont run this program with this region
            if (File.Exists(apachePath + "bin\\httpd.conf"))
                replace_from_file(apachePath + "bin\\httpd.conf", "/xampp/", pathh + "/");
            else
                logged("Program error code : 1005a - Apache Path not found!");
            if (File.Exists(mysqlPath + "bin\\mysql.ini"))
                replace_from_file(mysqlPath + "bin\\mysql.ini", "/xampp/", pathh.Replace("\\", "/") + "/");
            else
                logged("Program error code : 1005b - Mysql Path not found!");
            #endregion

            #region ApacheStart
            if (File.Exists(apachePath + "bin\\httpd.exe"))
            {
                try
                {
                    apache.StartInfo.UseShellExecute = false;
                    apache.StartInfo.FileName = apachePath + "bin\\httpd.exe";
                    apache.StartInfo.CreateNoWindow = true;
                    apache.Start();
                    logged("Service-1 is started");
                }
                catch (Exception ess) {
                    messaged("Program error code : 1000a");
                    save_log();
                }
            }
            else {
                messaged("Program error code : 1000b");
                save_log();
            }

            #endregion

            #region MysqlStart
            if (File.Exists(mysqlPath + "bin\\mysqld.exe"))
            {
                try
                {
                    mysql.StartInfo.UseShellExecute = false;
                    mysql.StartInfo.FileName = mysqlPath + "bin\\mysqld.exe";
                    mysql.StartInfo.Arguments = mysql_parameters;
                    mysql.StartInfo.CreateNoWindow = true;
                    mysql.Start();
                    logged("Service-2 is started");
                }
                catch (Exception ees){
                    messaged("Program error code : 1001a");
                    save_log();
                }
            }
            else
            {
                messaged("Program error code : 1001b");
                save_log();
            }
            #endregion

            #region PrismStart
            if (File.Exists(prismPath))
            {
                try
                {
                    
                    prism.StartInfo.UseShellExecute = true;
                    prism.StartInfo.FileName = prismPath;
                    prism.StartInfo.Arguments = prism_parameters;
                    prism.StartInfo.WorkingDirectory = prism_working_directory;
                    prism.StartInfo.CreateNoWindow = false;
                    prism.Start();
                    logged("Service-3 is started");
                    prism.WaitForExit();


                }
                catch (Exception ass) {
                    messaged("Program error code : 1002a");
                    save_log();
                }
            }
            else
            {
                messaged("Program error code : 1002b");
                save_log();
            }
            #endregion

            prism.Close();
            logged("Service-3 is ended");
            close_form();
            save_log();


        }

        private void close_form()
        {


            #region ApacheClosePart
            Process apacheClose = new Process();
            if (File.Exists(apacheClosePath))
            {
                try
                {
                    apacheClose.StartInfo.UseShellExecute = false;
                    apacheClose.StartInfo.FileName = apacheClosePath;
                    apacheClose.StartInfo.Arguments = apacheCloseParameters;
                    apacheClose.StartInfo.CreateNoWindow = true;
                    apacheClose.Start();
                    logged("Service-1 is ended");
                    apacheClose.WaitForExit();
					
                    if (File.Exists(apachePath + "logs\\httpd.pid"))
                    {
                        File.Delete(apachePath + "logs\\httpd.pid");
                    }
                }
                catch (Exception essd) {
                    messaged("Program error code : 1003a");
                }

            }
            else {
                messaged("Program error code : 1003b");
                save_log();
            }
            #endregion

            #region MysqlClosePart
            Process mysqlClose = new Process();
            if (File.Exists(mysqlClosePath))
            {
                try
                {
                    mysqlClose.StartInfo.UseShellExecute = false;
                    mysqlClose.StartInfo.FileName = mysqlClosePath;
                    mysqlClose.StartInfo.Arguments = mysqlCloseParameters;
                    mysqlClose.StartInfo.CreateNoWindow = true;
                    mysqlClose.Start();
                    logged("Service-2 is ended");
                    mysqlClose.WaitForExit();

                    if (File.Exists(mysqlPath + "data\\" + Environment.MachineName + ".pid"))
                    {
                        File.Delete(mysqlPath + "data\\" + Environment.MachineName + ".pid");
                    }

                    if (File.Exists(mysqlPath + "data\\" + Environment.MachineName.ToLower() + ".pid"))
                    {
                        File.Delete(mysqlPath + "data\\" + Environment.MachineName.ToLower() + ".pid");
                    }
                }
                catch (Exception aess) {
                    messaged("Program error code : 1004a");
                }
            }
            else
            {
                messaged("Program error code : 1004b");
                save_log();
            }
            #endregion

            apacheClose.Close();
            mysqlClose.Close();
            apache.Close();
            mysql.Close();
            prism.Close();

        }

        private void close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Loader_Load(object sender, EventArgs e)
        {
			//This part hide this application 
			//and prism is focus spontaneously
            this.Hide();
        }
    }
}
