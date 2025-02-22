using UnityEngine;
using UnityEngine.UI;

public class CanvasScalerHelper : MonoBehaviour
{

    CanvasScaler canvasScaler;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasScaler = GetComponent<CanvasScaler>();
        if(Screen.width/Screen.height > 16 / 9)
        {
            canvasScaler.matchWidthOrHeight = 1;
        }
        else
        {
            canvasScaler.matchWidthOrHeight = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
