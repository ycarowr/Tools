using UnityEngine;

namespace Usage
{
    public class PrefabPoolUsage : MonoBehaviour
    {
        [SerializeField] private GameObject[] Prefabs;

        [Button]
        //pool random object from inside prefabs array
        public void PoolRandomObject()
        {
            var randomIndex = Random.Range(0, Prefabs.Length);
            var randomObj = Prefabs[randomIndex];
            var obj = APooler.Instance.Get<APooled>(randomObj);
            obj.transform.SetParent(transform);
        }

        [Button]
        //release random child
        public void ReleaseRandomChild()
        {
            if (transform.childCount <= 0)
                return;

            var randomIndex = Random.Range(0, transform.childCount);
            var randomChild = transform.GetChild(randomIndex);
            APooler.Instance.Release(randomChild.gameObject);
        }
    }
}