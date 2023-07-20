using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMove : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;                     // 이동 속도
    [SerializeField] float boatRotSpeed = 40f;                  // 회전 속도

    [SerializeField] float shakeSpeed = 2f;                   // 흔들리는 속도
    [SerializeField] float shakeAmount = 0.1f;                  // 흔들리는 정도

    [SerializeField] float rotateAmount = 1.0f;                 // 흔들린 상태에서의 회전 정도

    GameObject[] Items;

    private Vector3 initialPosition;                            // 초기 회전값을 저장하는 변수
    private Quaternion initialRotation;                         // 초기 회전값

    private Rigidbody rigidBody;
    private bool isMovementEnabled = true;

    bool isSpeedBoosted = false; // 이동속도 증가 효과 여부
    float boostDuration = 1f; // 이동속도 증가 지속 시간
    float boostAmount = 10f; // 이동속도 증가량
    Coroutine boostCoroutine = null; // 현재 이동속도 증가 코루틴 참조



    private void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        rigidBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        if (isMovementEnabled)
        {
            BoatMovement();
        }

        // 보트의 흔들림 효과 적용
        wave();
    }

    private void BoatMovement()
    {
        float Horz = Input.GetAxis("Horizontal");
        float Vert = Input.GetAxis("Vertical");

        //이동
        float amount = moveSpeed * Time.deltaTime * Vert; // 현재 프레임에서 이동할 거리
        rigidBody.MovePosition(rigidBody.position + transform.forward * amount);

        //회전
        float amountRot = boatRotSpeed * Time.deltaTime * Horz; // 현재 프레임에서 회전할 각도
        rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(Vector3.up * amountRot));
    }

    private void wave()
    {
        // sin 함수를 사용하여 보트를 흔들도록 함
        float shake = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;

        // 보트 오브젝트의 로컬 회전값을 변경하여 흔들리는 효과를 줌
        Vector3 position = initialPosition;
        position.y += shake;
        transform.position = position;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "SpeedItem")               // 이동속도 아이템
        {
            other.gameObject.SetActive(false);      // 게임 오브젝트 비활성화
                                                    // 이동속도 증가 효과를 발동하고, boostCoroutine에 코루틴 참조 저장
            if (boostCoroutine != null)
                StopCoroutine(boostCoroutine);      // 이전 효과를 중단시킴
            boostCoroutine = StartCoroutine(ActivateSpeedBoost());
        }

        if(other.tag == "ScoreItem") // 점수 아이템
        {
            other.gameObject.SetActive(false);

        }

        if(other.tag == "TrapItem") // 느려지는 아이템
        {
            other.gameObject.SetActive(false);
        }

    }
    IEnumerator ActivateSpeedBoost()
    {
        isSpeedBoosted = true; // 이동속도 증가 효과 활성화
        moveSpeed += boostAmount; // 이동속도 증가

        yield return new WaitForSeconds(boostDuration);

        moveSpeed = 10f; // 이동속도 원래대로 되돌리기
        isSpeedBoosted = false; // 이동속도 증가 효과 비활성화

        // 코루틴 참조 초기화
        boostCoroutine = null;
    }

    public void EnableMovement()
    {
        isMovementEnabled = true;
    }

    public void DisableMovement()
    {
        isMovementEnabled = false;
    }
}
