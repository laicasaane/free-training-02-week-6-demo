using System.IO;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Game.Runtime
{
    public static class PathUtility
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetRootPath()
            => Path.Combine(Application.dataPath, "..").ToUnixPath();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetRelativePath(string path)
            => Path.GetRelativePath(GetRootPath(), path ?? string.Empty).ToUnixPath();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetAbsolutePath(string relativePath)
            => Path.Combine(GetRootPath(), relativePath ?? string.Empty).ToUnixPath();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string ToUnixPath(this string path)
            => path?.Replace(Path.DirectorySeparatorChar, '/') ?? string.Empty;
    }
}
