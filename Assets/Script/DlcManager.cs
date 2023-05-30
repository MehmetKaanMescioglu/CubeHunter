using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DlcManager : MonoBehaviour  
{
    public static DlcManager Instance;
    public int mainHealth = 0;
    public bool mainIsOwned { get; set; }
    public bool mainIsPremium { get; set; }
    public string mainStatus = "Standard";
    // Start is called before the first frame update
    void Start()
    {
        mainHealth = LevelManager.Instance.GetHealth();
        mainIsOwned = LevelManager.Instance.isOwned;
        mainStatus = LevelManager.Instance.status;
        mainIsPremium = LevelManager.Instance.isPremium;
    }

    // Update is called once per frame
    void Update()
    {
        LevelManager.Instance.SetHealth(mainHealth);
        LevelManager.Instance.isOwned = mainIsOwned;
        LevelManager.Instance.status = mainStatus;
        LevelManager.Instance.isPremium = mainIsPremium;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //Singleton();

        if (Instance == null || Instance == this)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    

    public void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }

}
