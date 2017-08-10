using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BinHong.FlightViewerCore
{
    public class ComponentList<TItem> : IChildContainer<AbstractComponent>, IGetItem<TItem> where TItem : class
    {
        private readonly List<AbstractComponent> _list=new List<AbstractComponent>();

        public IEnumerator<AbstractComponent> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IAdd item)
        {
            AbstractComponent component = item as AbstractComponent;
            _list.Add(component);
        }

        public void Delete(IDelete item)
        {
            AbstractComponent component = item as AbstractComponent;
            _list.Remove(component);
        }

        public void Delete(int index)
        {
            _list.RemoveAt(index);
        }

        public TItem GetSpecificItem(int index)
        {
            return _list[index] as TItem;
        }

        public TItem GetSpecificItem(string name)
        {
            foreach (var component in _list)
            {
                IName nameItem = (IName)component;
                if (nameItem.Name == name)
                {
                    return component as TItem;
                }
            }
            return null;
        }
    }

    public class ComponentDic<TItem> : IChildContainer<AbstractComponent>, IGetItem<TItem> where TItem : class
    {
        private readonly Dictionary<string, AbstractComponent> _dictionary = new Dictionary<string, AbstractComponent>();

        public IEnumerator<AbstractComponent> GetEnumerator()
        {
            return _dictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IAdd item)
        {
            IName nameItem = (IName)item;
            _dictionary.Add(nameItem.Name, (AbstractComponent)nameItem);
        }

        public void Delete(IDelete item)
        {
            IName nameItem = (IName)item;
            _dictionary.Remove(nameItem.Name);
        }

        public void Delete(int index)
        {
            if (index < 0 || index > _dictionary.Values.Count - 1)
            {
                return;
            }

            string key = _dictionary.Keys.ElementAt(index);
            _dictionary.Remove(key);
        }

        public TItem GetSpecificItem(int index)
        {
            if (index < 0 || index > _dictionary.Values.Count - 1)
            {
                return null;
            }
            return _dictionary.Values.ElementAt(index) as TItem;
        }

        public TItem GetSpecificItem(string name)
        {
            if (!_dictionary.ContainsKey(name))
            {
                return null;
            }
            return _dictionary[name] as TItem;
        }
    }
}
