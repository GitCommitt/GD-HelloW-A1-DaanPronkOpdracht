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
            // Zorg ervoor dat er een AudioSource-component is
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                // Voeg een AudioSource-component toe als deze nog niet bestaat
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        // Deze functie wordt aangeroepen wanneer een ander object de trigger raakt
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(playerTag))
            {
                if (objectToDelete != null)
                {
                    // Speel het geluid af
                    PlayPickupSound();

                    // Verwijder het object na een korte vertraging
                    Destroy(objectToDelete);

                    // Verhoog de score met 1
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

        // Functie om het geluidseffect af te spelen
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
