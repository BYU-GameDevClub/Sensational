using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // private bool up;
    // private bool down;
    // private bool left;
    // private bool right;
    public Vector2 moveVal = Vector2.zero;
    [SerializeField] float moveSpeed = 10;

    private PhillipActions actions;


    void Awake()
    {
        actions = new PhillipActions();
    }

    void OnEnable()
    {
        actions.Enable();
    }

    void OnDisable()
    {
        actions.Disable();
    }

    
    void FixedUpdate()
    {
        Move();
    }

    public void Interact(InputAction.CallbackContext context)
    {
        Debug.Log("Interacted! Phase: " + context.phase);

    }

    public void SetMoveDirection(InputAction.CallbackContext value)
    {
        moveVal = value.ReadValue<Vector2>();
        //transform.Translate(new Vector2(moveVal.x, moveVal.y) * moveSpeed * Time.fixedDeltaTime);
    }

    public void Move()
    {
        transform.Translate(new Vector2(moveVal.x, moveVal.y) * moveSpeed * Time.fixedDeltaTime);
    }
}
