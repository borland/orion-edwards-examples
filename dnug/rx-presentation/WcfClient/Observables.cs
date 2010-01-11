using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows;

namespace WcfClient
{
    public static class Observables
    {
        public static IObservable<IEvent<MouseEventArgs>> GetMouseMove(this UIElement element)
        {
            return Observable.FromEvent<MouseEventArgs>(element, "MouseMove");
        }
        
        public static IObservable<IEvent<MouseButtonEventArgs>> GetMouseUp(this UIElement element)
        {
            return Observable.FromEvent<MouseButtonEventArgs>(element, "MouseUp");
        }

        public static IObservable<IEvent<MouseButtonEventArgs>> GetMouseDown(this UIElement element)
        {
            return Observable.FromEvent<MouseButtonEventArgs>(element, "MouseDown");
        }
    }
}
