using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class AudioManagerScript : MonoBehaviour
    {
        private static AudioManagerScript _audioManager;
        private void Awake()
        {
            if (!_audioManager)
            {
                _audioManager = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                 Destroy(gameObject);
            }
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        // called second
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if(scene.name == "networkMVP")
            {
                Destroy(gameObject);
            }
        }

        // called when the game is terminated
        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
