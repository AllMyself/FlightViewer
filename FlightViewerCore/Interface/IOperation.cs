using System.Security.Cryptography.X509Certificates;

namespace BinHong.FlightViewerCore
{
    public interface IStartStop
    {
        void Start();
        void Stop();
    }

    public interface IAddDelete<T> : IAdd<T>, IDelete<T>, IRemove
    {
    }

    public interface IAdd<T>
    {
        void Add(T t);
    }

    public interface IDelete<T>
    {
        void Delete(T t);
    }

    public interface IRemove
    {
        void Remove(int index);
    }

    public interface ISelectByName
    {
        void Select(string name);
    }

    public interface IIsSelected
    {
        bool IsSelected { get; set; }
    }

    public interface IFilter
    {
    }

    public interface ISummary
    {

    }
}
