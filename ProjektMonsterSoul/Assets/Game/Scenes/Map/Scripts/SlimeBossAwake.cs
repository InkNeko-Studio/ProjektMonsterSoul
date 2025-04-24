using UnityEngine;

public class SlimeBossAwake : MonoBehaviour
{
    [SerializeField] private Animator EntranceAnimator;
    void Start()
    {
        StartSlimeFight();
    }

    void Update()
    {
        
    }

    private void StartSlimeFight()
    {
        SoundManager.Instance.PlayOST(1);
    }

}
