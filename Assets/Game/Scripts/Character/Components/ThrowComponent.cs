using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ThrowComponent : MonoBehaviour
{

    public float ThrowRate = 1f;
    public Vector2 ThrowForce;

    protected GameObject _ProjectileTemplate;

    protected Character _OwnerCharacter;

    protected float _Cooldown;

    protected int _ProjectileCount = 0;


    public void Initialise(Character Owner)
    {
        _OwnerCharacter = Owner;
    }

    public void Reload(int count, GameObject prefab)
    {
        _ProjectileTemplate = prefab;
        _ProjectileCount = count;

        UIManager.Instance.BeersText.text = _ProjectileCount.ToString();
    }

    void Start ()
    {
        _Cooldown = 0f;
    }

	void Update ()
    {
        _Cooldown -= Time.deltaTime;
        _Cooldown = Mathf.Max(_Cooldown, 0f);
    }

    public bool StartThrow()
    {
        if (_Cooldown <= 0f && _ProjectileCount > 0)
        {
            _Cooldown = ThrowRate;
            return true;
        }
        return false;
    }

    public void ThrowProjectile()
    {
        GameObject projectile = Instantiate<GameObject>(_ProjectileTemplate);
        projectile.transform.position = transform.position;

        Vector2 Force = ThrowForce;
        Force.x *= _OwnerCharacter.CharacterMovementComponent.Direction;

        projectile.GetComponent<Rigidbody2D>().AddForce(Force);
        projectile.GetComponent<Rigidbody2D>().AddTorque(1500f);

        _ProjectileCount--;
        UIManager.Instance.BeersText.text = _ProjectileCount.ToString();
    }
}
