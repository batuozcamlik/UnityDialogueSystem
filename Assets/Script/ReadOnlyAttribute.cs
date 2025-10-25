// ReadOnlyAttribute.cs
// Inspector'da alanlar� g�r�n�r yapar ama d�zenlemeyi kilitler (read-only).
// Kullan�m: [ReadOnly] public int myValue;

using System;
using UnityEditor;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Inspector'da alan� okunur (read-only) g�sterir.
/// </summary>
public class ReadOnlyAttribute : PropertyAttribute
{
    /// <summary>
    /// �ste�e ba�l�: Sadece Play Mode'da kilitle (false = her zaman kilitli).
    /// </summary>
    public bool playModeOnly { get; private set; }

    public ReadOnlyAttribute(bool playModeOnly = false)
    {
        this.playModeOnly = playModeOnly;
    }
}

#if UNITY_EDITOR


[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var attr = (ReadOnlyAttribute)attribute;

        // E�er sadece Play Mode'da kilitle se�ildiyse ve �u an Edit Mode'daysak, normal davran
        bool shouldLock = attr.playModeOnly ? EditorApplication.isPlaying : true;

        bool prevEnabled = GUI.enabled;
        if (shouldLock) GUI.enabled = false;

        // includeChildren: true => struct, Vector, Quaternion, custom serializable type, array/list i�indeki alanlar d�hil d�zg�n �izilir
        EditorGUI.PropertyField(position, property, label, true);

        GUI.enabled = prevEnabled;
    }

    // Child alanlar� da d�zg�nce yer kaplas�n diye y�ksekli�i aynen hesapla
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}
#endif
