using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class DemoRuntime : MonoBehaviour
    {
        [SerializeField]
        internal int _intField;

        private void Start()
        {
            FolderSettings x = default;
            x.RelativePath = "D:/Projects/Misc/Training02-W6/ProjectSettings";
        }
    }
}
