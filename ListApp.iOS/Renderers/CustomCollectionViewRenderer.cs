using Foundation;
using ListApp.iOS.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomCollectionViewRenderer), typeof(CollectionView))]
namespace ListApp.iOS.Renderers
{
    public class CustomCollectionViewRenderer : CollectionViewRenderer
    {
        public CustomCollectionViewRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<GroupableItemsView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {

            }

            if (e.NewElement != null)
            {
                var d = NativeView;
                var b = Control;
            }
        }
    }
}