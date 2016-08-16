﻿using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(My2DArray))]
public class MyArrayDrawer : PropertyDrawer
{

    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        //var amountRect = new Rect(position.x, position.y, 30, position.height);
        //var unitRect = new Rect(position.x + 35, position.y, 50, position.height);
        var theArrayRect = new Rect(position.x , position.y, position.width - 90, position.height);

        //SingletonGodController.instance.vehicleControlScript.GetComponent<Gridilizer>().;

        // Draw fields - pass GUIContent.none to each so they are drawn without labels
        //EditorGUI.PropertyField(theArrayRect, property.FindPropertyRelative("theArray"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}