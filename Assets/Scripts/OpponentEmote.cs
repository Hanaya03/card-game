using UnityEngine;
using System.Collections;

/*
/allow the opponent to communicate with the player using simple emotes
*/
public class OpponentEmote : MonoBehaviour
{
    [SerializeField] private GameObject _textBox;
    [SerializeField] private GameObject _gloatEmote;
    [SerializeField] private GameObject _sadEmote;
    [SerializeField] private AudioClip _gloatSFX;
    [SerializeField] private AudioClip _sadSFX;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private float _emoteTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayHappyEmote()
    {
        _textBox.SetActive(true);
        _gloatEmote.SetActive(true);
        _audioSource.PlayOneShot(_gloatSFX);
        StartCoroutine(TurnOffEmotes());
    }

    public void PlaySadEmote()
    {
        _textBox.SetActive(true);
        _sadEmote.SetActive(true);
        _audioSource.PlayOneShot(_sadSFX);
        StartCoroutine(TurnOffEmotes());
    }

    IEnumerator TurnOffEmotes()
    {
        yield return new WaitForSeconds(_emoteTime);
        _textBox.SetActive(false);
        _gloatEmote.SetActive(false);
        _sadEmote.SetActive(false);
    }
}
