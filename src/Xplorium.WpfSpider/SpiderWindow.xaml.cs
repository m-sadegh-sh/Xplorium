namespace Xplorium.WpfSpider {
    using System;
    using System.ComponentModel;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shell;

    using Xplorium.WSE;

    using Brush = System.Windows.Media.Brush;
    using Color = System.Drawing.Color;

    public partial class SpiderWindow {
        private bool _closeWithoutQuestion;
        private bool _forceToClose;
        private Spider _spider;

        public SpiderWindow() {
            InitializeComponent();
        }

        public BackgroundWorker SpiderWorker { get; private set; }

        private delegate void LoggerCallback(string message, Color foreColor);

        private delegate void TaskbarItemInfoCallback(TaskbarItemProgressState taskbarItemProgressState);

        public bool Closedd { get; set; }

        private void TrackChanges() {
            if (SpiderWorker.IsBusy)
                btnStartCancel.Content = SpiderWorker.CancellationPending ? "Cancelling" : "Cancel";
            else
                btnStartCancel.Content = "Start";
            btnStartCancel.IsEnabled = !SpiderWorker.CancellationPending;
        }

        private void BtnCloseClick(object sender, RoutedEventArgs e) {
            Close();
        }

        private void SpiderWindow_Closing(object sender, CancelEventArgs e) {
            if (_forceToClose) {
                pl.Visible("Closing is pending.\n\nPlease wait to completing task.");
                e.Cancel = true;
            } else if (!_closeWithoutQuestion) {
                if (SpiderWorker.IsBusy) {
                    var messageWindowResult = App.ShowWindow(this, string.Format("Spidering haven't been completed yet!\n\nAre you sure you want to close {0}?", Title), Title, MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageWindowResult == MessageBoxResult.Yes) {
                        if (SpiderWorker.IsBusy) {
                            SpiderWorker.CancelAsync();
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
            Closedd = !e.Cancel;
        }

        private void SpiderWindow_Loaded(object sender, RoutedEventArgs e) {
            SpiderWorker = new BackgroundWorker {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };
            SpiderWorker.DoWork += BwSpiderDoWork;
            SpiderWorker.ProgressChanged += BwSpiderProgressChanged;
            SpiderWorker.RunWorkerCompleted += BwSpiderRunWorkerCompleted;

            ResetUi();
        }

        private void ResetUi() {
            tbCurrentTask.Text = tbOverallProgress.Text = tbSubSectionProgress.Text = string.Empty;
            icLogs.Items.Clear();
            pbSubSectionProgress.Maximum = pbOverallProgress.Maximum = 1;
            pbSubSectionProgress.Value = pbOverallProgress.Value = 0;
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

        private void InvokeCounter() {
            if (Dispatcher.Thread == Thread.CurrentThread) {
                pbSubSectionProgress.Maximum = _spider.CurrentSubSectionCount;
                pbSubSectionProgress.Value = _spider.CurrentSubSectionIndex;
                var percent = (double) (_spider.CurrentSubSectionIndex*100)/_spider.CurrentSubSectionCount;
                tbSubSectionProgress.Text = string.Format("{0:0.##}% of {1}", percent, _spider.CurrentSubSectionCount);
            } else
                Dispatcher.BeginInvoke(new CounterEventHandler(InvokeCounter));
        }

        private void InvokeMessageBox() {
            App.ShowWindow(this, "There isn't any cache to be fetching.", Title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        private void BtnStartCancelClick(object sender, EventArgs e) {
            if (SpiderWorker.IsBusy) {
                var messageWindowResult = App.ShowWindow(this, "Are you sure you want to cancel spidering?", Title, MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageWindowResult == MessageBoxResult.Yes) {
                    SpiderWorker.CancelAsync();
                    pl.Visible("Please wait while we're trying to cancelling");
                }
            } else
                StartSpidering();
            TrackChanges();
        }

        private void StartSpidering() {
            ConfigureTaskbarItemInfo(TaskbarItemProgressState.Indeterminate);
            _spider = new Spider(InvokeLogger, InvokeCounter, 100);
            if (_spider.TotalFetchedCaches == 0) {
                Dispatcher.BeginInvoke(new CounterEventHandler(InvokeMessageBox));
                ResetUi();
            } else
                SpiderWorker.RunWorkerAsync();
        }

        private void BwSpiderDoWork(object sender, DoWorkEventArgs e) {
            ConfigureTaskbarItemInfo(TaskbarItemProgressState.Normal);
            while (_spider.CurrentCacheIndex < _spider.TotalFetchedCaches && _spider.CanBeContinued) {
                SpiderWorker.ReportProgress(_spider.CurrentCacheIndex);
                _spider.Next();
                if (SpiderWorker.CancellationPending || !_spider.CanBeContinued) {
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

        private void BwSpiderProgressChanged(object sender, ProgressChangedEventArgs e) {
            if (_spider == null)
                return;

            pbOverallProgress.Maximum = _spider.TotalFetchedCaches;
            pbOverallProgress.Value = e.ProgressPercentage;
            TaskbarItemInfo.ProgressValue = (double) e.ProgressPercentage/_spider.TotalFetchedCaches;

            if (_spider.TotalFetchedCaches == 0)
                tbOverallProgress.Text = "Unknown";
            else {
                var percent = (double) (_spider.CurrentCacheIndex*100)/_spider.TotalFetchedCaches;
                tbOverallProgress.Text = string.Format("{0:0.##}% of {1}", percent, _spider.TotalFetchedCaches);
            }
        }

        private void BwSpiderRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            pl.Hidden();
            if (e.Cancelled)
                App.ShowWindow(this, "The spidering task has been cancelled", Title, MessageBoxButton.OK, MessageBoxImage.Information);
            else if (e.Error != null) {
                TaskbarItemInfo.ProgressState = TaskbarItemProgressState.Error;
                App.ShowWindow(this, e.Error.Message, Title, MessageBoxButton.OK, MessageBoxImage.Exclamation);
            } else
                App.ShowWindow(this, "All of fetched caches have beed spidered successfully.", Title, MessageBoxButton.OK, MessageBoxImage.Information);
            if (!_spider.Unlocked)
                _spider.Unlock();
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

        private void SpiderWindow_Closed(object sender, EventArgs e) {
            Closedd = true;
        }
    }
}