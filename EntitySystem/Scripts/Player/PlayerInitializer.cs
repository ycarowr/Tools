using UnityEngine;

namespace YWR.Tools
{
    public class PlayerInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerProvider playerProvider;

        [SerializeField] private PlayerData playerData;

        private void Start()
        {
            Initialize();
        }

        private void OnDestroy()
        {
            playerProvider.RemovePlayer(playerData.id);
        }

        private void Initialize()
        {
            playerProvider.AddPlayer(playerData);
        }
    }
}