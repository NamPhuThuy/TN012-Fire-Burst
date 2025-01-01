using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHelper : MonoBehaviour
{
    public static string ConvertSecondsToTimeFormat(int totalSeconds)
    {
        int hours = totalSeconds / 3600;
        int minutes = (totalSeconds % 3600) / 60;
        int seconds = totalSeconds % 60;

        return $"{minutes:D2}:{seconds:D2}";
    }
}
