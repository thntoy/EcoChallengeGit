using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TruckController : MonoBehaviour
{
    public static TruckController Instance;
    public bool Freeze;

    [SerializeField] private float speed = 17.5f;
    [SerializeField] private float _minClampX;
    [SerializeField] private float _maxClampX;
    private float _horizontalInput;
    private Vector3 _direction;
    private SpriteRenderer _spriteRenderer;

    private Collider2D _selectedGarbage;  // Variable to keep track of the selected garbage
    private Collider2D _lastSelectedGarbage; // Variable to keep track of the last selected garbage
    private GameObject _popupKey;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _direction = Vector3.right;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        FreezeTruck();
    }

    private void OnEnable()
    {
        GameManager.OnTimesUp += FreezeTruck;
    }

    private void OnDisable()
    {
        GameManager.OnTimesUp -= FreezeTruck;
    }

    private void Update()
    {
        if(!Freeze)
        {
            _horizontalInput = Input.GetAxis("Horizontal");
            transform.position += new Vector3(_horizontalInput, 0, 0) * speed * Time.deltaTime;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, _minClampX, _maxClampX), transform.position.y, transform.position.z);

            // Direction and sprite flipping
            if (_horizontalInput < 0)
            {
                _spriteRenderer.flipX = true;
                _direction = Vector3.left;
            }
            else if (_horizontalInput > 0)
            {
                _spriteRenderer.flipX = false;
                _direction = Vector3.right;
            }
            

            // Detect the closest garbage object
            _selectedGarbage = FindClosestGarbage();

            // Check if the selected garbage has changed
            if (_lastSelectedGarbage != _selectedGarbage)
            {
                // Reset the scale and remove popup of the last selected garbage
                if (_lastSelectedGarbage != null)
                {
                    _lastSelectedGarbage.transform.localScale = Vector3.one;
                    
                    if (_popupKey != null)
                    {
                        Destroy(_popupKey);
                    }
                }

                // Scale up the newly selected garbage and spawn popup only once
                if (_selectedGarbage != null)
                {
                    _selectedGarbage.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    _popupKey = PopupHandler.Instance.SpawnPopupKey("E", _selectedGarbage.transform.position + new Vector3(0, 2, 0), 30);
                    _popupKey.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        GarbageQuestionController.Instance.ShowQuestion(_selectedGarbage.GetComponent<Garbage>().ItemName); // Show the question for the selected garbage
                        Freeze = true;
                        GarbageSpawner.Instance.DestroyGarbage(_selectedGarbage.gameObject); 
                        Destroy(_popupKey);
                        _selectedGarbage = null; // Clear the selected garbage after destroying it
                        _lastSelectedGarbage = null; // Clear the last selected garbage as well
                    });
                }
                
                // Update the last selected garbage
                _lastSelectedGarbage = _selectedGarbage;
            }

            // If E is pressed and garbage is selected, destroy it and show question
            if (_selectedGarbage != null && Input.GetKeyDown(KeyCode.E))
            {
                GarbageQuestionController.Instance.ShowQuestion(_selectedGarbage.GetComponent<Garbage>().ItemName); // Show the question for the selected garbage
                Freeze = true;
                GarbageSpawner.Instance.DestroyGarbage(_selectedGarbage.gameObject); 
                Destroy(_popupKey);
                _selectedGarbage = null; // Clear the selected garbage after destroying it
                _lastSelectedGarbage = null; // Clear the last selected garbage as well
                
            }
        }
    }

    private Collider2D FindClosestGarbage()
    {
        Collider2D closestGarbage = null;
        float closestDistance = Mathf.Infinity;

        // Check for garbage within a certain distance
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(transform.position + (_direction * 5), new Vector2(1, 5), 0);
        foreach (var collider2D in collider2Ds)
        {
            if (collider2D.CompareTag("Garbage"))
            {
                float distance = Vector2.Distance(transform.position, collider2D.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestGarbage = collider2D;
                }
            }
        }
        return closestGarbage;
    }

    public void FreezeTruck()
    {
        Freeze = true;
    }

    public void UnfreezeTruck()
    {
        Freeze = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (_direction * 5), new Vector3(1, 5, 0));

        if (_selectedGarbage != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(_selectedGarbage.transform.position, 0.5f); // Highlight the selected garbage
        }
    }


}
