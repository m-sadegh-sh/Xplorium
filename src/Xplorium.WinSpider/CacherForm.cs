namespace Xplorium.WinSpider {
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    using Xplorium.Core;
    using Xplorium.WSE;

    public partial class CacherForm : Form {
        private Cacher cacher;

        private delegate void LoggerCallback(string message, Color foreColor);

        private bool _closeWithoutQuestion;
        private bool _forceToClose;

        private void TrackChanges() {
            if (bwCacher.IsBusy) {
                if (bwCacher.CancellationPending)
                    btnStartCancel.Text = "&Cancelling";
                else
                    btnStartCancel.Text = "&Cancel";
            } else
                btnStartCancel.Text = "&Start";
            btnStartCancel.Enabled = !bwCacher.CancellationPending;
        }

        public CacherForm() {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e) {
            Close();
        }

        private void CacherForm_Load(object sender, EventArgs e) {
            ResetUI();
        }

        private void ResetUI() {
            lblCurrentTask.Text = lblElapsedTime.Text = lblOverallPercent.Text = lblTransferredBytes.Text = string.Empty;
            lvLogs.Items.Clear();
            pbOverall.Maximum = pbOverall.Value = 0;
            TrackChanges();
        }

        private void InvokeLogger(string message, Color foreColor) {
            if (InvokeRequired)
                Invoke(new LoggerCallback(InvokeLogger), new object[] {message, foreColor});
            else {
                if (foreColor == new StatusColors().Title)
                    lblCurrentTask.Text = message;
                else {
                    lvLogs.Items.Add(new ListViewItem(message) {
                        ForeColor = foreColor
                    });
                    if (chbAutoScroll.Checked)
                        lvLogs.EnsureVisible(lvLogs.Items.Count - 1);
                }
            }
        }

        private void btnStartCancel_Click(object sender, EventArgs e) {
            if (bwCacher.IsBusy) {
                var dialogResult = MessageBox.Show("Are you sure you want to cancel caching?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                    bwCacher.CancelAsync();
            } else
                bwCacher.RunWorkerAsync();
            TrackChanges();
        }

        private void CacherForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (_forceToClose) {
                MessageBox.Show("Closing is pending.\n\nPlease wait to completing task.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            } else if (!_closeWithoutQuestion) {
                var temp = string.Empty;
                if (bwCacher.IsBusy)
                    temp = "Caching haven't been completed yet!\n\n";
                var dialogResult = MessageBox.Show(string.Format("{0}Are you sure you want to close {1}?", temp, Text), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes) {
                    if (bwCacher.IsBusy) {
                        bwCacher.CancelAsync();
                        btnClose.Text = "Closing...";
                        btnClose.Enabled = false;
                        TrackChanges();
                        _forceToClose = true;
                        e.Cancel = true;
                    } else
                        e.Cancel = dialogResult == DialogResult.No;
                } else
                    e.Cancel = true;
            }
        }

        private void bwCacher_DoWork(object sender, DoWorkEventArgs e) {
            cacher = new Cacher(InvokeLogger, 100);
            if (cacher.TotalFetchedUrls == 0) {
                MessageBox.Show("There isn't any url to be loading.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
            while (cacher.CurrentUrlIndex < cacher.TotalFetchedUrls) {
                cacher.Next();
                bwCacher.ReportProgress(cacher.CurrentUrlIndex);
                if (bwCacher.CancellationPending) {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void bwCacher_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            if (cacher == null)
                return;

            pbOverall.Maximum = cacher.TotalFetchedUrls;
            pbOverall.Value = e.ProgressPercentage;

            if (cacher.TotalFetchedUrls == 0)
                lblOverallPercent.Text = lblTransferredBytes.Text = "Unknown";
            else {
                var percent = (double) (cacher.CurrentUrlIndex*100)/cacher.TotalFetchedUrls;
                lblOverallPercent.Text = string.Format("{0:0.##}% completed", percent);
                var average = Convert.ToInt64(cacher.TransferredBytes/cacher.Watcher.Elapsed.TotalSeconds);
                lblTransferredBytes.Text = string.Format("Transferred: {0}, Speed average : {1}", cacher.TransferredBytes.FormatBytes(), average.FormatBytes());
            }
        }

        private void bwCacher_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Cancelled)
                MessageBox.Show("The caching task has been cancelled", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (e.Error != null)
                MessageBox.Show(e.Error.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                MessageBox.Show("All of fetched urls have beed cached successfully.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            cacher.Unlock();
            ResetUI();
            if (_forceToClose) {
                _closeWithoutQuestion = true;
                _forceToClose = false;
                Close();
            }
        }

        private void btnToBackground_Click(object sender, EventArgs e) {}

        private void lvLogs_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e) {
            e.Item.Selected = false;
        }
    }
}