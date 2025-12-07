using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    [SerializeField] private float parallaxMultiplier;
    [SerializeField] private Transform layer;
    private float offset = 10f;

    private float halfWidth;

    public void Init()
    {
        halfWidth = layer.GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    public void Move(float distance)
    {
        layer.position += new Vector3(distance * parallaxMultiplier, 0, 0);
    }

    public void Loop(float cameraLeftLimit, float cameraRightLimit)
    {
        float layerLeftLimit = layer.position.x - halfWidth + offset;
        float layerRightLimit = layer.position.x + halfWidth - offset;

        if (layerRightLimit < cameraLeftLimit)
        {
            layer.position += new Vector3(halfWidth * 2, 0, 0);
        }
        else if(layerLeftLimit > cameraRightLimit)
        {
            layer.position -= new Vector3(halfWidth * 2, 0, 0);
        }
        
    }
}
