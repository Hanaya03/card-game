using UnityEngine;
using UnityEngine.EventSystems;

public class BCard : MonoBehaviour
{
    [SerializeField] private AudioClip _cardGrabSFX;
    [SerializeField] private AudioClip _cardDropSFX;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private CardScriptableObject _cardData;
    public bool _moving = false;
    protected Vector3 targetPosition;
    protected Vector3 _posOriginal;
    public GameObject _targetPile;
    public GameManager _manager;
    public Deck DECK;
    protected int _value;
    protected string _name;
    protected int _idx;
    protected bool _dragging;
    public bool Dragging{set{ _dragging = value; }}
    protected RectTransform rectTransform;
    public int IDX { get => _idx; set => _idx = value; }
    public int Value { get { return _cardData.Value; } }
    public string Name { get { return _cardData.Name; } }

    void Start()
    {
    }

    public void OnDrop()
    {
        _dragging = false;
        if(_targetPile == null)
        {
            transform.position = _posOriginal;
        }
        else
        {
            transform.position = _targetPile.transform.position;
            _audioSource.PlayOneShot(_cardDropSFX, 1);
            _manager.ReferencePile(gameObject, this, _targetPile);
            DECK.RemoveFromHand(_idx);
        }
    }
    
    public void PickUpCard()
    {
        _posOriginal = transform.position;
        _audioSource.PlayOneShot(_cardGrabSFX, 1);
        _dragging = true;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Pile")
        {
            _targetPile = collider.gameObject;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        _targetPile = null;
    }
    
    public void MoveTo(Vector3 pos)
    {
        targetPosition = pos;
        _moving = true;
    }


    void Update()
    {
        if (!_dragging) return;
        Vector3 _newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(_newPos.x, _newPos.y, 0);
        // if (Input.GetMouseButtonUp(0))
        // {
        //     MouseUp();
        // }
    }
    
    void FixedUpdate()
    {
        if (_moving)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 2);
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                transform.position = targetPosition;
                _moving = false;
            }
        }
    }
}
