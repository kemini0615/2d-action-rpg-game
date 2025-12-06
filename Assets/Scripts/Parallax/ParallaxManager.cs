using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private ParallaxLayer[] layers;

    private Camera mainCamera;
    private float lastPositionX;

    void Awake()
    {
        mainCamera = Camera.main;
        lastPositionX = mainCamera.transform.position.x;
    }

    void Update()
    {
        float currentPositionX = mainCamera.transform.position.x; // 카메라의 현재 위치.
        float distance = currentPositionX - lastPositionX; // 카메라의 이동 거리.
        lastPositionX = currentPositionX; // 카메라의 마지막 위치 갱신.

        foreach (ParallaxLayer layer in layers)
        {
            layer.Move(distance);
        }
    }
}
