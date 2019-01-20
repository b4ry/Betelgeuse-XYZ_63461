using System;

namespace Assets.Scripts.Enums
{
    [Flags]
    public enum Biome
    {
        Invalid = 0,
        Plains = 1,
        Mountains = 2,
        Plateau = 4,
        River = 8,
        Forest = 16,
        Desert = 32,
        Lake = 64,
        Cave = 128
    }
}