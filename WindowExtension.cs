using System;
using System.Windows;

namespace Lab3
{
    public static class WindowExtension
    {
        public static void Information(this Window window, string message)
        {
            MessageBox.Show(window, message, "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void Error(this Window window, string message)
        {
            MessageBox.Show(window, message, "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}