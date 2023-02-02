using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Game : MonoBehaviour
{
    [SerializeField]  List<GameObject> CollectionsPlanets = new List<GameObject>();
    [SerializeField]  TextMeshProUGUI textTime;
    [SerializeField]  TextMeshProUGUI textScore;
    [SerializeField]  TextMeshProUGUI textNumberDestroyPlanets;
    [SerializeField]  TextMeshProUGUI textLimitPlanets;
    [SerializeField]  GameObject swordLeft;
    [SerializeField]  GameObject swordRight;
    [SerializeField]  GameObject player;
    [SerializeField]  AudioSource clockSound;

    [Header("Difficulty settings")]
    [SerializeField] int LimitPlanets = 100;
    [SerializeField] int MaxPlanets = 5;

    [Header("Planet creation settings")]
    [Range(0, 1)]
    [SerializeField] float widthRangeFromPlayer = 0.5f;
    [SerializeField] float maxDistanceFromPlayer = 1f;
    [SerializeField] float minDistanceFromPlayer = 0.5f;
    [SerializeField] float maxHeightCreatePlanet = 1f;
    [SerializeField] float minHeightCreatePlanet = 0f;
    [SerializeField] float maxForcePlanet = 0.8f;
    [SerializeField] float minForcePlanet = 0.4f;

    private List<GameObject> planets = new List<GameObject>();
    private int score = 0;
    private int numberDestroyPlanet = 0;
    private int numberDeactivePlanet = 0;
    private int numberActivePlanet = 0;
    private int planetIterator;


    void Start()
    {
        textLimitPlanets.text = "Liczba planet do zniszczenia: " + LimitPlanets;
        planetIterator = LimitPlanets;
        for (int i = 0; i < LimitPlanets; i++)
        {
            GameObject newPlanet = createNewObject();
            newPlanet.SetActive(false);
        }
        StartCoroutine(GameCoroutine());
    }

    IEnumerator GameCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        ResetPositionPlayer();

        for (int i = 5; i >= 0; i--)
        {
            textTime.text = "Gra rozpocznie siê za: " + i +"s";
            clockSound.Play();
            yield return new WaitForSeconds(1);
        }
        textTime.text = "";

        while (true)
        {
            if (numberActivePlanet  < MaxPlanets && planetIterator > 0)
            {
                planetIterator--;
                throwPlanet(planets[planetIterator]);  
            }
            RemovingDestroyedPlanets();
            if (numberDeactivePlanet >= LimitPlanets)
                NextGame();

            yield return new WaitForSeconds(0.1f);
        }
    }


    private void RemovingDestroyedPlanets()
    {
        for (int i = 0; i < planets.Count; i++)
        {
            if (planets[i].GetComponent<Planet>().GetRemoveStatus())
            {
                GameObject buf = planets[i];
                planets.RemoveAt(i);
                Destroy(buf);
                numberActivePlanet--;
                numberDeactivePlanet++;
                updateScoreboards();
            }
        }
    }

    private void ResetPositionPlayer()
    {
        player.GetComponent<Player>().ResetPosition();
    }

    private float randForce()
    {
        float force = Random.Range(minForcePlanet, maxForcePlanet);
        return force;
    }

    private GameObject randPlanets()
    {
        int planetNumber = Random.Range(0, CollectionsPlanets.Count);
        return CollectionsPlanets[planetNumber];
    }

    private Vector3 randPosition()
    {
        float x = Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
        float y = Random.Range(minHeightCreatePlanet, maxHeightCreatePlanet);
        float z = Random.Range(-widthRangeFromPlayer, widthRangeFromPlayer);
        Vector3 position = new Vector3(x, y, z);
        return position;
    }

    private GameObject createNewObject()
    {
        GameObject newPlanet = Instantiate(randPlanets(), randPosition(), Quaternion.identity);
        planets.Add(newPlanet);
        return newPlanet;
    }

    private void throwPlanet(GameObject planet)
    {
        planet.SetActive(true);
        numberActivePlanet++;
        Rigidbody rigi = planet.gameObject.GetComponent<Rigidbody>();
        rigi.AddForce(0, randForce(), 0, ForceMode.Impulse);
    }

    private void updateScoreboards()
    {
        score += swordLeft.GetComponent<Sword>().GetScore();
        score += swordRight.GetComponent<Sword>().GetScore();
        textScore.text = "Wynik: " + score.ToString();
        numberDestroyPlanet += swordLeft.GetComponent<Sword>().GetNumberDestroyPlanet();
        numberDestroyPlanet += swordRight.GetComponent<Sword>().GetNumberDestroyPlanet();
        textNumberDestroyPlanets.text = "Zniszczone planety: " + numberDestroyPlanet + "/" + numberDeactivePlanet;
        swordLeft.GetComponent<Sword>().ResetScore();
        swordRight.GetComponent<Sword>().ResetScore();
        swordLeft.GetComponent<Sword>().ResetNumberDestroyPlanet();
        swordRight.GetComponent<Sword>().ResetNumberDestroyPlanet();
    }

    private void NextGame()
    {
        GameManager.Instance.SetScore(score);
        GameManager.Instance.PlayGame();
    }
}
