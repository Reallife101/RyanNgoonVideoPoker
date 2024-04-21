using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class FilePathTest : EditorWindow
{
    private List<string> imageFilePaths = new List<string>();

    [MenuItem("Window/Image Drag Drop Editor")]
    static void Init()
    {
        FilePathTest window = (FilePathTest)EditorWindow.GetWindow(typeof(FilePathTest));
        window.Show();
    }

    void OnGUI()
    {
        GUILayout.Label("Create CardSO(s)", EditorStyles.boldLabel);

        Event evt = Event.current;
        Rect dropArea = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));
        GUI.Box(dropArea, "Drop Image(s) Here");

        switch (evt.type)
        {
            case EventType.DragUpdated:
            case EventType.DragPerform:
                if (!dropArea.Contains(evt.mousePosition))
                    break;

                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();

                    foreach (Object draggedObject in DragAndDrop.objectReferences)
                    {
                        if (draggedObject is Texture2D)
                        {
                            string imagePath = AssetDatabase.GetAssetPath(draggedObject);
                            if (!string.IsNullOrEmpty(imagePath))
                            {
                                imageFilePaths.Add(imagePath);
                            }
                        }
                    }
                }
                break;
        }

        GUILayout.Label("Image File Paths:");
        foreach (string path in imageFilePaths)
        {
            GUILayout.Label(path);
        }
    }
}
