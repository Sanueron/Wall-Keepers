using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BlockLevel : MonoBehaviour
{
    #region Variables
    private enum state {Disable, Enable, Reclamed};
    private state blockstate;
    private enum reward { Key,Keyx5,ManyKeys, Mana, EarthRune, FireRune, LightingRune, WaterRune, WindRune};
    [SerializeField] private reward enum_rewardItem;
    [SerializeField] int i_itemsAmount = 19;
    [Header("Reward panel")]
    [SerializeField] GameObject go_rewardPanel;
    [SerializeField] Image imageRewardPanel;
    [SerializeField] TextMeshProUGUI tmp_itemsAmout;
    [SerializeField] List<Sprite> list_rewardImages;  
    #endregion
    #region Methods
    public void ToEnable()
    {
        blockstate = state.Enable;
        gameObject.GetComponent<Image>().color = Color.white;
    }
    public void GetReward()
    {
        if (blockstate == state.Enable)
        {
            print("RewardReclamed");
            blockstate = state.Reclamed;
            gameObject.GetComponent<Image>().color = Color.green;
            switch (enum_rewardItem)//Set Sprite 
            {
                case reward.Key:
                    imageRewardPanel.sprite = list_rewardImages[0];
                break;
                case reward.Keyx5:
                    imageRewardPanel.sprite = list_rewardImages[1];
                break;
                case reward.ManyKeys:
                    imageRewardPanel.sprite = list_rewardImages[2];
                break;
                case reward.Mana:
                    imageRewardPanel.sprite = list_rewardImages[3];
                break;
                case reward.EarthRune:
                   imageRewardPanel.sprite = list_rewardImages[4];
                break;
                case reward.FireRune:
                    imageRewardPanel.sprite = list_rewardImages[5];
                break;
                case reward.LightingRune:
                    imageRewardPanel.sprite = list_rewardImages[6];
                break;
                case reward.WaterRune:
                   imageRewardPanel.sprite = list_rewardImages[7];
                break;
                case reward.WindRune:
                    imageRewardPanel.sprite = list_rewardImages[8];
                break;
            }
            tmp_itemsAmout.text = i_itemsAmount.ToString();//Set amount text
            print(i_itemsAmount);    
            go_rewardPanel.gameObject.SetActive(true);//Activate reward panel

        }  
    }
    public void OpenRewardPanel()
    {
        go_rewardPanel.SetActive(true);
    }
    public void CloseRewardPanel()
    {
        go_rewardPanel.SetActive(false);
    }

    #endregion
}
