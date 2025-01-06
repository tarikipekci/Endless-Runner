using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerManager.numberOfCoins++;
            if (PlayerManager.audioManager != null)
            {
                PlayerManager.audioManager.soundEffects[0].Play();
            }
            Destroy(gameObject);
        }
    }
}
