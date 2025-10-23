using UnityEngine;

public class Opponent : MonoBehaviour
{
    [SerializeField] private GameObject[] _objPiles;
    [SerializeField] private Pile[] _sPiles;
    [SerializeField] private Deck _deck;
    private int _timeToThink;
    private bool _thinking;
    public bool IsThinking => _thinking;
    private BCard _currentCard;
    private int _lowestPile = 0;
    private int _selectedCard;

    public Pile PileL{get{return _sPiles[0]; }}
    public Pile PileC{get{return _sPiles[1]; }}
    public Pile PileR{get{return _sPiles[2]; }}

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float ThinkingTime()
    {
        _thinking = true;
        return Random.Range(.1f, 1f);
    }

    public void TakeTurn()
    {
        Debug.Log("opponent turn");
        SelectPile();
        SelectCard();
        PlayCard();
    }

    public void SelectPile()
    {
        _lowestPile = Random.Range(0, 3);
    }

    public void SelectCard()
    {
        _selectedCard = Random.Range(0, _deck.HandSize);
        if(_deck.Hand[_selectedCard] == null)
        {
            for(int i = 0; i < _deck.HandSize; i++)
            {
                if (_deck.Hand[i] != null)
                    _selectedCard = i;
            }
        }
    }
    
    public void PlayCard()
    {
        _currentCard = _deck.CARDS[_selectedCard].GetComponent<BCard>();

        _currentCard.MoveTo(_objPiles[_lowestPile].transform.position);
        _sPiles[_lowestPile].AddToPile(_deck.CARDS[_selectedCard], _currentCard);

        _deck.RemoveFromHand(_currentCard.IDX);
        _thinking = false;
    }
}
