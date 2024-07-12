using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayModesManager : MonoBehaviour
{
    #region Local Variables
    [SerializeField] Animator animator_endlessWaves;
    int i_animatorBool_Show = Animator.StringToHash("Show");
    [SerializeField] private TMP_Text tmp_wavesRecord;
    #endregion

    #region Unity Life Cycle
    private void Start()
    {
        GetWavesRecord();
    }
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

    public void GetWavesRecord()
    {
        tmp_wavesRecord.text = PlayerData.i_maxRound.ToString();
    }
    #endregion

}
