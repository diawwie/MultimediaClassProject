using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int points = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        GameManager.Instance.AddScore(points);
        Destroy(gameObject);
    }
}