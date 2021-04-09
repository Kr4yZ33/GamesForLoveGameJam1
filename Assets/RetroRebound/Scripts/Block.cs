// Created by team AdaptEvolve for the 
// GamesForLove Game Jam 1 April 2020
// Primary contributers: Kamatis (Discord)

using System;
using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    private int _currentHitPoints;

    [Header("Data")]
    public bool invulnerable;
    public int hitsToDestroy;
    public float destroyDelay = 0.583f;

    [Header("SFX")]
    [SerializeField]
    private AudioClip _hitAudioClip;
    [SerializeField]
    private AudioClip _deathAudioClip;

    [Header("Dependencies")]
    private Animator _animator;
    private BoxCollider2D _collider;

    public Action<Block> onBlockDestroyed;

    private void Awake()
    {
        _currentHitPoints = hitsToDestroy;
        _animator = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
    }

    public void ToggleCollider(bool enable)
    {
        if (_collider == null)
        {
            GetComponent<BoxCollider2D>().enabled = enable;
        }
        else
        {
            _collider.enabled = enable;
        }
    }

    public void ResetBlock()
    {
        if (_collider == null)
        {
            GetComponent<Animator>().Play("Idle");
        }
        else
        {
            _animator.Play("Idle");
        }
        _currentHitPoints = hitsToDestroy;
    }

    /// <summary>
    /// Called when block is hit
    /// </summary>
    public virtual void OnHit()
    {
        if (_hitAudioClip != null)
        {
            AudioSource.PlayClipAtPoint(_hitAudioClip, Vector3.zero);
        }

        _animator.Play("Hit", -1, 0f);
    }

    /// <summary>
    /// Called when block is destroyed
    /// Hide gameobject after destroy animation
    /// </summary>
    public virtual void Die()
    {
        if (_deathAudioClip != null)
        {
            AudioSource.PlayClipAtPoint(_deathAudioClip, Vector3.zero);
        }
        ToggleCollider(false);

        _animator.Play("Die");
        ScoreSystem scoreboard = FindObjectOfType<ScoreSystem>();
        if (scoreboard != null)
        {
            scoreboard.IncrementScore(1);
        }

        PowerupManager.Instance.OnBlockDestroyed(this);
        onBlockDestroyed?.Invoke(this);

        StartCoroutine(HideBlock());
    }

    /// <summary>
    /// Checks if ball collides with block
    /// If invulnerable, don't do anything aside from being hit
    /// otherwise, decrease <see cref="_currentHitPoints"/> by 1.
    /// If <see cref="_currentHitPoints"/> is less than or equal to 0,
    /// <see cref="Die"/>
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            OnCollisionInternal();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            OnCollisionInternal();
            collision.GetComponent<Bullet>().Die();
        }
    }

    private void OnCollisionInternal()
    {
        if (invulnerable)
        {
            OnHit();
            return;
        }
        else
        {
            _currentHitPoints--;
            if (_currentHitPoints <= 0)
            {
                Die();
            }
            else
            {
                OnHit();
            }
        }
    }

    private IEnumerator HideBlock()
    {
        yield return new WaitForSeconds(destroyDelay);
        gameObject.SetActive(false);
    }
}
