using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip mainAudioClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Image musicImage;
    [SerializeField] private Sprite activeMusicSprite;
    [SerializeField] private Sprite notActiveMusicSprite;

    private void Start()
    {
        audioSource.clip = mainAudioClip;
        audioSource.Play();
    }
    public void ChangeActivityStateMusic()
    {
        if (audioSource.enabled)
        {
            audioSource.enabled = false;
            musicImage.sprite = notActiveMusicSprite;
        }
        else
        {
            audioSource.enabled = true;
            musicImage.sprite = activeMusicSprite;
        }
    }
}
