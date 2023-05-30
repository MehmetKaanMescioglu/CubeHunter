using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

using HmsPlugin;
using HuaweiMobileServices.Ads;
using HuaweiMobileServices.Id;
using HuaweiMobileServices.Utils;
using HuaweiMobileServices.IAP;


public class MenuInMain : MonoBehaviour
{
    public GameObject firstPlay, gameMenu, PauseMenu, FailMenu, WinMenu;
    public GameObject StartMenu, goBackToMenuButton, musicButton;
    public GameObject SettingMenu, LevelsMenu, AboutGameMenu, MarketMenu, DeveloperInfoMenu;
    public GameObject MainMenuContentsMenu;
    public GameObject FailAdsMenu, TryAgainInitialMenu;
    public GameObject NativeAdsMenu;
    public GameObject plusBtn;

    [SerializeField] private TextMeshProUGUI healthTxt;
    [SerializeField] private TextMeshProUGUI Status;

    

    AudioSource audioSource;

 

    List<InAppPurchaseData> consumablePurchaseRecord = new List<InAppPurchaseData>();
    List<InAppPurchaseData> activeNonConsumables = new List<InAppPurchaseData>();
    List<InAppPurchaseData> activeSubscriptions = new List<InAppPurchaseData>();

    void Start()
    {

        /*HMSAccountKitManager.Instance.OnSignInSuccess = SignInSuccess;
        HMSAccountKitManager.Instance.OnSignInFailed = SignInFailed;
        //HMSAccountKitManager.Instance.SignIn();
        HMSAccountKitManager.Instance.SilentSignIn();*/


        HMSAdsKitManager.Instance.OnRewarded = OnRewarded;
        
        audioSource = GetComponent<AudioSource>();

        HMSIAPManager.Instance.InitializeIAP();
        RestoreProducts();


        if (LevelManager.Instance.isOwned == false)
        {
            HMSAdsKitManager.Instance.ShowBannerAd();
            NativeAdsMenu.SetActive(true);
        }
        else
        {
            HMSAdsKitManager.Instance.HideBannerAd();
            NativeAdsMenu.SetActive(false);
        }



        


    }

    private void SignInSuccess(AuthAccount authAccount)
    {
        Debug.Log(" OnLoginSuccess User Name :" + authAccount.DisplayName);
        
    }

    private void SignInFailed(HMSException exception)
    {
        Debug.Log(" SignInFailed. Exception details:" + exception.Message);
    }

    void Update()
    {

        float health = LevelManager.Instance.GetHealth();
        healthTxt.text = health.ToString();

        string status = LevelManager.Instance.status;
        Status.text = status;

        if (LevelManager.Instance.isOwned == true && LevelManager.Instance.isPremium != true)
        {
            LevelManager.Instance.status = "Pro";
            DlcManager.Instance.mainStatus = "Pro";
        }
        else if (LevelManager.Instance.isOwned == true && LevelManager.Instance.isPremium == true)
        {
            LevelManager.Instance.status = "Premium";
            DlcManager.Instance.mainStatus = "Premium";
        }
        else
        {
            LevelManager.Instance.status = "Standard";
            DlcManager.Instance.mainStatus = "Standard";
        }



    }

    private void Awake()
    {
        Singleton();
    }

    #region Singleton

    public static MenuInMain Instance;

