using System;

using UnityEngine;
using Zilon.Core.Combat;

public class CombatPersonModel : MonoBehaviour
{
    public GameObject Graphics;
    public GameObject CorpsePrefab;

    private ICombatPerson _combatPerson;
    private bool _isDead;

    public event EventHandler Clicked;
    public event EventHandler Dead;

    public void OnMouseDown()
    {
        Clicked?.Invoke(this, new EventArgs());
    }

    public void Init(ICombatPerson combatPerson)
    {
        _combatPerson = combatPerson;
        _combatPerson.TakenDamage += CombatPersonOnTakenDamage;
    }

    private void CombatPersonOnTakenDamage(object sender, TakeDamageEventArgs e)
    {
        Debug.Log(ToString() + " " + e.Value);
        if (e.Dead)
        {
            _isDead = true;
            Dead?.Invoke(this, new EventArgs());
            CreateCorpse();
            Destroy(gameObject);
        }
    }

    private void CreateCorpse()
    {
        var corpseObject = Instantiate(CorpsePrefab, transform.parent);
        corpseObject.transform.position = transform.position;
        Graphics.transform.SetParent(corpseObject.transform);
    }
}
