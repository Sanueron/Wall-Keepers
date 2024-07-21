using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialsManager : MonoBehaviour
{
    #region Tutorial Control variables
    public int i_tutorialIndex;
    private enum TutorialType { Welcome, HowToPlay, Deck};
    [Header("Control Variables")]
    [SerializeField] private TutorialType e_tutorialType;
    private int i_currentStep = 0;
    #endregion

    #region Tutorials Main Components
    [Header("Animator Components")]
    [SerializeField] Animator animator_Tutorial;
    private int animatorIntVariable = Animator.StringToHash("Step");
    [Header("Audio Components")]
    [SerializeField] AudioSource aS_tutorial;
    [SerializeField] List<AudioClip> listOf_aC_forTutorial = new List<AudioClip>();
    [SerializeField] AudioClip aC_tutorialButton;
    [Header("Text Components")]
    [SerializeField] TextMeshProUGUI tmp_displayedTextInChatBox;
    #endregion

    #region Unity Life Cycle
    private void Start()
    {
        switch (e_tutorialType)
        {
            case TutorialType.Welcome:
                if (PlayerData.b_tutorialComplete[i_tutorialIndex]) // Tutorial complete
                {
                    gameObject.SetActive(false);
                }
                else // Tutorial not complete
                {
                    AudioManager.instance.StopMusicPlaying();
                }
                break;

            case TutorialType.HowToPlay:
                AudioManager.instance.StopMusicPlaying();
                break;
        }
    }
    #endregion

    #region Methods to Move to next step
    internal void UpdateCurrentStep()
    {
        i_currentStep++;
    }
    public void PlayNextStepAnim()
    {
        animator_Tutorial.SetInteger(animatorIntVariable, i_currentStep);
    }
    #endregion

    #region Change Text Methods
    internal void UpdateText(string s_newText)
    {
        tmp_displayedTextInChatBox.text = s_newText;
    }
    #endregion

    #region Audio Play Methods
    internal void PlayAudioClipOfCurrentStep()
    {
        aS_tutorial.Stop();
        aS_tutorial.clip = listOf_aC_forTutorial[i_currentStep];
        aS_tutorial.Play();
    }
    public void PlayTutorialButtonSFX()
    {
        aS_tutorial.Stop();
        aS_tutorial.clip = aC_tutorialButton;
        aS_tutorial.Play();
    }
    #endregion

    #region Tutorial Complete Methods
    internal void CompleteTutorial()
    {
        PlayerData.b_tutorialComplete[i_tutorialIndex] = true;
        print("Complete tutorial " + PlayerData.b_tutorialComplete[i_tutorialIndex]);
        SaveData.instance.AssignDataAndSave();
    }
    internal void WelcomeTutorialRewards()
    {
        PlayerData.b_spellCardObtained[1] = true; // Fire ball
        PlayerData.b_spellCardObtained[4] = true; // Fire Wall 
        PlayerData.b_spellCardObtained[2] = true; // Rock ball
        PlayerData.b_spellCardObtained[3] = true; // Water ball
        PlayerData.b_spellCardObtained[7] = true; // Wind Arrow
        PlayerData.b_spellCardObtained[5] = true; // Rock Wall
        PlayerData.b_spellCardObtained[6] = true; // Tornado
        PlayerData.b_spellCardObtained[9] = true; // Ignite
    }
    #endregion
}
