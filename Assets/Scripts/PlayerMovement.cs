using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput input;
    private Animator animator;
    private Rigidbody rb;

    public float moveSpeed = 5f;

    public LayerMask groundLayer;

	private void Awake()
	{
		input = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        var dir = new Vector3();
        dir = Camera.main.transform.forward * input.Vertical +
			Camera.main.transform.right * input.Horizontal;
        dir.y = 0;

        var moveAmount = dir * moveSpeed * Time.deltaTime;
		rb.MovePosition(moveAmount + transform.position);

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(mouseRay, out RaycastHit hit, float.PositiveInfinity, groundLayer))
        {
            var point = hit.point;
            point.y = transform.position.y;
            transform.LookAt(point);
        }

        animator.SetFloat(Defines.animatorMove, dir.magnitude);
    }
}
