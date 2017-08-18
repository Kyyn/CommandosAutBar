using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CourseraUtils;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public Texture2D m_LogoTexture;
    public GUISkin m_Skin;
    // Use this for initialization
    void Start ()
	{
		
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButtonUp("Fire1"))
        {
            SceneManager.LoadScene("GameScene");
        }
	}
    private void OnGUI()
    {
        GUI.skin = m_Skin;
        GUI.DrawTexture(GUIController.GetRectangleGUI(0.0f, 0.0f, 0.1898f, 0.4472f), m_LogoTexture);
        GUI.Label(GUIController.GetRectangleGUI(0.2f, 0.8f, 0.6f, 0.1f), "- Press Button to Start Game -", "MainMenuLabelStyle");
    }
}
