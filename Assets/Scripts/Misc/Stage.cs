using System;

public enum Stage
{
    Tutorial,
    Bottom_Sea,
    Beach,
    Forest,
    Mountain,
    Clouds,
    Stratosphere
}

public static class StageHelper
{
    public static Stage GetStageByIndex(int index)
    {
        return (Stage)Enum.GetValues(typeof(Stage)).GetValue(index);
    }
}