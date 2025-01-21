using UnityEngine;
using TMPro;

namespace MyGameNamespace
{
    public class ScoreManager : MonoBehaviour
    {
        public TextMeshProUGUI scoreText;
        public int totalObjects = 4;
        public AudioClip completeSound;
        public AudioClip collectSound;

        private AudioSource audioSource;
        private int collectedObjects = 0;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }

            UpdateScoreText();
        }

        public void AddScore(int amount)
        {
            collectedObjects += amount;
            PlayCollectSound();
            UpdateScoreText();

            if (collectedObjects >= totalObjects)
            {
                PlayCompleteSound();
            }
        }

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
