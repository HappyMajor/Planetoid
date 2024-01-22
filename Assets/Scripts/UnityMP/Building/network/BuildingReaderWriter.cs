using Mirror;
using static UnityEditor.Progress;
using System;

public static class BuildingReaderWriter
{
    const byte CONSTRUCTION = 1;
    const byte SHELTER = 2;
    const byte HQ = 3;
    
    private static void WriteBuilding(Building building, NetworkWriter writer)
    {
    }
    public static void WriteItem(this NetworkWriter writer, Building building)
    {
        if (building is Construction construction)
        {
            writer.WriteByte(CONSTRUCTION);
            writer.WriteString(construction.buildsTo);
        }
        if (building is Shelter shelter)
        {
            writer.WriteByte(SHELTER);
        }
        if (building is HQ hq)
        {
            writer.WriteByte(HQ);
        }
    }

    public static Building ReadItem(this NetworkReader reader)
    {
        byte type = reader.ReadByte();
        switch (type)
        {
            case CONSTRUCTION:
                return new Construction()
                {
                    buildsTo = reader.ReadString()
                };
            case SHELTER:
                return new Shelter();
            case HQ:
                return new HQ();
            default:
                throw new Exception($"Invalid weapon type {type}");
        }
    }
}