    public void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }
    #endregion
    public void FirstPlayButton()
    {
        //firstPlay.SetActive(false);
        StartMenu.SetActive(false);
        SceneManager.LoadScene(1);
        //LevelManager.Instance.Setlevel(1);
    }

    public void settingsButton()
    {
        StartMenu.SetActive(false);
        SettingMenu.SetActive(true);
        goBackToMenuButton.SetActive(true);
        //musicButton.SetActive(true);
    }

    public void levelsButton()
    {
        StartMenu.SetActive(false);
        LevelsMenu.SetActive(true);
        goBackToMenuButton.SetActive(true);
    }

    public void gameRulesButton()
    {
        StartMenu.SetActive(false);
        AboutGameMenu.SetActive(true);
        goBackToMenuButton.SetActive(true);

    }

    public void marketButton()
    {
        RestoreProducts();
        StartMenu.SetActive(false);
        MarketMenu.SetActive(true);
        goBackToMenuButton.SetActive(true);
        HMSIAPManager.Instance.InitializeIAP();

        if(LevelManager.Instance.isPremium == true)
        {
            plusBtn.SetActive(true);
        }
        else
        {
            plusBtn.SetActive(false);
        }


    }

    public void developerInfoButton()
    {
        StartMenu.SetActive(false);
        DeveloperInfoMenu.SetActive(true);
        goBackToMenuButton.SetActive(true);
    }

    public void exitButton()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
    public void closeMusicButton()
    {
        audioSource.Stop();
    }

    public void playMusicButton()
    {
        audioSource.Play();
    }


    public void backToMenuButton()
    {
        MainMenuContentsMenu.SetActive(true);
        goBackToMenuButton.SetActive(false);
        AboutGameMenu.SetActive(false);
        LevelsMenu.SetActive(false);
        MarketMenu.SetActive(false);
        DeveloperInfoMenu.SetActive(false);
        SettingMenu.SetActive(false);
        StartMenu.SetActive(true);
        plusBtn.SetActive(false);

    }


    public void PauseButton()
    {
        Time.timeScale = 0;
        gameMenu.SetActive(false);
        PauseMenu.SetActive(true);
        FailMenu.SetActive(false);
    }

    public void PlayButton()
    {
        Time.timeScale = 1;
        gameMenu.SetActive(true);
        PauseMenu.SetActive(false);
        FailMenu.SetActive(false);
    }


    public void RestartButton()
    {
        Time.timeScale = 1;
        //SceneManager.LoadScene(1);

        float chapter = LevelManager.Instance.Getlevel();
        LevelManager.Instance.RePoint();
        Debug.Log("$chapter");
        if (chapter % 5 == 0)
            SceneManager.LoadScene(5);
        if (chapter % 5 == 4)
            SceneManager.LoadScene(4);
        if (chapter % 5 == 3)
            SceneManager.LoadScene(3);
        if (chapter % 5 == 2)
            SceneManager.LoadScene(2);
        if (chapter % 5 == 1)
            SceneManager.LoadScene(1);

    }

    public void MenuButton()
    {
        Time.timeScale = 1;
        Application.Quit();
    }

    public void ActiveFail()
    {

        //retry button

        TryAgainInitialMenu.SetActive(true);

        /* int currentHealth = LevelManager.Instance.GetHealth();



         if(LevelManager.Instance.GetHealth() > 0)
         {
             FailAdsMenu.SetActive(false);
             FailMenu.SetActive(true);
             gameMenu.SetActive(true);
         }
         else
         {
             FailAdsMenu.SetActive(true);
             FailMenu.SetActive(false);
             gameMenu.SetActive(true);
         }*/
    }

    public void FailButton()
    {

        if(LevelManager.Instance.GetHealth() > 0)
        {
            Time.timeScale = 1;
            gameMenu.SetActive(true);
            plusBtn.SetActive(false);
            LevelManager.Instance.RePoint();
            LevelManager.Instance.DecreaseHealth();
            DlcManager.Instance.mainHealth = LevelManager.Instance.GetHealth();

            float chapter = LevelManager.Instance.Getlevel();

            Debug.Log("$chapter");
            if (chapter % 5 == 0)
                SceneManager.LoadScene(5);
            if (chapter % 5 == 4)
                SceneManager.LoadScene(4);
            if (chapter % 5 == 3)
                SceneManager.LoadScene(3);
            if (chapter % 5 == 2)
                SceneManager.LoadScene(2);
            if (chapter % 5 == 1)
                SceneManager.LoadScene(1);
        }

    }

    public void FailWatchAdsButton()
    {
        Time.timeScale = 1;
        gameMenu.SetActive(true);

        // rewarded ads 
        HMSAdsKitManager.Instance.ShowRewardedAd();
        Time.timeScale = 1;
        FailAdsMenu.SetActive(false);
        TryAgainInitialMenu.SetActive(true);

        // bunu onrewarded da çaðýr !!!!!!!!!!!!!!!!!!!!!!!
        /*if(LevelManager.Instance.GetHealth() > 0)
        {
            FailAdsMenu.SetActive(false);
            FailMenu.SetActive(true);
            gameMenu.SetActive(true);
        }
        else
        {
            FailAdsMenu.SetActive(true);
            FailMenu.SetActive(false);
            gameMenu.SetActive(true);
        }*/
    }

    public void retryThisLevelButton()
    {
        int currentHealth = LevelManager.Instance.GetHealth();

        //retry button

        if (LevelManager.Instance.GetHealth() > 0)
        {
            TryAgainInitialMenu.SetActive(false);
            FailAdsMenu.SetActive(false);
            FailMenu.SetActive(true);
            gameMenu.SetActive(true);
            if(LevelManager.Instance.isPremium == true)
            {
                plusBtn.SetActive(true);
            }
            else
            {
                plusBtn.SetActive(false);
            }
        }
        else
        {
            TryAgainInitialMenu.SetActive(false);
            FailAdsMenu.SetActive(true);
            FailMenu.SetActive(false);
            gameMenu.SetActive(true);
        }
    }

    public void AfterReward()
    {
        int currentHealth = LevelManager.Instance.GetHealth();

        if (LevelManager.Instance.GetHealth() > 0)
        {
            FailAdsMenu.SetActive(false);
            FailMenu.SetActive(true);
            gameMenu.SetActive(true);
        }
        else
        {
            FailAdsMenu.SetActive(true);
            FailMenu.SetActive(false);
            gameMenu.SetActive(true);
        }
    }

    public void OnRewarded(Reward reward)
    {
        LevelManager.Instance.IncreaseHealth();
        DlcManager.Instance.mainHealth = LevelManager.Instance.GetHealth();
    }

    public void ActiveWin()
    {
        WinMenu.SetActive(true);
        gameMenu.SetActive(true);
    }

    public void WinButton()
    {
        Time.timeScale = 1;
        gameMenu.SetActive(true);
        WinMenu.SetActive(false);

        if(LevelManager.Instance.isOwned == false)
        {
            HMSAdsKitManager.Instance.ShowInterstitialAd();
        }
        

        float chapter = LevelManager.Instance.Getlevel();
        LevelManager.Instance.Setlevel(1);
        Debug.Log("$chapter");
        if (chapter % 5 == 4)
            SceneManager.LoadScene(5);
        if (chapter %5 == 3)
            SceneManager.LoadScene(4);
        if (chapter %5  == 2)
            SceneManager.LoadScene(3);
        if (chapter % 5 == 1)
            SceneManager.LoadScene(2);
        if (chapter %5 == 0)
            SceneManager.LoadScene(1);
    }

    public void GotoMainMenu()
    {
        Time.timeScale = 1;
        LevelManager.Instance.SetAllStart();
        SceneManager.LoadScene(0);
        
        // burada level sifirlamasi yapilabilir.
    }

    public void RestartFromStart()
    {
        Time.timeScale = 1;
        LevelManager.Instance.SetLevelStart();
        SceneManager.LoadScene(1);

        // burada level sifirlamasi yapilabilir.
    }

    public void NoAdsButton()
    {
        HMSIAPManager.Instance.OnBuyProductSuccess = OnBuyProductSuccess;
        HMSIAPManager.Instance.PurchaseProduct(HMSIAPConstants.RemoveAds);
        Debug.Log(" Buy Button "); 
    }

    public void BuyHealthPotionButton()
    {
        HMSIAPManager.Instance.OnBuyProductSuccess = OnBuyProductSuccess;
        HMSIAPManager.Instance.PurchaseProduct(HMSIAPConstants.BuyHealth);
        Debug.Log(" Buy Health Button ");
    }

    public void BuyPremiumButton()
    {
        HMSIAPManager.Instance.OnBuyProductSuccess = OnBuyProductSuccess;
        HMSIAPManager.Instance.PurchaseProduct(HMSIAPConstants.BuyPremium);
        Debug.Log(" Buy Premium Button ");
    }

    private void OnBuyProductSuccess(PurchaseResultInfo result)
    {
        if(result.InAppPurchaseData.ProductId == "RemoveAds")
        {
            Debug.Log(" OnBuyAdProductSuccess ");
            Debug.Log(" OnBuy RemoveAds CubeHunter ");
            LevelManager.Instance.isOwned = true;
            DlcManager.Instance.mainIsOwned = true;
            PlayerPrefs.SetInt("own", LevelManager.Instance.isOwned ? 1 : 0);
            HMSAdsKitManager.Instance.HideBannerAd();
            DlcManager.Instance.mainHealth = LevelManager.Instance.GetHealth();
            if (DlcManager.Instance.mainStatus != "Premium" && LevelManager.Instance.status != "Premium")
            {
                DlcManager.Instance.mainStatus = "Pro";
                LevelManager.Instance.status = "Pro";
            }
            NativeAdsMenu.SetActive(false);

        }

        else if (result.InAppPurchaseData.ProductId == "BuyHealth")
        {
            Debug.Log(" OnBuyHealthProductSuccess ");
            Debug.Log(" OnBuy BuyHealth CubeHunter ");
            //LevelManager.Instance.health += 1;
            LevelManager.Instance.SetHealthPlus(1);
            //PlayerPrefs.SetInt("health", LevelManager.Instance.health);
            DlcManager.Instance.mainHealth = LevelManager.Instance.GetHealth();
        }

        else if (result.InAppPurchaseData.ProductId == "BuyPremium")
        {
            Debug.Log(" OnBuyPremimuProductSuccess ");
            Debug.Log(" OnBuy buyPremium CubeHunter ");
            LevelManager.Instance.isOwned = true;
            DlcManager.Instance.mainIsOwned = true;
            PlayerPrefs.SetInt("own", LevelManager.Instance.isOwned ? 1 : 0);
            LevelManager.Instance.isPremium = true;
            DlcManager.Instance.mainIsPremium = true;
            LevelManager.Instance.SetHealth(99);
            plusBtn.SetActive(true);
            //LevelManager.Instance.SetHealthPlus(50);
            //PlayerPrefs.SetInt("health", LevelManager.Instance.health);
            DlcManager.Instance.mainHealth = LevelManager.Instance.GetHealth();
            DlcManager.Instance.mainStatus = "Premium";
            LevelManager.Instance.status = "Premium";
            HMSAdsKitManager.Instance.HideBannerAd();
            NativeAdsMenu.SetActive(false);
        }
    }

    public void OpenSubscriptionEditingScreenButton()
    {
        HMSIAPManager.Instance.RedirectingtoSubscriptionEditingScreen("BuyPremium");
    }

    public void OpenSubscriptionManagementScreenButton()
    {
        HMSIAPManager.Instance.RedirectingtoSubscriptionManagementScreen();
    }

    private void RestoreProducts()
    {

        HMSIAPManager.Instance.RestorePurchaseRecords((restoredProducts) =>
        {
            foreach (var item in restoredProducts.InAppPurchaseDataList)
            {
                if ((IAPProductType)item.Kind == IAPProductType.Consumable)
                {
                    Debug.Log($"Consumable: ProductId {item.ProductId} , SubValid {item.SubValid} , PurchaseToken {item.PurchaseToken} , OrderID  {item.OrderID}");
                    consumablePurchaseRecord.Add(item);
                }
                if (item.ProductId == "RemoveAds")
                {
                    Debug.Log(" OnBuy buyRemoveAds1 ");
                    LevelManager.Instance.isOwned = true;
                    PlayerPrefs.SetInt("own", LevelManager.Instance.isOwned ? 1 : 0);
                    DlcManager.Instance.mainIsOwned = true;
                    if (DlcManager.Instance.mainStatus != "Premium" && LevelManager.Instance.status != "Premium")
                    {
                        DlcManager.Instance.mainStatus = "Pro";
                        LevelManager.Instance.status = "Pro";
                    }
                }
            }
        });

        HMSIAPManager.Instance.RestoreOwnedPurchases((restoredProducts) =>
        {
            foreach (var item in restoredProducts.InAppPurchaseDataList)
            {
                if ((IAPProductType)item.Kind == IAPProductType.Subscription)
                {
                    Debug.Log($"Subscription: ProductId {item.ProductId} , ExpirationDate {item.ExpirationDate} , AutoRenewing {item.AutoRenewing} , PurchaseToken {item.PurchaseToken} , OrderID {item.OrderID}");
                    activeSubscriptions.Add(item);
                    if (item.ProductId == "BuyPremium")
                    {
                        LevelManager.Instance.isOwned = true;
                        PlayerPrefs.SetInt("own", LevelManager.Instance.isOwned ? 1 : 0);
                        DlcManager.Instance.mainIsOwned = true;
                        LevelManager.Instance.isPremium = true;
                        DlcManager.Instance.mainIsPremium = true;
                        LevelManager.Instance.SetHealth(99);
                        DlcManager.Instance.mainStatus = "Premium";
                        LevelManager.Instance.status = "Premium";
                        Debug.Log(" OnBuy buyPremium aaaa ");
                    }

                    if (item.ProductId == "RemoveAds")
                    {
                        Debug.Log(" OnBuy buyRemoveAds2 ");
                        LevelManager.Instance.isOwned = true;
                        DlcManager.Instance.mainIsOwned = true;
                        PlayerPrefs.SetInt("own", LevelManager.Instance.isOwned ? 1 : 0);
                        if (DlcManager.Instance.mainStatus != "Premium" && LevelManager.Instance.status != "Premium")
                        {
                            DlcManager.Instance.mainStatus = "Pro";
                            LevelManager.Instance.status = "Pro";
                        }
                    }

                }

                if ((IAPProductType)item.Kind == IAPProductType.NonConsumable)
                {
                    Debug.Log($"NonConsumable: ProductId {item.ProductId} , DaysLasted {item.DaysLasted} , SubValid {item.SubValid} , PurchaseToken {item.PurchaseToken} ,OrderID {item.OrderID}");
                    activeNonConsumables.Add(item);
                    if (item.ProductId == "RemoveAds")
                    {
                        Debug.Log(" OnBuy buyRemoveAds3 ");
                        LevelManager.Instance.isOwned = true;
                        DlcManager.Instance.mainIsOwned = true;
                        PlayerPrefs.SetInt("own", LevelManager.Instance.isOwned ? 1 : 0);
                        if (DlcManager.Instance.mainStatus != "Premium" && LevelManager.Instance.status != "Premium")
                        {
                            DlcManager.Instance.mainStatus = "Pro";
                            LevelManager.Instance.status = "Pro";
                        }
                    }
                }
                if(item.ProductId == "RemoveAds")
                {
                    Debug.Log(" OnBuy buyRemoveAds4 ");
                    LevelManager.Instance.isOwned = true;
                    DlcManager.Instance.mainIsOwned = true;
                    PlayerPrefs.SetInt("own", LevelManager.Instance.isOwned ? 1 : 0);
                    if (DlcManager.Instance.mainStatus != "Premium" && LevelManager.Instance.status != "Premium")
                    {
                        DlcManager.Instance.mainStatus = "Pro";
                        LevelManager.Instance.status = "Pro";
                    }
                }
            }
        });

    }
}
