using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zilon.Core.Combat;
using Zilon.Core.Spatial;

public class CombatPersonModel : MonoBehaviour
{
    public GameObject Graphics;
    public GameObject CorpsePrefab;

    public ICombatPerson CombatPerson { get; private set; }
    private bool _isDead;

    public event EventHandler Clicked;
    public event EventHandler Dead;
    public event EventHandler<SkillUsedEventArgs> SkillUsed;

    public void OnMouseDown()
    {
        Clicked?.Invoke(this, new EventArgs());
    }

    public void Init(ICombatPerson combatPerson)
    {
        CombatPerson = combatPerson;

        CombatPerson.SkillUsed += CombatPersonOnSkillUsed;
        CombatPerson.TakenDamage += CombatPersonOnTakenDamage;
    }

    private void CombatPersonOnSkillUsed(object sender, SkillUsedEventArgs e)
    {
        SkillUsed?.Invoke(this, e);
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
