using System;
using System.Collections.Generic;
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
        public static Dictionary<string,double> IndicatorItemSizes { get; set; }

        public FlipViewIndicator()
        {
            IndicatorItemSizes = new Dictionary<string, double>();
            DefaultStyleKey = typeof(FlipViewIndicator);
        }

        public FlipView Flipview
        {
            get { return (FlipView) GetValue(FlipviewProperty); }
            set { SetValue(FlipviewProperty, value); }
        }

        public static readonly DependencyProperty FlipviewProperty =
            DependencyProperty.Register("Flipview", typeof (FlipView), typeof (FlipViewIndicator),
                new PropertyMetadata(null, FlipView_Changed));

        private static void FlipView_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var indicator = d as FlipViewIndicator;
            var flipView = (e.NewValue as FlipView);
            if (indicator != null && flipView != null)
            {
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
                    var availableSize = indicator.Flipview.ActualWidth - count*2; // margin 2
                    IndicatorItemSizes.Add(flipView.Name, Math.Floor(availableSize/count));
                }
            }
        }
    }
}
