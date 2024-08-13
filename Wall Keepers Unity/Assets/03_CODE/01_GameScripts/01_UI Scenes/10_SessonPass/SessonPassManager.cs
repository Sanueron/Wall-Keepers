using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SessonPassManager : MonoBehaviour
{
    #region Variables
    [SerializeField] private List<GameObject> go_lvlBlocks;
    public float f_lvl = 0;// This level is provisional, the real one will be on PlayerData
    [SerializeField] private Slider slider_lvl;
    [SerializeField] private GameObject go_rewardPanel;
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
        print(GetBarValue());
        slider_lvl.value = GetBarValue();
        RewardSystem();
    }
    private float GetBarValue()
    {
        float fillBarValue = f_lvl / 10;
        if (f_lvl >= 10)
        {
            fillBarValue = 1f;
        }
        return fillBarValue;
    }
    private void RewardSystem()
    {
        switch (f_lvl) 
        {
            case <2:
                go_lvlBlocks[0].gameObject.GetComponent<BlockLevel>().ToEnable();
            break;
            case <3:
                for (int i = 0; i < 2; i++)
                {
                    go_lvlBlocks[i].gameObject.GetComponent<BlockLevel>().ToEnable();
                }
                break;
            case <4:
                for (int i = 0; i < 3; i++)
                {
                    go_lvlBlocks[i].gameObject.GetComponent<BlockLevel>().ToEnable();
                }
                break;
            case <5:
                for (int i = 0; i < 4; i++)
                {
                    go_lvlBlocks[i].gameObject.GetComponent<BlockLevel>().ToEnable();
                }
                break;
            case <6:
                for (int i = 0; i < 5; i++)
                {
                    go_lvlBlocks[i].gameObject.GetComponent<BlockLevel>().ToEnable();
                }
                break;
            case <7:
                for (int i = 0; i < 6; i++)
                {
                    go_lvlBlocks[i].gameObject.GetComponent<BlockLevel>().ToEnable();
                }
                break;
            case <8:
                for (int i = 0; i < 7; i++)
                {
                    go_lvlBlocks[i].gameObject.GetComponent<BlockLevel>().ToEnable();
                }
                break;
            case <9:
                for (int i = 0; i < 8; i++)
                {
                    go_lvlBlocks[i].gameObject.GetComponent<BlockLevel>().ToEnable();
                }
                break;
            case <10:
                for (int i = 0; i < 9; i++)
                {
                    go_lvlBlocks[i].gameObject.GetComponent<BlockLevel>().ToEnable();
                }
                break;
            case < 11:
                for (int i = 0; i < 10; i++)
                {
                    go_lvlBlocks[i].gameObject.GetComponent<BlockLevel>().ToEnable();
                }
                break;

        }
    }
    public void CloseRewardPanel()
    {
        go_rewardPanel.SetActive(false);
    }
  
    
    #endregion

}



