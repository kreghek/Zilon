using UnityEngine;

public class CorpseModel : MonoBehaviour
{
    private float counter = 0;

    void Update()
    {
        counter += Time.deltaTime * 2;
        if (counter > 1)
        {
            counter = 1;
        }

        var targetRotation = Quaternion.AngleAxis(90, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, counter);

        if (counter > 1)
        {
            Destroy(this);
        }
    }
}
