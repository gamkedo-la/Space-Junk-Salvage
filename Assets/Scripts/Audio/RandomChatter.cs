using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomChatter : MonoBehaviour
{
    public AudioSource audioSource;
    public ChatterEntry[] entries;
    public float initialDelay;
    [Header("Delay between chatters")] public float minDelay;
    public float maxDelay;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(initialDelay);
        
        while (true)
        {
            var totalWeight = 0f;
            foreach (var entry in entries)
            {
                totalWeight += entry.weight;
            }

            var pick = Random.Range(0f, totalWeight);

            foreach (var entry in entries)
            {
                if (entry.weight >= pick)
                {
                    audioSource.PlayOneShot(entry.clip);
                    Debug.Log(entry.text);
                    break;
                }

                pick -= entry.weight;
            }

            yield return null;

            while (audioSource.isPlaying)
            {
                yield return null;
            }

            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
    }

    [Serializable]
    public struct ChatterEntry
    {
        [Range(0, 1)] public float weight;
        public AudioClip clip;
        public string text;
    }
}