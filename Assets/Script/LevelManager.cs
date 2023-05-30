using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using HmsPlugin;
using HuaweiMobileServices.Ads;
using HuaweiMobileServices.Id;
using HuaweiMobileServices.Utils;
using HuaweiMobileServices.IAP;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public float level = 1;
    public int point = 0;
    public int levelPoint = 0;

    public bool isOwned {  get; set; }

    public bool isPremium { get; set; }

    public int health = 0;

    public string status = "Standard";
    //private TextMeshProUGUI levelTxt;

    private void Awake()
    {
        //Instance = this;
        DontDestroyOnLoad(gameObject);
        //Singleton();
        //levelTxt = GameObject.Find("levelTxt").GetComponent<TextMeshProUGUI>();
        //float chapter = LevelManager.Instance.Getlevel();
        //levelTxt.text = level.ToString();

        if(Instance == null || Instance == this)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {

        HMSAccountKitManager.Instance.OnSignInSuccess = SignInSuccess;
        HMSAccountKitManager.Instance.OnSignInFailed = SignInFailed;
        //HMSAccountKitManager.Instance.SignIn();
        HMSAccountKitManager.Instance.SilentSignIn();


        if (level == 1 || level == 0)
            level = 1;
        else 
            level += 1;
        //chapter++;

        //health = PlayerPrefs.GetInt("health");

        if (PlayerPrefs.HasKey("bar"))
        {
            if(isPremium == false && health == 99)
            {
                SetHealth(-99);
                PlayerPrefs.SetInt("bar", health);
            }
            health = PlayerPrefs.GetInt("bar");
            DlcManager.Instance.mainHealth = PlayerPrefs.GetInt("bar");
        }
        else
        {
            PlayerPrefs.SetInt("bar", health);
        }

        if (PlayerPrefs.HasKey("own"))
        {
            isOwned = PlayerPrefs.GetInt("own") == 1 ? true : false;
            DlcManager.Instance.mainIsOwned = PlayerPrefs.GetInt("own") == 1 ? true : false;
        }
        else
        {
            PlayerPrefs.SetInt("own", isOwned?1:0);
        }

    }

    private void Update()
    {
        //Instance = this;
    }

    private void SignInSuccess(AuthAccount authAccount)
    {
        Debug.Log(" OnLoginSuccess User Name :" + authAccount.DisplayName);
    }

    private void SignInFailed(HMSException exception)
    {
        Debug.Log(" SignInFailed. Exception details:" + exception.Message);
    }

    #region Singleton

    //public static LevelManager Instance;

    /*public void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }*/
    #endregion

    IEnumerator DebugLogIEnumerator()
    {
        var yieldReturn = new WaitForSeconds(1f);

        while(true)
        {
            yield return yieldReturn;
            Debug.Log("DebugLogIEnumerator");
        }
    }

    public void Setlevel(int index)   
    {
        levelPoint = 0;
        level += index;
    }

    public void SetAllStart()
    {
        level = 1;
        point = 0;
        levelPoint = 0;
    }

    public void SetLevelStart()
    {
        level = 1;
        point = 0;
        levelPoint = 0;
    }

    public float Getlevel()
    {
        return level;
    }

    public void RePoint()
    {
        point -= levelPoint;
        levelPoint = 0;
    }

    public void SetPoint()
    {
        point += 10;
        levelPoint += 10;
    }

    public int GetPoint()
    {
        return point;
    }

    public int GetLocPoint()
    {
        return levelPoint;
    }

    public void SetHealth(int value)
    {
        
        if (isPremium == false)
        {
            health = value;
        }
        else
        {
            health = 99;
        }
        PlayerPrefs.SetInt("bar", health);
        //PlayerPrefs.SetInt("health", health);
    }

    public void SetHealthPlus(int value)
    {
        health += value;
        //PlayerPrefs.SetInt("health", health);
        PlayerPrefs.SetInt("bar", health);
    }

    public void IncreaseHealth()
    {
        if (isPremium == false)
        {
            health += 1;
        }
        PlayerPrefs.SetInt("bar", health);

        //PlayerPrefs.SetInt("health", health);
    }

    public void DecreaseHealth()
    {
        if(isPremium == false)
        {
            health -= 1;
        }
        PlayerPrefs.SetInt("bar", health);

        //PlayerPrefs.SetInt("health", health);
    }

    public int GetHealth()
    {
        return health;
    }




}
