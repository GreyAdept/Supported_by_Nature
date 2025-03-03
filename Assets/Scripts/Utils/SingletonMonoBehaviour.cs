using UnityEngine;

namespace Utils
{
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; } 

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this); //make sure class is singleton
            }
            else
            {
                Instance = this as T; //Instance can be accessed from anywhere
                DontDestroyOnLoad(this); //persist between scenes
            }
        }
    }
}