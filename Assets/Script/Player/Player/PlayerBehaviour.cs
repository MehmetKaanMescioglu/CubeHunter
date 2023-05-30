using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private Animator animatorOfPlayer;

    public PlayerMoverRunner playerMoverRunner;

    

    private void Awake()
    {
        Singleton();
    }

    #region Singleton

    public static PlayerBehaviour Instance;

    public void Singleton()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }

        Instance = this;
    }
    #endregion

    public void VictoryAnimation()
    {
        animatorOfPlayer.SetTrigger("Victory");
    }

    public void FailAnimation()
    {
        
        animatorOfPlayer.SetTrigger("Fail");
    }

    public void StopPlayer()
    {
        playerMoverRunner.Velocity = 0;
        
    }

    public void FastPlayer()
    {
        playerMoverRunner.Velocity = 3;

    }

    public void SlowPlayer()
    {
        playerMoverRunner.Velocity = 1;

    }

    public void NormalPlayer()
    {
        playerMoverRunner.Velocity = 2;

    }





}
