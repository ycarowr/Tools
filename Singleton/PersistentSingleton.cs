using UnityEngine;

namespace YWR.Tools
{
    public class PersistentSingleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    CreateInstance();
                }
                else
                {
                    HandleDuplication();
                }

                return instance;
            }
        }

        private static void CreateInstance()
        {
            GameObject go = new GameObject(typeof(T).ToString());
            instance = go.AddComponent<T>();
        }

        private static void HandleDuplication()
        {
            Object[] copies = FindObjectsOfType(typeof(T));
            foreach (Object copy in copies)
            {
                if (copy != instance)
                {
                    Destroy(copy);
                }
            }
        }
    }
}