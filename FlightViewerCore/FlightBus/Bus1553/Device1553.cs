namespace BinHong.FlightViewerCore
{
    public class Device1553 : AbstractDevice, IGetItem<Channel1553>
    {
        public override void BuildModule()
        {
            throw new System.NotImplementedException();
        }

        public override IGetItemByIndex GetItem(int index)
        {
            throw new System.NotImplementedException();
        }

        public override IGetItemByName GetItem(string name)
        {
            throw new System.NotImplementedException();
        }

        protected override IChildContainer<AbstractComponent> ChildComponents
        {
            get { return _componentDic; }
        }
        private readonly ComponentDic<Channel1553> _componentDic = new ComponentDic<Channel1553>();


        public Channel1553 GetSpecificItem(string name)
        {
            return _componentDic.GetSpecificItem(name);
        }

        public Channel1553 GetSpecificItem(int index)
        {
            return _componentDic.GetSpecificItem(index);
        }
    }
}
