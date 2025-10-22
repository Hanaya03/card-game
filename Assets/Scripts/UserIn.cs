using UnityEngine;
using UnityEngine.InputSystem;

public class UserIn : MonoBehaviour
{
    private InputSystem_Actions controls;
    private InputAction _mouse;
    private Ray ray;
    private BCard _target = null;

    private void Awake()
    {
        controls = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        _mouse = controls.UI.Click;
        _mouse.Enable();
        _mouse.performed += ctx => { CheckTarget(); };
        _mouse.canceled += ctx => { CloseTarget(); };
    }

    private void OnDisable()
    {
        _mouse.Disable();
    }

    private void CheckTarget()
    {
        RaycastHit raycastHit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out raycastHit, 100f))
        {
            if (raycastHit.transform.CompareTag("Card"))
            {
                //Our custom method. 
                _target = raycastHit.transform.gameObject.GetComponent<BCard>();
                _target.PickUpCard();
            }
        }
    }

    private void CloseTarget()
    {
        if (_target != null)
        {
            _target.OnDrop();
            _target = null;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
