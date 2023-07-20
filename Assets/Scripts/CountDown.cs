using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countDownText;
    BoatMove boatMove;
    float limitTime = 60; // 제한 시간
    int sec; // 초 변수
    int min; // 분 변수
    public TMP_Text text_Time; // 타이머 UI
    private void Start()
    {
        boatMove = GameObject.Find("Boat").GetComponent<BoatMove>();

        StartCoroutine(StartCownDown());
    }
    void Update()
    {
        Invoke("TimeRule", 3.5f);

    }

    public void TimeRule()
    {
        limitTime -= Time.deltaTime; // 남은 시간 감소
        // 전체 시간이 60보다 클 때
        if (limitTime >= 60f)
        {
            min = (int)limitTime / 60; // 분 단위 변경
            sec = (int)limitTime % 60; // 초 단위 변경
            text_Time.text = string.Format("{0}:{1}", min, sec); // UI
        }

        // 전체 시간이 60보다 작을 때
        if (limitTime < 60f)
        {
            text_Time.text = "00:" + (int)limitTime; // UI
            if (limitTime < 10f)
            {
                text_Time.text = "00:0" + (int)limitTime; // UI
            }
        }

        // 전체 시간이 0보다 작을 때
        if (limitTime < 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("duckBoat"); //
        }
    }

    
    private IEnumerator StartCownDown()
    {
        boatMove.DisableMovement();

        countDownText.text = "3";
        yield return new WaitForSeconds(1f);

        countDownText.text = "2";
        yield return new WaitForSeconds(1f);

        countDownText.text = "1";
        yield return new WaitForSeconds(1f);

        countDownText.text = "Go";

        yield return new WaitForSeconds(0.5f);
        countDownText.text = "";
        boatMove.EnableMovement();
        
    }
}
