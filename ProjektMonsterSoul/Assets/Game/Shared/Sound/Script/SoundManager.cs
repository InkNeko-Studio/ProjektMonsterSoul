using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [Header("OST")] 
    [SerializeField] private AudioSource SlimeMusic;
    
    [Header("SFX")]
    [SerializeField] private AudioSource SwordSwing;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayOST(int ost_id)
    {
        switch (ost_id)
        {
            case 1:
                    SlimeMusic.Play();
                break;
        }
    }

    public void PlaySFX(int sfx_id)
    {
        switch (sfx_id)
        {
            case 1:
                SwordSwing.Play();
                break;
        }
    }

    private void Awake()
    {
        Instance = this;
    }
}
