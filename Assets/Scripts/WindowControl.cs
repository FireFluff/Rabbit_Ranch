using System.Drawing;
using UnityEngine;
using System.Windows.Forms;

public class WindowControl : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Vector2 WindowOriginPos;
    private int GameHeight;
    void Awake()
    {
        /*System.Windows.Forms.Screen.GetWorkingArea();*/
        GameHeight = UnityEngine.Screen.currentResolution.height / 6;
        WindowOriginPos = new(0, UnityEngine.Screen.currentResolution.height - GameHeight);
        BorderlessWindow.SetWindowPos(WindowOriginPos, UnityEngine.Screen.currentResolution.width, GameHeight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
