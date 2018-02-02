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
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
            //    Debug.Log("alo");
                 Destroy(this.gameObject);
            }
        }

        // public void DestroyOnLoad()
        // {
        //     Destroy(this.gameObject);
        // }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        // called second
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if(scene.name == "win" || scene.name =="menu"){
                GetComponent<AudioSource>().mute = true;
            }
            else{
                GetComponent<AudioSource>().mute = false;

                //if(menuwin)GetComponent<AudioSource>().Pause();
                //else GetComponent<AudioSource>().Play();
            }
        }

        // called when the game is terminated
        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}
