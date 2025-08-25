using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AnimatedSpriteRenderer fire;

    public void SetActiveRenderer(AnimatedSpriteRenderer renderer)
    {
        fire.enabled = renderer == fire;
    }

    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }
}
