using UnityEngine.UIElements;

namespace CustomControls
{
    public delegate void FolderBrowserPathChanged(string absolutePath, string relativePath);

    public class FolderBrowser : VisualElement
    {
        public static readonly string ussClassName = "folder-browser";
        public static readonly string rootGroupUssClassName = "root";
        public static readonly string inputGroupUssClassName = "input-group";
        public static readonly string absolutePathGroupUssClassName = "absolute-path-group";
        public static readonly string hiddenUssClassName = "hidden";

        private const string BROWSER_TITLE = "Select Folder";

        private readonly TextField _textField;
        private readonly Button _button;
        private readonly VisualElement _absolutePathGroup;
        private readonly Label _absolutePath;

        public IOpenFolderHandler openFolderHandler { get; set; }

        public string label
        {
            get => _textField.label;
            set => _textField.label = value;
        }

        public bool multiline
        {
            get => _textField.multiline;
            set => _textField.multiline = value;
        }

        public string buttonText
        {
            get => _button.text;
            set => _button.text = value;
        }

        public string text
        {
            get => _textField.text;
        }

        public string bindingPath
        {
            get => _textField.bindingPath;
            set => _textField.bindingPath = value;
        }

        public string browserTitle { get; set; }

        public bool isRelativePath { get; set; }

        public bool showAbsolutePathInfo
        {
            get => _absolutePathGroup.visible;

            set
            {
                _absolutePathGroup.visible = value;

                if (value)
                    _absolutePathGroup.RemoveFromClassList(hiddenUssClassName);
                else
                    _absolutePath.AddToClassList(hiddenUssClassName);
            }
        }

        public event FolderBrowserPathChanged onPathChanged;

        public FolderBrowser() : this(null) { }

        public FolderBrowser(string label)
        {
            AddToClassList(ussClassName);

            var root = new VisualElement();
            root.AddToClassList(rootGroupUssClassName);

            var inputGroup = new VisualElement();
            inputGroup.AddToClassList(inputGroupUssClassName);

            _textField = new TextField(label) {
                isReadOnly = true
            };

            _button = new Button();
            _button.clicked += Button_clicked;

            inputGroup.Add(_textField);
            inputGroup.Add(_button);

            _absolutePathGroup = new VisualElement {
                visible = false
            };

            _absolutePathGroup.AddToClassList(absolutePathGroupUssClassName);
            _absolutePathGroup.AddToClassList(hiddenUssClassName);

            _absolutePath = new Label();
            _absolutePathGroup.Add(_absolutePath);

            root.Add(inputGroup);
            root.Add(_absolutePathGroup);
            Add(root);
        }

        private void Button_clicked()
        {
            var absolutePath = isRelativePath ? PathUtility.GetAbsolutePath(_textField.text) : _textField.text;

            if (openFolderHandler != null)
                absolutePath = openFolderHandler.OpenFolder(browserTitle ?? BROWSER_TITLE, absolutePath, "");

            _absolutePath.text = absolutePath;

            onPathChanged?.Invoke(absolutePath, PathUtility.GetRelativePath(absolutePath));
        }

        public new class UxmlFactory : UxmlFactory<FolderBrowser, UxmlTraits> { }

        public new class UxmlTraits : VisualElement.UxmlTraits
        {
            readonly UxmlStringAttributeDescription _label = new() {
                name = "label",
                defaultValue = "Folder Browser"
            };

            readonly UxmlBoolAttributeDescription _multiline = new() {
                name = "multiline",
                defaultValue = false
            };

            readonly UxmlStringAttributeDescription _buttonText = new() {
                name = "button-text",
                defaultValue = "Browse"
            };

            readonly UxmlStringAttributeDescription _bindingPath = new() {
                name = "binding-path",
                defaultValue = string.Empty
            };

            readonly UxmlStringAttributeDescription _browserTitle = new() {
                name = "browser-title",
                defaultValue = BROWSER_TITLE
            };

            readonly UxmlBoolAttributeDescription _isRelativePath = new() {
                name = "is-relative-path",
                defaultValue = false
            };

            readonly UxmlBoolAttributeDescription _showAbsolutePathInfo = new() {
                name = "show-absolute-path",
                defaultValue = false
            };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);

                var browser = ve as FolderBrowser;

                browser.label = _label.GetValueFromBag(bag, cc);
                browser.multiline = _multiline.GetValueFromBag(bag, cc);
                browser.buttonText = _buttonText.GetValueFromBag(bag, cc);
                browser.bindingPath = _bindingPath.GetValueFromBag(bag, cc);
                browser.browserTitle = _browserTitle.GetValueFromBag(bag, cc);
                browser.isRelativePath = _isRelativePath.GetValueFromBag(bag, cc);
                browser.showAbsolutePathInfo = _showAbsolutePathInfo.GetValueFromBag(bag, cc);
            }
        }
    }
}
