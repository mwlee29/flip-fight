using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayUIController : MonoBehaviour
{
    /* 
        I'm currently in the process of moving most of the code from the LevelController script to here
        so that LevelController will only have to manage player and game states
    */
    private int winningPlayerIndex;

    private PlayerConfiguration[] playerConfigs;

    private TextMeshProUGUI winningPlayerText;
    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private Button yesButton;
    [SerializeField]
    private Button noButton;

    private void Start()
    {
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
    }

    private void OnEnable()
    {
        winningPlayerText.SetText("Player " + (winningPlayerIndex + 1) + " Wins!!!!");
        gameOverPanel.SetActive(true);
        StartCoroutine(EndGame());
    }

    public void SetWinningPlayerIndex(int index)
    {
        winningPlayerIndex = index;
    }

    public void PlayAgain()
    {
        foreach (PlayerConfiguration p in playerConfigs)
        {
            p.PlayerScore = 0;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LeaveGame()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(2f);
        yesButton.interactable = true;
        noButton.interactable = true;
        yesButton.Select();
    }
}
