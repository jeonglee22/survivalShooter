using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }
    public bool Shoot { get; private set; }

	public UIManager uIManager;

	// Update is called once per frame
	void Update()
    {
        if(uIManager.IsPaused) return;

        Horizontal = Input.GetAxis(Defines.horizontal);
        Vertical = Input.GetAxis(Defines.vertical);

        Shoot = Input.GetMouseButton(0);
    }
}
