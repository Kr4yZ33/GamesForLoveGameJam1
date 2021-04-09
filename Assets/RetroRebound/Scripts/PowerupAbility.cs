using UnityEngine;

public abstract class PowerupAbility : MonoBehaviour
{
    [Header("Data")]
    public bool isActivated;

    protected Animator _animator;

    public virtual void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public virtual void Start()
    {
        GameManager.Instance.OnLevelChange += OnLevelChange;
    }

    public virtual void OnDisable()
    {
        GameManager.Instance.OnLevelChange -= OnLevelChange;
    }
    public abstract void Activate();

    public abstract void Deactivate();

    protected void OnLevelChange(int level)
    {
        if (isActivated)
        {
            Deactivate();
        }
    }
}
