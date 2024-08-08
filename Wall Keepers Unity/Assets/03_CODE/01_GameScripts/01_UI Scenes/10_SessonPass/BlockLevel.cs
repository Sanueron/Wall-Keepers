using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockLevel : MonoBehaviour
{
    public enum state { Disable, Enable, Reclamed };
    public state blockstate;

    public void ToEnable()
    {
        blockstate = state.Enable;
        gameObject.GetComponent<Image>().color = Color.white;
    }

    public void Reclamed()
    {
        if(blockstate == state.Enable)
        {
            blockstate = state.Reclamed;
            gameObject.GetComponent<Image>().color = Color.green;
        }
        
    }

}
