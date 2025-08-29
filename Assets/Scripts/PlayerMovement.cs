using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput input;
    private Rigidbody rb;

    public float moveSpeed = 5f;

    public LayerMask groundLayer;

	private void Awake()
	{
		input = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update()
    {
        var dir = new Vector3(input.Horizontal,0,input.Vertical);
        dir = dir.normalized;
        transform.Translate(dir * moveSpeed * Time.deltaTime);

        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(mouseRay, out RaycastHit hit, float.PositiveInfinity, groundLayer))
        {
            var point = hit.point;
            point.y = transform.position.y;
            transform.LookAt(point);
        }
    }
}
