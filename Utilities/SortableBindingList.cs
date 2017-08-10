using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace BinHong.Utilities
{
    #region 实现一个可以排序的BindingList

    /// <summary>
    /// 一个传入PropertyDescriptor，然后对PropertyDescriptor对应的值排序。
    /// </summary>
    /// <remarks>代码参考自 http://www.it165.net/pro/html/201309/7026.html </remarks>
    /// <typeparam name="T">PropertyDescriptor对应的值的类型，以泛型匹配多种类型。</typeparam>
    public class PropertyComparer<T> : IComparer<T>
    {
        /// <summary>
        /// 设置默认比较器
        /// </summary>
        private readonly IComparer _comparer;

        /// <summary>
        /// 要比较的属性的PropertyDescriptor
        /// </summary>
        private readonly PropertyDescriptor _property;

        /// <summary>
        /// 排序的方向
        /// </summary>
        private ListSortDirection _sortDirection;

        /// <summary>
        /// 构造函数中确定比较中要用到的属性PropertyDescriptor和方向ListSortDirection
        /// </summary>
        /// <param name="property">比较中要用到的属性</param>
        /// <param name="sortDirection">比较中要用到的方向</param>
        public PropertyComparer(PropertyDescriptor property, ListSortDirection sortDirection)
        {
            this._property = property;
            this._sortDirection = sortDirection;
            this._comparer = Comparer.Default;
        }

        /// <summary>
        /// 比较两个值
        /// </summary>
        /// <param name="x">要比较的值1</param>
        /// <param name="y">要比较的值2</param>
        /// <returns></returns>
        public int Compare(T x, T y)
        {
            //先确定方向
            int reverse;
            if (this._sortDirection == ListSortDirection.Ascending)
            {
                reverse = 1;
            }
            else
            {
                reverse = -1;
            }
            return reverse * this._comparer.Compare(this._property.GetValue(x), this._property.GetValue(y));
        }

        /// <summary>
        /// 设置排序的方向
        /// </summary>
        /// <param name="sortDirection">排序的方向</param>
        public void SetDirection(ListSortDirection sortDirection)
        {
            this._sortDirection = sortDirection;
        }
    }

    /// <summary>
    /// 可排序的BindingList。
    /// </summary>
    /// <remarks>代码参考自 http://www.it165.net/pro/html/201309/7026.html </remarks>
    /// <typeparam name="T"></typeparam>
    public class SortableBindingList<T> : BindingList<T>
    {
        /// <summary>
        /// 要排序的属性字典
        /// </summary>
        private readonly Dictionary<string, PropertyComparer<T>> _comparerList = new Dictionary<string, PropertyComparer<T>>();

        private ListSortDirection _sortDirection;
        private PropertyDescriptor _property;

        /// <summary>
        /// 重载SortPropertyCore，提供要排序的属性
        /// </summary>
        protected override PropertyDescriptor SortPropertyCore
        {
            get { return this._property; }
        }

        /// <summary>
        /// 排序方向
        /// </summary>
        protected override ListSortDirection SortDirectionCore
        {
            get { return this._sortDirection; }
        }

        /// <summary>
        /// 使集合支持排序
        /// </summary>
        protected override bool SupportsSortingCore
        {
            get { return true; }
        }

        /// <summary>
        /// 使集合支持排序
        /// </summary>
        protected override bool IsSortedCore
        {
            get { return true; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SortableBindingList()
            : this(new List<T>())
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SortableBindingList(IEnumerable<T> enumerable)
            : base(new List<T>(enumerable))
        {
        }

        /// <summary>
        /// 实现排序的关键方法
        /// </summary>
        /// <param name="property">要排序的属性的PropertyDescriptor</param>
        /// <param name="sortDirection">排序的方向</param>
        protected override void ApplySortCore(PropertyDescriptor property, ListSortDirection sortDirection)
        {
            //从比较列表中找（没有就新建）当前属性对应的比较器comparer.
            var name = property.Name;
            PropertyComparer<T> comparer;
            if (!this._comparerList.TryGetValue(name, out comparer))
            {
                comparer = new PropertyComparer<T>(property, sortDirection);
                this._comparerList.Add(name, comparer);
            }

            //设置comparer的方向，并且把本集合的数据采用这个comparer来进行排序
            comparer.SetDirection(sortDirection);
            List<T> list = (List<T>)this.Items;
            list.Sort(comparer);

            //排序完成，设置事件更新界面。
            this._property = property;
            this._sortDirection = sortDirection;
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
    }

    #endregion
}
