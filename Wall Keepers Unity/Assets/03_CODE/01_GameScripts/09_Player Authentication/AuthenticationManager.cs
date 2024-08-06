using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using Unity.Services.Authentication.PlayerAccounts;
using System;
using TMPro;
using UnityEngine;

public class AuthenticationManager : MonoBehaviour
{
    [SerializeField] GameObject go_loginPanel;
    [SerializeField] GameObject go_startButton;
    public TextMeshProUGUI tmp_playerID;
    private async void Start()
    {
        if (PlayerAccountService.Instance.IsSignedIn)
        {
            print("Player is Sign in");
            // Hide Log in Objects
            go_loginPanel.SetActive(false);
            go_startButton.SetActive(true);
        }
        else
        {
            if (AuthenticationService.Instance.PlayerId != null)
            {
                // Hide Log in Objects
                print("Player is Logged In as Guest");
                go_loginPanel.SetActive(false);
                go_startButton.SetActive(true);
                print(AuthenticationService.Instance.PlayerId);
            }
            else
            {
                // Show Log in objects
                print("Player is NOT sign in");
                go_startButton.SetActive(false);
                go_loginPanel.SetActive(true);
            }
        }
        // Update player ID
        tmp_playerID.text = AuthenticationService.Instance.PlayerId;
    }

    public void HideLogInPanel()
    {
        go_loginPanel.SetActive(false);
    }
    public void ShowLogInPanel()
    {
        go_loginPanel.SetActive(true);
    }
}