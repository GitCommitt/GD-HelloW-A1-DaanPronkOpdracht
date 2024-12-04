using UnityEngine;
using TMPro;

namespace MyGameNamespace
{
    public class ScoreManager : MonoBehaviour
    {
        [Tooltip("Sleep hier de TextMeshPro-component die de score toont.")]
        public TextMeshProUGUI scoreText;

        [Tooltip("Het totale aantal objecten dat verzameld moet worden.")]
        public int totalObjects = 4;

        [Tooltip("Het geluid dat moet worden afgespeeld als alle objecten zijn verzameld.")]
        public AudioClip completeSound;

        [Tooltip("Het geluid dat moet worden afgespeeld bij het verzamelen van elk object.")]
        public AudioClip collectSound;

        private AudioSource audioSource;
        private int collectedObjects = 0;

        private void Start()
        {
            // Zorg ervoor dat er een AudioSource-component is
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                // Voeg een AudioSource-component toe als deze nog niet bestaat
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            // Initialiseer de scoretekst bij het begin van het spel
            UpdateScoreText();
        }

        // Functie om de score te verhogen en de tekst bij te werken
        public void AddScore(int amount)
        {
            // Verhoog het aantal verzamelde objecten
            collectedObjects += amount;

            // Speel het geluid af voor elke verzamelde object
            PlayCollectSound();

            // Werk de tekst bij
            UpdateScoreText();

            // Controleer of alle objecten zijn verzameld
            if (collectedObjects >= totalObjects)
            {
                PlayCompleteSound();
            }
        }

        // Functie om de scoretekst bij te werken in het formaat '1/4'
        private void UpdateScoreText()
        {
            if (scoreText != null)
            {
                scoreText.text = collectedObjects + "/" + totalObjects;
            }
            else
            {
                Debug.LogWarning("ScoreText is niet ingesteld in de Inspector.");
            }
        }

        // Functie om het geluid af te spelen wanneer een object wordt verzameld
        private void PlayCollectSound()
        {
            if (collectSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(collectSound);
            }
            else
            {
                Debug.LogWarning("CollectSound of AudioSource is niet ingesteld.");
            }
        }

        // Functie om het geluid af te spelen als alle objecten zijn verzameld
        private void PlayCompleteSound()
        {
            if (completeSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(completeSound);
            }
            else
            {
                Debug.LogWarning("CompleteSound of AudioSource is niet ingesteld.");
            }
        }
    }
}
