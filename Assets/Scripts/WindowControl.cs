using System.Drawing;
using UnityEngine;
using System.Windows.Forms;
using TMPro;

public class WindowControl : MonoBehaviour
{
    private Vector2 WindowOriginPos;
    private int GameHeight;
    /* More features can be added later
    [Range(1,100)]
    int GameScreenCoverage = 6;
    [SerializeField] private TMP_Text debugText;
    */
    
    void Awake()
    {
        var workingArea = System.Windows.Forms.Screen.GetWorkingArea(new Point(0, 0));
        GameHeight = workingArea.Height / 6;
        WindowOriginPos = new(0, workingArea.Height - GameHeight);
        BorderlessWindow.SetWindowPos(WindowOriginPos, UnityEngine.Screen.currentResolution.width, GameHeight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
