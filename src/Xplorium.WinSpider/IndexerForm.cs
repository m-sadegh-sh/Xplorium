namespace Xplorium.WinSpider
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Xplorium.Core;
    using Xplorium.WSE;

    public partial class IndexerForm : Form
    {
        private Indexer indexer;
        private delegate void LoggerCallback(string message, Color foreColor);
        private delegate void CounterCallback();
        private bool _closeWithoutQuestion;
        private bool _forceToClose;

        private void TrackChanges()
        {
            if (bwIndexer.IsBusy)
                if (bwIndexer.CancellationPending)
                    btnStartCancel.Text = "&Cancelling";
                else
                    btnStartCancel.Text = "&Cancel";
            else
                btnStartCancel.Text = "&Start";
            btnStartCancel.Enabled = !bwIndexer.CancellationPending;
        }

        public IndexerForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void IndexingForm_Load(object sender, EventArgs e)
        {
            ResetUI();
        }

        private void ResetUI()
        {
            lblCurrentTask.Text = lblOverallPercent.Text = lblSubSectionPercent.Text = string.Empty;
            lvLogs.Items.Clear();
            pbSubSection.Maximum = pbSubSection.Value = pbOverall.Maximum = pbOverall.Value = 0;
            TrackChanges();
        }

        private void InvokeLogger(string message, Color foreColor)
        {
            if (InvokeRequired)
                Invoke(new LoggerCallback(InvokeLogger), new object[] { message, foreColor });
            else
            {
                if (foreColor == new StatusColors().Title)
                    lblCurrentTask.Text = message;
                else
                {
                    lvLogs.Items.Add(new ListViewItem(message) { ForeColor = foreColor });
                    if (chbAutoScroll.Checked)
                        lvLogs.EnsureVisible(lvLogs.Items.Count - 1);
                }
            }
        }

        private void InvokeCounter()
        {
            if (InvokeRequired)
                Invoke(new CounterEventHandler(InvokeCounter));
            else
            {
                pbSubSection.Maximum = indexer.CurrentSubSectionCount;
                pbSubSection.Value = indexer.CurrentSubSectionIndex;
                var percent = (double)(indexer.CurrentSubSectionIndex * 100) / indexer.CurrentSubSectionCount;
                lblSubSectionPercent.Text = string.Format("{0:0.##}% completed", percent);
            }
        }

        private void btnStartCancel_Click(object sender, EventArgs e)
        {
            if (bwIndexer.IsBusy)
            {
                var dialogResult = MessageBox.Show("Are you sure you want to cancel indexing?", Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                    bwIndexer.CancelAsync();
            }
            else
                bwIndexer.RunWorkerAsync();
            TrackChanges();
        }

        private void IndexerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_forceToClose)
            {
                MessageBox.Show("Closing is pending.\n\nPlease wait to completing task.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
            else if (!_closeWithoutQuestion)
            {
                var temp = string.Empty;
                if (bwIndexer.IsBusy)
                    temp = "Indexing haven't been completed yet!\n\n";
                var dialogResult = MessageBox.Show(string.Format("{0}Are you sure you want to close {1}?", temp, Text), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                    if (bwIndexer.IsBusy)
                    {
                        bwIndexer.CancelAsync();
                        btnClose.Text = "Closing...";
                        btnClose.Enabled = false;
                        TrackChanges();
                        _forceToClose = true;
                        e.Cancel = true;
                    }
                    else
                        e.Cancel = dialogResult == DialogResult.No;
                else
                    e.Cancel = true;
            }
        }

        private void bwIndexer_DoWork(object sender, DoWorkEventArgs e)
        {
            indexer = new Indexer(new LoggerEventHandler(InvokeLogger), new CounterEventHandler(InvokeCounter), 100);
            if (indexer.TotalFetchedCaches == 0)
            {
                MessageBox.Show("There isn't any cache to be loading.", Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
            while (indexer.CurrentCacheIndex < indexer.TotalFetchedCaches)
            {
                bwIndexer.ReportProgress(indexer.CurrentCacheIndex);
                indexer.Next();
                if (bwIndexer.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void bwIndexer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (indexer == null)
                return;

            pbOverall.Maximum = indexer.TotalFetchedCaches;
            pbOverall.Value = e.ProgressPercentage;

            if (indexer.TotalFetchedCaches == 0)
                lblOverallPercent.Text = "Unknown";
            else
            {
                var percent = (double)(indexer.CurrentCacheIndex * 100) / indexer.TotalFetchedCaches;
                lblOverallPercent.Text = string.Format("{0:0.##}% completed", percent);
            }
        }

        private void bwIndexer_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
                MessageBox.Show("The indexing task has been cancelled", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (e.Error != null)
                MessageBox.Show(e.Error.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                MessageBox.Show("All of fetched caches have beed indexed successfully.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
            indexer.Lock();
            ResetUI();
            if (_forceToClose)
            {
                _closeWithoutQuestion = true;
                _forceToClose = false;
                Close();
            }
        }

        private void btnToBackground_Click(object sender, EventArgs e)
        {

        }

        private void lvLogs_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            e.Item.Selected = false;
        }

        private void lblSubSectionPercent_TextChanged(object sender, EventArgs e)
        {
            var _sender = (Label)sender;
            _sender.Visible = !string.IsNullOrWhiteSpace(_sender.Text);
        }
    }
}
