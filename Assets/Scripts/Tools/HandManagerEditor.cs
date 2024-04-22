using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace VideoPoker
{
    [CustomEditor(typeof(HandManager))]
    public class HandManagerEditor : Editor
    {
        private int dictLength = 1;
        private List<CardSO> cards = new List<CardSO>();

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            HandManager handManager = (HandManager)target;

            CreateDictGUI();

            if (GUILayout.Button("Generate Specific Hand"))
            {
                GenerateSpecificHand(handManager);
            }
        }

        private void CreateDictGUI()
        {
            // Start Dictionary Section
            GUILayout.Space(10f);
            GUILayout.Label("CUSTOM HAND GENERATOR", EditorStyles.boldLabel);
            dictLength = EditorGUILayout.IntField("Number of Entries:", dictLength);
            GUILayout.Space(7.5f);

            if (dictLength < 0)
            {
                dictLength = 0;
            }

            // Resize fields as necessary
            while (cards.Count < dictLength)
            {
                cards.Add(null);

            }

            while (cards.Count < dictLength)
            {
                cards.RemoveAt(cards.Count - 1);
            }

            // Add slots for additional dictionary items
            for (int i = 0; i < dictLength; i++)
            {
                // Suit selection drop-down
                EditorGUILayout.BeginHorizontal();
                cards[i] = (CardSO)EditorGUILayout.ObjectField("Select CardSO:", cards[i], typeof(CardSO), true);
                EditorGUILayout.EndHorizontal();
            }

        }

        private void GenerateSpecificHand(HandManager handManager)
        {
            for (int i = 0; i < Mathf.Min(5, cards.Count); i++)
            {
                
                if (cards[i] != null)
                {
                    handManager.setCardSOAtIndex(i, cards[i]);

                }
            }

            // Repaint the inspector to reflect the changes
            Repaint();
        }
    }
}
