using ModernWpf.Demo.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace ModernWpf.Demo.Views
{
    public sealed partial class ListView2Page : UserControl
    {
        ObservableCollection<ListViewPage.Contact> contacts1 = new ObservableCollection<ListViewPage.Contact>();
        readonly ObservableCollection<ListViewPage.Contact> contacts2 = new ObservableCollection<ListViewPage.Contact>();
        ObservableCollection<ListViewPage.Contact> contacts3 = new ObservableCollection<ListViewPage.Contact>();
        ObservableCollection<ListViewPage.Contact> contacts3Filtered = new ObservableCollection<ListViewPage.Contact>();
        readonly CollectionViewSource ContactsCVS;

        public ListView2Page()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            ContactsCVS = (CollectionViewSource)Resources[nameof(ContactsCVS)];
        }

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            //Items = ControlInfoDataSource.Instance.Groups.Take(3).SelectMany(g => g.Items).ToList();
            BaseExample.ItemsSource = await ListViewPage.Contact.GetContactsAsync();
            Control2.ItemsSource = await ListViewPage.Contact.GetContactsAsync();
            contacts1 = await ListViewPage.Contact.GetContactsAsync();

            contacts2.Add(new ListViewPage.Contact("John", "Doe", "ABC Printers"));
            contacts2.Add(new ListViewPage.Contact("Jane", "Doe", "XYZ Refridgerators"));
            contacts2.Add(new ListViewPage.Contact("Santa", "Claus", "North Pole Toy Factory Inc."));

            Control4.ItemsSource = CustomDataObject.GetDataObjects();
            ContactsCVS.Source = await ListViewPage.Contact.GetContactsAsync();

            // Initialize list of contacts to be filtered
            contacts3 = await ListViewPage.Contact.GetContactsAsync();
            contacts3Filtered = new ObservableCollection<ListViewPage.Contact>(contacts3);

            FilteredListView.ItemsSource = contacts3Filtered;
        }

        //===================================================================================================================
        // Selection Modes Example
        //===================================================================================================================
        private void SelectionModeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Control2 != null)
            {
                string selectionMode = e.AddedItems[0].ToString();
                switch (selectionMode)
                {
                    case "None":
                        Control2.IsSelectionEnabled = false;
                        break;
                    case "Single":
                        Control2.SelectionMode = SelectionMode.Single;
                        Control2.IsSelectionEnabled = true;
                        break;
                    case "Multiple":
                        Control2.SelectionMode = SelectionMode.Multiple;
                        Control2.IsSelectionEnabled = true;
                        break;
                    case "Extended":
                        Control2.SelectionMode = SelectionMode.Extended;
                        Control2.IsSelectionEnabled = true;
                        break;
                }
            }
        }

        //===================================================================================================================
        // Filtered List Example
        //===================================================================================================================
        private void Remove_NonMatching(IEnumerable<ListViewPage.Contact> filteredData)
        {
            for (int i = contacts3Filtered.Count - 1; i >= 0; i--)
            {
                var item = contacts3Filtered[i];
                // If contact is not in the filtered argument list, remove it from the ListView's source.
                if (!filteredData.Contains(item))
                {
                    contacts3Filtered.Remove(item);
                }
            }
        }

        private void AddBack_Contacts(IEnumerable<ListViewPage.Contact> filteredData)
        // When a user hits backspace, more contacts may need to be added back into the list
        {
            foreach (var item in filteredData)
            {
                // If item in filtered list is not currently in ListView's source collection, add it back in
                if (!contacts3Filtered.Contains(item))
                {
                    contacts3Filtered.Add(item);
                }
            }
        }

        private void OnFilterChanged(object sender, TextChangedEventArgs args)
        {
            // Linq query that selects only items that return True after being passed through Filter function
            var filtered = contacts3.Where(contact => Filter(contact));
            Remove_NonMatching(filtered);
            AddBack_Contacts(filtered);
        }

        private bool Filter(ListViewPage.Contact contact)
        {
            // When the text in any filter is changed, contact list is ran through all three filters to make sure
            // they can properly interact with each other (i.e. they can all be applied at the same time).

            return contact.FirstName.IndexOf(FilterByFirstName.Text, StringComparison.InvariantCultureIgnoreCase) > -1 &&
                   contact.LastName.IndexOf(FilterByLastName.Text, StringComparison.InvariantCultureIgnoreCase) > -1 &&
                   contact.Company.IndexOf(FilterByCompany.Text, StringComparison.InvariantCultureIgnoreCase) > -1;
        }
    }

    public class NoPaddingFlowDocument : FlowDocument
    {
        static NoPaddingFlowDocument()
        {
            PagePaddingProperty.OverrideMetadata(typeof(NoPaddingFlowDocument), new FrameworkPropertyMetadata { CoerceValueCallback = CoercePagePadding });
        }

        public NoPaddingFlowDocument()
        {
            SetResourceReference(StyleProperty, typeof(FlowDocument));
        }

        private static object CoercePagePadding(DependencyObject d, object baseValue)
        {
            return new Thickness();
        }
    }
}
