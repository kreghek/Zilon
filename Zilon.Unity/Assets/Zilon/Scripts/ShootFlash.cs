using UnityEngine;

public class ShootFlash : MonoBehaviour
{
    private float _lifetime = 0.05f;

    void Update()
    {
        _lifetime -= Time.deltaTime;
        if (_lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
