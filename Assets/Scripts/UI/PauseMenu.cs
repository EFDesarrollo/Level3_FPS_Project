using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool startAtive;
    public GameObject PanelPauseMenu;
    public KeyCode keyCode;
    private bool cursorLocked;
    // Start is called before the first frame update
    void Start()
    {
        PanelPauseMenu.SetActive(startAtive);
        LockCursor();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyCode))
        {
            PanelPauseMenu.SetActive(!PanelPauseMenu.activeInHierarchy);
            LockCursor();
        }
    }
    public void LockCursor()
    {
        if (PanelPauseMenu.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
        if (!PanelPauseMenu.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }
}
