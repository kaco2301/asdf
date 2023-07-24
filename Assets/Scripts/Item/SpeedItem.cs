using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedItem : MonoBehaviour
{
    public float boostDuration = 1f; // 이동 속도를 올려주는 시간(초)
    public float boostAmount = 1.5f; // 이동 속도를 얼마나 올려줄지 배율

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 플레이어 이동 속도를 올려줌
            BoatMove playerMovement = other.GetComponent<BoatMove>();

            if (playerMovement != null)
            {
                playerMovement.ApplySpeedBoost(boostDuration, boostAmount);
            }

            Destroy(gameObject); // 아이템을 먹으면 사라짐
        }
    }
}
