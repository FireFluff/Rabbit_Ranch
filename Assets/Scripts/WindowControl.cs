using System.Drawing;
using UnityEngine;
using TMPro;

public class WindowControl : MonoBehaviour
{
#if !UNITY_EDITOR
    // Non serialized fields
    private Vector2 _windowOriginPos;
    private int _gameHeight;
    
    // Serialized fields
    [Range(1,100), SerializeField] private int gameScreenCoverPerc = 20;
    [SerializeField] private TMP_Text debugText;
    
    void Awake()
    {
        // Get the screen dimensions of the primary screen without taskbar.
        var workingArea = System.Windows.Forms.Screen.GetWorkingArea(new Point(0, 0));
        // Calculate the game height based on the desired screen cover %.
        _gameHeight = workingArea.Height * gameScreenCoverPerc / 100;
        // Calculate the position of the top left corner of the game screen.
        _windowOriginPos = new(0, workingArea.Height - _gameHeight);
        // Set the window position and size.
        BorderlessWindow.SetWindowPos(_windowOriginPos, Screen.currentResolution.width, _gameHeight);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
#endif
}
