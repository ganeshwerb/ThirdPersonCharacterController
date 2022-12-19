using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody bulletRB;

    // Start is called before the first frame update
    private void Start()
    {
        bulletRB.velocity = transform.forward * 30;
    }

    // Update is called once per frame
    private void Update()
    {
        Destroy(this.gameObject, 3f);
    }
}