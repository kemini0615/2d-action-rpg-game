using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private float parallaxMultiplier;
    [SerializeField] private Transform layer;

    public void Move(float distance)
    {
        layer.position += new Vector3(distance * parallaxMultiplier, 0, 0);
    }
}
