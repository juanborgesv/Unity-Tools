/// <summary>
/// Represents a pool of objects that can be reused to improve performance.
/// </summary>
/// <typeparam name="T">The type of objects in the pool.</typeparam>
public interface IPool<T>
{
    T Get();
    void Return(T instance);
}
