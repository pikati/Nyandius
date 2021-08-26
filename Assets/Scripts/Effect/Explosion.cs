using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("End", 0.3f);
    }
    private void End()
    {
        Destroy(gameObject);
    }
}
