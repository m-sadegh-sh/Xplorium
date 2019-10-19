namespace Xplorium.WinSpider
{
    partial class CacherForm
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
            this.tlpContainer = new System.Windows.Forms.TableLayoutPanel();
            this.tlpControls = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pbOverall = new System.Windows.Forms.ProgressBar();
            this.lblCurrentTask = new System.Windows.Forms.Label();
            this.lvLogs = new System.Windows.Forms.ListView();
            this.cTasks = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.chbAutoScroll = new System.Windows.Forms.CheckBox();
            this.lblTransferredBytes = new System.Windows.Forms.Label();
            this.pnlButtonContainer = new System.Windows.Forms.Panel();
            this.tlpButtons = new System.Windows.Forms.TableLayoutPanel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnStartCancel = new System.Windows.Forms.Button();
            this.btnToBackground = new System.Windows.Forms.Button();
            this.bwCacher = new System.ComponentModel.BackgroundWorker();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblElapsedTime = new System.Windows.Forms.Label();
            this.lblOverallPercent = new System.Windows.Forms.Label();
            this.tlpContainer.SuspendLayout();
            this.tlpControls.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.pnlButtonContainer.SuspendLayout();
            this.tlpButtons.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpContainer
            // 
            this.tlpContainer.AutoSize = true;
            this.tlpContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpContainer.ColumnCount = 1;
            this.tlpContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpContainer.Controls.Add(this.tlpControls, 0, 0);
            this.tlpContainer.Controls.Add(this.pnlButtonContainer, 0, 1);
            this.tlpContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContainer.Location = new System.Drawing.Point(0, 0);
            this.tlpContainer.Margin = new System.Windows.Forms.Padding(0);
            this.tlpContainer.Name = "tlpContainer";
            this.tlpContainer.RowCount = 2;
            this.tlpContainer.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpContainer.Size = new System.Drawing.Size(794, 400);
            this.tlpContainer.TabIndex = 0;
            // 
            // tlpControls
            // 
            this.tlpControls.AutoSize = true;
            this.tlpControls.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpControls.ColumnCount = 1;
            this.tlpControls.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpControls.Controls.Add(this.lblTitle, 0, 0);
            this.tlpControls.Controls.Add(this.pbOverall, 0, 2);
            this.tlpControls.Controls.Add(this.lblCurrentTask, 0, 1);
            this.tlpControls.Controls.Add(this.lvLogs, 0, 4);
            this.tlpControls.Controls.Add(this.tableLayoutPanel1, 0, 3);
            this.tlpControls.Controls.Add(this.tableLayoutPanel2, 0, 5);
            this.tlpControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpControls.Location = new System.Drawing.Point(10, 10);
            this.tlpControls.Margin = new System.Windows.Forms.Padding(10);
            this.tlpControls.Name = "tlpControls";
            this.tlpControls.RowCount = 6;
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpControls.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpControls.Size = new System.Drawing.Size(774, 335);
            this.tlpControls.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblTitle.Location = new System.Drawing.Point(3, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(3, 0, 3, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(768, 18);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Xplorium.NET Cacher";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbOverall
            // 
            this.pbOverall.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbOverall.Location = new System.Drawing.Point(0, 51);
            this.pbOverall.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.pbOverall.Name = "pbOverall";
            this.pbOverall.Size = new System.Drawing.Size(774, 16);
            this.pbOverall.TabIndex = 3;
            // 
            // lblCurrentTask
            // 
            this.lblCurrentTask.AutoSize = true;
            this.lblCurrentTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCurrentTask.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblCurrentTask.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentTask.Location = new System.Drawing.Point(3, 28);
            this.lblCurrentTask.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.lblCurrentTask.Name = "lblCurrentTask";
            this.lblCurrentTask.Size = new System.Drawing.Size(768, 13);
            this.lblCurrentTask.TabIndex = 4;
            this.lblCurrentTask.Text = "[Current task]";
            this.lblCurrentTask.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lvLogs
            // 
            this.lvLogs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cTasks});
            this.lvLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvLogs.FullRowSelect = true;
            this.lvLogs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvLogs.Location = new System.Drawing.Point(0, 90);
            this.lvLogs.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.lvLogs.MultiSelect = false;
            this.lvLogs.Name = "lvLogs";
            this.lvLogs.ShowItemToolTips = true;
            this.lvLogs.Size = new System.Drawing.Size(774, 222);
            this.lvLogs.TabIndex = 6;
            this.lvLogs.UseCompatibleStateImageBehavior = false;
            this.lvLogs.View = System.Windows.Forms.View.Details;
            this.lvLogs.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lvLogs_ItemSelectionChanged);
            // 
            // cTasks
            // 
            this.cTasks.Text = "Detailed reports";
            this.cTasks.Width = 678;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel2.Controls.Add(this.chbAutoScroll, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.lblTransferredBytes, 2, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 317);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(774, 18);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // chbAutoScroll
            // 
            this.chbAutoScroll.AutoSize = true;
            this.chbAutoScroll.Checked = true;
            this.chbAutoScroll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbAutoScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chbAutoScroll.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.chbAutoScroll.Location = new System.Drawing.Point(0, 0);
            this.chbAutoScroll.Margin = new System.Windows.Forms.Padding(0);
            this.chbAutoScroll.Name = "chbAutoScroll";
            this.chbAutoScroll.Size = new System.Drawing.Size(124, 18);
            this.chbAutoScroll.TabIndex = 1;
            this.chbAutoScroll.Text = "&Auto scroll logger";
            this.chbAutoScroll.UseVisualStyleBackColor = true;
            // 
            // lblTransferredBytes
            // 
            this.lblTransferredBytes.AutoSize = true;
            this.lblTransferredBytes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTransferredBytes.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblTransferredBytes.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblTransferredBytes.Location = new System.Drawing.Point(670, 0);
            this.lblTransferredBytes.Name = "lblTransferredBytes";
            this.lblTransferredBytes.Size = new System.Drawing.Size(101, 18);
            this.lblTransferredBytes.TabIndex = 4;
            this.lblTransferredBytes.Text = "[Transferred bytes]";
            this.lblTransferredBytes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlButtonContainer
            // 
            this.pnlButtonContainer.AutoSize = true;
            this.pnlButtonContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlButtonContainer.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.pnlButtonContainer.Controls.Add(this.tlpButtons);
            this.pnlButtonContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlButtonContainer.Location = new System.Drawing.Point(0, 355);
            this.pnlButtonContainer.Margin = new System.Windows.Forms.Padding(0);
            this.pnlButtonContainer.Name = "pnlButtonContainer";
            this.pnlButtonContainer.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.pnlButtonContainer.Size = new System.Drawing.Size(794, 45);
            this.pnlButtonContainer.TabIndex = 1;
            // 
            // tlpButtons
            // 
            this.tlpButtons.AutoSize = true;
            this.tlpButtons.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tlpButtons.BackColor = System.Drawing.SystemColors.Control;
            this.tlpButtons.ColumnCount = 4;
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpButtons.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tlpButtons.Controls.Add(this.btnClose, 3, 0);
            this.tlpButtons.Controls.Add(this.btnStartCancel, 2, 0);
            this.tlpButtons.Controls.Add(this.btnToBackground, 0, 0);
            this.tlpButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpButtons.Location = new System.Drawing.Point(0, 1);
            this.tlpButtons.Margin = new System.Windows.Forms.Padding(0);
            this.tlpButtons.Name = "tlpButtons";
            this.tlpButtons.Padding = new System.Windows.Forms.Padding(10);
            this.tlpButtons.RowCount = 1;
            this.tlpButtons.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tlpButtons.Size = new System.Drawing.Size(794, 44);
            this.tlpButtons.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.AutoSize = true;
            this.btnClose.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.FlatAppearance.BorderSize = 10;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnClose.Location = new System.Drawing.Point(715, 10);
            this.btnClose.Margin = new System.Windows.Forms.Padding(0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Padding = new System.Windows.Forms.Padding(10, 1, 10, 1);
            this.btnClose.Size = new System.Drawing.Size(69, 24);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnStartCancel
            // 
            this.btnStartCancel.AutoSize = true;
            this.btnStartCancel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStartCancel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnStartCancel.FlatAppearance.BorderSize = 10;
            this.btnStartCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnStartCancel.Location = new System.Drawing.Point(596, 10);
            this.btnStartCancel.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnStartCancel.Name = "btnStartCancel";
            this.btnStartCancel.Padding = new System.Windows.Forms.Padding(10, 1, 10, 1);
            this.btnStartCancel.Size = new System.Drawing.Size(109, 24);
            this.btnStartCancel.TabIndex = 0;
            this.btnStartCancel.Text = "[Start/Cancel]";
            this.btnStartCancel.UseVisualStyleBackColor = false;
            this.btnStartCancel.Click += new System.EventHandler(this.btnStartCancel_Click);
            // 
            // btnToBackground
            // 
            this.btnToBackground.AutoSize = true;
            this.btnToBackground.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnToBackground.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnToBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnToBackground.FlatAppearance.BorderSize = 10;
            this.btnToBackground.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnToBackground.Location = new System.Drawing.Point(10, 10);
            this.btnToBackground.Margin = new System.Windows.Forms.Padding(0);
            this.btnToBackground.Name = "btnToBackground";
            this.btnToBackground.Padding = new System.Windows.Forms.Padding(10, 1, 10, 1);
            this.btnToBackground.Size = new System.Drawing.Size(119, 24);
            this.btnToBackground.TabIndex = 0;
            this.btnToBackground.Text = "To &background";
            this.btnToBackground.UseVisualStyleBackColor = false;
            this.btnToBackground.Click += new System.EventHandler(this.btnToBackground_Click);
            // 
            // bwCacher
            // 
            this.bwCacher.WorkerReportsProgress = true;
            this.bwCacher.WorkerSupportsCancellation = true;
            this.bwCacher.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwCacher_DoWork);
            this.bwCacher.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwCacher_ProgressChanged);
            this.bwCacher.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwCacher_RunWorkerCompleted);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.lblOverallPercent, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblElapsedTime, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 72);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(774, 13);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(92, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "[0% have been completed]";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "[00:00 Elapsed]";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblElapsedTime
            // 
            this.lblElapsedTime.AutoSize = true;
            this.lblElapsedTime.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblElapsedTime.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblElapsedTime.Location = new System.Drawing.Point(3, 0);
            this.lblElapsedTime.Name = "lblElapsedTime";
            this.lblElapsedTime.Size = new System.Drawing.Size(83, 13);
            this.lblElapsedTime.TabIndex = 4;
            this.lblElapsedTime.Text = "[00:00 Elapsed]";
            this.lblElapsedTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOverallPercent
            // 
            this.lblOverallPercent.AutoSize = true;
            this.lblOverallPercent.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblOverallPercent.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblOverallPercent.Location = new System.Drawing.Point(630, 0);
            this.lblOverallPercent.Name = "lblOverallPercent";
            this.lblOverallPercent.Size = new System.Drawing.Size(141, 13);
            this.lblOverallPercent.TabIndex = 4;
            this.lblOverallPercent.Text = "[0% have been completed]";
            this.lblOverallPercent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CacherForm
            // 
            this.AcceptButton = this.btnStartCancel;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.CancelButton = this.btnToBackground;
            this.ClientSize = new System.Drawing.Size(794, 400);
            this.Controls.Add(this.tlpContainer);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CacherForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xplorium.NET Cacher";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CacherForm_FormClosing);
            this.Load += new System.EventHandler(this.CacherForm_Load);
            this.tlpContainer.ResumeLayout(false);
            this.tlpContainer.PerformLayout();
            this.tlpControls.ResumeLayout(false);
            this.tlpControls.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.pnlButtonContainer.ResumeLayout(false);
            this.pnlButtonContainer.PerformLayout();
            this.tlpButtons.ResumeLayout(false);
            this.tlpButtons.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpContainer;
        private System.Windows.Forms.TableLayoutPanel tlpControls;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlButtonContainer;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TableLayoutPanel tlpButtons;
        private System.Windows.Forms.Button btnStartCancel;
        private System.Windows.Forms.ProgressBar pbOverall;
        private System.Windows.Forms.CheckBox chbAutoScroll;
        private System.Windows.Forms.Label lblCurrentTask;
        private System.Windows.Forms.ListView lvLogs;
        private System.Windows.Forms.ColumnHeader cTasks;
        private System.Windows.Forms.Label lblTransferredBytes;
        private System.ComponentModel.BackgroundWorker bwCacher;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button btnToBackground;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblOverallPercent;
        private System.Windows.Forms.Label lblElapsedTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}