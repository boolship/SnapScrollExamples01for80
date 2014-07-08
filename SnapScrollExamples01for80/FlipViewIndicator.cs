using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace SnapScrollExamples01for80
{
    public sealed class FlipViewIndicator : ListBox
    {
        /// <summary>
        /// Calculated item sizes.
        /// </summary>
        public static Dictionary<string, Point> IndicatorItemSizes { get; set; }

        public FlipViewIndicator()
        {
            IndicatorItemSizes = new Dictionary<string, Point>();
            DefaultStyleKey = typeof (FlipViewIndicator);

        }

        public FlipView FlipView
        {
            get { return (FlipView) GetValue(FlipViewProperty); }
            set { SetValue(FlipViewProperty, value); }
        }

        public static readonly DependencyProperty FlipViewProperty =
            DependencyProperty.Register("FlipView", typeof (FlipView), typeof (FlipViewIndicator),
                new PropertyMetadata(null, FlipView_Changed));

        private static void FlipView_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var indicator = d as FlipViewIndicator;
            var flipView = (e.NewValue as FlipView);
            if (indicator != null && flipView != null)
            {
                // horizontal or vertical sizing (code or xaml or style) 
                //indicator.Height = 16;
                //indicator.Width = 16;

                // binding
                indicator.ItemsSource = flipView.ItemsSource;
                var binding = new Binding
                {
                    Mode = BindingMode.TwoWay,
                    Source = flipView,
                    Path = new PropertyPath("SelectedItem")
                };
                indicator.SetBinding(SelectedItemProperty, binding);
                indicator.SelectionChanged += indicator_SelectionChanged;
                indicator.KeyUp += indicator_KeyUp;
                // item sizes
                var source = flipView.ItemsSource as ItemCollection;
                if (source != null)
                {
                    var count = source.Count;
                    var availableWidth = indicator.FlipView.ActualWidth - count*2; // margin 2
                    var availableHeight = indicator.FlipView.ActualHeight - count*2; // margin 2
                    IndicatorItemSizes.Add(flipView.Name, new Point
                    {
                        X = Math.Floor(availableWidth/count),
                        Y = Math.Floor(availableHeight/count)
                    });
                }
            }
        }

        static void indicator_KeyUp(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e != null)
            {
                switch (e.Key)
                {
                    case VirtualKey.Right:
                        break;
                    case VirtualKey.Left:
                        break;
                    case VirtualKey.Up:
                        break;
                    case VirtualKey.Down:
                        break;
                }
            }
        }

        private static void indicator_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listBox = sender as ListBox;
            if (listBox != null && e != null && e.AddedItems.Count > 0)
            {
                listBox.ScrollIntoView(e.AddedItems.First());
            } 
        }

    }
}
