using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader : MonoBehaviour
{
    #region Local Variables
    Animator scenesLoaderAnimator;
    int i_animatorCTRLVariable = Animator.StringToHash("start");
    int sceneToLoadIndex = 1;
    #endregion

    #region Unity Life Cycle
    private void Start()
    {
        scenesLoaderAnimator = GetComponent<Animator>();
    }
    #endregion

    public void SceneTransitionAnimation()
    {
        scenesLoaderAnimator.SetTrigger(i_animatorCTRLVariable);
    }
    public void LoadIndicatedScene()
    {
        SceneManager.LoadScene(sceneToLoadIndex, LoadSceneMode.Single);
    }
    public void SetNextSceneToLoad(int i_nextScene)
    {
        sceneToLoadIndex = i_nextScene;
    }

    public void CheckDeckBeforeLoadScene()
    {
        if(PlayerData.b_isDeckFull)
        {
            LoadIndicatedScene();
        }
        else
        {
            ContextualMenuManager.instance.ShowDeckNotFull();
        }
    }
}
