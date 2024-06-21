using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    #region Variables
    public static ManaManager instance { get; private set; } // Can be read by all scripts but only modified by this
    [SerializeField] private int i_currentManaValue;
    private int i_MaxManaValue = 10;
    [SerializeField] private Image img_manaBarFront;
    [SerializeField] private Image img_manaBarBack;
    [SerializeField] private float f_manaIncreaseSpeed;
    [SerializeField] private TextMeshProUGUI manaTMP;
    #endregion
    #region Unity Life Cycle

    void Start()
    {
        InitializeMana();
        InvokeRepeating("IncreaseMana", 0f, f_manaIncreaseSpeed);
    }

    private void Awake()
    {
        // Implement Singleton Pattern
        if (instance != null) // Error check to avoid more than one Instance of this script to be created
        {
            Debug.LogError("There is more than one ManaManager! " + transform + " - " + instance);
            Destroy(gameObject);
            return;
        }
        instance = this; // Equal the instance to this script
    }

    #endregion
    #region Mana Management Methods
    //Initialize Mana Value to MAX
    private void InitializeMana()
    {
        i_currentManaValue = i_MaxManaValue;
    }
    //Increase Mana Method (InvokeRepeating on start)
    private void IncreaseMana()
    {
        if (i_currentManaValue < i_MaxManaValue)
        {
            i_currentManaValue += 1;
            UpdateManaBar();
        }

    }
    //Method called by ActionsManager when Player cast a card 
    public void ReduceCurrentMana(int amount)
    {
        i_currentManaValue -= amount;
        UpdateManaBar();
    }

    //Get ManaValue normalized 
    private float GetManaBarFillValue()
    {
        float fillBarValue = (float)i_currentManaValue / i_MaxManaValue;
        if (i_currentManaValue >= i_MaxManaValue)
        {
            fillBarValue = 1f;
        }
        return fillBarValue;
    }

    //Update ManaBar
    private void UpdateManaBar()
    {
        StartCoroutine(LerpLifeBar());
        manaTMP.text = i_currentManaValue.ToString();

    }
    //Smoothly ManaBar Increase & Decrease 
    private IEnumerator LerpLifeBar()
    {
        float elapsedTime = 0;
        while (elapsedTime < f_manaIncreaseSpeed)
        {
            img_manaBarBack.fillAmount = Mathf.Lerp(img_manaBarBack.fillAmount, GetManaBarFillValue(), elapsedTime / f_manaIncreaseSpeed);
            if (img_manaBarBack.fillAmount < 1)
            {
                img_manaBarFront.fillAmount = Mathf.Lerp(img_manaBarFront.fillAmount, GetManaBarFillValue() - 0.1f, elapsedTime / f_manaIncreaseSpeed);

            }
            else
            {
                print("nopasa");
                img_manaBarFront.fillAmount = 1;
            }
            
            elapsedTime += Time.deltaTime;
            yield return null;
        }

    }
    public int GetCurrentMana()
    {
        return i_currentManaValue;
    }

    #endregion
}
