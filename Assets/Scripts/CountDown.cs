using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countDownText;
    [SerializeField] TextMeshProUGUI text_Time;
    BoatMove boatMove;
    int limitTime = 60; // 제한 시간
    int sec; // 초 변수
    int min; // 분 변수
     // 타이머 UI
    private void Start()
    {
        boatMove = GameObject.Find("Boat").GetComponent<BoatMove>();

        StartCoroutine(StartCownDown());
    }
    void FixedUpdate()
    {
        Invoke("TimeRule", 3.5f);

    }

    private IEnumerator TimeRule()
    {
        while (limitTime > 0)
        {
            int minutes = limitTime / 60;
            int seconds = limitTime % 60;

            string timeText = string.Format("{0:00}:{1:00}", minutes, seconds);
            text_Time.text = timeText;

            yield return new WaitForSeconds(1f);
            limitTime--;
        }

        text_Time.text = "Time's Up!";
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
        StartCoroutine(TimeRule());
        
    }
}
