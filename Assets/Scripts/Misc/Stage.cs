using System;

public enum Stage
{
    Main_Menu,
    Ocean_1,
    Ocean_2,
    Ocean_3,
    Beach_1,
    Beach_2,
    Beach_3,
    Forest_1,
    Forest_2,
    Forest_3,
    Mountain_1,
    Mountain_2,
    Mountain_3,
    Clouds_1,
    Clouds_2,
    Clouds_3,
    Ending
}

public static class StageHelper
{
    public static Stage GetStageByIndex(int index)
    {
        return (Stage)Enum.GetValues(typeof(Stage)).GetValue(index);
    }
}