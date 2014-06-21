using System.Collections.ObjectModel;

namespace SnapScrollExamples01for80
{
    public class ItemCollection : ObservableCollection<object>
    {
        private ObservableCollection<Item> _itemCollection = new ObservableCollection<Item>();
    }
}
