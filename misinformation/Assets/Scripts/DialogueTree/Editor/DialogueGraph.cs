using UnityEngine;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;

public class DialogueGraph : EditorWindow
{
    private DialogueGraphView _graphView;
    private string _fileName = "New Tree";

    [MenuItem("Graph/Dialogue Graph")]
    public static void OpenDialogueGraphWindow()
    {
        var window = GetWindow<DialogueGraph>();
        window.titleContent = new GUIContent(text: "Dialogue Graph");
    }

    private void OnEnable()
    {
        ConstructGraph();
        GenerateToolbar();

        GenerateMinimap();
        
    }

    private void GenerateMinimap()
    {
        var miniMap = new MiniMap { anchored = true };
        miniMap.SetPosition(new Rect(10, 35, 200, 140));

        var toggleButton = new Toggle("Show Minimap");
        toggleButton.transform.position = new Vector2(0, 20);
        _graphView.Add(toggleButton);

        toggleButton.RegisterValueChangedCallback(evt =>
        {
            if (evt.newValue == true)
            {
                _graphView.Add(miniMap);
            } else {
                _graphView.Remove(miniMap);
            }
        });
    }

    private void ConstructGraph()
    {
        _graphView = new DialogueGraphView
        {
            name = "Dialogue Graph"
        };

        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    private void GenerateToolbar()
    {
        var toolbar = new UnityEditor.UIElements.Toolbar();

        TextField nameField = new TextField(label: "File Name: ");
        nameField.SetValueWithoutNotify(_fileName);
        nameField.MarkDirtyRepaint();
        nameField.RegisterValueChangedCallback(evt => _fileName = evt.newValue);
        toolbar.Add(nameField);

        toolbar.Add(child: new Button(clickEvent: () => SaveLoadData(true)) { text = "Save Tree"});
        toolbar.Add(child: new Button(clickEvent: () => SaveLoadData(false)) { text = "Load Tree" });

        Button dialogueNodeCreateButton = new Button(clickEvent: () =>
        {
            _graphView.CreateNode("New Node");
        });
        dialogueNodeCreateButton.text = "Create Dialogue";
        toolbar.Add(dialogueNodeCreateButton);

        Button gameplayNodeCreateButton = new Button(clickEvent: () =>
        {
            _graphView.CreateNode("New Node");
        });
        gameplayNodeCreateButton.text = "Create Tweet";
        toolbar.Add(gameplayNodeCreateButton);

        rootVisualElement.Add(toolbar);
    }

    private void SaveLoadData(bool save)
    {
        if (string.IsNullOrEmpty(_fileName))
        {
            EditorUtility.DisplayDialog("Invalid file name!", "Please enter a new name", "Continue");
            return;
        }

        GraphSaveUtil saveUtil = GraphSaveUtil.GetInstance(_graphView);
        if (save) {
            saveUtil.SaveGraph(_fileName);
        } else {
            saveUtil.LoadGraph(_fileName);
        }
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
}
