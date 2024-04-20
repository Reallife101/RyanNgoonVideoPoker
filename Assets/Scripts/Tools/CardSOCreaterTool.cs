using System.IO;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace VideoPoker
{

    public class CardSOCreaterTool : EditorWindow
    {
        private string folderName = "";
        private List<Sprite> imageSprites = new List<Sprite>();

        private Dictionary<string, Suit> suitDictionary = new Dictionary<string, Suit>()
        {
            {"Spade", Suit.Spade},
            {"Club", Suit.Club},
            {"Heart", Suit.Heart},
            {"Diamond", Suit.Diamond}
        };

        private string[] suitNames;

        private int dictLength = 1;
        private string[] imageFiles;
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
            GUILayout.Label("Create CardSO", EditorStyles.boldLabel);

            // Image selection drop-down
            folderName = EditorGUILayout.TextField("Folder Name:", folderName);

            // Load image
            if (GUILayout.Button("Load Folder"))
            {
                LoadFolder();
            }

            CreateDictGUI();

            // Display loaded image [come back to this later for better tool]
            /*
            if (imageSprite != null)
            {
                GUILayout.Label("Image Loaded:", EditorStyles.boldLabel);
                GUILayout.Label(imageSprite.texture, GUILayout.Width(100), GUILayout.Height(100));
            }
            */

            // Create CardSO button
            EditorGUI.BeginDisabledGroup(imageFiles== null || imageFiles.Length <= 0);
            if (GUILayout.Button("Create CardSOs"))
            {
                foreach (string image in imageFiles)
                {
                    CreateCardSO(image);
                }
            }

            EditorGUI.EndDisabledGroup();

            GUILayout.Label("Num Cards in Folder: " + imageFiles == null ? "0" : imageFiles.Length.ToString(), EditorStyles.boldLabel);

            // Delete CardSOs button
            GUILayout.Space(10f);
            if (GUILayout.Button("Delete CardSOs"))
            {
                DeleteCardSOs();
            }
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

        private void LoadFolder()
        {
            if (!string.IsNullOrEmpty(folderName))
            {
                imageFiles = Directory.GetFiles(folderName);
                foreach (string f in imageFiles)
                {
                    Debug.Log(f);
                }
            }
            else
            {
                Debug.LogWarning("Please enter an folder name.");
            }
        }

        private void CreateCardSO(string imageName)
        {
            //Get File Name
            string _filename = Path.GetFileName(imageName);

            UpdateSuitNames();

            Debug.LogError("Images: " + imageName);

            // Create a new CardSO
            CardSO card = CreateInstance<CardSO>();
            card.img = Resources.Load<Sprite>(imageName);
            if (imageFiles == null)
            {
                Debug.LogError("Images " + imageName + " not found!");
                return;
            }
            card.value = int.Parse(_filename.Substring(_filename.Length - 2));
            card.isRoyal = card.value>10;
            card.suit = suitDictionary[_filename.Substring(_filename.Length - 3, 1)];

            // Save the CardSO as an asset
            string path = "Assets/Current Card SOs/" + card.value.ToString() + "_"+ card.suit.ToString() + ".asset";
            AssetDatabase.CreateAsset(card, path);
            AssetDatabase.SaveAssets();

            Debug.Log("CardSO created: " + path);

            // Reset fields
            folderName = "";
        }

        
        private void UpdateSuitNames()
        {
            suitDictionary = new Dictionary<string, Suit>();

            // Iterate through both lists and add key-value pairs to the dictionary
            // NOTE: THIS IS SLOW. UPDATE LATER
            for (int i = 0; i < selectedKeyList.Count; i++)
            {
                
                suitDictionary[selectedKeyList[i]] = (Suit)selectedSuitIndexList[i];
            }
        }

        private void DeleteCardSOs()
        {
            // Default path for this tool
            string folderPath = "Assets/Current Card SOs/";

            // Get all files in the folder
            string[] files = Directory.GetFiles(folderPath);

            // Loop through each file and delete it
            foreach (string file in files)
            {
                File.Delete(file);
                Debug.Log("Deleted file: " + file);
            }
        }
    }

}
