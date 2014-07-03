using System;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace SnapScrollExamples01for80
{
    public class SnappingItemsControl : ItemsControl, IScrollSnapPointsInfo
    {
        // TODO fixme bug: snap Near trims last item because scrolling last would exceed Size
        private const int NumItems = 30;
        private const float VerticalSize = 210f;

        public IReadOnlyList<float> GetIrregularSnapPoints(Orientation orientation, SnapPointsAlignment alignment)
        {
            //Returns the set of distances between irregular snap points for a specified orientation and alignment.
            var result = new List<float>();
            for (int i = 0; i < NumItems; i++)
            {
                result.Add(i * VerticalSize);
            }

            return result;
        }

        public float GetRegularSnapPoints(Orientation orientation, SnapPointsAlignment alignment, out float offset)
        {
            //Out parameter. The offset of the first snap point.
            offset = 0f;
            //The distance between the equidistant snap points. Returns 0 when no snap points are present.
            return VerticalSize;
        }

        public bool AreHorizontalSnapPointsRegular { get; private set; }
        public bool AreVerticalSnapPointsRegular { get; private set; }
        public event EventHandler<object> HorizontalSnapPointsChanged;
        public event EventHandler<object> VerticalSnapPointsChanged;
    }
}
