using CustomerManagement.Core.Common;

namespace CustomerManagement.Core.Events.CommonEvents;

public class CompleteEvent<T> : BaseEvent
{
    public readonly T obj;
    public readonly List<T> lstObj;
    public Guid id;
    public CompleteEvent(T obj)
    {
        this.obj = obj;
    }

    public CompleteEvent(List<T> lstObj)
    {
        this.lstObj = lstObj;
    }

    public CompleteEvent(Guid id)
    {
        this.id = id;
    }
}