using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
/Keeps track of turn order, when to end rounds, and when to end the game
/all win and lose condition logic is stored here.
*/

public class GameManager : MonoBehaviour
{
    private int _vicroys = 0;
    private int _oVicroys = 0;
    private int _roundsWon = 0;
    private int _oRoundsWon = 0;
    private int _turnCount = 0;
    private bool playerTurn = true;
    public GameObject _selectedCard;
    [SerializeField] private UserIn _in;
    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _winIMG;
    [SerializeField] private GameObject _loseIMG;
    [SerializeField] private AudioClip _cardMovingSFX;
    [SerializeField] private AudioClip _loseSFX;
    [SerializeField] private AudioClip _victorySFX;
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

        UpdateColors();

        if (!playerTurn && !_opp.IsThinking)
        {
            StartCoroutine(OpponentTurn());
        }
    }

    private void UpdateColors()
    {
        if (_pileL.PointSum > _opp.PileL.PointSum)
        {
            _pileL.ChangeColor(Color.green);
            _opp.PileL.ChangeColor(Color.red);
        }
        else if (_pileL.PointSum == _opp.PileL.PointSum)
        {
            _pileL.ChangeColor(Color.white);
            _opp.PileL.ChangeColor(Color.white);
        }
        else if (_pileL.PointSum < _opp.PileL.PointSum)
        {
            _pileL.ChangeColor(Color.red);
            _opp.PileL.ChangeColor(Color.green);
        }
        if (_pileC.PointSum > _opp.PileC.PointSum)
        {
            _pileC.ChangeColor(Color.green);
            _opp.PileC.ChangeColor(Color.red);
        }
        else if (_pileC.PointSum == _opp.PileC.PointSum)
        {
            _pileC.ChangeColor(Color.white);
            _opp.PileC.ChangeColor(Color.white);
        }
        else if (_pileC.PointSum < _opp.PileC.PointSum)
        {
            _pileC.ChangeColor(Color.red);
            _opp.PileC.ChangeColor(Color.green);
        }
        if (_pileR.PointSum > _opp.PileR.PointSum)
        {
            _pileR.ChangeColor(Color.green);
            _opp.PileR.ChangeColor(Color.red);
        }
        else if (_pileR.PointSum == _opp.PileR.PointSum)
        {
            _pileR.ChangeColor(Color.white);
            _opp.PileR.ChangeColor(Color.white);
        }
        else if (_pileR.PointSum < _opp.PileR.PointSum)
        {
            _pileR.ChangeColor(Color.red);
            _opp.PileR.ChangeColor(Color.green);
        }
    }

    private void EndRound()
    {
        if (_pileL.PointSum > _opp.PileL.PointSum)
        {
            _vicroys++;
            Debug.Log("Player won left lane");
        }
        else if (_pileL.PointSum < _opp.PileL.PointSum)
        {
            _oVicroys++;
        }
        if (_pileC.PointSum > _opp.PileC.PointSum)
        {
            _vicroys++;
            Debug.Log("Player won center lane");
        }
        else if (_pileC.PointSum < _opp.PileC.PointSum)
        {
            _oVicroys++;
        }
        if (_pileR.PointSum > _opp.PileR.PointSum)
        {
            _vicroys++;
            Debug.Log("Player won right lane");
        }
        else if (_pileR.PointSum < _opp.PileR.PointSum)
        {
            _oVicroys++;
        }

        if (_vicroys > 1)
        {
            _roundsWon++;
            Debug.Log("Player won this round");
            _opp.OnLose();
        }
        else if (_oVicroys > 1)
        {
            _opp.OnWin();
            _oRoundsWon++;
        }

        if (_roundsWon > 1)
        {
            _winIMG.SetActive(true);
            _audioSource.PlayOneShot(_victorySFX);
            _playButton.SetActive(true);
            _in.enabled = false;
        }
        else if (_oRoundsWon > 1)
        {
            _loseIMG.SetActive(true);
            _audioSource.PlayOneShot(_loseSFX);
            _playButton.SetActive(true);
            _in.enabled = false;
        }

        _vicroys = 0;
        _oVicroys = 0;

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
