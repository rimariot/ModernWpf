using ModernWpf.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace ModernWpf.Demo.Views
{
    /// <summary>
    ///     Interaction logic for SplitViewDemo.xaml
    /// </summary>
    public partial class SplitViewPage : UserControl
    {
        public SplitViewPage()
        {
            InitializeComponent();
        }

        public ObservableCollection<NavLink> NavLinks { get; } = new ObservableCollection<NavLink>
        {
            new NavLink {Label = "People", Symbol = Symbol.People},
            new NavLink {Label = "Globe", Symbol = Symbol.Globe},
            new NavLink {Label = "Message", Symbol = Symbol.Message},
            new NavLink {Label = "Mail", Symbol = Symbol.Mail}
        };

        private void togglePaneButton_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow.ActualWidth >= 640)
            {
                if (splitView.IsPaneOpen)
                {
                    splitView.DisplayMode = SplitViewDisplayMode.CompactOverlay;
                    splitView.IsPaneOpen = false;
                }
                else
                {
                    splitView.IsPaneOpen = true;
                    splitView.DisplayMode = SplitViewDisplayMode.Inline;
                }
            }
            else
            {
                splitView.IsPaneOpen = !splitView.IsPaneOpen;
            }
        }

        private void PanePlacement_Toggled(object sender, RoutedEventArgs e)
        {
            var ts = sender as ToggleSwitch;
            if (ts.IsOn)
            {
                splitView.PanePlacement = SplitViewPanePlacement.Right;
            }
            else
            {
                splitView.PanePlacement = SplitViewPanePlacement.Left;
            }
        }

        private void NavLinksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            content.Text = (e.ClickedItem as NavLink).Label + " Page";
        }

        private void displayModeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            splitView.DisplayMode = (SplitViewDisplayMode)Enum.Parse(typeof(SplitViewDisplayMode),
                (e.AddedItems[0] as ComboBoxItem).Content.ToString());
        }

        private void paneBackgroundCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var colorString = (e.AddedItems[0] as ComboBoxItem).Content.ToString();

            VisualStateManager.GoToElementState((FrameworkElement)Content, colorString, false);
        }
    }

    public class NavLink
    {
        public string Label { get; set; }
        public Symbol Symbol { get; set; }
    }
}