using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    // Public property to access the instance
    public static T Instance
    {
        get
        {
            // Check if an instance already exists
            if (_instance == null)
            {
                // Search for an existing instance in the scene
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    // If not found, create a new one
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    _instance = singletonObject.AddComponent<T>();
                }
            }

            return _instance;
        }
    }

    // Ensure this script is not destroyed when loading a new scene
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Optionally add a method to explicitly clear the instance (useful for testing or resetting the singleton)
    public static void ClearInstance()
    {
        _instance = null;
    }
}
