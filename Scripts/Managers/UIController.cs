using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }


    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Transform gameOverPrefab;
    PlayerMovement Bird;

    [SerializeField] private Animator backgroundAnimator;
    [SerializeField] private Animator backgroundAndSkyAnimator;


    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private GameObject inputField;



    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There's more than one UIController!" + Instance + " - " + transform);
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        // DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        backgroundAndSkyAnimator = GameObject.Find("Background").GetComponent<Animator>();
        backgroundAndSkyAnimator.enabled = true;

        GameManager.Instance.OnScoreEvent += UpdateScore;

        GameManager.Instance.OnPlayerDeathEvent += GameOver;
        GameManager.Instance.OnPlayerDeathEvent += TurnOffBackgroundAnimators;


        Bird = GameObject.FindObjectOfType<PlayerMovement>(); 
        if (Bird == null)  
        {
            Debug.LogError("UIController.Bird is NULL!");
        }
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + Bird.score.ToString();
    }

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerDeathEvent -= TurnOffBackgroundAnimators;
    }

    public void GameOver()
    {
        if (Bird != null) 
        {
            GameManager.Instance.gameOver = true;
            restartButton.SetActive(true);
            quitButton.SetActive(true);
            inputField.gameObject.SetActive(true);
            Instantiate(gameOverPrefab, this.transform);
        }
    }

    public void TurnOffBackgroundAnimators()
    {
        if (backgroundAndSkyAnimator != null) 
        {
            backgroundAndSkyAnimator.enabled = false;
        }
        else
        {
            Debug.Log("backgroundAndSkyAnimator is NULL!");
        }
        if (backgroundAnimator != null) 
        {
            backgroundAnimator.enabled = false;
        }
        else
        {
            Debug.Log("backgroundAnimator is NULL!");
        }
    }

    public void QuitButton()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void RestartButton()
    {
        //Debug.Log("RESTART!");

        GameManager.Instance.gameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
