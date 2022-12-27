using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("SCRIPTS")]
    public PlayerController player;

    [Header("GAMEOBJECTS")]
    public GameObject buttons;
    public GameObject icon;
    public GameObject optionsPanel;
    public GameObject pausePanel;
    public GameObject levelCompletePanel;
    public GameObject hintsPanel;
    public GameObject firstHintPanel;
    public GameObject howToPlayPanel;
    public GameObject levelTextObject;

    [Header("ARRAY")]
    public GameObject[] hintsImg;


    [Header("BUTTONS")]
    public Button play;
    public Button options;
    public Button quit;
    public Button pause;
    public Button resume;
    public Button hints;
    public Button nextHint;
    public Button backHint;
    public Button backToMenu;
    public Button continueHint;
    public Button howToPlay;
    public Button back;
    public Button nextLevel;

    [Header("ANIMATIONS")]
    public Animation buttonPopUpAnim;

    [Header("PARTICLE")]
    public ParticleSystem confetti;

    [Header("BOOL")]
    public bool isGameStart;
    public bool isGameFinish;
    public bool isLevelComplete;
    public bool isLevelFail;

    [Header("INT")]
    public int hintIndex;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        hintIndex = 0;

        Button playButton = play.GetComponent<Button>();
        Button optionsButton = options.GetComponent<Button>();
        Button quitButton = quit.GetComponent<Button>();
        Button pauseButton = pause.GetComponent<Button>();
        Button resumeButton = resume.GetComponent<Button>();
        Button hintsButton = hints.GetComponent<Button>();
        Button backToMenuButton = backToMenu.GetComponent<Button>();
        Button nextHintButton = nextHint.GetComponent<Button>();
        Button backHintButton = backHint.GetComponent<Button>();
        Button firstHintContinueButton = continueHint.GetComponent<Button>();
        Button howToPlayButton = howToPlay.GetComponent<Button>();
        Button backButton = back.GetComponent<Button>();
        Button nextLevelButton = nextLevel.GetComponent<Button>();

        playButton.onClick.AddListener(PlayOnClick);
        optionsButton.onClick.AddListener(OptionsOnClick);
        quitButton.onClick.AddListener(QuitOnClick);
        pauseButton.onClick.AddListener(PauseOnClick);
        resumeButton.onClick.AddListener(ResumeOnClick);
        hintsButton.onClick.AddListener(HintsOnClick);
        backToMenuButton.onClick.AddListener(BackToMenuOnClick);
        nextHintButton.onClick.AddListener(NextHintOnClick);
        backHintButton.onClick.AddListener(BackHintOnClick);
        firstHintContinueButton.onClick.AddListener(FirstHintContinue);
        howToPlayButton.onClick.AddListener(HowToPlayOnClick);
        backButton.onClick.AddListener(Back);
        nextLevelButton.onClick.AddListener(LoadLevel);
    }


    void Update()
    {
        
    }

    void PlayOnClick()
    {
        play.GetComponent<Animation>().Play();
        isGameStart = true;
        icon.SetActive(false);
        buttons.SetActive(false);
        optionsPanel.SetActive(false);
        levelTextObject.SetActive(false);
        StartCoroutine(FirstHintShowCorotine());
    }

    void OptionsOnClick()
    {
        buttons.SetActive(false);
        options.GetComponent<Animation>().Play();
        optionsPanel.SetActive(true);
        icon.SetActive(false);
        levelTextObject.SetActive(false);

    }

    void QuitOnClick()
    {
        quit.GetComponent<Animation>().Play();
    }

    void PauseOnClick()
    {
        if (!isGameStart)
        {
            icon.SetActive(false);
            buttons.SetActive(false);
            levelTextObject.SetActive(false);
        }
        optionsPanel.SetActive(false);
        hintsPanel.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    void ResumeOnClick()
    {
        if (!isGameStart)
        {
            icon.SetActive(true);
            buttons.SetActive(true);
            levelTextObject.SetActive(true);
        }

        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    void HintsOnClick()
    {
        icon.SetActive(false);
        buttons.SetActive(false);
        optionsPanel.SetActive(false);
        hintsPanel.SetActive(true);
        levelTextObject.SetActive(false);
    }

    public void BackToMenuOnClick()
    {
        hintsPanel.SetActive(false);
        icon.SetActive(true);
        buttons.SetActive(true);
        optionsPanel.SetActive(false);
        levelTextObject.SetActive(true);

    }

    void NextHintOnClick()
    {
        if (hintsImg[hintIndex+1] != null)
        {
            hintsImg[hintIndex].SetActive(false);
            hintsImg[hintIndex + 1].SetActive(true);

            hintIndex += 1;
        }
    }
    void BackHintOnClick()
    {
        if (hintsImg[hintIndex-1] != null)
        {
            hintsImg[hintIndex].SetActive(false);
            hintsImg[hintIndex - 1].SetActive(true);

            hintIndex -= 1;
        }
    }

    public IEnumerator FirstHintShowCorotine()
    {
        yield return new WaitForSeconds(1.25f);
        firstHintPanel.SetActive(true);
        firstHintPanel.transform.DOScale(new Vector3(1.5f,1.5f,1.5f),0.25f).OnComplete(()=>
        firstHintPanel.transform.DOScale(Vector3.one,0.25f));
        //Time.timeScale = 0;
      
    }

    void FirstHintShow()
    {
        StartCoroutine(FirstHintShowCorotine());
    }

    void FirstHintContinue()
    {
        firstHintPanel.SetActive(false);
        //Time.timeScale = 1;
    }

    void HowToPlayOnClick()
    {
        buttons.SetActive(false);
        icon.SetActive(false);
        howToPlayPanel.SetActive(true);
    }

    void Back()
    {
        howToPlayPanel.SetActive(false);
        buttons.SetActive(true);
        icon.SetActive(true);
    }

    public void LoadLevel()
    {
        // load the nextlevel
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
