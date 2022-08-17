using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public abstract class TableKit<TDataItem>: IEnumerable<TDataItem>,IDisposable
{
    public void Add(TDataItem item)
    {
        OnAdd(item);
    }

    public void Remove(TDataItem item)
    {
        OnRemove(item);
    }

    public void Clear()
    {
        OnClear();
    }

    // ËÇ£¬ë¦éÍ TDataItem ÷×ßÈãÀìÚéÄ?úş£¬á¶ì¤òÁïÈËÇ??Ê¦£¬å¥êóÊ¦ÒöãÀ??úş ì¤ı¨î¢?
    public void Update()
    {
    }

    protected abstract void OnAdd(TDataItem item);
    protected abstract void OnRemove(TDataItem item);

    protected abstract void OnClear();


    public abstract IEnumerator<TDataItem> GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Dispose()
    {
        OnDispose();
    }

    protected abstract void OnDispose();
}

public class TableIndex<TKeyType, TDataItem> : IDisposable
{
    public void Dispose()
    {
        throw new NotImplementedException();
    }
}