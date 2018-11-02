using UnityEngine;
using UnityEngine.UI;

public class DamageIndicator : MonoBehaviour
{
    private Vector3 _targetPosition;
    private float _counter;

    public Text DamageText;
    public Canvas Canvas;

    public void SetDamage(int value)
    {
        DamageText.text = value.ToString();
    }

    public void Start()
    {
        _targetPosition = transform.position + Vector3.up * 1.5f;
    }

    public void Update()
    {
        _counter += Time.deltaTime;
        if (_counter >= 1)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.deltaTime);
        }
    }
}
