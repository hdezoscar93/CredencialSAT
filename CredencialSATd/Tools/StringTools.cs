using System;
using System.Collections.Generic;
using System.Text;

namespace CredencialSAT.Tools
{
    public static class StringTools
    {
        public static IEnumerable<string> ChunkSplitArray(this string str, int maxChunkSize, string separator)
        {
            for (int i = 0; i < str.Length; i += maxChunkSize)
                yield return str.Substring(i, Math.Min(maxChunkSize, str.Length - i)) + separator;
        }

        public static string ChunkSplit(this string str, int maxChunkSize, string separator)
        {
            var result = ChunkSplitArray(str, maxChunkSize, separator);
            return string.Join("",result);
        }
    }
}
