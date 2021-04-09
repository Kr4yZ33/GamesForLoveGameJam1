using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField]
    private List<Block> _blocks = new List<Block>();

    private int _activeBlocks;

    private void OnEnable()
    {
        EnableOnBlockDestroyEvent();
        ResetLevel();
    }

    private void OnDisable()
    {
       
        DisableOnBlockDestroyEvent();
    }

    public void ResetLevel()
    {
        _activeBlocks = _blocks.Count;
        ActivateBlocks();
    }

    public bool HasActiveBlocks()
    {
        return _activeBlocks > 0;
    }

    private void OnBlockDestroy(Block blockDestroyed)
    {
        _activeBlocks--;
    }

    private void ActivateBlocks()
    {
        foreach(Block block in _blocks)
        {
            block.ToggleCollider(true);
            block.gameObject.SetActive(true);
            block.ResetBlock();
        }
    }

    private void EnableOnBlockDestroyEvent()
    {
        foreach (Block block in _blocks)
        {
            block.onBlockDestroyed += OnBlockDestroy;
        }
    }

    private void DisableOnBlockDestroyEvent()
    {
        foreach (Block block in _blocks)
        {
            block.onBlockDestroyed -= OnBlockDestroy;
        }
    }

    private void OnValidate()
    {
        _blocks.Clear();
        _blocks.AddRange(GetComponentsInChildren<Block>());
    }
}
