using UnityEngine;

public class EntireGameScript : MonoBehaviour

{
    public static EntireGameScript Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make it persist across scenes
        }
        else
        {
            Destroy(gameObject); // Prevent duplicates
        }
    }

    void OnApplicationQuit()
    {
        Debug.Log("Application is quitting!");
        SaveManager.DeleteSaved(); //delete before quitting game. does not handle crashes.
    }
}
