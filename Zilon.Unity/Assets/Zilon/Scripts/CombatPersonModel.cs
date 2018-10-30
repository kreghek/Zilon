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
    public GameObject ShootFlashPrefab;
    public GameObject ShootTracerPrefab;

    private IEnumerable<CombatPersonModel> _persons;
    private ICombatPerson _combatPerson;
    private bool _isDead;

    public event EventHandler Clicked;
    public event EventHandler Dead;

    public void OnMouseDown()
    {
        Clicked?.Invoke(this, new EventArgs());
    }

    public void Init(ICombatPerson combatPerson, IEnumerable<CombatPersonModel> persons)
    {
        _combatPerson = combatPerson;
        _persons = persons;

        _combatPerson.SkillUsed += CombatPersonOnSkillUsed;
        _combatPerson.TakenDamage += CombatPersonOnTakenDamage;
    }

    private void CombatPersonOnSkillUsed(object sender, SkillUsedEventArgs e)
    {
        var targetModel = _persons.Single(x => x._combatPerson == e.Target);

        CreateWeaponTracer(transform.position, targetModel.transform.position);
        CreateShootFlash(gameObject.transform.position);
        ShakeCamera(.5f, .05f);
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



    private void CreateShootFlash(Vector3 spawnPosition)
    {
        var flashObject = Instantiate(ShootFlashPrefab, transform.parent.parent);
        flashObject.transform.position = spawnPosition;
    }

    private void CreateWeaponTracer(Vector3 fromPosition, Vector3 targetPosition)
    {
        var tracerObject = Instantiate(ShootTracerPrefab, transform.parent.parent);
        var tracer = tracerObject.GetComponent<BulletTracer>();
        tracer.fromPosition = fromPosition;
        tracer.targetPosition = targetPosition;
        //flashObject.transform.position = spawnPosition;
    }



    public static void ShakeCamera(float intensity, float timer)
    {
        //Vector3 lastCameraMovement = Vector3.zero;
        //FunctionUpdater.Create(delegate () {
        //    timer -= Time.unscaledDeltaTime;
        //    Vector3 randomMovement = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized * intensity;
        //    Camera.main.transform.position = Camera.main.transform.position - lastCameraMovement + randomMovement;
        //    lastCameraMovement = randomMovement;
        //    return timer <= 0f;
        //}, "CAMERA_SHAKE");
    }
}
