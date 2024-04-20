using System.IO;

namespace VideoPoker
{
    using UnityEngine;
    using UnityEditor;

    public class CardSOCreaterTool : EditorWindow
    {
        private string imageName = "";
        private Sprite imageSprite;

        [MenuItem("Tools/Create CardSO")]
        public static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(CardSOCreaterTool));
        }

        private void OnGUI()
        {
            GUILayout.Label("Create CardSO", EditorStyles.boldLabel);

            // Image selection drop-down
            imageName = EditorGUILayout.TextField("Image Name:", imageName);

            // Load image
            if (GUILayout.Button("Load Image"))
            {
                LoadImage();
            }

            // Display loaded image
            if (imageSprite != null)
            {
                GUILayout.Label("Image Loaded:", EditorStyles.boldLabel);
                GUILayout.Label(imageSprite.texture, GUILayout.Width(100), GUILayout.Height(100));
            }

            // Create CardSO button
            EditorGUI.BeginDisabledGroup(imageSprite == null);
            if (GUILayout.Button("Create CardSO"))
            {
                CreateCardSO();
            }
            EditorGUI.EndDisabledGroup();
        }

        private void LoadImage()
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                imageSprite = Resources.Load<Sprite>(imageName);
                if (imageSprite == null)
                {
                    Debug.LogError("Image " + imageName + " not found!");
                }
            }
            else
            {
                Debug.LogWarning("Please enter an image name.");
            }
        }

        private void CreateCardSO()
        {
            // Create a new CardSO
            CardSO card = CreateInstance<CardSO>();
            card.img = imageSprite;

            // You can set other properties of the CardSO as needed

            // Save the CardSO as an asset
            string path = "Assets/Current Card SOs/" + Path.GetFileName(imageName) + ".asset";
            AssetDatabase.CreateAsset(card, path);
            AssetDatabase.SaveAssets();

            Debug.Log("CardSO created: " + path);

            // Reset fields
            imageName = "";
            imageSprite = null;
        }
    }

}
