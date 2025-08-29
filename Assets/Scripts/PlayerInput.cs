using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public bool Shoot { get; private set; }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw(Defines.horizontal);
        Vertical = Input.GetAxisRaw(Defines.vertical);

        if (Input.GetMouseButton(0))
            Shoot = true;
    }
}
