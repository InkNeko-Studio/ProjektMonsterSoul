using System;
using Game.Shared.Enemies.Slime.Scripts;
using UnityEngine;

public class CubeZoneControll : MonoBehaviour
{
    private bool _InZone;
    public MultikubeController cube;
    public float _Count;
    private void Awake()
    {
        _InZone = false;
    }

    private void Update()
    {
        if (_InZone == false && _Count <= 5)
        {
            _Count += Time.deltaTime;
            if (_Count >= 5)
            {
                CallCubeZone();
            }
            
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _InZone = true;
            _Count = 0;
            CallCubeZone();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _InZone = false;
        }
    }

    private void CallCubeZone()
    {
        cube.ChekZone(_InZone);
    }
}
