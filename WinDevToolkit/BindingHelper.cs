﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;

namespace WinDevToolkit
{
    // workaround for applying bindings in style definitions (has side-effects!)
    // inspired by http://stackoverflow.com/questions/26345820/binding-selecteditems-in-listview-to-a-viewmodel-in-windows-phone-8-1

    public class BindingHelper
    {
        public static string GetIsSelectedContainerBinding(DependencyObject obj)
        {
            return (string)obj.GetValue(IsSelectedContainerBindingProperty);
        }

        public static void SetIsSelectedContainerBinding(DependencyObject obj, string value)
        {
            obj.SetValue(IsSelectedContainerBindingProperty, value);
        }

        public static readonly DependencyProperty IsSelectedContainerBindingProperty =
            DependencyProperty.RegisterAttached("IsSelectedContainerBinding", typeof(string), typeof(BindingHelper), new PropertyMetadata(null, IsSelectedContainerBindingPropertyChangedCallback));

        public static void IsSelectedContainerBindingPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            BindingOperations.SetBinding(d, SelectorItem.IsSelectedProperty, new Binding
            {
                Source = d,
                Path = new PropertyPath("Content." + e.NewValue),
                Mode = BindingMode.TwoWay
            });
        }
    }
}
