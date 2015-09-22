using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Reploy.SSRSWebService;
using System.Text.RegularExpressions;
using System.Web.Services.Protocols;
using System.IO;
using System.Diagnostics;

namespace Reploy
{
    public partial class Form1 : Form
    {
        private const string SUMMARY = "{0} report(s), {1} model(s)";
        private ReportingService2005 rs;

        private BackgroundWorker bgwDS;
        private BackgroundWorker bgwFolders;

        public Form1()
        {
            InitializeComponent();
            //InitializeBWDS();
            bgwFolders = new BackgroundWorker();
            bgwDS = new BackgroundWorker();
            InitializeBWF();
            InitializeBWDS();
        }

        #region Background workers plumbing

        private void InitializeBWF()
        {
            bgwFolders.DoWork += new DoWorkEventHandler(bgwFolders_DoWork);
            bgwFolders.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwFolders_RunWorkerCompleted);

        }

        private void InitializeBWDS()
        {
            bgwDS.DoWork += new DoWorkEventHandler(bgwDS_DoWork);
            bgwDS.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwDS_RunWorkerCompleted);

        }

        private void bgwFolders_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.
            e.Result = GetFoldersOnServer((ReportingService2005)e.Argument, worker, e);

        }

        private void bgwFolders_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.ToString());
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                MessageBox.Show("Cancelled"); // no cancel facility anyway
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                PopulateFolders((CatalogItem[])e.Result);
            }

            folderStatus.Visible = false;

            if (!dataSourcesStatus.Visible)
            {
                //other thread is complete, activate the Go button again and the deploy
                btnGo.Enabled = true;
                btnDeploy.Enabled = true;
            }
        }

        private void bgwDS_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            // Assign the result of the computation
            // to the Result property of the DoWorkEventArgs
            // object. This is will be available to the 
            // RunWorkerCompleted eventhandler.
            e.Result = GetDatasourcesOnServer((object[])e.Argument, worker, e);

        }

        private void bgwDS_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // First, handle the case where an exception was thrown.
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.ToString());
            }
            else if (e.Cancelled)
            {
                // Next, handle the case where the user canceled 
                // the operation.
                // Note that due to a race condition in 
                // the DoWork event handler, the Cancelled
                // flag may not have been set, even though
                // CancelAsync was called.
                MessageBox.Show("Cancelled"); // no cancel facility anyway
            }
            else
            {
                // Finally, handle the case where the operation 
                // succeeded.
                PopulateDataSources((CatalogItem[])e.Result);
            }

            dataSourcesStatus.Visible = false;

            if (!folderStatus.Visible)
            {
                //other thread is complete, activate the Go button again and the deploy
                btnGo.Enabled = true;
                btnDeploy.Enabled = true;
            }
        }
