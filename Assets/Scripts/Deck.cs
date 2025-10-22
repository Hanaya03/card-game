using UnityEngine;
using System.Collections.Generic;

public class Deck : MonoBehaviour
{
    public bool faceDown;
    public GameManager _manager;
    public Transform ui;
    public DeckScriptableObject References;
    private int _handSize = 5;
    public int HandSize{get{ return _handSize; }}
    private int _deckSize = 32;
    public int DeckSize{get{ return _deckSize; }}
    [SerializeField] private BCard[] _cards;
    private BCard[] _hand = new BCard[5];
    public BCard[] Hand{ get{ return _hand; } }
    private GameObject[] _cardArr = new GameObject[5];
    public GameObject[] CARDS { get{ return _cardArr; }}
    private GameObject tmp;
    public int _cardY;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShuffleDeck();
        FillHand();
        DisplayCards();
    }

    private void DisplayCards()
    {
        BCard tmpCard;
        Quaternion q = Quaternion.identity;

        if (faceDown)
            q = Quaternion.Inverse(q);

        
        for (int i = 0; i < _handSize; i++)
        {
            switch (_hand[i].Name)
            {
                case "Son Goku":
                    _cardArr[i] = Instantiate(References.Deck[1], new Vector3(i - 2, _cardY, 0), q, gameObject.transform);
                    break;
                case "Ancient Arch Wizard":
                    _cardArr[i] = Instantiate(References.Deck[0], new Vector3(i - 2, _cardY, 0), q, gameObject.transform);
                    break;
                case "King Rex":
                    _cardArr[i] = Instantiate(References.Deck[2], new Vector3(i - 2, _cardY, 0), q, gameObject.transform);
                    break;
                case "Great Pirate Overlord":
                    _cardArr[i] = Instantiate(References.Deck[3], new Vector3(i - 2, _cardY, 0), q, gameObject.transform);
                    break;
            }

            tmpCard = _cardArr[i].GetComponent<BCard>();

            tmpCard.IDX = i;
            tmpCard._manager = _manager;
            tmpCard.DECK = this;
        }
    }
    
    private void DisplayCard(int idx)
    {
        BCard tmpCard;

        switch (_hand[idx].Name)
        {
            case "Son Goku":
                _cardArr[idx] = Instantiate(References.Deck[1], new Vector3(idx - 2, _cardY, 0), Quaternion.identity, gameObject.transform);
                break;
            case "Ancient Arch Wizard":
                _cardArr[idx] = Instantiate(References.Deck[0], new Vector3(idx - 2, _cardY, 0), Quaternion.identity, gameObject.transform);
                break;
            case "King Rex":
                _cardArr[idx] = Instantiate(References.Deck[2], new Vector3(idx - 2, _cardY, 0), Quaternion.identity, gameObject.transform);
                break;
            case "Great Pirate Overlord":
                _cardArr[idx] = Instantiate(References.Deck[3], new Vector3(idx - 2, _cardY, 0), Quaternion.identity, gameObject.transform);
                break;
        }

        tmpCard = _cardArr[idx].GetComponent<BCard>();
        
        tmpCard.IDX = idx;
        tmpCard._manager = _manager;
        tmpCard.DECK = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void DrawCard(int idx)
    {
        if (DeckSize == 0)
            return;
        _hand[idx] = _cards[_deckSize - 1];
        _deckSize -= 1;
        DisplayCard(idx);
    }

    private void FillHand()
    {
        for (int i = 0; i < _handSize; i++)
        {
            _hand[i] = _cards[_deckSize - i - 1];
        }
        _deckSize = _deckSize - _handSize;
    }

    public void RemoveFromHand(int idx)
    {
        _hand[idx] = null;
        DrawCard(idx);
    }

    public void ShuffleDeck()
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

        // ListDeck();

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
