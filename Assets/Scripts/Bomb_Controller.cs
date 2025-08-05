using System.Collections;
using UnityEngine;

public class Bomb_Controller : MonoBehaviour
{
    public GameObject bombPrefab;

    public KeyCode inputKey = KeyCode.Space;

    public float bombFuseTime = 3f;
    public int bombAmout = 1;
    private int bombRemaining;

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
}
