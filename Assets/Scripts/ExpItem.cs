using UnityEngine;

public class ExpItem : MonoBehaviour
{
    public float despawnTime = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, despawnTime);
    }

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag(Defines.playerStr))
        {

        }
	}
}
