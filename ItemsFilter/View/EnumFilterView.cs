// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Model;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace BolapanControl.ItemsFilter.View
{
    /// <summary>
    /// Defile View control for IMultiValueFilter model.
    /// </summary>
    [ModelView]
    public class EnumFilterView : MultiValueFilterView
    {
        // static EnumFilterView() {
        //    DefaultStyleKeyProperty.OverrideMetadata(typeof(EnumFilterView),
        //        new FrameworkPropertyMetadata(typeof(EnumFilterView)));
        //}
        private bool isModelAttached;
        private ListBox _itemsCtrl;
        /// <summary>
        /// Create new instance of EnumFilterView;
        /// </summary>
        public EnumFilterView()
            : base()
        {

        }
        /// <summary>
        /// Create new instance of EnumFilterView and accept model.
        /// </summary>
        /// <param name="model">IMultiValueFilter model</param>
        public EnumFilterView(object model)
            : base(model)
        {

        }
    }
}
