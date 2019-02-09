using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using NotifSync.UI.Annotations;

namespace NotifSync.UI.Windows
{
    /// <summary>
    /// Interaction logic for ReplyWindowMessageBox.xaml
    /// </summary>
    public partial class ReplyWindowMessageBox : Window, INotifyPropertyChanged
    {
        private string _messageContent = "";
        public ManualResetEvent InputEnteredEvent { get; set; } = new ManualResetEvent(false);

        public string MessageContent
        {
            get => _messageContent;
            set
            {
                if (value == _messageContent) return;
                _messageContent = value;
                OnPropertyChanged();
            }
        }

        public bool IsClosing { get; set; }

        public ReplyWindowMessageBox()
        {
            InitializeComponent();
        }

        public void FinishInput()
        {
            InputEnteredEvent.Set();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            IsClosing = true;
            FinishInput();
            base.OnClosing(e);
        }

        public void AdjustWindowToNotificationWindow([Annotations.NotNull] NotificationWindow window)
        {
            var width = window.OuterBorder.ActualWidth;
            Width = width;

            var point = window.OuterBorder.PointToScreen(new Point(0, 0));
            Left = point.X;
            Top = point.Y + window.OuterBorder.ActualHeight - ActualHeight;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ReplyButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            MessageContent = "";
            Close();
        }
    }
}