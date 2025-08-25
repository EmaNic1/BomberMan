using System.Collections;
using UnityEngine;

public class Bomb_Controller : MonoBehaviour
{
    [Header("Bomb")]
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombFuseTime = 3f;
    public int bombAmout = 1;
    private int bombRemaining;

    [Header("Explosion")]
    public Explosion explosionPrefab;
    public LayerMask explosionMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    private void OnEnable()
    {
        bombRemaining = bombAmout;
    }

    private void Update()
    {
        if (bombRemaining > 0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(PlaceBomb());
        }
    }

    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity);
        bombRemaining--;

        yield return new WaitForSeconds(bombFuseTime);

        position = bomb.transform.position;

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.fire);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Destroy(bomb);
        bombRemaining++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            collision.isTrigger = false;
        }
    }

    private void Explode(Vector2 position, Vector2 direction, int lenght)
    {
        if(lenght <= 0)
        {
            return;
        }

        position += direction;

        if(Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionMask))
        {
            return;
        }

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.fire);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);

        Explode(position, direction, lenght - 1);
    }
}
