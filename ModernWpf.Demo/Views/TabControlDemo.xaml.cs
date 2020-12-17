﻿using ModernWpf.Controls;
using ModernWpf.Controls.Primitives;
using ModernWpf.Demo.SamplePages;
using System.Windows;
using System.Windows.Controls;
using Frame = ModernWpf.Controls.Frame;

namespace ModernWpf.Demo.Views
{
    /// <summary>
    ///     Interaction logic for TabControlDemo.xaml
    /// </summary>
    public partial class TabControlDemo : UserControl
    {
        public TabControlDemo()
        {
            InitializeComponent();
            for (var i = 0; i < 3; i++)
            {
                tabControl.Items.Add(CreateNewTab(i));
                tabControl2.Items.Add(CreateNewTab(i));
                tabControl3.Items.Add(CreateNewTab(i));
            }
        }

        private void TabView_Loaded(object sender, RoutedEventArgs e)
        {
            for (var i = 0; i < 3; i++)
            {
                (sender as TabControl).Items.Add(CreateNewTab(i));
            }
        }

        private TabItem CreateNewTab(int index)
        {
            var newItem = new TabItem();

            newItem.Header = $"Document {index}";
            TabItemHelper.SetIcon(newItem, new SymbolIcon(Symbol.Document));

            // The content of the tab is often a frame that contains a page, though it could be any UIElement.
            var frame = new Frame();

            frame.Navigated += (s, e) => { ((FrameworkElement)frame.Content).Margin = new Thickness(-18, 0, -18, 0); };

            switch (index % 3)
            {
                case 0:
                    frame.Navigate(typeof(SamplePage1));
                    break;
                case 1:
                    frame.Navigate(typeof(SamplePage2));
                    break;
                case 2:
                    frame.Navigate(typeof(SamplePage3));
                    break;
            }

            newItem.Content = frame;

            return newItem;
        }
    }
}