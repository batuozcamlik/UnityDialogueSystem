// ReadOnlyAttribute.cs
// Inspector'da alanlarý görünür yapar ama düzenlemeyi kilitler (read-only).
// Kullaným: [ReadOnly] public int myValue;

using System;
using UnityEditor;
using UnityEngine;
using UnityEditor;

/// <summary>
/// Inspector'da alaný okunur (read-only) gösterir.
/// </summary>
public class ReadOnlyAttribute : PropertyAttribute
{
    /// <summary>
    /// Ýsteðe baðlý: Sadece Play Mode'da kilitle (false = her zaman kilitli).
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

        // Eðer sadece Play Mode'da kilitle seçildiyse ve þu an Edit Mode'daysak, normal davran
        bool shouldLock = attr.playModeOnly ? EditorApplication.isPlaying : true;

        bool prevEnabled = GUI.enabled;
        if (shouldLock) GUI.enabled = false;

        // includeChildren: true => struct, Vector, Quaternion, custom serializable type, array/list içindeki alanlar dâhil düzgün çizilir
        EditorGUI.PropertyField(position, property, label, true);

        GUI.enabled = prevEnabled;
    }

    // Child alanlarý da düzgünce yer kaplasýn diye yüksekliði aynen hesapla
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}
#endif
