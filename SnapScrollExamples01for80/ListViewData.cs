
namespace SnapScrollExamples01for80
{
    public class ListViewData
    {
        public ListViewData()
        {
            string[] names =
            {
                "First", "Second", "Third", "Fourth", "Fifth", "Sixth", "Seventh", "Eighth", "Nineth",
                "Tenth"
            };
            for (int i = 0; i < 10; i++)
            {
                var name = i < names.Length ? names[i] : "default";
                var item = new Item { Title = name };
                Collection.Add(item);
            }


        }

        private readonly ItemCollection _collection = new ItemCollection();

        public ItemCollection Collection
        {
            get
            {
                return _collection;
            }
        }
    } 
}
