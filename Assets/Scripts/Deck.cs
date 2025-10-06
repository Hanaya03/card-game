using UnityEngine;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    private int _handSize = 5;
    private int _deckSize = 32;
    private BCard[] _cards = new BCard[32];
    private BCard[] _hand = new BCard[5];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            _cards[i * 4] = new GokuCard();
            _cards[i * 4 + 1] = new WizardCard();
            _cards[i * 4 + 2] = new RexCard();
            _cards[i * 4 + 3] = new PirateCard();
        }

        FillHand();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DrawCard()
    {

    }

    private void FillHand()
    {
        for (int i = 0; i < _handSize; i++)
        {
            _hand[i] = _cards[_deckSize - i - 1];
        }
        _deckSize = _deckSize - _handSize;
    }

    private void ShuffleDeck()
    {
        //Fisher–Yates shuffle
        BCard tmp;
        int idx;

        for (int i = 0; i < _deckSize - 2; i++)
        {
            idx = Random.Range(0, _deckSize);
            tmp = _cards[idx];
            _cards[idx] = _cards[i];
            _cards[i] = tmp;
        }

    }

    private void ListDeck()
    {
        for (int i = 0; i < _deckSize; i++)
        {
            Debug.Log(_cards[i].Name);
        }
    }

    private void ListHand()
    {
        for (int i = 0; i < _handSize; i++)
        {
            Debug.Log(_hand[i].Name);
        }
    }
}
