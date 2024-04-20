using System.IO;
using System.Collections.Generic;

namespace VideoPoker
{
    using UnityEngine;
    using UnityEditor;

    public class CardSOCreaterToolSingle : EditorWindow
    {
        private string imageName = "";

        private Dictionary<string, Suit> suitDictionary = new Dictionary<string, Suit>()
        {
            {"Spade", Suit.Spade},
            {"Club", Suit.Club},
            {"Heart", Suit.Heart},
            {"Diamond", Suit.Diamond}
        };

        private string[] suitNames;

        private int dictLength = 1;
        private Sprite imageSprite;
        private List<int> selectedSuitIndexList = new List<int>();
        private List<string> selectedKeyList = new List<string>();
        private bool isDragging = false;

        [MenuItem("Tools/Create CardSO Single")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(CardSOCreaterToolSingle));
        }
        private void OnEnable()
        {
            UpdateSuitNames();
        }

        private void OnGUI()
        {
            GUILayout.Label("Create CardSO Single", EditorStyles.boldLabel);

            Event evt = Event.current;
            Rect dropArea = GUILayoutUtility.GetRect(0.0f, 50.0f, GUILayout.ExpandWidth(true));
            GUI.Box(dropArea, "Drop Image Here");

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
                                Texture2D texture = draggedObject as Texture2D;
                                imageSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
                                imageSprite.name = texture.name;
                                break;
                            }
                        }
                        isDragging = false;
                    }
                    break;
            }

            GUILayout.Space(60); // Space below drop area

            CreateDictGUI();

            // Display loaded image
            if (imageSprite != null)
            {
                GUILayout.Label("Image Loaded:"+imageSprite.name, EditorStyles.boldLabel);
                GUILayout.Label(imageSprite.texture, GUILayout.Width(100), GUILayout.Height(100));
                
            }

            EditorGUI.BeginDisabledGroup(imageSprite == null);
            if (GUILayout.Button("Create CardSO"))
            {
                CreateCardSO();
            }

            EditorGUI.EndDisabledGroup();

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

        private void CreateCardSO()
        {
            //Get File Name
            string _filename = imageSprite.name;

            updateSuitDict();

            // Create a new CardSO
            CardSO card = CreateInstance<CardSO>();
            card.img = imageSprite;
            card.value = int.Parse(_filename.Substring(_filename.Length - 2));
            card.isRoyal = card.value > 10;
            card.suit = suitDictionary[_filename.Substring(_filename.Length - 3, 1)];

            // Save the CardSO as an asset
            string path = "Assets/Current Card SOs/" + card.value.ToString() + "_" + card.suit.ToString() + ".asset";
            AssetDatabase.CreateAsset(card, path);
            AssetDatabase.SaveAssets();

            Debug.Log("CardSO created: " + path);

            // Reset fields
            imageName = "";
            imageSprite = null;
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
            // NOTE: THIS IS SLOW. UPDATE LATER
            for (int i = 0; i < selectedKeyList.Count; i++)
            {

                suitDictionary[selectedKeyList[i]] = (Suit)selectedSuitIndexList[i];
            }
        }

    }

}
