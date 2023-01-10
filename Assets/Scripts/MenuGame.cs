using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuGame : MonoBehaviour
{

    public TextMeshProUGUI textScore;
    public TextMeshProUGUI textBonus;
    public GameObject player;

    void Start()
    {
        textScore.text = "Wynik: " + GameManager.Instance.GetScore();
        StartCoroutine(MainCoroutine());
    }

    IEnumerator MainCoroutine()
    {
        while (true)
        {
            if (GameManager.Instance.GetActiveBonus() != "")
            {
                textBonus.text = "Bonus: " + GameManager.Instance.GetActiveBonus();
            }
            yield return new WaitForSeconds(1);
        }
    }

    public void NextGame()
    {
        GameManager.Instance.PlayGame();
    }

    public void EndGame()
    {
        Application.Quit();
    }

    private void ResetPositionPlayer()
    {
        player.GetComponent<Player>().ResetPosition();
    }

}
