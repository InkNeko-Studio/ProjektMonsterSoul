using Framework.SaveSystem;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class LifeBarController : MonoBehaviour
{
    [Header("HUD Itens")] 
    [SerializeField] private Image _HealthBarFill;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ChanceHealth();
    }

    private void ChanceHealth()
    {
       _HealthBarFill.fillAmount = SaveController.CurrentSave.playerData.life / 100f;
    }
}
