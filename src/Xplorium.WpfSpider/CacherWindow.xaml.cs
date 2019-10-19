namespace Xplorium.WpfSpider {
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shell;

    using Xplorium.Core;
    using Xplorium.WSE;

    using Brush = System.Windows.Media.Brush;
    using Color = System.Drawing.Color;

    public partial class CacherWindow {
        private Cacher _cacher;

        private delegate void LoggerCallback(string message, Color foreColor);

        private delegate void TaskbarItemInfoCallback(TaskbarItemProgressState taskbarItemProgressState);

        private bool _closeWithoutQuestion;
        private bool _forceToClose;
        public BackgroundWorker CacherWorker { get; private set; }
        public bool Closedd { get; set; }

        private void TrackChanges() {
            if (CacherWorker.IsBusy)
                btnStartCancel.Content = CacherWorker.CancellationPending ? "Cancelling" : "Cancel";
            else
                btnStartCancel.Content = "Start";
            btnStartCancel.IsEnabled = !CacherWorker.CancellationPending;
        }

        public CacherWindow() {
            InitializeComponent();
        }

        private void BtnCloseClick(object sender, RoutedEventArgs e) {
            Close();
        }

        private void CacherWindow_Closing(object sender, CancelEventArgs e) {
            if (_forceToClose) {
                pl.Visible("Closing is pending.\n\nPlease wait to completing task.");
                e.Cancel = true;
            } else if (!_closeWithoutQuestion) {
                if (CacherWorker.IsBusy) {
                    var messageWindowResult = App.ShowWindow(this, string.Format("Spidering haven't been completed yet!\n\nAre you sure you want to close {0}?", Title), Title, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageWindowResult == MessageBoxResult.Yes) {
                        if (CacherWorker.IsBusy) {
                            CacherWorker.CancelAsync();
                            btnClose.Content = "Closing...";
                            btnClose.IsEnabled = false;
                            TrackChanges();
                            pl.Visible("Please wait while we're trying to closing");
                            _forceToClose = true;
                            e.Cancel = true;
                        } else
                            e.Cancel = messageWindowResult == MessageBoxResult.No;
                    } else
                        e.Cancel = true;
                }
            }
        }

        private void CacherWindow_Loaded(object sender, RoutedEventArgs e) {
            CacherWorker = new BackgroundWorker {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            CacherWorker.DoWork += BwCacherDoWork;
            CacherWorker.ProgressChanged += BwCacherProgressChanged;
            CacherWorker.RunWorkerCompleted += BwCacherRunWorkerCompleted;

            ResetUi();
        }

        private void ResetUi() {
            tbCurrentTask.Text = tbOverallProgress.Text = tbTransferredBytes.Text = string.Empty;
            icLogs.Items.Clear();
            pbOverallProgress.Maximum = 1;
            pbOverallProgress.Value = 0;
            TaskbarItemInfo.ProgressState = TaskbarItemProgressState.None;
            TaskbarItemInfo.ProgressValue = 0;
            TrackChanges();
        }

        private void InvokeLogger(string message, Color foreground) {
            if (Dispatcher.Thread == Thread.CurrentThread) {
                if (foreground == new StatusColors().Title)
                    tbCurrentTask.Text = message;
                else {
                    var brush = (Brush) new BrushConverter().ConvertFromString(foreground.Name);
                    icLogs.Items.Add(new ListViewItem {
                        Content = message,
                        Foreground = brush
                    });
                    if (chbAutoScrollLogger.IsChecked != null) {
                        if (chbAutoScrollLogger.IsChecked.Value)
                            svLogs.ScrollToBottom();
                    }
                }
            } else
                Dispatcher.BeginInvoke(new LoggerCallback(InvokeLogger), new object[] {message, foreground});
        }

        private void InvokeMessageBox() {
            App.ShowWindow(this, "There isn't any url to be fetching.", Title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void BtnStartCancelClick(object sender, EventArgs e) {
            if (CacherWorker.IsBusy) {
                var messageWindowResult = App.ShowWindow(this, "Are you sure you want to cancel caching?", Title, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageWindowResult == MessageBoxResult.Yes) {
                    CacherWorker.CancelAsync();
                    pl.Visible("Please wait while we're trying to cancelling");
                }
            } else
                StartCaching();
            TrackChanges();
        }

        private void StartCaching() {
            ConfigureTaskbarItemInfo(TaskbarItemProgressState.Indeterminate);
            _cacher = new Cacher(InvokeLogger, 100);
            if (_cacher.TotalFetchedUrls == 0) {
                Dispatcher.BeginInvoke(new CounterEventHandler(InvokeMessageBox));
                ResetUi();
            } else
                CacherWorker.RunWorkerAsync();
        }

        private void BwCacherDoWork(object sender, DoWorkEventArgs e) {
            ConfigureTaskbarItemInfo(TaskbarItemProgressState.Normal);
            while (_cacher.CurrentUrlIndex < _cacher.TotalFetchedUrls) {
                CacherWorker.ReportProgress(_cacher.CurrentUrlIndex);
                _cacher.Next();
                if (CacherWorker.CancellationPending || !_cacher.CanBeContinued) {
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void ConfigureTaskbarItemInfo(TaskbarItemProgressState taskbarItemProgressState) {
            if (Dispatcher.Thread == Thread.CurrentThread)
                TaskbarItemInfo.ProgressState = taskbarItemProgressState;
            else
                Dispatcher.BeginInvoke(new TaskbarItemInfoCallback(ConfigureTaskbarItemInfo), new object[] {taskbarItemProgressState});
        }

        private void BwCacherProgressChanged(object sender, ProgressChangedEventArgs e) {
            if (_cacher == null)
                return;

            pbOverallProgress.Maximum = _cacher.TotalFetchedUrls;
            pbOverallProgress.Value = e.ProgressPercentage;
            TaskbarItemInfo.ProgressValue = (double) e.ProgressPercentage/_cacher.TotalFetchedUrls;
            tbTransferredBytes.Text = _cacher.TransferredBytes.FormatBytes();

            if (_cacher.TotalFetchedUrls == 0)
                tbOverallProgress.Text = "Unknown";
            else {
                var percent = (double) (_cacher.CurrentUrlIndex*100)/_cacher.TotalFetchedUrls;
                tbOverallProgress.Text = string.Format("{0:0.##}% of {1}", percent, _cacher.TotalFetchedUrls);
            }
        }

        private void BwCacherRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            pl.Hidden();
            if (e.Cancelled)
                App.ShowWindow(this, "The caching task has been cancelled", Title, MessageBoxButton.OK, MessageBoxImage.Information);
            else if (e.Error != null) {
                TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Error;
                App.ShowWindow(this, e.Error.Message, Title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            } else
                App.ShowWindow(this, "All of fetched urls have beed cached successfully.", Title, MessageBoxButton.OK, MessageBoxImage.Information);
            if (!_cacher.Unlocked)
                _cacher.Unlock();
            ResetUi();
            if (_forceToClose) {
                _closeWithoutQuestion = true;
                _forceToClose = false;
                Close();
            }
        }

        private void BtnToBackgroundClick(object sender, RoutedEventArgs e) {
            Hide();
        }

        private void CacherWindow_Closed(object sender, EventArgs e) {
            Closedd = true;
        }
    }
}