using UnityEngine;
using UnityEngine.UI;

public class BackgroundColorChanger : MonoBehaviour
{
    public Camera mainCamera; 
    public Button changeColorButton; 

    void Start()
    {
      
        if (changeColorButton != null)
        {
            changeColorButton.onClick.AddListener(ChangeBackgroundColor);
        }

       
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    
    void ChangeBackgroundColor()
    {
        
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        mainCamera.backgroundColor = randomColor;
    }
}
