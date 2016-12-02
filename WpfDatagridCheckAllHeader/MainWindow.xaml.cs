using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;

namespace WpfDatagridCheckAllHeader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgProducts.ItemsSource = Connections.GetProduct().AsDataView();
        }

        /// <summary>
        /// Checkall activity
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            var row = GetDataGridRows(dgProducts);

            if (((CheckBox)sender).IsChecked == true)
            {
                SetCheckbox(row, true);
            }
            else
            {
                SetCheckbox(row, false);
            }
        }

        /// <summary>
        /// Individual Checking
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkDiscontinue_Click(object sender, RoutedEventArgs e)
        {
            object a = e.Source;
            CheckBox chk = (CheckBox)sender;

            DataGridRow row = FindAncestor<DataGridRow>(chk);
            if (row != null)
            {
                DataRowView rv = (DataRowView)row.Item;

                //LINQ or Database Method to Update Product discontinue status
                Connections.UpdateProductDiscontinue((bool)chk.IsChecked, rv["ProductName"].ToString());
            }
        }

        private void SetCheckbox(IEnumerable<DataGridRow> row, bool value)
        {
            //loop through datagrid rows
            foreach (DataGridRow r in row)
            {
                DataRowView rv = (DataRowView)r.Item;
                foreach (DataGridColumn column in dgProducts.Columns)
                {
                    if (column.GetType().Equals(typeof(DataGridTemplateColumn)))
                    {
                        rv.Row["Discontinue"] = value;

                        //LINQ or Database Method to Update Product discontinue status
                        Connections.UpdateProductDiscontinue(value, rv.Row["productname"].ToString());
                    }
                }
            }
        }

        public IEnumerable<DataGridRow> GetDataGridRows(DataGrid grid)
        {
            var itemsSource = grid.ItemsSource as IEnumerable;
            if (null == itemsSource) yield return null;
            foreach (var item in itemsSource)
            {
                var row = grid.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (null != row) yield return row;
            }
        }

        #region visual tree helpers

        /// <summary>
        /// Returns the first ancester of specified type
        /// </summary>
        public static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            current = VisualTreeHelper.GetParent(current);
            while (current != null)
            {
                if (current is T)
                {
                    return (T)current;
                }

                current = VisualTreeHelper.GetParent(current);
            };
            return null;
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        #endregion
    }
}
