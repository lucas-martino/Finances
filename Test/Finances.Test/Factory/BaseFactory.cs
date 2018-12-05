namespace Test.Finances.Factory
{
    public abstract class BaseFactory<T>
        where T : new()
    {
        protected T Instance = new T();

        protected abstract void PopulateValid();

        public T Build()
        {
            return Instance;
        }
    }
}