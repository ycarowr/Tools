using System.Collections.Generic;
using UnityEngine;

namespace YWR.Tools
{
    [CreateAssetMenu]
    public class PlayerProvider : ScriptableObject
    {
        private readonly List<PlayerData> m_Registry = new List<PlayerData>();

        private void OnEnable()
        {
            m_Registry.Clear();
        }

        public void AddPlayer(PlayerData data)
        {
            m_Registry.Add(data);
        }

        public void RemovePlayer(ulong id)
        {
            PlayerData? data = GetPlayerData(id);
            if (data != null)
            {
                m_Registry.Remove(data.Value);
            }
        }

        public PlayerData? GetPlayerData(ulong id)
        {
            foreach (PlayerData data in m_Registry)
            {
                if (data.id == id)
                {
                    return data;
                }
            }

            return null;
        }

        public bool IsPlayer(GameObject entity)
        {
            foreach (PlayerData data in m_Registry)
            {
                bool isPlayer = data.IsPlayer(entity);
                if (isPlayer)
                {
                    return true;
                }
            }

            return false;
        }

        public PlayerData GetNearestPlayer(Vector3 position)
        {
            int index = -1;
            float min = float.MaxValue;
            int count = m_Registry.Count;
            for (int i = 0; i < count; ++i)
            {
                PlayerData current = m_Registry[i];
                float distance = Vector3.Distance(current.transform.position, position);
                if (distance < min)
                {
                    min = distance;
                    index = i;
                }
            }

            return m_Registry[index];
        }

        public GameObject[] GetAllPlayers()
        {
            int count = m_Registry.Count;
            GameObject[] allPlayers = new GameObject[count];

            for (int i = 0; i < count; ++i)
            {
                allPlayers[i] = m_Registry[i].gameObject;
            }

            return allPlayers;
        }
    }
}