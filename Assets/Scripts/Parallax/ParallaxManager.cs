using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [SerializeField] private ParallaxLayer[] layers;

    private Camera mainCamera;
    private float cameraHalfWidth;
    private float lastPositionX;

    void Awake()
    {
        mainCamera = Camera.main;
        lastPositionX = mainCamera.transform.position.x;

        // orthographicSize: 카메라의 세로 절반(half-height).
        // aspect: 카메라의 가로 / 세로 비율.
        // orthographicSize * aspect: 카메라의 가로 절반(half-width).
        cameraHalfWidth = mainCamera.orthographicSize * mainCamera.aspect;

        foreach (ParallaxLayer layer in layers)
        {
            layer.Init();
        }
    }

    // 카메라를 따라 움직일 때 float 연산의 오차로 인해 지터링(Jittering) 발생.
    // 이를 막기 위해 FixedUpdate() 메소드로 변경.
    void LateUpdate()
    {
        float currentPositionX = mainCamera.transform.position.x; // 카메라의 현재 위치.
        float distance = currentPositionX - lastPositionX; // 카메라의 이동 거리.
        lastPositionX = currentPositionX; // 카메라의 마지막 위치 갱신.

        float cameraLeftLimit = currentPositionX - cameraHalfWidth; // 카메라의 왼쪽 끝.
        float cameraRightLimit = currentPositionX + cameraHalfWidth; // 카메라의 오른쪽 끝.

        foreach (ParallaxLayer layer in layers)
        {
            layer.Move(distance);
            layer.Loop(cameraLeftLimit, cameraRightLimit);
        }
    }
}
