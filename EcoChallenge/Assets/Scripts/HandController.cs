using System.Collections;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private Sprite _handIdle;
    [SerializeField] private Sprite _handGrab;
    [SerializeField] private float smoothness = 0.1f;
    [SerializeField] private float returnSpeed = 5f; 

    [SerializeField] private AudioClip _correctSFX;
    [SerializeField] private AudioClip _wrongSFX;

    private SpriteRenderer _spriteRenderer;
    private Vector3 targetPosition;
    private GameObject draggedObject;
    private Vector3 originalPosition; 
    [SerializeField] private bool isReturning; 

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        FreezeHand();
    }
    private void Update()
    {
        targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0; // Keep hand on the same Z-axis
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothness);

        if (Input.GetMouseButton(0))
        {
            _spriteRenderer.sprite = _handGrab;

            if (draggedObject == null && !isReturning)
            {
                DetectAndStartDragging();
            }
            else if (draggedObject != null && !isReturning)
            {
                draggedObject.transform.position = transform.position;
            }
        }
        else
        {
            _spriteRenderer.sprite = _handIdle;

            if (draggedObject != null && !isReturning)
            {
                TryDropInBinOrReset();
            }
        }
    }

    private void DetectAndStartDragging()
    {
        RaycastHit2D hit = Physics2D.Raycast(targetPosition, Vector2.zero);
        
        if (hit.collider != null && hit.collider.CompareTag("Garbage"))
        {
            draggedObject = hit.collider.gameObject;
            originalPosition = draggedObject.transform.position; 
        }
    }

    private void TryDropInBinOrReset()
    {
        Collider2D binCollider = Physics2D.OverlapPoint(transform.position, LayerMask.GetMask("Bin"));
        
        if (binCollider != null && binCollider.CompareTag("Bin"))
        {
            Debug.Log("Dropped in bin!");
            if (binCollider.GetComponent<Bin>().Type == draggedObject.GetComponent<Garbage>().MatchingBinType)
            {
                Debug.Log("Correct bin!");
                ScoreManager.Instance.IncreaseScore(1);
                ScoreManager.Instance.IncreaseScoreLevel1(1);
                PopupHandler.Instance.SpawnCorrectPopup(transform.position, 1f, true);
                AudioManager.Instance.PlayEffect(_correctSFX);

                GarbageSpawner.Instance.DestroyGarbage(draggedObject);
                draggedObject = null; // Clear the dragged object
            }
            else
            {
                Debug.Log("Incorrect bin!");
                ScoreManager.Instance.DecreaseScore(1);
                ScoreManager.Instance.DecreaseScoreLevel1(1);
                PopupHandler.Instance.SpawnWrongPopup(transform.position, 1f, true);
                PopupHandler.Instance.SpawnPopupText(draggedObject.GetComponent<Garbage>().GetItemNameThai() + " - " + draggedObject.GetComponent<Garbage>().GetMatchingBinTypeThai(), transform.position, 30);
                AudioManager.Instance.PlayEffect(_wrongSFX);

                GarbageSpawner.Instance.DestroyGarbage(draggedObject);
                draggedObject = null; // Clear the dragged object
            }

            if(GarbageSpawner.Instance.IsNoGarbageLeft())
            {
                GameManager.Instance.SetGameState(GameState.Result);
            }
        }
        else
        {
            // Not in bin - start coroutine to return to original position
            StartCoroutine(ReturnToOriginalPosition());
            Debug.Log("Returning to original position...");
        }
        
    }

    private IEnumerator ReturnToOriginalPosition()
    {
        isReturning = true; 

        while (draggedObject != null && Vector3.Distance(draggedObject.transform.position, originalPosition) > 0.01f)
        {
            draggedObject.transform.position = Vector3.Lerp(draggedObject.transform.position, originalPosition, returnSpeed * Time.deltaTime);
            yield return null;
        }

        if (draggedObject != null)
        {
            draggedObject.transform.position = originalPosition;
        }

        draggedObject = null;
        isReturning = false;
    }

    public void FreezeHand()
    {
        enabled = false;
        Cursor.visible = true;
    }

    public void UnfreezeHand()
    {
        enabled = true;
        Cursor.visible = false;
    }
}
