using UnityEngine;

public class SpawnBullet : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject bulletOrigin;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void ShootBullet(Vector3 target)
    {
        Instantiate(bulletPrefab, transform.position, (Quaternion.LookRotation(target - transform.position)));
    }
}