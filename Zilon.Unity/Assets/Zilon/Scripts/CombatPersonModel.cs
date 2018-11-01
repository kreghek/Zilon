using System;

using JetBrains.Annotations;

using UnityEngine;

using Zilon.Core.Combat;
// ReSharper disable CheckNamespace

public class CombatPersonModel : MonoBehaviour
{
    public GameObject Graphics;
    public GameObject CorpsePrefab;

    public ICombatPerson CombatPerson { get; private set; }

    public event EventHandler Clicked;
    public event EventHandler HoverEnter;
    public event EventHandler HoverExit;

    [UsedImplicitly]
    public void OnMouseDown()
    {
        Clicked?.Invoke(this, new EventArgs());
    }

    [UsedImplicitly]
    public void OnMouseEnter()
    {
        HoverEnter?.Invoke(this, new EventArgs());
    }

    [UsedImplicitly]
    public void OnMouseExit()
    {
        HoverExit?.Invoke(this, new EventArgs());
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
