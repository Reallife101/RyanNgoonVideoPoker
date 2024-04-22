using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace VideoPoker
{

    public class CardSOCreaterTool : EditorWindow
    {

        private Dictionary<string, Suit> suitDictionary = new Dictionary<string, Suit>()
        {
            {"Spade", Suit.Spade},
            {"Club", Suit.Club},
            {"Heart", Suit.Heart},
            {"Diamond", Suit.Diamond}
        };

        private string[] suitNames;

        private int dictLength = 1;
        private List<string> imageFilePaths = new List<string>();
        private List<int> selectedSuitIndexList = new List<int>();
        private List<string> selectedKeyList = new List<string>();

        [MenuItem("Tools/Create CardSO")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(CardSOCreaterTool));
        }
        private void OnEnable()
        {
            UpdateSuitNames();
        }

        private void OnGUI()
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

            GUILayout.Space(60); // Space below drop area

            CreateDictGUI();

            // Display loaded images
            if (imageFilePaths.Count > 0)
            {
                GUILayout.Label("Images Loaded:", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal();
                int _counter = 0;

                Sprite imageSprite;

                foreach (string sprite in imageFilePaths)
                {
                    // Load Image Sprites for preview
                    imageSprite = AssetDatabase.LoadAssetAtPath<Sprite>(sprite);
                    if (imageSprite == null)
                    {
                        Debug.LogError("Image " + sprite + " not found!");
                    }

                    GUILayout.Label(imageSprite.texture, GUILayout.Width(50), GUILayout.Height(50));
                    _counter++;

                    if(_counter>=8)
                    {
                        _counter = 0;
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.BeginHorizontal();
                    }
                }
                EditorGUILayout.EndHorizontal();
            }

            EditorGUI.BeginDisabledGroup(imageFilePaths.Count <= 0);
            if (GUILayout.Button("Create CardSO"))
            {
                CreateCardSOs();

            }

            GUILayout.Space(10); // Space below drop area

            if (GUILayout.Button("Clear Loaded Images"))
            {
                ResetImageList();
            }

            EditorGUI.EndDisabledGroup();

        }

        private void ResetImageList()
        {
            imageFilePaths = new List<string>();
        }

        private void CreateDictGUI()
        {
            // Start Dictionary Section
            GUILayout.Space(10f);
            GUILayout.Label("Dictionary", EditorStyles.boldLabel);
            dictLength = EditorGUILayout.IntField("Number of Entries:", dictLength);
            GUILayout.Space(7.5f);

            if (dictLength < 0)
            {
                dictLength = 0;
            }

            // Resize fields as necessary
            while (selectedKeyList.Count < dictLength)
            {
                selectedKeyList.Add("");

                // if selected key has changed, so has suit index
                selectedSuitIndexList.Add(0);
            }

            while (selectedKeyList.Count < dictLength)
            {
                selectedKeyList.RemoveAt(selectedKeyList.Count - 1);

                // if selected key has changed, so has suit index
                selectedSuitIndexList.RemoveAt(selectedSuitIndexList.Count - 1);
            }

            // Add slots for additional dictionary items
            for (int i = 0; i < dictLength; i++)
            {
                // Suit selection drop-down
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Key:");
                selectedKeyList[i] = EditorGUILayout.TextField(selectedKeyList[i]);
                GUILayout.Label("Suit:", GUILayout.Width(50f));
                selectedSuitIndexList[i] = EditorGUILayout.Popup(selectedSuitIndexList[i], suitNames);
                EditorGUILayout.EndHorizontal();
            }

        }

        private void CreateCardSOs()
        {
            foreach (string sprite in imageFilePaths)
            {
                //Get File Name
                string _filename = Path.GetFileNameWithoutExtension(sprite);

                updateSuitDict();

                // Create a new CardSO
                CardSO card = CreateInstance<CardSO>();
                card.img = AssetDatabase.LoadAssetAtPath<Sprite>(sprite);
                card.value = int.Parse(_filename.Substring(_filename.Length - 2));
                card.isRoyal = card.value > 10;
                card.suit = suitDictionary[_filename.Substring(_filename.Length - 3, 1)];

                // Save the CardSO as an asset
                string path = "Assets/Current Card SOs/" + card.value.ToString() + "_" + card.suit.ToString() + ".asset";
                AssetDatabase.CreateAsset(card, path);
                AssetDatabase.SaveAssets();

                Debug.Log("CardSO created: " + path);

            }
            imageFilePaths.Clear(); ;
        }


        private void UpdateSuitNames()
        {
            suitNames = new string[suitDictionary.Count];
            suitDictionary.Keys.CopyTo(suitNames, 0);

        }

        private void updateSuitDict()
        {
            suitDictionary = new Dictionary<string, Suit>();

            // Iterate through both lists and add key-value pairs to the dictionary
            // NOTE: This is not the most optimal, can be improved
            for (int i = 0; i < selectedKeyList.Count; i++)
            {

                suitDictionary[selectedKeyList[i]] = (Suit)selectedSuitIndexList[i];
            }
        }

    }

}
