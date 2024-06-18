using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class WallManager : MonoBehaviour
{
    [Header("Wall variables")]
    [SerializeField] internal Image img_wallLifeBar;

   
    float f_wallLife;
    float f_MaxwallLife = 1000f;

    public static WallManager instance { get; private set; } // Can be read by all scripts but only modified by this

    public static event EventHandler e_OnWallDestroyed;

    private void Awake()
    {
        // Implement Singleton Pattern
        if (instance != null) // Error check to avoid more than one Instance of this script to be created
        {
            Debug.LogError("There is more than one WallManager! " + transform + " - " + instance);
            Destroy(gameObject);
            return;
        }
        instance = this; // Equal the instance to this script

    }

    private void Start()
    {
        f_wallLife = f_MaxwallLife;
    }
    //Method accessed by the enemy to deal damage to the wall
    public void ReceiveHit( float EnemyDmg)
    {

        f_wallLife -= EnemyDmg;
       img_wallLifeBar.fillAmount = GetHpFillBarValue();

        if (f_wallLife <= 0)
        {
        
            e_OnWallDestroyed?.Invoke(this, EventArgs.Empty);
        }
    }
   

    private float GetHpFillBarValue()
    {
        float fillBarValue = (float)f_wallLife / f_MaxwallLife;
        if (f_wallLife >= f_MaxwallLife)
        {
            fillBarValue = 1f;
        }
        return fillBarValue;
    }

    

}
