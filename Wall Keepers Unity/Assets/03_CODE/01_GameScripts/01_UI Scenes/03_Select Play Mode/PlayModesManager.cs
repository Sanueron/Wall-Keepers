using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayModesManager : MonoBehaviour
{
    #region Local Variables
    [SerializeField] Animator animator_endlessWaves;
    int i_animatorBool_Show = Animator.StringToHash("Show");
    #endregion

    #region UNity Life Cycle

    #endregion

    #region Endless Waves Methods
    public void PlayEndlessWavesMode()
    {
        animator_endlessWaves.SetBool(i_animatorBool_Show, true);
    }
    public void CloseEndlessWavesPlayMode()
    {
        animator_endlessWaves.SetBool(i_animatorBool_Show, false);
    }
    #endregion

}
