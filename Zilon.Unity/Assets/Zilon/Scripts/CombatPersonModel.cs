using System;

using UnityEngine;

using Zilon.Core.Combat;

public class CombatPersonModel : MonoBehaviour
{
    public GameObject Graphics;
    public GameObject CorpsePrefab;

    public ICombatPerson CombatPerson { get; private set; }
    private bool _isDead;

    public event EventHandler Clicked;
    public event EventHandler Dead;

    public void OnMouseDown()
    {
        Clicked?.Invoke(this, new EventArgs());
    }

    public void Init(ICombatPerson combatPerson)
    {
        CombatPerson = combatPerson;
    }

    public void ProcessDead()
    {
        CreateCorpse();
        Destroy(gameObject);
    }

    private void CreateCorpse()
    {
        var corpseObject = Instantiate(CorpsePrefab, transform.parent);
        corpseObject.transform.position = transform.position;
        Graphics.transform.SetParent(corpseObject.transform);
    }
}
