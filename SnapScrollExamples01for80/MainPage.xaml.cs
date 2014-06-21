
namespace SnapScrollExamples01for80
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            var gridViewData1 = new GridViewData();
            NormalGridView.ItemsSource = gridViewData1.Collection;

            var gridViewData2 = new GridViewData();
            VerticalGridView.ItemsSource = gridViewData2.Collection;

            var listViewData1 = new ListViewData();
            NormalListView.ItemsSource = listViewData1.Collection;

            var listViewData2 = new ListViewData();
            HorizontalListView.ItemsSource = listViewData2.Collection;
        }
    }
}
