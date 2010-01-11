using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WcfClient
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        WcfServer.EchoServiceClient echoService;

        public Window1()
        {
            InitializeComponent();
            echoService = new WcfServer.EchoServiceClient();
            echoService.EchoCompleted += OnEchoCompleted;
        }

        // --------------------------------------------------------------------

        void Simple_Click(object sender, RoutedEventArgs e)
        {
            // just call the service synchronously
            OutputTextblock.Text += echoService.Echo(TextTextBox.Text) + " [Simple]\n";
        }

        // --------------------------------------------------------------------

        // use the Begin/End methods
        void BeginEnd_Click(object sender, RoutedEventArgs e)
        {
            var asyncResult = echoService.BeginEcho(TextTextBox.Text, ar => {
                var result = echoService.EndEcho(ar);

                // OH NO. This function is running on a threadpool thread!
                // We have to jump back to the dispatcher to update the UI
                Dispatcher.Invoke(new Action(() => {
                    OutputTextblock.Text += result + " [Begin/End]\n";
                }));
            }, null);
        }

        // --------------------------------------------------------------------

        // Use the silverlight-style event signaling way
        void EventBased_Click(object sender, RoutedEventArgs e)
        {
            echoService.EchoAsync(TextTextBox.Text);
            // our EchoCompleted function is now disconnected from the method call
            // which is not cool, but at least the result comes back on the right thread
        }

        void OnEchoCompleted(object sender, WcfServer.EchoCompletedEventArgs e)
        {
            OutputTextblock.Text += e.Result + " [EventBased]\n";
        }

        // --------------------------------------------------------------------

        // Create the observable by wrapping the Begin/End pair
        IObservable<string> EchoFunc(string input)
        {
            return Observable.FromAsyncPattern<string, string>(echoService.BeginEcho, echoService.EndEcho)(input);
        }

        void Observable_Click(object sender, RoutedEventArgs e)
        {
            EchoFunc(TextTextBox.Text)
                .Subscribe(result => OutputTextblock.Text += result + " [Observable]\n");
        }

        void Fancy_Observable_Click(object sender, RoutedEventArgs e)
        {
            EchoFunc(TextTextBox.Text)
                .SelectMany(s => s.ToCharArray())
                .SuspendUi(this)
                .Subscribe(result => OutputTextblock.Text += result.ToString() + " [Observable2]\n");
        }
    }

    static class Extensions
    {
        public static IObservable<T> SuspendUi<T>(this IObservable<T> source, FrameworkElement uiElement)
        {
            return Observable.CreateWithDisposable<T>(observer => {
                uiElement.IsEnabled = false;

                return source.Subscribe(
                    x => observer.OnNext(x),
                    () => { uiElement.IsEnabled = true; observer.OnCompleted(); });
            })
            .SubscribeOnDispatcher();
        }
    }
}
