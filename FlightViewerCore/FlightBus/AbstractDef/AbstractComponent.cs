namespace BinHong.FlightViewerCore
{
    public abstract class AbstractComponent : IComponent, IUnIntialize, IAdd, IDelete, IGetItem
    {
        /// <summary>
        /// 子组件。子组件需要先Add然后再执行Build,initialize
        /// </summary>
        protected abstract IChildContainer<AbstractComponent> ChildComponents { get; }

        public IOwner Owner { get; protected set; }//组件有一个所有者

        public virtual void UnInitialize()//初始化
        {
            foreach (var component in ChildComponents)
            {
                component.UnInitialize();
            }
        }

        public virtual void Dispose()
        {
            foreach (var component in ChildComponents)
            {
                component.Dispose();
            }
        }

        public virtual void Add(IAdd item)//添加组件
        {
            AbstractComponent component = item as AbstractComponent;
            if (component != null)
            {
                component.Owner = this;
                ChildComponents.Add(item);
            }
        }

        public virtual void Delete(IDelete item)//删除组件
        {
            ChildComponents.Delete(item);
        }

        public virtual void Delete(int index)//删除组件 
        {
            ChildComponents.Delete(index);
        }

        public abstract IGetItemByIndex GetItem(int index);//获取组件

        public abstract IGetItemByName GetItem(string name);
    }

    public abstract class AbstractPathComponent : AbstractComponent, IPathAndName
    {
        public string Path { get; set; }

        public string Name { get; protected set; }

        public override void Add(IAdd item)
        {
            base.Add(item);
            IPathAndName pathItem = item as IPathAndName;
            if (pathItem != null)
            {
                pathItem.Path = this.Path + "_" + pathItem.Name;
            }
        }
    }
}
