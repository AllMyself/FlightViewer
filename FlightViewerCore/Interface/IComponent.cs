using System;
using System.Collections.Generic;

namespace BinHong.FlightViewerCore
{
    // ***************** 对象 性质接口定义 ******************

    /// <summary>
    /// 组件接口。
    /// 1，每个组件都有Owner
    /// 2，每个组件可以Disposable
    /// </summary>
    public interface IComponent : IOwner, IDisposable
    {

    }

    /// <summary>
    /// 孩子的容器接口
    /// 1，父容器中必然有个孩子容器，用于存放孩子组件
    /// 2，孩子容器有枚举和添加删除获取的功能
    /// </summary>
    public interface IChildContainer<T> : IEnumerable<T>, IAdd, IDelete
    {

    }

    // ***************** 行为 接口定义 ******************

    public interface IBuildModule
    {
        /// <summary>
        /// 构建对象需要的模块
        /// </summary>
        void BuildModule();
    }

    public interface IInitializeModule
    {
        /// <summary>
        /// 初始化模块
        /// </summary>
        void InitializeModule();
    }

    public interface IInitialize
    {
        /// <summary>
        /// 初始化
        /// </summary>
        void Initialize();
    }

    public interface IUnIntialize
    {
        /// <summary>
        /// 反初始化。在Dispose之前
        /// </summary>
        void UnInitialize();
    }

    public interface IOwner
    {
        /// <summary>
        /// 只是对象所有者。
        /// </summary>
        IOwner Owner { get;}
    }

    public interface IAdd
    {
        void Add(IAdd item);
    }

    public interface IDelete
    {
        void Delete(IDelete item);
        void Delete(int index);
    }

    public interface IGetItemByIndex<T>
    {
        T GetSpecificItem(int index);
    }

    public interface IGetItemByName<T>
    {
        T GetSpecificItem(string name);
    }

    public interface IGetItem<T> : IGetItemByIndex<T>, IGetItemByName<T>
    {
    }

    public interface IGetItemByIndex
    {
        IGetItemByIndex GetItem(int index);
    }

    public interface IGetItemByName
    {
        IGetItemByName GetItem(string name);
    }

    public interface IGetItem : IGetItemByIndex, IGetItemByName
    {
    }

    public interface IName
    {
        /// <summary>
        /// 名字。大多数Name是一个对象唯一的标识,类似于ID。比如在Component中
        /// </summary>
        string Name { get; }
    }

    /// <summary>
    /// 别名
    /// </summary>
    public interface IAlias
    {
        /// <summary>
        /// 别名。
        /// </summary>
        string AliasName { get; set; }
    }

    public interface IID
    {
        int ID { get; }
    }

    public interface IPath
    {
        string Path { get; set; }
    }

    public interface IPathAndName : IPath, IName
    {
        
    }

    public interface ICreateID
    {
        int GetOneID();
    }
}
