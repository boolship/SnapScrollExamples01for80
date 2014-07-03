
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;

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

            var flipViewData1 = new FlipViewData();
            NormalFlipView.ItemsSource = flipViewData1.Collection;

            var flipViewData2 = new FlipViewData();
            VerticalFlipView.ItemsSource = flipViewData2.Collection;

            // debug only
            WinRTXamlToolkit.Debugging.DC.ShowVisualTree();

            Workarounds();
        }

        /// <summary>
        /// What implements IScrollSnapPointsInfo?? 
        /// (Y) Create custom class derived from ItemsControl and implement IScrollSnapPointsInfo interface.
        /// (N) Create custom style for items control and set HorizontalSnapPointsType property on ScrollViewer inside the style
        /// 
        /// Ref: Windows.UI.Xaml.Controls.XXX implements IScrollSnapPointsInfo ??
        /// 
        /// Panel\StackPanel** - yes** (working)
        /// Control\ItemsControl\local:SnappingItemsControl** - yes** (working)
        /// 
        /// ItemsPresenter** - yes** (not working)
        /// Panel\VirtualizingPanel\OrientedVirtualizingPanel**\WrapGrid - yes**  (not working)
        /// Panel\VirtualizingPanel\OrientedVirtualizingPanel**\VirtualizingStackPanel** - yes**  (not working)
        /// 
        /// Control\ContentControl\ScrollViewer - no
        /// Control\ItemsControl - no
        /// Control\ItemsControl\Selector\ListViewBase\ListView - no
        /// Control\ItemsControl\Selector\ListViewBase\GridView - no
        /// Control\ListControl\ListBox - no
        /// Panel\ItemsWrapGrid - no
        /// </summary>
        private void Workarounds()
        {
            // TODO Configure ScrollViewer in style or code below. Snaps iff ScrollViewer contains StackPanel, SnappingItemsControl
            //var scrollViewer = FindScrollViewer(SnappingScrollViewer1);
            //if (scrollViewer != null)
            //{
            //    // Mandatory, MandatorySingle, Optional, OptionalSingle, None
            //    scrollViewer.VerticalSnapPointsType = SnapPointsType.MandatorySingle;

            //    // Center, Far, Near
            //    // For a vertically oriented element, Near is the top and Far is the bottom.
            //    // For a horizontally oriented element, Near is left and Far is right.
            //    scrollViewer.VerticalSnapPointsAlignment = SnapPointsAlignment.Near;

            //    // Center, Left, Right, Stretch
            //    scrollViewer.HorizontalContentAlignment = HorizontalAlignment.Center;
            //}
        }

        private ScrollViewer FindScrollViewer(DependencyObject d)
        {
            if (d == null) return null;
            if (d is ScrollViewer) return d as ScrollViewer;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(d); i++)
            {
                var scrollViewer = FindScrollViewer(VisualTreeHelper.GetChild(d, i));
                if (scrollViewer != null) return scrollViewer;
            }

            return null;
        }
    }

}
