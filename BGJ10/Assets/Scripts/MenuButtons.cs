using UnityEngine;
using System.Collections;

public class MenuButtons : MonoBehaviour {

    public void LoadLevel(string name)
    {
        Application.LoadLevel(name);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
