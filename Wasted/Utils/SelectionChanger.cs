using System;
using Xamarin.Forms;

namespace Wasted.Utils
{
    public static class SelectionChanger
    {
        public static void ListSelectionChanged(object sender, Action method)
        {
            if (((CollectionView)sender).SelectedItem == null)
                return;
            method();
            ((CollectionView)sender).SelectedItem = null; //Prevents borders from appearing
        }
    }
}