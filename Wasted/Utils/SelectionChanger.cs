using System;
using Xamarin.Forms;

namespace Wasted.Utils
{
    public class SelectionChanger
    {
        public static void ListSelectionChanged(object sender, SelectionChangedEventArgs e, Action method)
        {
            if (((CollectionView)sender).SelectedItem == null)
                return;
            method();
            ((CollectionView)sender).SelectedItem = null; //Prevents borders from appearing
        }
    }
}