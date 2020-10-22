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
    /* 
        I'm currently in the process of moving most of the code from the LevelController script to
        GameplayUIController so that LevelController will only have to manage player and game states
    */
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

    private PlayerConfiguration[] playerConfigs;

    void Start()
    {
        playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            var player = Instantiate(playerPrefab, playerSpawns[i].position, playerSpawns[i].rotation, gameObject.transform);
            player.GetComponent<Player>().InitializePlayer(playerConfigs[i]);
            playerScoreText[i].SetText(playerConfigs[i].PlayerScore.ToString());
        }

    }

    //Update Scores, reload the level
    public void ResetLevel(PlayerConfiguration player)
    {
        player.PlayerScore += 1;

        if (player.PlayerScore >= MaxScore)
            EndGame(player.PlayerIndex);
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void EndGame(int playerIndex)
    {
        winningPlayerText.SetText("Player " + (playerIndex + 1) + " Wins!!!!");
        gameOverPanel.SetActive(true);
    }

    public void LeaveGame()
    {
        Debug.Log("LeaveGame works");
        //Destroy(FindObjectOfType<PlayerConfigurationManager>());
        SceneManager.LoadSceneAsync(0);
        Debug.Log("Error loading scene??");
    }
}
