using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardParameter : MonoBehaviour
{
    void Update()
    {
        if (Keyboard.current == null) return;
    }

    void OnGUI()
    {
        if (Keyboard.current == null) return;

        GUILayout.Label($"W: {Keyboard.current.wKey.isPressed}");
        GUILayout.Label($"A: {Keyboard.current.aKey.isPressed}");
        GUILayout.Label($"S: {Keyboard.current.sKey.isPressed}");
        GUILayout.Label($"D: {Keyboard.current.dKey.isPressed}");
        GUILayout.Label($"Space: {Keyboard.current.spaceKey.isPressed}");
        GUILayout.Label($"Shift: {Keyboard.current.shiftKey.isPressed}");
    }
}
