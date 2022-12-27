using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionController : MonoBehaviour
{
    [Header("SCRIPT")]
    public PlayerController playerController;

    [Header("BUTTONS")]
    public Button buyTime;
    public Button buyNewQuestion;
    public Button trueAnswer;
    public Button wrongAnswer;
    public Button wrongAnswer_1;
    public Button wrongAnswer_2;
    public Button continueBt;

    [Header("GAMEOBJETS")]
    public GameObject trueText;
    public GameObject falseText;
    public GameObject question;
    public GameObject continueButtonObject;

    [Header("ARRAY")]
    public GameObject[] answerButtons;

    [Header("INT")]
    public int timeCost;
    public int questionCost;


    [Header("TEXT")]
    public TextMeshProUGUI timeCostText;
    public TextMeshProUGUI questionCostText;
    
    void Start()
    {

        timeCostText.text = timeCost.ToString();
        questionCostText.text = questionCost.ToString();


        playerController = FindObjectOfType<PlayerController>();

        Button buyTimeButton = buyTime.GetComponent<Button>();
        buyTimeButton.onClick.AddListener(BuyMoreTime);

        Button buyNewQuestionButton = buyNewQuestion.GetComponent<Button>();
        buyNewQuestionButton.onClick.AddListener(BuyNewQuestion);

        Button trueButton = trueAnswer.GetComponent<Button>();
        trueButton.onClick.AddListener(True);

        Button wrongButton = wrongAnswer.GetComponent<Button>();
        wrongButton.onClick.AddListener(False);


        Button wrongButton_1 = wrongAnswer_1.GetComponent<Button>();
        wrongButton.onClick.AddListener(False);


        Button wrongButton_2 = wrongAnswer_2.GetComponent<Button>();
        wrongButton.onClick.AddListener(False);

        Button continueButton = continueBt.GetComponent<Button>();
        continueButton.onClick.AddListener(ContinueOnClick);
    }


    void Update()
    {
        timeCostText.text = timeCost.ToString();
        questionCostText.text = questionCost.ToString();


        if (playerController.score<timeCost)
        {
            buyTime.interactable = false;
        }

        if (playerController.score<questionCost)
        {
            buyNewQuestion.interactable = false;
        }

        if (playerController.beforeTimer.activeSelf==true)
        {
            trueAnswer.interactable = false;
            wrongAnswer.interactable = false;
            wrongAnswer_1.interactable = false;
            wrongAnswer_2.interactable = false;
            buyNewQuestion.interactable = false;
            buyTime.interactable = false;
        }
        else
        {
            trueAnswer.interactable = true;
            wrongAnswer.interactable = true;
            wrongAnswer_1.interactable = true;
            wrongAnswer_2.interactable = true;
            buyNewQuestion.interactable = true;
            buyTime.interactable = true;
        }
    }

    public void True()
    {
        trueText.SetActive(true);
        playerController.score += 1;
        playerController.scoreText.text = playerController.score.ToString();
        StartCoroutine(Destroy());
    }

    public void False()
    {
        buyNewQuestion.gameObject.SetActive(false);
        buyTime.gameObject.SetActive(false);
        playerController.timer.SetActive(false);
        question.SetActive(false);
        falseText.SetActive(true);
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].SetActive(false);
        }
        continueButtonObject.SetActive(true);

    }

    public void BuyMoreTime()
    {
        if (playerController.score>=timeCost)
        {
            playerController.BuyTime(timeCost);
            timeCost += 2;
            timeCostText.text = timeCost.ToString();
        }

    }

    public void BuyNewQuestion()
    {
        if (playerController.score>=questionCost)
        {
            playerController.BuyQuestion(questionCost);
            questionCost += 2;
            questionCostText.text = questionCost.ToString();
        }


    }

    public void ContinueOnClick()
    {
        Destroy(gameObject);
    }

    IEnumerator Destroy()
    {

        playerController.timer.SetActive(false);
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
        Destroy(gameObject,1);
    }
}
