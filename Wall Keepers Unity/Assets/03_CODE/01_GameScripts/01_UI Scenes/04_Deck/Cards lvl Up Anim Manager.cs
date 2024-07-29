using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardslvlUpAnimManager : MonoBehaviour
{
    [SerializeField] ImproveCardManager script_improveCardManager;

    public void StopSpellLevelUpAnim()
    {
        script_improveCardManager.StopLevelUpAnim();
    }


}
