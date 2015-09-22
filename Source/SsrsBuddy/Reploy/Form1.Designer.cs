namespace Reploy
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.reportServer = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGo = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ssrsFolders = new System.Windows.Forms.TreeView();
            this.label3 = new System.Windows.Forms.Label();
            this.dataSources = new System.Windows.Forms.ListView();
            this.label4 = new System.Windows.Forms.Label();
            this.reportList = new System.Windows.Forms.ListView();
            this.dsLocation = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.listSummary = new System.Windows.Forms.Label();
            this.btnDeploy = new System.Windows.Forms.Button();
            this.progress = new System.Windows.Forms.ProgressBar();
            this.folderStatus = new System.Windows.Forms.Label();
            this.dataSourcesStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // reportServer
            // 
            this.reportServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportServer.Location = new System.Drawing.Point(126, 17);
            this.reportServer.Name = "reportServer";
            this.reportServer.Size = new System.Drawing.Size(347, 20);
            this.reportServer.TabIndex = 0;
            this.reportServer.Click += new System.EventHandler(this.reportServer_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Report Service Url :";
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.Location = new System.Drawing.Point(487, 15);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 3;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.getFolders_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(126, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(293, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ex : http://jinpc:8080/reportserver/ReportService2005.asmx";
            // 
            // ssrsFolders
            // 
            this.ssrsFolders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ssrsFolders.HideSelection = false;
            this.ssrsFolders.Location = new System.Drawing.Point(306, 91);
            this.ssrsFolders.Name = "ssrsFolders";
            this.ssrsFolders.Size = new System.Drawing.Size(249, 142);
            this.ssrsFolders.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(303, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Folders on server :";
            // 
            // dataSources
            // 
            this.dataSources.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataSources.HideSelection = false;
            this.dataSources.Location = new System.Drawing.Point(18, 117);
            this.dataSources.MultiSelect = false;
            this.dataSources.Name = "dataSources";
            this.dataSources.Size = new System.Drawing.Size(275, 116);
            this.dataSources.TabIndex = 7;
            this.dataSources.UseCompatibleStateImageBehavior = false;
            this.dataSources.View = System.Windows.Forms.View.List;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Data Sources on server :";
            // 
            // reportList
            // 
            this.reportList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportList.Location = new System.Drawing.Point(18, 306);
            this.reportList.Name = "reportList";
            this.reportList.Size = new System.Drawing.Size(537, 158);
            this.reportList.TabIndex = 9;
            this.reportList.UseCompatibleStateImageBehavior = false;
            this.reportList.View = System.Windows.Forms.View.List;
            // 
            // dsLocation
            // 
            this.dsLocation.Location = new System.Drawing.Point(18, 91);
            this.dsLocation.Name = "dsLocation";
            this.dsLocation.Size = new System.Drawing.Size(275, 20);
            this.dsLocation.TabIndex = 10;
            this.dsLocation.Text = "/Data Sources";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 257);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Reports";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBrowse.Location = new System.Drawing.Point(18, 277);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 12;
            this.btnBrowse.Text = "Browse ...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // listSummary
            // 
            this.listSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.listSummary.Location = new System.Drawing.Point(383, 280);
            this.listSummary.Name = "listSummary";
            this.listSummary.Size = new System.Drawing.Size(172, 23);
            this.listSummary.TabIndex = 14;
            this.listSummary.Text = "{0} report(s), {1} model(s)";
            this.listSummary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.listSummary.Visible = false;
            // 
            // btnDeploy
            // 
            this.btnDeploy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeploy.Enabled = false;
            this.btnDeploy.Location = new System.Drawing.Point(478, 492);
            this.btnDeploy.Name = "btnDeploy";
            this.btnDeploy.Size = new System.Drawing.Size(76, 23);
            this.btnDeploy.TabIndex = 15;
            this.btnDeploy.Text = "Deploy";
            this.btnDeploy.UseVisualStyleBackColor = true;
            this.btnDeploy.Click += new System.EventHandler(this.btnDeploy_Click);
            // 
            // progress
            // 
            this.progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progress.Location = new System.Drawing.Point(15, 492);
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(417, 23);
            this.progress.Step = 1;
            this.progress.TabIndex = 16;
            this.progress.Visible = false;
            // 
            // folderStatus
            // 
            this.folderStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.folderStatus.AutoSize = true;
            this.folderStatus.Location = new System.Drawing.Point(493, 71);
            this.folderStatus.Name = "folderStatus";
            this.folderStatus.Size = new System.Drawing.Size(62, 13);
            this.folderStatus.TabIndex = 17;
            this.folderStatus.Text = "Updating ...";
            this.folderStatus.Visible = false;
            // 
            // dataSourcesStatus
            // 
            this.dataSourcesStatus.AutoSize = true;
            this.dataSourcesStatus.Location = new System.Drawing.Point(231, 71);
            this.dataSourcesStatus.Name = "dataSourcesStatus";
            this.dataSourcesStatus.Size = new System.Drawing.Size(62, 13);
            this.dataSourcesStatus.TabIndex = 18;
            this.dataSourcesStatus.Text = "Updating ...";
            this.dataSourcesStatus.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 537);
            this.Controls.Add(this.dataSourcesStatus);
            this.Controls.Add(this.folderStatus);
            this.Controls.Add(this.progress);
            this.Controls.Add(this.btnDeploy);
            this.Controls.Add(this.listSummary);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dsLocation);
            this.Controls.Add(this.reportList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dataSources);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ssrsFolders);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.reportServer);
            this.MinimumSize = new System.Drawing.Size(500, 480);
            this.Name = "Form1";
            this.Text = "SSRSBuddy - Reports deployment tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox reportServer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView ssrsFolders;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView dataSources;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView reportList;
        private System.Windows.Forms.TextBox dsLocation;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label listSummary;
        private System.Windows.Forms.Button btnDeploy;
        private System.Windows.Forms.ProgressBar progress;
        private System.Windows.Forms.Label folderStatus;
        private System.Windows.Forms.Label dataSourcesStatus;
    }
}

