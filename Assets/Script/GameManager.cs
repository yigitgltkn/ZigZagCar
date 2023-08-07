using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager  : MonoBehaviour
{
    public bool gameStarted;
    public static GameManager instance;
    
    public GameObject platformSpawner;
    public GameObject gamePlayUI;
    public GameObject menuUI;

    public Text scoreText;
    public Text highScoreText;

    int score = 0;
    int highScore;


    AudioSource audioSource;
    public AudioClip[] gameMusic; // bu sekilde inspector'da liste halinde muzikler eklenir. 0 index sayisi ilk muziktir.

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }

    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = " Best Score : " + highScore;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (!gameStarted) // eger oyun baslamadiysa ilk tik ile GameStart fonksiyonunu cagir.
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameStart();   
            }
        }
    }

    void GameStart() // mouse ile tiklanildiktan sonra oyun baslar ve GameStart metodu devreye girer.
    {
        gameStarted = true;
        platformSpawner.SetActive(true); // normale disable halde. mouse ile tiklanildiginda bu kod calisir ve spawn baslar

        gamePlayUI.SetActive(true); // skoru tutan text. normalde disable, oyu basladiginda aktif olur
        menuUI.SetActive(false); //ana menu oyun baslayinca kapanir.

        audioSource.clip = gameMusic[1]; // 1 index numarali muzik bu sekilde devreye girer
        audioSource.Play(); //...ve calisir.

       
    }

    public void GameOver()
    {
        platformSpawner.SetActive(false); //platformspawner disable olur.
        SaveHighScore();
        Invoke("ReloadLevel", 1f);
        
    }

    void ReloadLevel()  //GameOver oldugunda yukarida yazdigi gibi 1 saniye sonra tekrar basa donulur.
    {
        SceneManager.LoadScene(0);
    }

    public void IncrementScore() // bu metod araba elmasa temas ettiginde tetiklenir. CarControl kisminda cagirilmalidir.
    {
        score += 1;
        scoreText.text = score.ToString(); // skoru birer birer arttirir ve ekrana yazdirir.
        audioSource.PlayOneShot(gameMusic[2], 0.2f); //her temazda 2 index no'lu ses calisir.
    }
    void SaveHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore")) // 
        {
            if (score > PlayerPrefs.GetInt("HighScore")) //oynadigimiz oyundaki skor daha onceki highskor'dan buyukse skoru kaydeder
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }
        else // degilse de skoru kaydeder ama yukaridaki if saglanirsa burasi ise yaramaz. yani en yuksek skor her turlu yazilmis olur.
        {
            PlayerPrefs.SetInt("HighScore", score); 
        }
    }
}
