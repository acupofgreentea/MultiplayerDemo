using UnityEngine;

public class Managers : MonoBehaviour
{
    public SafeZoneManager SafeZoneManager {get; private set;}
    public GameManager GameManager {get; private set;}
    public DropBoxManager DropBoxManager {get; private set;}

    public static Managers Instance {get; private set;}

    private void Awake() 
    {
        if(Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;  

        CacheManagers();
    }

    private void CacheManagers()
    {
        SafeZoneManager = GetComponentInChildren<SafeZoneManager>();
        GameManager = GetComponentInChildren<GameManager>();      
        DropBoxManager = GetComponentInChildren<DropBoxManager>();
    }
}
