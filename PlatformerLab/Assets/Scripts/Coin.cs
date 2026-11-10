using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int points = 10;
    [SerializeField] private AudioClip pickupClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        GameManager.Instance.AddScore(points);
        AudioManager.Instance.PlaySfx(pickupClip);
        Destroy(gameObject);
    }
}
