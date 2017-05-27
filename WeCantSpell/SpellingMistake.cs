﻿using Microsoft.CodeAnalysis;

namespace WeCantSpell
{
    public class SpellingMistake
    {
        public Location Location { get; }

        public string Text { get; }

        public SpellingMistakeKind Kind { get; }

        public SpellingMistake(
            Location location,
            string text,
            SpellingMistakeKind kind)
        {
            Location = location;
            Text = text;
            Kind = kind;
        }
    }
}
