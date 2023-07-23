using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveEffectScript : MonoBehaviour
{
    [SerializeField] float shakeSpeed = 2f;                   // 흔들리는 속도
    [SerializeField] float shakeAmount = 0.1f;                  // 흔들리는 정도

    [SerializeField] float rotateAmount = 1.0f;                 // 흔들린 상태에서의 회전 정도

    private Vector3 initialPosition;                            // 초기 회전값을 저장하는 변수



    private void Start()
    {
        initialPosition = transform.position;
    }
    private void Update()
    {
        // 보트의 흔들림 효과 적용
        //Wave();
    }
    private void LateUpdate()
    {
       // Wave();
    }
    private void Wave()
    {
        // sin 함수를 사용하여 보트를 흔들도록 함
        float shake = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

        // 보트 오브젝트의 로컬 회전값을 변경하여 흔들리는 효과를 줌
        Vector3 position = initialPosition;
        position.y += shake;
        transform.position = position;
    }
}
