using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followBoat : MonoBehaviour
{/*
    public Vector3 position = new Vector3(0, 3.6f, -7.8f);
    public Vector3 rotation = new Vector3(14, 0, 0);
    public float fov = 30f;

    float boatSpeed = 10f;
    float turnSpeed = 10f;

    Transform boat; // 추적 대상 = 오리배
    Transform cam; // 카메라
    Transform pivot; // 카메라 이동, 회전축 */

    // Start is called before the first frame update
    void Start()
    {/*
        boat = GameObject.Find("Boat").transform; // 타깃 설정
        inItCamera(); // 카메라 초기화 */
    }
    /*
    void inItCamera()
    {
        // 카메라 설정
        cam = Camera.main.transform;
        cam.GetComponent<Camera>().fieldOfView = fov;

        // Pivot 만들기
        pivot = new GameObject("Pivot").transform;
        pivot.position = boat.position;

        // 카메라를 Pivot의 Child로 설정
        cam.parent = pivot;
        cam.localPosition = position;
        cam.localEulerAngles = rotation;
    }

    void LateUpdate()
    {
        // 선형 보간
        // 목적값
        Vector3 pos = boat.position;
        Quaternion rot = boat.rotation;

        pivot.position = Vector3.Lerp(pivot.position, pos, boatSpeed * Time.deltaTime); // 이동
        pivot.rotation = Quaternion.Lerp(pivot.rotation, rot, turnSpeed * Time.deltaTime); // 회전
    } */
}
