using System;
using Xamarin.Forms;

namespace SwingSocial.Controls
{
    public class HoleLayerView : Xamarin.Forms.View
    {
        public event EventHandler DrawRectangleHole;

        public Point TopLeftCorner { get; set; }
        public Point TopRightCorner { get; set; }
        public Point BottomLeftCorner { get; set; }
        public Point BottomRightCorner { get; set; }

        public void DrawRectangle()
        {
            DrawRectangleHole?.Invoke(this, EventArgs.Empty);
        }
    }
}
