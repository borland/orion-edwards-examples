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
using System.Windows.Shapes;

namespace WcfClient
{
    /// <summary>
    /// Interaction logic for DragDropWindow.xaml
    /// </summary>
    public partial class DragDropWindow : Window
    {
        public DragDropWindow()
        {
            InitializeComponent();

            Box.EnableCapture().Dispose();
            Box.GetMouseDrag(this).Subscribe(v => {
                Console.WriteLine("Moving by {0}/{1}", v.X, v.Y);

                Canvas.SetLeft(Box, Canvas.GetLeft(Box) + v.X);
                Canvas.SetTop(Box, Canvas.GetTop(Box) + v.Y);
            });
        }
    }

    public static class Ext
    {
        public static IDisposable EnableCapture(this UIElement element)
        {
            return element.GetMouseDown().Do(_ => element.CaptureMouse()).Merge(
                element.GetMouseUp().Do(_ => element.ReleaseMouseCapture()))
                .Subscribe();
        }

        public static IObservable<Vector> GetMouseDrag(this UIElement element, IInputElement relativeTo)
        {
            var positions = element.GetMouseMove().Select(m => m.EventArgs.GetPosition(relativeTo));

            var mouseDiffs = positions.Zip(positions.Skip(1),
                (l, r) => r - l);

            return from down in element.GetMouseDown()
                   from diff in mouseDiffs.Until(element.GetMouseUp())
                   select diff;
        }
    }
}
