using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NolowaFrontend.Extensions
{
    public static class ObservableCollectionExtension
    {
        public static void AddRange<T>(this ObservableCollection<T> src, IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                src.Add(item);
            }
        }

        public static void Refresh<T>(this ObservableCollection<T> src)
        {
            CollectionViewSource.GetDefaultView(src).Refresh();
        }
    }
}
