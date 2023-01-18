using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helpers
{
    private static Camera _camera;

    public static Camera Camera
    {
        get
        {
            if (_camera == null) _camera = Camera.main;
            return _camera;
        }
	}

    private static PointerEventData _eventDataCurrentPosition;
    private static List<RaycastResult> _results;
    public static bool IsOverUi()
    {
        _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        _results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_eventDataCurrentPosition, _results);
        return _results.Count > 0;
	}

    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera, out var result);
        return result;
	}

    private static System.Random rng = new System.Random();
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static float VarySoundPitch()
    {
        return Random.Range(.9f, 1.1f);
	}

    public static void PlaySoundFromArray(AudioSource source,AudioClip[] clips, float vol)
    {
        int n = Random.Range(0, clips.Length - 1);
        source.PlayOneShot(clips[n], vol);
    }

    public static string AddSpacesToSentence(string text, bool preserveAcronyms)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;
        StringBuilder newText = new StringBuilder(text.Length * 2);
        newText.Append(text[0]);
        for (int i = 1; i < text.Length; i++)
        {
            if (char.IsUpper(text[i]))
                if ((text[i - 1] != ' ' && !char.IsUpper(text[i - 1])) ||
                    (preserveAcronyms && char.IsUpper(text[i - 1]) &&
                     i < text.Length - 1 && !char.IsUpper(text[i + 1])))
                    newText.Append(' ');
            newText.Append(text[i]);
        }
        return newText.ToString();
    }
    public static string SplitCamelCase(string input)
    {
        return System.Text.RegularExpressions.Regex.Replace(input, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
    }

    public static Color ChangeColorAlpha(Color color, float value)
    {
        float clamp = Mathf.Clamp(value, 0, 1);
        color.a = clamp;
        //Debug.Log($"AlphaValue = {clamp}");
        return color;
    }

    public static float ReversePercentage(float count, float maxCount)
    {
        float percentage = count / maxCount;
        return 1 - percentage;
    }

    public static float TruncateWithTwoDecimals(float f)
    {
        return (float)(System.Math.Truncate((double)f * 100.0) / 100.0);
    }

    public static Transform GetNearestTransform(List<Transform> trans, Vector3 startPosition)
    {
        if (trans.Count < 1) return null;
        trans.OrderBy(x => Vector3.Distance(startPosition, x.position)).ToList();
        return trans[0];
    }

    public static float NearestMultipleOfNumber(float value, float multiple)
    {
        var rem = value % multiple;
        var result = value - rem;
        if (rem >= (multiple / 2))
            result += multiple;
        return result;
    }
}
