using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SessonPassManager : MonoBehaviour
{
    #region Variables
    private int i_passMaxLevel = 50, i_passCurrentLevel = 0;
    private float f_currentLevelBarFillAmount;

    [Header("Scene UI variables")]
    [SerializeField] Image img_fillBar;
    #endregion

    #region Unity Life Cycle
    private void Start()
    {
        UpdatelvlBar();
    }
    #endregion

    #region Methods
    private void UpdatelvlBar()
    {
        img_fillBar.fillAmount = GetPassLevelNormalized();
    }
    public float GetPassLevelNormalized() // To use Image fill component instead of Sliders
    {
        return (float)i_passCurrentLevel / i_passMaxLevel;
    }
    public void IncreaseOneLevel()
    {
        // Increase Level
        i_passCurrentLevel++;
        // Show level increase
        StartCoroutine(IncreaseLevel(GetPassLevelNormalized()));
    }
    public void IncreaseSeveralLevels(int levelsIncreased)
    {
        // Increase Level
        i_passCurrentLevel += levelsIncreased;
        // Show level increase
        StartCoroutine(IncreaseLevel(GetPassLevelNormalized()));
    }
    IEnumerator IncreaseLevel(float f_newLevelBarFillAmount)
    {
        float f_duration = 1f;
        var rate = Mathf.Abs(f_newLevelBarFillAmount - f_currentLevelBarFillAmount) / f_duration;
        while (f_currentLevelBarFillAmount != f_newLevelBarFillAmount)
        {
            f_currentLevelBarFillAmount = Mathf.MoveTowards(f_currentLevelBarFillAmount, f_newLevelBarFillAmount, rate * Time.deltaTime);
            img_fillBar.fillAmount = f_currentLevelBarFillAmount;
            yield return null;
        }
    }
    #endregion
}



