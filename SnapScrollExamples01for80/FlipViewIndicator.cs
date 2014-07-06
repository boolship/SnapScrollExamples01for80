using System;
using System.Collections.Generic;
using Windows.Foundation;
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
                // binding
                indicator.ItemsSource = flipView.ItemsSource;
                var binding = new Binding
                {
                    Mode = BindingMode.TwoWay,
                    Source = flipView,
                    Path = new PropertyPath("SelectedItem")
                };
                indicator.SetBinding(SelectedItemProperty, binding);

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
    }
}
