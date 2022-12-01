using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Vector2 rawInput;
    [SerializeField] private float moveSpeed;

    private Vector2 minBounds;
    private Vector2 maxBounds;

    [SerializeField] private float leftPadding;
    [SerializeField] private float rightPadding;
    [SerializeField] private float upPadding;
    [SerializeField] private float downPadding;

    private Shooter shooter;

    private void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    private void Start()
    {
        SetBounds();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 delta = rawInput * moveSpeed * Time.deltaTime;
        Vector2 newPosition = new Vector2();
        newPosition.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + leftPadding, maxBounds.x - rightPadding);
        newPosition.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + downPadding, maxBounds.y - upPadding);

        transform.position = newPosition;
    }

    private void SetBounds()
    {
        Camera mainCamera = Camera.main;

        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    private void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }    

    private void OnFire(InputValue value)
    {
        if (shooter != null)
        {
            shooter.IsShooting = value.isPressed;
        }
    }
}
