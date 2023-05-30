using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LevelShower : MonoBehaviour
{
    private TextMeshProUGUI pointTxt;
    private TextMeshProUGUI levelTxt;

    [SerializeField] private TextMeshProUGUI LevelValue;

    [SerializeField] private TextMeshProUGUI healthTxt;
    [SerializeField] private TextMeshProUGUI Status;



    //private TextMeshProUGUI LocScore;




    private void Awake()
    {
        //LocScore = GameObject.Find("LocScore").GetComponent<TextMeshProUGUI>();
        levelTxt = GameObject.Find("levelTxt").GetComponent<TextMeshProUGUI>();
        pointTxt = GameObject.Find("Point").GetComponent<TextMeshProUGUI>();
       



    }
    void Start()
    {

        //chapter++;


    }

    // Update is called once per frame
    void Update()
    {
        int pointNo = LevelManager.Instance.GetPoint();
        pointTxt.text = pointNo.ToString();

        float chapter = LevelManager.Instance.Getlevel();
        
        LevelValue.text = chapter.ToString();
        //levelTxt.text = chapter.ToString();

        int health = LevelManager.Instance.GetHealth();
        healthTxt.text = health.ToString();

        string status = LevelManager.Instance.status;
        Status.text = status;


        //int LocPointScore = LevelManager.Instance.GetLocPoint();
        //LocScore.text = LocPointScore.ToString();
    }
}
