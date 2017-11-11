using BolapanControl.ItemsFilter;
// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************
using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.Model;
using BolapanControl.ItemsFilter.View;
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;

namespace BolapanControl.ItemsFilter.Model
{
    [View(typeof(DateTimeRangeFilterView))]
    public class DateTimeRangeFilter:RangeFilter<DateTime> 
    {
        Func<object, DateTime> getter;

        public DateTimeRangeFilter(ItemPropertyInfo propertyInfo,DateTime beginDate,DateTime endDate)
            : base(propertyInfo) 
        {
            Debug.Assert(propertyInfo != null, "propertyInfo is null.");
            Debug.Assert(typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType), "The typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType) return False.");
            base.PropertyInfo = propertyInfo;       
            minDateTime = beginDate;
            maxDateTime = endDate;
            _compareFrom = beginDate;
            _compareTo = endDate;
            Func<object, object> getterItem = ((PropertyDescriptor)(PropertyInfo.Descriptor)).GetValue;
            getter = t => ((DateTime)getterItem(t));
            base.Name = "In range:";
        }

        public override void IsMatch(FilterPresenter sender, FilterEventArgs e)
        {
            if (e.Accepted)
            {
                if (e.Item == null)
                    e.Accepted = false;
                else
                {
                    DateTime value = getter(e.Item);
                    e.Accepted = (Object.ReferenceEquals(_compareFrom, null) | value.CompareTo(_compareFrom) >= 0)
                        && (Object.ReferenceEquals(_compareTo, null) | value.CompareTo(_compareTo) <= 0);
                }
            }
        }

        //protected override void OnAttachPresenter(FilterPresenter presenter)
        //{
        //    base.OnAttachPresenter(presenter);
        //    CompareFrom = MinDateTime; //DateTime.Now.Subtract(TimeSpan.FromMinutes(40));
        //    CompareTo = MaxDateTime;// DateTime.Now.Subtract(TimeSpan.FromMinutes(20)); 
        //}

        private void RefreshIsActive()
        {
            base.IsActive = !(Object.ReferenceEquals(_compareFrom, null) && Object.ReferenceEquals(_compareTo, null));

        }

        private DateTime maxDateTime;// = DateTime.Now;
        public DateTime MaxDateTime
        {
            get
            {
                return maxDateTime;
            }
            set
            {
                if(maxDateTime!=value)
                {
                    maxDateTime = value;
                    OnPropertyChanged("MaxDateTime");
                }
            }
        }


        private DateTime minDateTime;// = DateTime.Now.Subtract(TimeSpan.FromHours(12));
        public DateTime MinDateTime
        {
            get
            {
                return minDateTime;
            }
            set
            {
                if (minDateTime != value)
                {
                    minDateTime = value;
                    OnPropertyChanged("MinDateTime");
                }
            }
        }

        private DateTime _compareFrom;
        public new DateTime CompareFrom
        {
            get { return _compareFrom; }
            set
            {
                if (!Object.Equals(_compareFrom, value))
                {
                    _compareFrom = value;
                    OnPropertyChanged("CompareFrom");
                    RefreshIsActive();
                    OnIsActiveChanged();
                }
            }
        }

        private DateTime _compareTo;
        public new DateTime CompareTo
        {
            get { return _compareTo; }
            set
            {
                if (!Object.Equals(_compareTo, value))
                {
                    _compareTo = value;
                    OnPropertyChanged("CompareTo");
                    RefreshIsActive();
                    OnIsActiveChanged();
                }
            }
        }

        //private DateTime upperValue;
        //public DateTime UpperValue
        //{
        //    get { return upperValue; }
        //    set
        //    {
        //        if (!Object.Equals(upperValue, value))
        //        {
        //            upperValue = value;
        //            RefreshIsActive();
        //            OnIsActiveChanged();
        //            OnPropertyChanged("CompareTo");
        //        }
        //    }
        //}


    }

    public class DateTimeRangeFilterInitializer : PropertyFilterInitializer
    {
        private const string _filterName = "Between";
        #region IPropertyFilterInitializer Members

        protected override PropertyFilter NewFilter(FilterPresenter filterPresenter, ItemPropertyInfo propertyInfo)
        {
            Debug.Assert(filterPresenter != null);
            Debug.Assert(propertyInfo != null);

            Type propertyType = propertyInfo.PropertyType;
            if (filterPresenter.ItemProperties.Contains(propertyInfo)
                && typeof(DateTime).IsAssignableFrom(propertyType)
                && !propertyType.IsEnum
                )
            {
                //IEnumerable source = filterPresenter.CollectionView.SourceCollection;
                //if (source != null)
                //{
                //    var start = source.OfType<DateTime>().Min();
                //    var end = source.OfType<DateTime>().Max();
                //    return new DateTimeRangeFilter(propertyInfo, source.OfType<DateTime>().Min(), source.OfType<DateTime>().Max());
                //}
                  
                return new DateTimeRangeFilter(propertyInfo,DateTime.Now.Subtract(TimeSpan.FromHours(12)),DateTime.Now);
            }
            return null;
        }



        #endregion
    }
}