#endregion 

        #region SSRS nasty stuff

        private object GetFoldersOnServer(ReportingService2005 reportingService2005, BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                return rs.ListChildren("/", true);
                //this returns : CatalogItem[]
            }
            catch (SoapException ex)
            {
                throw new Exception("SOAP Exception occured : " + ex.Detail.InnerXml.ToString(), ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected exception occured while retrieving Folders on reporting server", ex);

            }

        }


        private object GetDatasourcesOnServer(object[] p, BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                ReportingService2005 myrs = (ReportingService2005)p[0];
                string dsPath = (string)p[1];

                return rs.ListChildren(dsLocation.Text, false);

            }
            catch (SoapException ex)
            {
                throw new Exception("SOAP Exception occured : " + ex.Detail.InnerXml.ToString(), ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Unexpected exception occured while retrieving Data sources on reporting server", ex);

            }
        }

        private string DeployReport(string localPath, string serverPath, string dataSourcePath, string dataSourceName)
        {
            byte[] definition = null;
            Warning[] warnings = null;
            string retRes = String.Empty;

            try
            {
                // Read the file and put it into a byte array to pass to SRS
                FileStream stream = File.OpenRead(localPath);
                definition = new byte[stream.Length];
                stream.Read(definition, 0, (int)(stream.Length));
                stream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // We are going to use the name of the rdl file as the name of our report
            string reportName = Path.GetFileNameWithoutExtension(localPath);


            // Now lets use this information to publish the report
            try
            {
                warnings = rs.CreateReport(reportName, serverPath, true, definition, null);

                if (warnings != null)
                {
                    retRes = String.Format("Report {0} created with warnings :\n", reportName);
                    foreach (Warning warning in warnings)
                    {
                        retRes += warning.Message + "\n";
                    }
                }
                else
                {
                    retRes = String.Format("Report {0} created successfully with no warnings\n", reportName);

                }

                // check xml datasource
                var isXmlProvider = IsXmlDataProvider(localPath);
                if (isXmlProvider)
                {
                    return retRes;
                }

                //set the datasource
                DataSourceReference dsr = new DataSourceReference();
                dsr.Reference = dataSourcePath + "/" + dataSourceName;


                DataSource[] dsarray = rs.GetItemDataSources(serverPath + "/" + reportName);
                DataSource ds = new DataSource();
                if (dsarray.Length > 0)
                {
                    ds = dsarray[0];
                    ds.Item = (DataSourceReference)dsr;
                    rs.SetItemDataSources(serverPath + "/" + reportName, dsarray);
                    retRes += String.Format("Data source succesfully set to {0}\n", dsr.Reference);
                }
            }
            catch (SoapException ex)
            {
                return String.Format("Report {0} failed with exception {1}\n", reportName, ex.Detail.InnerXml.ToString());
            }

            return retRes;
        }

        private bool IsXmlDataProvider(string reportPath)
        {
            var fs = File.OpenRead(reportPath);
            var b = new byte[fs.Length];
            fs.Read(b, 0, b.Length);
            fs.Close();

            XmlDocument xmldoc;

            using (var ms = new MemoryStream(b))
            {
                xmldoc = new XmlDocument();
                xmldoc.Load(ms);
                ms.Close();
            }

            XmlNode root = xmldoc.DocumentElement;

            // Get all the elements under the root node.
            if (root != null)
            {
                var nodes = root.SelectNodes("descendant::*");

                if (nodes != null)
                {
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Name == "DataSources") // Search for Report Parameters
                        {
                            var listDataSource = node.ChildNodes;
                            foreach (XmlNode ds in listDataSource)
                            {
                                foreach (XmlNode child in ds.ChildNodes)
                                {
                                    if (child.Name == "ConnectionProperties")
                                    {
                                        foreach (XmlNode xmlNode in child.ChildNodes)
                                        {
                                            if (xmlNode.Name == "DataProvider" && xmlNode.InnerText.ToUpper().Trim() == "XML")
                                            {
                                                return true;
                                            }
                                        } 
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }

        private string DeployModel(string localPath, string serverPath, string dataSourcePath, string dataSourceName)
        {
            byte[] definition = null;
            Warning[] warnings = null;
            string retRes = String.Empty;

            try
            {
                // Read the file and put it into a byte array to pass to SRS
                FileStream stream = File.OpenRead(localPath);
                definition = new byte[stream.Length];
                stream.Read(definition, 0, (int)(stream.Length));
                stream.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            // We are going to use the name of the rdl file as the name of our report
            string reportName = Path.GetFileNameWithoutExtension(localPath);


            // Now lets use this information to publish the report
            try
            {
                warnings = rs.CreateModel(reportName, serverPath, definition, null);
                
                if (warnings != null)
                {
                    retRes = String.Format("Report {0} failed with warnings :\n", reportName);
                    foreach (Warning warning in warnings)
                    {
                        retRes += warning.Message + "\n";
                    }
                }
                else
                {
                    retRes = String.Format("Report {0} created successfully with no warnings\n", reportName);

                }

                //set the datasource
                DataSourceReference dsr = new DataSourceReference();
                dsr.Reference = dataSourcePath + "/" + dataSourceName;


                DataSource[] dsarray = rs.GetItemDataSources(serverPath + "/" + reportName);
                DataSource ds = new DataSource();

                ds = dsarray[0];
                ds.Item = (DataSourceReference)dsr;

                rs.SetItemDataSources(serverPath + "/" + reportName, dsarray);


            }
            catch (SoapException ex)
            {
                return String.Format("Report {0} failed with exception {1}\n", reportName, ex.Detail.InnerXml.ToString());
            }

            return retRes;
        }



        #endregion

        #region UI bits

        private void PopulateFolders(CatalogItem[] items)
        {
            // create root
            TreeNode root = new TreeNode();
            root.Text = "Root";
            ssrsFolders.Nodes.Add(root);
            ssrsFolders.SelectedNode = ssrsFolders.TopNode;

            int j = 1;

            // Iterate through the list of items and find all of the folders and display them to the user
            foreach (CatalogItem ci in items)
            {
                if (ci.Type == ItemTypeEnum.Folder)
                {
                    Regex rx = new Regex("/");
                    int matchCnt = rx.Matches(ci.Path).Count;
                    if (matchCnt > j)
                    {
                        ssrsFolders.SelectedNode = ssrsFolders.SelectedNode.LastNode;
                        j = matchCnt;
                    }
                    else if (matchCnt < j)
                    {
                        ssrsFolders.SelectedNode = ssrsFolders.SelectedNode.Parent;
                        j = matchCnt;
                    }
                    AddNode(ci.Name);
                }
            }
            //we want to see them all !
            ssrsFolders.ExpandAll();
        }

        private void PopulateDataSources(CatalogItem[] items)
        {
            int j = 0;

            // Iterate through the list of items and find all of the folders and display them to the user
            foreach (CatalogItem ci in items)
            {
                if (ci.Type == ItemTypeEnum.DataSource)
                {
                    dataSources.Items.Add(ci.Name);
                    j++;
                }
            }
            if (j == 0)
                dataSources.Items.Add("No data source found in " + dsLocation.Text);

        }

        private void ClearServerInfo()
        {
            //Clear server dependant lists
            ssrsFolders.Nodes.Clear();
            dataSources.Items.Clear();
        }

        private void AddNode(string name)
        {
            TreeNode newNode = new TreeNode(name);
            ssrsFolders.SelectedNode.Nodes.Add(newNode);
        }

        #endregion



        private void InitSSRS()
        {
            //init ssrs stuff
            rs = new ReportingService2005();
            rs.Credentials = System.Net.CredentialCache.DefaultCredentials;
            rs.Url = GetRSURL();
        }

        private string GetRSURL()
        {
            //if (reportServer.Text.StartsWith("http://"))
            //    return reportServer.Text + "/reportserver/ReportService2005.asmx";
            //else
            //    return "http://" + reportServer.Text + "/reportserver/ReportService2005.asmx";

            var url = reportServer.Text;

            if (!url.StartsWith("http")) // support: https
            {
                url = "http://" + url;
            }

            url = url.Trim('/');

            if (!url.EndsWith(".asmx"))
            {
                url = url + "/ReportService2005.asmx";
            }

            return url;
        }






        private void Form1_Load(object sender, EventArgs e)
        {
            //initialize server name with config ??
            
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Report stuff (*.rdl, *.smdl)|*.rdl;*.smdl|All files (*.*)|*.*";
            dlg.FilterIndex = 1;
            dlg.Multiselect = true;

            int report=0;
            int model = 0;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                reportList.Items.Clear();

                foreach (string filename in dlg.FileNames)
                {
                    reportList.Items.Add(filename,Path.GetFileName(filename), 0);
                    if (Path.GetExtension(filename).ToUpper() == ".RDL")
                        report++;
                    if (Path.GetExtension(filename).ToUpper() == ".SMDL")
                        model++;
                }

                listSummary.Text = String.Format(SUMMARY, report, model);
                listSummary.Visible = true;

            }

			

        }

        // based on book : Pro SQL Server 2005 Reporting Services By Rodney Landrum, Walter J. Voytek
        private void getFolders_Click(object sender, EventArgs e)
        {
            btnGo.Enabled = false;
            btnDeploy.Enabled = false;

            InitSSRS();
            //lock the server textbox. Only unlock Deploy once both bw completed
            reportServer.ReadOnly = true;

            //empty the list and treeview
            ClearServerInfo();


            // start bg workers
            folderStatus.Visible = true;
            dataSourcesStatus.Visible = true;

            bgwFolders.RunWorkerAsync(rs);

            // need 2 arguments here : rs + data source location path
            object[] arguments = new object[2];
            arguments[0] = rs;
            arguments[1] = dsLocation.Text;
            bgwDS.RunWorkerAsync(arguments);

            //that's all we want to do here
        }


        private void btnDeploy_Click(object sender, EventArgs e)
        {


            //validation
            if (dataSources.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a data source", "Data source missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (reportList.Items.Count == 0)
            {
                MessageBox.Show("No report or model has been selected", "No report", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //starts the deployment of all the reports and report models onto SSRS
            // should be in a background worker as well ..

            //figure out the deployment path
            string pathName = ssrsFolders.SelectedNode.FullPath;
            if (pathName == "Root")
            {
                if (MessageBox.Show("Are you sure you want to deploy to the Root folder ?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning)== DialogResult.Cancel)
                    return;
                pathName = "/";
            }
            else
            {
                // Strip off the Root name from the path and correct the path separators for use with SRS
                pathName = pathName.Substring(4, pathName.Length - 4);
                pathName = pathName.Replace(@"\", "/");
            }

            // we're good to go with deployment, "lock" the UI
            btnGo.Enabled = false;
            btnDeploy.Enabled = false;

            // set the progress bar
            progress.Maximum = reportList.Items.Count;
            progress.Visible = true;

            //get the datasource
            string selectedDS = String.Empty;
            selectedDS = dataSources.SelectedItems[0].Text;

            StringBuilder result = new StringBuilder();

            // go through items
            foreach (ListViewItem item in reportList.Items)
            {
                if (Path.GetExtension(item.Name).ToUpper() == ".RDL")
                {
                    //we're deploying a report
                    result.AppendLine("Starting deployement of report " + item.Name);
                    result.AppendLine(DeployReport(item.Name, pathName, dsLocation.Text, selectedDS));
                }
                else if (Path.GetExtension(item.Name).ToUpper() == ".SMDL")
                {
                    //we're deploying a model - NOT TESTED
                    result.AppendLine("Starting deployement of model " + item.Name);
                    result.AppendLine(DeployModel(item.Name, pathName, dsLocation.Text, selectedDS));
                }

                //update the progress bar
                progress.Increment(1);
            }

            result.AppendLine("Job completed");

            DoLoggingStuff(result);

            progress.Visible = false;
            btnGo.Enabled = true;
            btnDeploy.Enabled = true;
        }


        private void DoLoggingStuff(StringBuilder result)
        {
            string path = Path.GetTempFileName();
            File.AppendAllText(path, result.ToString());
            File.Move(path, path + ".txt");
            Process.Start(path + ".txt");
        }


        private void reportServer_Click(object sender, EventArgs e)
        {
            if (reportServer.ReadOnly == true)
            {
                // reset the rs object
                btnDeploy.Enabled = false;
                btnGo.Enabled = true;

                rs = null;

                ClearServerInfo();
                
                //and give control back to the user
                reportServer.ReadOnly = false;
            }
        }


    }
}