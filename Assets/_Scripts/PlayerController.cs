using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("SCRIPTS")]
    public GameUI gameUI;

    [Header("FLOAT")]
    public float speed;
    public float swerveSpeed;
    public float swerveRange;
    
    [Header("INT")]
    public int score;
    public int questionIndex;
    public int newQuestionsIndex;

    [Header("BOOL")]
    public bool isCountDownStart;
    public bool isQuestionSpawned;
    public bool isStopped;

    [Header("GAMEOBJECT")]
    public GameObject beforeTimer;
    public GameObject timer;
    public GameObject timesUp;
    public GameObject question;
    public GameObject spawnedQuestion;

    [Header("ARRAY")]
    public GameObject[] questionPrefabs;
    public GameObject[] newQuestions;

    [Header("ANIMATOR")]
    public Animator anim;

    [Header("ANIMATION")]
    public Animation scoreTextPopUpAnim;

    [Header("TRANSFORM")]
    public Transform player;

    [Header("TEXT")]
    public Text scoreText;
    public Text timeText;
    public Text beforeTimerText;

    void Start()
    {
        gameUI = FindObjectOfType<GameUI>();
        anim = GetComponent<Animator>();

        //player = transform;
        questionIndex = 0;
        newQuestionsIndex = 0;
    }


    void Update()
    {
        if (gameUI.isGameStart && !gameUI.isGameFinish)
        {
            spawnedQuestion = GameObject.FindGameObjectWithTag("Question");
            scoreText.text = score.ToString();

            anim.SetBool("Run",true);
            HandleMovement();



            if (spawnedQuestion != null)
            {

                Stop();
            }
            else
            {

                Continue();
                StopAllCoroutines();

            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            //lastPos = Input.mousePosition.x;
        }
    }

    float? lastPos = null;
    void HandleMovement()
    {


        float ratio = Screen.width / (swerveRange * 2 * swerveSpeed);

        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.World);

        if (Input.GetMouseButton(0) && speed > 0)
        {
            var touchPoint = Input.mousePosition.x - lastPos.GetValueOrDefault(Screen.width / 2);
            lastPos = Input.mousePosition.x;
            Vector3 startPos = player.position;
            Vector3 targetPos = new Vector3(Mathf.Clamp(startPos.x + touchPoint / ratio, -swerveRange, swerveRange), player.position.y, player.position.z);

            player.position = targetPos;

        }


    }
    public float currCountdownValue;
    public IEnumerator StartCountdown(float countdownValue = 60)
    {
        currCountdownValue = countdownValue;
        
        while (currCountdownValue > 0)
        {
            timeText.text = currCountdownValue.ToString();
            Debug.Log("Countdown: " + currCountdownValue);
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
            if (currCountdownValue==0)
            {
                timer.SetActive(false);
                timesUp.SetActive(true);
                yield return new WaitForSeconds(0.5f);
                timesUp.SetActive(false);
                Destroy(spawnedQuestion);
                break;
            }
        }
    }

    float time;
    public IEnumerator BeforeTimerStart(float countdownValue = 3)
    {
        yield return new WaitForSeconds(1f);
        beforeTimer.SetActive(true);
        time = countdownValue;
        beforeTimerText.text = "Sürenin baþlamasýna " + time.ToString();


        while (countdownValue>0)
        {
            beforeTimerText.text = "Sürenin baþlamasýna " + time.ToString();
            yield return new WaitForSeconds(1.0f);
            time--;
            if (time == 0)
            {
                beforeTimer.SetActive(false);
                timer.SetActive(true);
                StartCoroutine(StartCountdown());
                break;
            }
        }
    }
    

    public void BuyTime(int cost)
    {
        score -= cost;
        scoreTextPopUpAnim.Play();
        scoreText.text = score.ToString();
        currCountdownValue += 20f;
    }

    public void BuyQuestion(int cost)
    {
        score -= cost;
        scoreTextPopUpAnim.Play();
        Destroy(spawnedQuestion);
        Instantiate(newQuestions[newQuestionsIndex], new Vector3(-1.25f, 1.5f, transform.position.z + 5), Quaternion.identity);
        newQuestionsIndex += 1;

    }

    public void Stop()
    {
        speed = 0;
        anim.SetBool("Run", false);
    }
    public void Continue()
    {
        speed = 5;
        anim.SetBool("Run", true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            //anim.SetBool("isHit",true);
            anim.SetTrigger("isHit");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            score += 1;
            scoreTextPopUpAnim.Play();
            scoreText.text = score.ToString();
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("QuestionCollider"))
        {
            if (!isQuestionSpawned)
            {

                Instantiate(questionPrefabs[questionIndex], new Vector3(-1.25f, 1.5f, transform.position.z + 5), Quaternion.identity);
                questionIndex += 1;
                StartCoroutine(BeforeTimerStart());
                isQuestionSpawned = true;
            }
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            gameUI.confetti.Play();
            gameUI.isGameFinish = true;
            gameUI.levelCompletePanel.SetActive(true);
            speed = 0;
            anim.SetBool("Run",false);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("QuestionCollider"))
        {
            isQuestionSpawned = false;
        }
    }
}
