using System.Collections;
using UnityEngine;

public class EntityVFX : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;

    [SerializeField] private Material onHitMaterial;
    [SerializeField] private float onHitDuration = 0.15f;

    private Coroutine onHitCoroutine;

    public void OnHit()
    {
        if (onHitCoroutine != null)
            StopCoroutine(onHitCoroutine);

        onHitCoroutine = StartCoroutine(Co_OnHit());
    }

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }

    private IEnumerator Co_OnHit()
    {
        spriteRenderer.material = onHitMaterial;
        yield return new WaitForSeconds(onHitDuration);
        spriteRenderer.material = originalMaterial;
    }
}
