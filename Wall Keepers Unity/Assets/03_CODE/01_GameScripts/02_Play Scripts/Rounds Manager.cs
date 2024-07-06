using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RoundsManager : MonoBehaviour
{
    [SerializeField] private GameObject roundTextUI_GameObject;
    public TMP_Text roundText;
    public TMP_Text pause_roundText;
    public int i_round;
    public void UpdateRoundText(int currentRound)
    {
        if (roundText != null)
        {
            i_round = currentRound;
            pause_roundText.text = "Round: " + currentRound;
            Debug.Log(i_round);
            if (currentRound > 1)
            {
                roundText.text = "Round: " + currentRound;
            }
            else
            {
                if (roundText.gameObject.tag != "StartGamePause")
                {
                    roundText.text = "Start Game";

                }
                else
                {
                    roundText.text = " ";
                }

            }
        }

        roundTextUI_GameObject.SetActive(true);
        StartCoroutine(DisableRoundText());

    }

    public IEnumerator DisableRoundText()
    {
        yield return new WaitForSeconds(2.1f);
        roundTextUI_GameObject.SetActive(false);

    }
}
