using UnityEngine;
using UnityEditor;
using Game.Runtime;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using CustomControls;
using CustomControls.Editor;

namespace Game.Editor
{
    [CustomEditor(typeof(FolderSettings))]
    public class FolderSettingsEditor : UnityEditor.Editor
    {
        [SerializeField]
        private VisualTreeAsset _visualTreeAsset;

        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            _visualTreeAsset.CloneTree(root);

            /// 
            var button = root.Q<Button>(className: Button.ussClassName);
            var textField = root.Q<TextField>(className: TextField.ussClassName);

            if (button != null)
                button.clicked += Button_clicked;

            if (textField != null)
                textField.bindingPath = nameof(FolderSettings._relativePath);

            /// USE CUSTOM CONTROL: FOLDER BROWSER
            var folderBrowser = root.Q<FolderBrowser>(className: FolderBrowser.ussClassName);

            if (folderBrowser != null)
            {
                folderBrowser.openFolderHandler = new EditorOpenFolderHandler();
                folderBrowser.bindingPath = nameof(FolderSettings._pathFromFolderBrowser);
                folderBrowser.onPathChanged += FolderBrowser_onPathChanged;
            }

            root.Bind(serializedObject);
            return root;
        }

        private void Button_clicked()
        {
            if (!(target is FolderSettings settings))
                return;

            var selectedPath = EditorUtility.OpenFolderPanel("Select Folder", settings.AbsolutePath, "");
            settings.RelativePath = selectedPath;

            EditorUtility.SetDirty(settings);
        }

        private void FolderBrowser_onPathChanged(string absolutePath, string relativePath)
        {
            if (!(target is FolderSettings settings))
                return;

            settings._pathFromFolderBrowser = absolutePath;

            EditorUtility.SetDirty(settings);
        }
    }
}
