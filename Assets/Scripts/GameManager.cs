using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    //게임은 정해진시간안에 아이템을 모아야하는 규칙.
    //다 모으고 남은 시간을 통해 점수와 순위를 집계
    //다 모으지 못하고 제한시간이 끝난 경우 게임오버 씬, 전부 모은 경우 나오는 게임 클리어 씬
    //아이템은 세가지, 이동 속도의 증가/ 모아야하는 아이템/ 시야를 가리는 함정아이템

    public static GameManager Instance { get; private set;}

    public GameObject gameOverPanel;
    public GameObject gameClearPanel;
    public GameObject joyStick;

    public TextMeshProUGUI leftTime;
    public TextMeshProUGUI remainingItemsText;

    private int totalScore = 0;
    private int CollectedItems = 0;

    CountDown countDown;


    // Start is called before the first frame update
    private void Awake()
    {
        CountDown countDownScript = FindObjectOfType<CountDown>();
        countDown = GetComponent<CountDown>();

        gameOverPanel.SetActive(false);
        gameClearPanel.SetActive(false);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        UpdateRemainingItemsText();
    }

    public void IncreaseScore(int scoreToAdd)
    {
        totalScore += scoreToAdd;
        CollectedItems++;

        UpdateRemainingItemsText();

        if (CollectedItems == 10)
        {
            GameClear();
        }
    }

    public void TimeOver()
    {
        if(CollectedItems!=10)
        { }
    }

    private void GameClear()
    {
        joyStick.SetActive(false);
        gameClearPanel.SetActive(true);
        countDown.DeactiveText();
        remainingItemsText.text = "";
        Time.timeScale = 0f;
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        countDown.DeactiveText();
        Time.timeScale = 0f; // 게임 일시정지
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // 게임 재개
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateRemainingItemsText()
    {
        remainingItemsText.text = CollectedItems.ToString() + "/10";
    }
}
