using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using Clipboard = System.Windows.Clipboard;
using DataFormats = System.Windows.DataFormats;
using TextBox = System.Windows.Controls.TextBox;

namespace WpfApplication1
{

    public class SmartCopy
    {

        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.RegisterAttached(
            "IsActive", typeof(bool), typeof(SmartCopy), new FrameworkPropertyMetadata(default(bool), PropertyChangedCallback));

        public static void SetIsActive(DependencyObject element, bool value)
        {
            element.SetValue(IsActiveProperty, value);
        }

        public static bool GetIsActive(DependencyObject element)
        {
            return (bool)element.GetValue(IsActiveProperty);
        }


        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            var textBox = dependencyObject as TextBox;

            if (textBox == null) return;

            textBox.PreviewKeyDown += TextBoxKeyDown;
            textBox.KeyDown += TextBoxOnKeyDown;
        }

        private static void TextBoxOnKeyDown(object sender, KeyEventArgs e)
        {
            var isCopy = e.Key == Key.C && e.KeyboardDevice.Modifiers == ModifierKeys.Control ||
                            e.Key == Key.Insert && e.KeyboardDevice.Modifiers == ModifierKeys.Control;

            if (!isCopy) return;

            e.Handled = true;
            Console.WriteLine();

        }

        private static void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            var isCopy = e.Key == Key.C && e.KeyboardDevice.Modifiers == ModifierKeys.Control ||
                             e.Key == Key.Insert && e.KeyboardDevice.Modifiers == ModifierKeys.Control;

            if (!isCopy) return;

            var textBox = sender as TextBox;
            if (textBox == null) return;
            if (textBox.SelectedText != textBox.Text) return;
            e.Handled = true;

            var x = BindingOperations.GetBindingExpression(textBox, TextBox.TextProperty);
            if (x == null) return;

            object data;

            if (!string.IsNullOrEmpty(x.ResolvedSourcePropertyName))
            {
                var property = x.DataItem.GetType().GetProperty(x.ResolvedSourcePropertyName);
                if (property == null) return;

                data = property.GetValue(x.DataItem);
            }
            else
            {
                data = x.DataItem;
            }

          //  Task.Factory.StartNew(() =>
            {

                try
                {
                    Clipboard.SetText(data.ToString());
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }
            }
            //);
        }
    }
}
