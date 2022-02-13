using System.Collections.Generic;
using UnityEngine;

namespace YWR.Tools
{
    public class PrefabPooler<T> : SingletonMB<T> where T : class
    {
        [Tooltip("All pooled models have to be inside this array before the initialization")]
        public GameObject[] models;

        [Tooltip("How many objects will be created as soon as the game loads")]
        public int startSize = 10;

        private readonly Dictionary<GameObject, List<GameObject>> m_Busy = new Dictionary<GameObject, List<GameObject>>();
        private readonly Dictionary<GameObject, List<GameObject>> m_Free = new Dictionary<GameObject, List<GameObject>>();

        private void OnEnable()
        {
            if (!Application.isPlaying)
            {
                return;
            }

            Initialize();
        }

        private void Initialize()
        {
            if (models.Length == 0)
            {
                Debug.LogError("Can't pool empty objects.");
            }

            foreach (GameObject model in models)
            {
                List<GameObject> pool = new List<GameObject>();
                List<GameObject> busy = new List<GameObject>();
                for (int i = 0; i < startSize; i++)
                {
                    GameObject obj = Instantiate(model, transform);
                    pool.Add(obj);
                    obj.SetActive(false);
                }

                m_Free.Add(model, pool);
                m_Busy.Add(model, busy);
            }
        }


        public virtual GameObject Get(GameObject prefabModel)
        {
            GameObject pooledObj = null;

            if (m_Free == null)
            {
                Debug.LogError("Nop! PoolAble objects list is not created yet!");
                return null;
            }

            if (m_Busy == null)
            {
                Debug.LogError("Nop! Busy objects list is not created yet!");
                return null;
            }

            if (!m_Free.ContainsKey(prefabModel))
            {
                return null;
            }

            if (m_Free[prefabModel].Count > 0)
            {
                int size = m_Free[prefabModel].Count;
                pooledObj = m_Free[prefabModel][size - 1];
            }

            if (pooledObj != null)
            {
                m_Free[prefabModel].Remove(pooledObj);
            }
            else
            {
                pooledObj = Instantiate(prefabModel, transform);
            }

            m_Busy[prefabModel].Add(pooledObj);

            pooledObj.SetActive(true);
            OnPool(pooledObj);

            return pooledObj;
        }

        public virtual T1 Get<T1>(GameObject prefabModel) where T1 : class
        {
            GameObject obj = Get(prefabModel);
            return obj.GetComponent<T1>();
        }

        public virtual T1 GetFirst<T1>() where T1 : class
        {
            GameObject prefabModel = models[0];
            GameObject obj = Get(prefabModel);
            return obj.GetComponent<T1>();
        }

        public virtual void Release(GameObject prefabModel, GameObject pooledObj)
        {
            if (m_Free == null)
            {
                Debug.LogError("Nop! PoolAble objects list is not created yet!");
                return;
            }

            if (m_Busy == null)
            {
                Debug.LogError("Nop! Busy objects list is not created yet!");
                return;
            }

            pooledObj.SetActive(false);
            m_Busy[prefabModel].Remove(pooledObj);
            m_Free[prefabModel].Add(pooledObj);
            pooledObj.transform.parent = transform;
            pooledObj.transform.localPosition = Vector3.zero;
            OnRelease(pooledObj);
        }

        public virtual void Release(GameObject pooledObj)
        {
            if (m_Free == null)
            {
                Debug.LogError("Nop! PoolAble objects list is not created yet!");
                return;
            }

            if (m_Busy == null)
            {
                Debug.LogError("Nop! Busy objects list is not created yet!");
                return;
            }

            pooledObj.SetActive(false);

            foreach (GameObject model in m_Busy.Keys)
            {
                if (m_Busy[model].Contains(pooledObj))
                {
                    m_Busy[model].Remove(pooledObj);
                    m_Free[model].Add(pooledObj);
                }
            }

            pooledObj.transform.parent = transform;
            pooledObj.transform.localPosition = Vector3.zero;
            OnRelease(pooledObj);
        }

        protected virtual void OnPool(GameObject prefabModel)
        {
        }

        protected virtual void OnRelease(GameObject prefabModel)
        {
        }
    }
}