//Better Audio Source
//by Jackson
//Last edited 10:45 am 13/9/23

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BetterAudioSource : MonoBehaviour
{
    private AudioSource m_AudioSource;
    [SerializeField]
    private List<AudioClip> m_clips;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();   
    }
    
    public void PlayAudio(int index)
    {
        m_AudioSource.clip = m_clips[index];
        m_AudioSource.Play();
    }
    public void AddAudioClip(AudioClip clip)
    {
        m_clips.Add(clip);
    }
    public void AddAudioClip(AudioClip[] clips)
    {
        m_clips.AddRange(clips);
    }
}
