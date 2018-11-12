namespace EasyOne.Common
{
    using System;

    [Flags]
    public enum SpellOptions
    {
        EnableUnicodeLetter = 4,
        FirstLetterOnly = 1,
        TranslateUnknowWordToInterrogation = 2
    }
}

