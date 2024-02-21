using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject gamerOver;
    private Player player;

    private int totalScore;
    public float score;
    public TextMeshProUGUI textMeshScore;
    public TextMeshProUGUI textMeshCoin;
    public int coinScore = 1;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        totalScore = PlayerPrefs.GetInt("score");

        Debug.Log(PlayerPrefs.GetInt("score"));
    }

    private void Update()
    {
        if (!player.isDead)
        {
            score += Time.deltaTime * 10f;
            textMeshScore.text = Mathf.Round(score).ToString() + "M";
        } 
    }


    public void AddCoin()
    {
        coinScore++;
        textMeshCoin.text = coinScore.ToString();
        totalScore++;
        PlayerPrefs.SetInt("score", totalScore);
    }
      


    public void ShowGameOver()
    {
        gamerOver.SetActive(true);
    }

    public void HideGameOver()
    {
        gamerOver.SetActive(false);
    }
}
