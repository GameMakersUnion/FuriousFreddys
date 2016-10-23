using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;


public class StringUtil
{
    /// <summary>
    /// Gets the String that is nested in between two Strings. Only the first match is returned.
    /// A null input String returns null. A null open/close returns null (no match). An empty("") open and close returns an empty string.
    /// (Note copy pasted from Java and adapted in a rush, not thoroughly checked, probably some mistakes)
    /// </summary>
    public static string SubstringBetween(string str, string open, string close)
    {
        /// <pre>
        /// str - the String containing the substring, may be null
        /// open - the String before the substring, may be null
        /// close - the String after the substring, may be null
        /// </pre>
        int pFrom = (str.IndexOf(open) == -1) ? 0 : str.IndexOf(open) + open.Length;
        int pTo = str.LastIndexOf(close);

        string result = str.Substring(pFrom, pTo - pFrom);
        return result;
    }
}


public static class CSharpUtil
{
    public static string print(bool[,] arr)
    {

        string result = "";

        int rowLength = arr.GetLength(0);
        int colLength = arr.GetLength(1);

        for (int i = 0; i < rowLength; i++)
        {
            for (int j = 0; j < colLength; j++)
            {
                result += (j > 0 && j < colLength) ? ", " : "";
                result += arr[i, j];
            }
            result += System.Environment.NewLine;
        }
        MonoBehaviour.print(result);
        return result;
    }

    public static string print(string[] arr)
    {
        string result = "";
        int count = 0;

        foreach (var item in arr)
        {
            count++;
            result += (count > 0 && count < arr.Length) ? ", " : "";
            result += (item.ToString());
        }
        MonoBehaviour.print(result);
        return result;
    }

    public static string print(Vector2[] arr)
    {
        string result = "";
        int count = 0;

        foreach (var item in arr)
        {
            result += (count > 0 && count < arr.Length) ? ", " : "";
            result += item;
            count++;
        }
        MonoBehaviour.print(result);
        return result;
    }

    public static string print(List<Vector2> list)
    {
        string result = print(list.ToArray());
        return result;
    }

}

public static class SpatialUtil
{
    public static Vector3 V2toV3(Vector2 v2)
    {
        return new Vector3(v2.x, v2.y, 0);
    }
}