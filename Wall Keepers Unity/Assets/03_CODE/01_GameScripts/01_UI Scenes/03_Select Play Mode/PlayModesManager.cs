using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayModesManager : MonoBehaviour
{
    [SerializeField] GameObject go_PlayModesViewPort;
    [SerializeField] GameObject go_EndlessWavesViewPort;
    [SerializeField] GameObject go_EndlessWavesBanner;
    private void Awake()
    {
        go_EndlessWavesViewPort.SetActive(false);
        go_EndlessWavesBanner.SetActive(false);
    }
    public void PlayEndlessWavesMode()
    {
        Disable_PlayModesViewPort();
        go_EndlessWavesViewPort.SetActive(true);
       

    }
    internal void ActiveEndlessWavesBanner()
    {
        go_EndlessWavesBanner.SetActive(true);

    }
    private void Disable_PlayModesViewPort()
    {
        go_PlayModesViewPort.SetActive(false);
    }


}
