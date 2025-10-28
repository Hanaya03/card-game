using UnityEngine;
using UnityEngine.EventSystems;

/*
/Base card Script.
/since all card logic is the same, only difference being the point value and name, we pull the point value 
/and name from a scriptable object
*/

public class BCard : MonoBehaviour
{
    [SerializeField] private AudioClip _cardGrabSFX;
    [SerializeField] private AudioClip _cardDropSFX;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private CardScriptableObject _cardData;
    private Pile _sPile;
    private float _zOffset;
    public float Z { get { return _zOffset; } set { _zOffset = value; } }
    public bool _moving = false;
    private Vector3 targetPosition;
    private Vector3 _posOriginal;
    public GameObject _targetPile;
    public GameManager _manager;
    public Deck DECK;
    private int _value;
    private string _name;
    private int _idx;
    private bool _dragging;
    public bool Dragging { set { _dragging = value; } }
    private RectTransform rectTransform;
    public int IDX { get => _idx; set => _idx = value; }
    public int Value { get { return _cardData.Value; } }
    public string Name { get { return _cardData.Name; } }

    void Start()
    {
    }

    public void OnDrop()
    {
        if (_sPile != null)
            _sPile.DeactivateHighlight();
        _dragging = false;
        if (_targetPile == null)
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
            _sPile = _targetPile.GetComponent<Pile>();
            if (_dragging)
                _sPile.ActivateHighlight();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (_dragging)
            _targetPile.GetComponent<Pile>().DeactivateHighlight();
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
