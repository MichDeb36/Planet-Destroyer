using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float bonusEnergyDrink = 2.0f;
    public float bonusMagnesia = 1.2f;
    public float speedCriterion = 0.5f;

    private int score = 0;
    private string activeBonus = "";
    private int actualyLevel = 0; 

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayGame()
    {
        actualyLevel++;
        if (actualyLevel == 1)
        {
            score = 0;
            SceneManager.LoadScene("PlayGame_level1");
        }
        else if (actualyLevel == 2)
            SceneManager.LoadScene("PlayGame_level2");
        else if (actualyLevel == 3)
            SceneManager.LoadScene("PlayGame_level3");
        else if (actualyLevel == 4)
            SceneManager.LoadScene("PlayGame_level4");
        else if (actualyLevel == 5)
            SceneManager.LoadScene("PlayGame_level5");
        else
            GoHome();
    }
    public void GoHome()
    {
        actualyLevel = 0;
        SceneManager.LoadScene("Home");
    }
    public void SetScore(int points)
    {
        score += points;
    }

    public int GetScore()
    {
        return score;
    }
    public void SetActiveBonus(string bonus)
    {
        activeBonus = bonus;
    }
    public string GetActiveBonus()
    {
        return activeBonus;
    }
    public float GetBonusEnergyDrink()
    {
        return bonusEnergyDrink;
    }
    public float GetBonusMagnesia()
    {
        return bonusMagnesia;
    }
    public float GetSpeedCriterion()
    {
        return speedCriterion;
    }

}
