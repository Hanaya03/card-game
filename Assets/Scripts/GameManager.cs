using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    private int _vicroys = 0;
    private int _roundsWon = 0;
    private int _turnCount = 0;
    private bool playerTurn = true;
    public GameObject _selectedCard;
    [SerializeField] private AudioClip _cardMovingSFX;
    [SerializeField] private AudioSource _audioSource; 
    [SerializeField] private Pile _pileR;
    [SerializeField] private Pile _pileC;
    [SerializeField] private Pile _pileL;
    [SerializeField] private Opponent _opp;

    public Dictionary<string, Pile> _piles = new Dictionary<string, Pile>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _piles.Add("Pile_Player_L", _pileL);
        _piles.Add("Pile_Player_C", _pileC);
        _piles.Add("Pile_Player_R", _pileR);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!playerTurn && !_opp.IsThinking)
        {
            StartCoroutine(OpponentTurn());
        }
    }
    
    private void EndRound()
    {
        if (_pileL.PointSum > _opp.PileL.PointSum)
        {
            _vicroys++;
            Debug.Log("Player won left lane");
        }
        if (_pileC.PointSum > _opp.PileC.PointSum)
        {
            _vicroys++;
            Debug.Log("Player won center lane");
        }
        if (_pileR.PointSum > _opp.PileR.PointSum)
        {
            _vicroys++;
            Debug.Log("Player won right lane");
        }

        if (_vicroys > 1)
        {
            _roundsWon++;
            Debug.Log("Player won this round");
        }

        if(_roundsWon > 1)
        {
            //player won the game
        }

        _vicroys = 0;

        _pileC.EmptyPile();
        _pileL.EmptyPile();
        _pileR.EmptyPile();
        _opp.PileC.EmptyPile();
        _opp.PileL.EmptyPile();
        _opp.PileR.EmptyPile();

        _audioSource.PlayOneShot(_cardMovingSFX, 1);
    }

    public void ReferencePile(GameObject cardObj, BCard card, GameObject pile)
    {
        _piles[pile.name].AddToPile(cardObj, card);
        playerTurn = !playerTurn;
    }

    IEnumerator OpponentTurn()
    {
        yield return new WaitForSeconds(_opp.ThinkingTime());
        _opp.TakeTurn();
            
        playerTurn = true;
        _turnCount++;
        if (_turnCount % 5 == 0)
        {
            yield return new WaitForSeconds(.5f);
            EndRound();
            _turnCount = 0;
        }
    }
}
