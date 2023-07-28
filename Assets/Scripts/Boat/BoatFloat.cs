using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatFloat : MonoBehaviour
{
    public float floatStrength = 0.1f; // 배 높낮이 변경 정도
    public float floatSpeed = 2.0f; // 배 진동 속도

    private Vector3 originalPosition;

    void Start()
    {
        // 시작 위치를 저장합니다.
        originalPosition = transform.position;
    }

    void Update()
    {
        // Time.time과 Mathf.Sin 함수를 사용하여 높낮이 움직임을 만듭니다.
        float newYPosition = originalPosition.y + Mathf.Sin(Time.time * floatSpeed) * floatStrength;

        // Transform의 위치를 새로운 위치로 업데이트합니다.
        transform.position = new Vector3(transform.position.x, newYPosition, transform.position.z);
    }
}
