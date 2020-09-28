using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngineInternal;

public class LevelController : MonoBehaviour
{
    [SerializeField]
    private int MaxScore = 7;

    [SerializeField]
    private Transform[] playerSpawns;
    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private TextMeshProUGUI[] playerScoreText;
    [SerializeField]
    private TextMeshProUGUI winningPlayerText;
    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private Button yesButton;
    [SerializeField]
    private Button noButton;

    private PlayerConfiguration[] playerConfigs;

    void Start()
    {
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            var player = Instantiate(playerPrefab, playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
            StartCoroutine(StartGame(i, player));
            player.GetComponent<Player>().InitializePlayer(playerConfigs[i]);
            playerScoreText[i].SetText(playerConfigs[i].PlayerScore.ToString());
        }

    }

    public void ResetLevel(int playerIndex)
    {
        var player = playerConfigs[playerIndex];
        player.PlayerScore += 1;

        if (player.PlayerScore >= MaxScore)
            EndGame(playerIndex);
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }


    private void EndGame(int playerIndex)
    {
        winningPlayerText.SetText("Player " + (playerIndex + 1) + " Wins!!!!");
        gameOverPanel.SetActive(true);
        StartCoroutine(EndGame());
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
        Debug.Log("LeaveGame works");
        //Destroy(FindObjectOfType<PlayerConfigurationManager>());
        SceneManager.LoadSceneAsync(0);
        Debug.Log("Error loading scene??");
    }

    IEnumerator StartGame(int i, GameObject player)
    {
        yield return new WaitForSeconds(1.5f);
        player.GetComponent<PlayerInputHandler>().InitializePlayer(playerConfigs[i]);
    }

    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(3f);
        yesButton.interactable = true;
        noButton.interactable = true;
        yesButton.Select();
    }
}
