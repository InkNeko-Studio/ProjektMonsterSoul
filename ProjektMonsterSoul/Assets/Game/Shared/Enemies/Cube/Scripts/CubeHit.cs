using System;
using Game.Shared.Enemies.Slime.Scripts;
using UnityEngine;

public class CubeHit : MonoBehaviour
{
    public string _ID;
    public float _Cont;
    public Collider2D[] colliders;

    public MultikubeController _Multikube;

    private bool _CanHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_CanHit == false)
        {
            _Cont += Time.deltaTime;
            if (_Cont >= 5)
            {
                _CanHit = true;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && _CanHit)
        {
            _Multikube.StopAndHit(_ID);
            _Cont = 0f;
            _CanHit = false;
        }
    }

    private void EnableHitAgain()
    {
    }
}
