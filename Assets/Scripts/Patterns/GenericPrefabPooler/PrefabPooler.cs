using System.Collections.Generic;
using UnityEngine;

namespace Patterns
{
    public interface IPooler
    {
        Dictionary<GameObject, List<GameObject>> Busy { get; }
        Dictionary<GameObject, List<GameObject>> Available { get; }
        GameObject[] Models { get; }
        int StartSize { get; }
    }

    /// <summary>
    ///     T is the Singleton Pooler class and T1 is the Pooled.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="T1"></typeparam>
    public class PrefabPooler<T> : SingletonMB<T>
        where T : class
    {
        [Tooltip("All pooled models have to be inside this array before the initialization")] [SerializeField]
        private GameObject[] models;

        [Tooltip("How many objects will be created as soon as the game loads")] [SerializeField]
        private int startSize = 10;
        //--------------------------------------------------------------------------------------------------------------

        public Dictionary<GameObject, List<GameObject>> Busy { get; set; } =
            new Dictionary<GameObject, List<GameObject>>();

        public Dictionary<GameObject, List<GameObject>> Available { get; set; } =
            new Dictionary<GameObject, List<GameObject>>();

        public int StartSize => startSize;
        public GameObject[] Models => models;

        //--------------------------------------------------------------------------------------------------------------

        #region Initialization

        /// <summary>
        ///     I am initializing it as soon as possible. You can move it to Awake or Start calls. It's up to you.
        /// </summary>
        private void OnEnable()
        {
            //avoiding execution when the game isn't playing
            if (!Application.isPlaying)
                return;

            //initialize the pool system
            Initialize();
        }

        /// <summary>
        ///     Here is the initialization of the pooler. All the models/prefabs which you need to pool have to be inside
        ///     the modelPooled array. They will be keys for the Lists inside the pool system.
        /// </summary>
        private void Initialize()
        {
            if (models.Length == 0)
                Debug.LogError("Can't pool empty objects.");

            foreach (var model in models)
            {
                //list for pool
                var pool = new List<GameObject>();

                //list for busy
                var busy = new List<GameObject>();

                //create the initial amount and add them to the pool
                for (var i = 0; i < startSize; i++)
                {
                    var obj = Instantiate(model, transform);
                    pool.Add(obj);
                    obj.SetActive(false);
                }

                Available.Add(model, pool);
                Busy.Add(model, busy);
            }
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Operations

        /// <summary>
        ///     Here you can pool the prefab objects. Currently the key is a reference to the prefab that you need to get.
        ///     Although I haven't had problems with this approach, you can come up with a solution that performs better
        ///     using an enumeration as key.
        /// </summary>
        /// <param name="prefabModel"></param>
        /// <returns></returns>
        public virtual GameObject Get(GameObject prefabModel)
        {
            GameObject pooledObj = null;

            if (Available == null)
                Debug.LogError("Nop! PoolAble objects list is not created yet!");

            if (Busy == null)
                Debug.LogError("Nop! Busy objects list is not created yet!");

            //if prefabModel is not contained inside the StatesRegister
            if (!Available.ContainsKey(prefabModel))
                return null;

            //try to grab the last element of the available objects
            if (Available[prefabModel].Count > 0)
            {
                var size = Available[prefabModel].Count;
                pooledObj = Available[prefabModel][size - 1];
            }

            if (pooledObj != null)
                Available[prefabModel].Remove(pooledObj);
            else
                pooledObj = Instantiate(prefabModel, transform);

            //add the pooled object to the used objects list
            Busy[prefabModel].Add(pooledObj);

            pooledObj.SetActive(true);
            OnPool(pooledObj);

            return pooledObj;
        }

        /// <summary>
        ///     Here you can pool the prefab objects. Currently the key is a reference to the prefab that you need to get.
        ///     Although I haven't had problems with this approach, you can come up with a solution that performs better
        ///     using an enumeration as key.
        /// </summary>
        /// <param name="prefabModel"></param>
        /// <returns></returns>
        public virtual T1 Get<T1>(GameObject prefabModel) where T1 : class
        {
            var obj = Get(prefabModel);
            return obj.GetComponent<T1>();
        }

        /// <summary>
        ///     Here you pool back objects that you no longer use. They are deactivated and
        ///     stored back for future usage using the prefab model as key to get it back later on.
        /// </summary>
        /// <param name="prefabModel"></param>
        /// <param name="pooledObj"></param>
        public virtual void Release(GameObject prefabModel, GameObject pooledObj)
        {
            if (Available == null)
                Debug.LogError("Nop! PoolAble objects list is not created yet!");

            if (Busy == null)
                Debug.LogError("Nop! Busy objects list is not created yet!");

            pooledObj.SetActive(false);
            Busy[prefabModel].Remove(pooledObj);
            Available[prefabModel].Add(pooledObj);
            pooledObj.transform.parent = transform;
            pooledObj.transform.localPosition = Vector3.zero;
            OnRelease(pooledObj);
        }

        /// <summary>
        ///     Here you pool back objects that you no longer use. They are deactivated and
        ///     stored back for future usage using the prefab model as key to get it back later on.
        /// </summary>
        /// <param name="pooledObj"></param>
        public virtual void Release(GameObject pooledObj)
        {
            if (Available == null)
                Debug.LogError("Nop! PoolAble objects list is not created yet!");

            if (Busy == null)
                Debug.LogError("Nop! Busy objects list is not created yet!");

            pooledObj.SetActive(false);

            foreach (var model in Busy.Keys)
                if (Busy[model].Contains(pooledObj))
                {
                    Busy[model].Remove(pooledObj);
                    Available[model].Add(pooledObj);
                }

            pooledObj.transform.parent = transform;
            pooledObj.transform.localPosition = Vector3.zero;
            OnRelease(pooledObj);
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Util

        /// <summary>
        ///     Override this method to do something when before pool an object.
        /// </summary>
        /// <param name="prefabModel"></param>
        protected virtual void OnPool(GameObject prefabModel)
        {
            // If you need to execute some code right BEFORE the object is pooled, you can do it here.
            // Clean references or reset variables are very common cases.
        }

        /// <summary>
        ///     Override this method to do something after release an object.
        /// </summary>
        /// <param name="prefabModel"></param>
        protected virtual void OnRelease(GameObject prefabModel)
        {
            // If you need to execute some code right AFTER the object is released, you can do it here.
            // Clean references or reset variables are very common cases.
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------
    }
}