namespace Xplorium.WinSpider {
    using System;
    using System.Windows.Forms;

    public partial class SelectorForm : Form {
        public SelectorForm() {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e) {
            Close();
        }

        private void btnIndexer_Click(object sender, EventArgs e) {
            //var indexerForm = new IndexerForm();
            //indexerForm.Show(this);
        }

        private void btnCacher_Click(object sender, EventArgs e) {
            var cacherForm = new CacherForm();
            cacherForm.Show(this);
        }

        private void SelectorForm_FormClosing(object sender, FormClosingEventArgs e) {
            var dialogResult = MessageBox.Show(string.Format("Are you sure you want to close {0}?", Text), Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            e.Cancel = dialogResult == DialogResult.No;
        }
    }
}