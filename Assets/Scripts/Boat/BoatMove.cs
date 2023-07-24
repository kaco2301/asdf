using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMove : MonoBehaviour
{
    public float moveSpeed = 10f;                     // 이동 속도
    private float currentSpeed;
    private float currentSpeedMultiplier = 1f; // 현재 이동 속도 비율 (기본값 1: 원래 이동 속도)

    [SerializeField] float rotationSpeed = 40f;                 // 회전 속도


    private Rigidbody rb;                                       //

    public bool isMovementEnabled = true;                       // 플레이어 이동 가능 여부

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        currentSpeed = moveSpeed;
    }

    private void Update()
    {
        if (isMovementEnabled)
        {
            BoatMovement();
            BoatRotation();
        }
    }

    private void BoatMovement()
    {
        float Vert = Input.GetAxis("Vertical");

        // 이동
        Move(Vert);
    }

    public void Move(float verticalInput)
    {
        // 이동
        float amount = currentSpeed * Time.deltaTime * verticalInput; // 현재 프레임에서 이동할 거리
        rb.MovePosition(rb.position + transform.forward * amount);
    }

    private void BoatRotation()
    {
        float Horz = Input.GetAxis("Horizontal");

        // 회전
        Rotate(Horz);
    }

    public void Rotate(float horizontalInput)
    {
        // 회전
        float amountRot = rotationSpeed * Time.deltaTime * horizontalInput; // 현재 프레임에서 회전할 각도                                                   
        rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up * amountRot));
    }

    public void ApplySpeedBoost(float duration, float boostAmount)
    {
        // 이동 속도를 올려주는 코루틴 실행
        StartCoroutine(BoostSpeed(duration, boostAmount));
    }

    public void ApplySpeedReduce(float duration,float reduceAmount)
    {
        StartCoroutine(ReduceSpeed(duration, reduceAmount));
    }

    private IEnumerator BoostSpeed(float duration, float boostAmount)
    {
        //SpeedItem에서 사용
        currentSpeed *= boostAmount;
        //부스트양만큼 속도증가
        yield return new WaitForSeconds(duration);
        //부스트양만큼 속도감소
        currentSpeed /= boostAmount;
    }
    private IEnumerator ReduceSpeed(float duration, float reduceAmount)
    {
        currentSpeed /= reduceAmount;
        yield return new WaitForSeconds(duration);
        currentSpeed *= reduceAmount;

    }

    public void SetSpeedMultiplier(float multiplier)
    {
        currentSpeedMultiplier = multiplier;
    }

    // 이동 속도 비율 원래대로 복구
    public void ResetSpeedMultiplier()
    {
        currentSpeedMultiplier = 1f;
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
