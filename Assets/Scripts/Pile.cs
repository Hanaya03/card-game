using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

/*
/Pile script that controls the 6 piles.
/provides methods for moving cards in and out of the pile
/and changing colors
*/
public class Pile : MonoBehaviour
{
    [SerializeField] private GameObject _highlight;
    [SerializeField] private Transform _graveYard;
    [SerializeField] private Image _img;
    [SerializeField] private TextMeshProUGUI _pointCounter;
    private int _points = 0;
    private int _cardsLength;
    private GameObject[] _cards = new GameObject[10];
    private BCard[] _scards = new BCard[10];


    public int PointSum
    {
        get { return _points; }
        set { _points = value; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeColor(Color newColor)
    {
        _img.color = newColor;
    }

    public void EmptyPile()
    {
        _points = 0;
        for (int c = 0; c < _cardsLength; c++)
        {
            _scards[c].MoveTo(_graveYard.position);
        }
        _pointCounter.text = $"{_points}";
    }

    public void ActivateHighlight()
    {
        _highlight.SetActive(true);
    }

    public void DeactivateHighlight()
    {
        _highlight.SetActive(false);
    }

    public void AddToPile(GameObject cardObj, BCard card)
    {
        _points += card.Value;

        if (_cardsLength == _cards.Length)
        {
            Array.Resize(ref _cards, _cards.Length + 5);
            Array.Resize(ref _scards, _cards.Length + 5);
        }

        _scards[_cardsLength] = card;
        _cards[_cardsLength] = cardObj;
        _cardsLength++;

        _pointCounter.text = $"{_points}";
    }
}
