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
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace RxWpfApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();

            //this.GetKeyDown().Subscribe(ie => KeyDownText.Opacity = 100);
            //this.GetKeyUp().Subscribe(ie => KeyDownText.Opacity = 0);

            //this.GetKeyDown().Select(_ => 100).Merge(
            //    this.GetKeyUp().Select(_ => 0)).Subscribe(x => KeyDownText.Opacity = x);

            var changes = this.GetKeyDown().Select(_ => 0).Merge(this.GetKeyUp().Select(_ => 100));

            changes.Zip(changes.Skip(1), (l, r) => Tween(l, r, 5))
                .SelectMany(x => x)
                .HoldUntilChanged()
                .Do(x => Debug.WriteLine(x))
                .Subscribe(x => Opacity = x / 100d); 
        }

        IObservable<int> Tween(int first, int second, int delay)
        {
            return Observable.Create<int>(o => {
                ThreadPool.QueueUserWorkItem(_ => {
                    while (first-- > second)
                    {
                        o.OnNext(first); Thread.Sleep(delay);
                    }

                    while (second > first++)
                    {
                        o.OnNext(first); Thread.Sleep(delay);
                    }

                });
                return () => { };
            });
        }

        void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Observable_Sample();
            //Enumerable_Sample();
        }

        void Enumerable_Sample()
        {
            var q = GetFilesRecursive(DirectoryTextbox.Text)
                    .Where(f => f.EndsWith(EndsWithTextbox.Text))
                    .Take(10);

            foreach (var f in q)
            {
                OutputTextblock.Text += f + "\n";
            }

            OutputTextblock.Text += "ALL FINISHED\n";
        }

        void Observable_Sample()
        {
            var q = GetFilesRecursiveO(DirectoryTextbox.Text)
                   .Where(f => f.EndsWith(EndsWithTextbox.Text))
                   .Take(10);

            q.Subscribe(
                    f => OutputTextblock.Text += f + "\n",
                    () => OutputTextblock.Text += "ALL FINISHED\n"
                );
        }

        static IEnumerable<string> GetFilesRecursive(string path)
        {
            return Directory.GetFiles(path).SleepEvery(1)
                .Concat(Directory.GetDirectories(path).SelectMany(d => GetFilesRecursive(d)));
        }

        static IObservable<string> GetFilesRecursiveO(string path)
        {
            return Directory.GetFiles(path).SleepEvery(1).ToObservable()
                .Concat(Directory.GetDirectories(path).ToObservable().SelectMany(d => GetFilesRecursiveO(d)));
        }
    }


    static class Ext
    {
        public static IEnumerable<T> SleepEvery<T>(this IEnumerable<T> source, int sleepTime)
        {
            return source.Select(x => { Thread.Sleep(sleepTime); return x; });
        }

        public static IObservable<IEvent<KeyEventArgs>> GetKeyDown(this FrameworkElement element)
        {
            return Observable.FromEvent<KeyEventHandler, KeyEventArgs>(
                handler => (s,e) => handler(s,e),
                handler => element.KeyDown += handler,
                handler => element.KeyDown -= handler);
        }

        public static IObservable<IEvent<KeyEventArgs>> GetKeyUp(this FrameworkElement element)
        {
            return Observable.FromEvent<KeyEventHandler, KeyEventArgs>(
                handler => (s, e) => handler(s, e),
                handler => element.KeyUp += handler,
                handler => element.KeyUp -= handler);
        }
    }
}
