using UnityEngine;

namespace Game.Runtime
{
    /// <summary>
    /// Tạo ra file "FolderSettings.asset"
    /// </summary>
    [CreateAssetMenu(fileName = nameof(FolderSettings), 
                     menuName = "Game/Folder Settings", 
                     order = 1)]
    public class FolderSettings : ScriptableObject
    {
        [SerializeField]
        internal string _relativePath;

        [SerializeField]
        internal string _pathFromFolderBrowser;
        
        /// <summary>
        /// Đường dẫn tương đối đến folder được chọn
        /// </summary>
        public string RelativePath
        {
            get => _relativePath ?? string.Empty;
            set => _relativePath = PathUtility.GetRelativePath(value ?? string.Empty);
        }

        /// <summary>
        /// Đường dẫn tuyệt đối đến folder được chọn
        /// </summary>
        public string AbsolutePath
            => PathUtility.GetAbsolutePath(_relativePath);
    }
}
