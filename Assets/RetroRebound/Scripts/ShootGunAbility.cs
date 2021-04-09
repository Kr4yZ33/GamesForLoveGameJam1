using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootType
{
    Time,
    Count
}

public class ShootGunAbility : PowerupAbility
{
    [Header("Config")]
    public ShootType shootType;
    public List<Transform> gunPositions;
    public float fireInterval = 0.7f;

    [Header("Dependencies")]
    [SerializeField]
    private GameObject _gunObject;
    [SerializeField]
    private Animator _gunAnimator;

    [Header("Prefabs")]
    public GameObject bulletPrefab;

    [Header("SFX")]
    public AudioClip shootAudioClip;

    [Header("Time - Shoot Type")]
    public float duration;

    [Header("Count - Shoot Type")]
    public int shootCount;

    private float _lastShot = 0f;
    private Coroutine _powerupTimerCoroutine;
    private Coroutine _hideGunsCoroutine;
    private int _currentShots;

    private void Start()
    {
        _currentShots = shootCount;
    }

    public override void Activate()
    {
        isActivated = true;
        _gunObject.SetActive(true);
        _gunAnimator.Play("EquipGun");

        if (_hideGunsCoroutine != null)
        {
            StopCoroutine(_hideGunsCoroutine);
        }

        if (shootType == ShootType.Time)
        {
            if(_powerupTimerCoroutine != null)
            {
                StopCoroutine(_powerupTimerCoroutine);
            }

            _powerupTimerCoroutine = StartCoroutine(PowerupTimer());
        } else
        {
            _currentShots = shootCount;
        }
    }

    public override void Deactivate()
    {
        isActivated = false;
        _gunAnimator.Play("UnequipGun");

        if (_hideGunsCoroutine != null)
        {
            StopCoroutine(_hideGunsCoroutine);
        }

        _hideGunsCoroutine = StartCoroutine(HideGameObject());
    }

    private IEnumerator HideGameObject()
    {
        yield return new WaitForSeconds(1f);
        _gunObject.SetActive(false);
    }

    private void Update()
    {
        if(!isActivated)
        {
            return;
        }

        if(Time.time - _lastShot <= fireInterval)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
            _lastShot = Time.time;
        }
    }

    private void Shoot()
    {
        _gunAnimator.Play("Shoot", -1, 0f);
        AudioSource.PlayClipAtPoint(shootAudioClip, Vector3.zero);
        foreach (Transform gunPosition in gunPositions)
        {
            Instantiate(bulletPrefab, gunPosition.position, Quaternion.identity);
        }

        if (shootType == ShootType.Count)
        {
            _currentShots--;
            if (_currentShots <= 0)
            {
                Deactivate();
            }
        }
    }

    IEnumerator PowerupTimer()
    {
        yield return new WaitForSeconds(duration);
        Deactivate();
    }
}
