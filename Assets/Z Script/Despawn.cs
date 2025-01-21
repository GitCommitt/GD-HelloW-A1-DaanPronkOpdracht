using UnityEngine;

namespace MyGameNamespace
{
    public class DeleteOnTouchConfigurable : MonoBehaviour
    {
        [Tooltip("Het object dat verwijderd moet worden wanneer de speler ermee in aanraking komt.")]
        public GameObject objectToDelete;

        [Tooltip("De tag van het object dat dit object kan verwijderen bij aanraking.")]
        public string playerTag = "Player";

        [Tooltip("Sleep hier de ScoreManager om de score op te hogen.")]
        public ScoreManager scoreManager;

        [Tooltip("Het geluidseffect dat moet worden afgespeeld bij het oppakken.")]
        public AudioClip pickupSound;

        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                if (objectToDelete != null)
                {
                    PlayPickupSound();

                    Destroy(objectToDelete);

                    if (scoreManager != null)
                    {
                        scoreManager.AddScore(1);
                    }
                    else
                    {
                        Debug.LogWarning("ScoreManager is niet ingesteld in de Inspector.");
                    }
                }
                else
                {
                    Debug.LogWarning("objectToDelete is niet ingesteld in de Inspector.");
                }
            }
        }

        private void PlayPickupSound()
        {
            if (pickupSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }
            else
            {
                Debug.LogWarning("PickupSound of AudioSource is niet ingesteld.");
            }
        }
    }
}
