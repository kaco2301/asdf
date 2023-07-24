using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapItem : MonoBehaviour
{
    public float trapDuration = 3f; // 함정 지속 시간 (초)
    public float reduceAmount = 2f; // 이동 속도 감소 비율
    public GameObject trapEffectPrefab; // 함정 효과 프리팹
    public Canvas canvas;


    private bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        BoatMove playerMovement = other.GetComponent<BoatMove>();
        if (other.CompareTag("Player") && !isTriggered)
        {
            isTriggered = true;

            // 이동 속도 감소 코루틴 시작
            if (playerMovement != null)
            {
                playerMovement.ApplySpeedReduce(trapDuration, reduceAmount);
            }

            Destroy(gameObject);

            StartCoroutine(TrapEffectCoroutine(other.gameObject));
            
        }
    }

    private IEnumerator TrapEffectCoroutine(GameObject player)
    {
        GameObject trapEffectInstance = Instantiate(trapEffectPrefab, canvas.transform);
        Image trapImage = trapEffectInstance.GetComponent<Image>();
        trapImage.color = new Color(trapImage.color.r, trapImage.color.g, trapImage.color.b, 1f);

        CanvasGroup canvasGroup = trapEffectInstance.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = trapEffectInstance.AddComponent<CanvasGroup>();
        }

        yield return new WaitForSeconds(trapDuration);

        // 이미지 서서히 사라지기
        float elapsedTime = 0f;
        float fadeDuration = 1f; // 이미지가 사라지는 시간 (초)

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            canvasGroup.alpha = alpha;
            elapsedTime += Time.deltaTime;

            // 다음 프레임까지 대기
            yield return new WaitForEndOfFrame();
        }

        Destroy(trapEffectInstance);
    }
}
