using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapItem : MonoBehaviour
{
    public float trapDuration = 3f; // 함정 지속 시간 (초)
    public float reduceAmount = 2f; // 이동 속도 감소 비율
    public GameObject trapEffectPrefab;  // 함정 효과 프리팹

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
            
            StartCoroutine(TrapEffectCoroutine(other.gameObject));

            Destroy(gameObject);
        }
    }


    private IEnumerator TrapEffectCoroutine(GameObject player)
    {
        Camera playerCamera = Camera.main;
        GameObject trapEffectInstance = Instantiate(trapEffectPrefab);
        trapEffectInstance.transform.SetParent(playerCamera.transform);
        trapEffectInstance.transform.localPosition = Vector3.forward;

        ParticleSystem[] allParticleSystems = trapEffectInstance.GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem particleSystem in allParticleSystems)
        {
            var main = particleSystem.main;
            main.loop = false;
            particleSystem.Play();
        }
        Debug.Log("PLAY");

        Debug.Log($"trapDuration: {trapDuration}");

        yield return new WaitForSeconds(trapDuration);

        foreach (ParticleSystem particleSystem in allParticleSystems)
        {
            particleSystem.Stop();
        }
        Debug.Log("STOP");

        float maxEffectDuration = 0.0f;
        foreach (ParticleSystem particleSystem in allParticleSystems)
        {
            if (particleSystem.main.duration > maxEffectDuration)
            {
                maxEffectDuration = particleSystem.main.duration;
            }
        }
        Debug.Log($"maxEffectDuration: {maxEffectDuration}");
        yield return new WaitForSeconds(maxEffectDuration);

        Destroy(trapEffectInstance);
        Debug.Log("DESTROY");
    }
}