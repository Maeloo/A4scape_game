using UnityEngine;

public interface Poolable<T> where T : Component, Poolable<T>, new()
{
    void Initialize(object[] args = null);
    bool IsReady();
    void Duplicate(T a_template);
    void Register(Pool<T> pool);
    void Release();
    void Pick();
}
