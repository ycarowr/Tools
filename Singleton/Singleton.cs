namespace YWR.Tools
{
    public class Singleton<T> where T : class, new()
    {
        protected Singleton()
        {
        }

        public static T Instance { get; private set; } = CreateInstance();

        private static T CreateInstance()
        {
            return Instance ??= new T();
        }

        public void InjectInstance(T instance)
        {
            if (instance != null)
            {
                Instance = instance;
            }
        }
    }
}