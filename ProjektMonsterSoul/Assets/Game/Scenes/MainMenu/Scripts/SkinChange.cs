using UnityEngine;
using Image = UnityEngine.UI.Image;

public class SkinChange : MonoBehaviour
{
    [SerializeField]private Image charimage;
    [SerializeField]private Color color;
    void Start()
    {
        
    }

    public void ChangeColor()
    {
        charimage.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
