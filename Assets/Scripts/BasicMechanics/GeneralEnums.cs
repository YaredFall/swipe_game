namespace GeneralEnums
{
    using UnityEngine;

    public enum Direction
    {
        Right,
        Up,
        Left,
        Down
    }

    public static class Directions
    {
        public static bool Same(Direction dir1, Direction dir2)
        {
            return dir1 == dir2;
        }
        public static bool Opposite(Direction dir1, Direction dir2)
        {
            return (dir1 == Direction.Right && dir2 == Direction.Left) ||
                (dir1 == Direction.Left && dir2 == Direction.Right) ||
                (dir1 == Direction.Up && dir2 == Direction.Down) ||
                (dir1 == Direction.Down && dir2 == Direction.Up);
        }
    }

    public enum ArrowMode
    {
        FilledOnly,
        BlankOnly,
        Base
    }

    public enum GameMode
    {
        MainMenu,
        Settings,
        Gameplay
    }

    public enum GameplayMode
    {
        None,
        BaseTutorial,
        Base,
        Colorized
    }

    public enum GameState
    {
        Pause,
        Deathscreen,
        Gameplay
    }

    public enum ColorName
    {
        Red,
        Green,
        Blue,
        Yellow,
        Orange,
        Purple,
        Cyan
    }

    public static class Colors
    {
        private static Color Red = new Color(0.95f, 0, 0);
        private static Color Green = new Color(0.1f, 0.8f, 0);
        private static Color Blue = new Color(0, 0, 0.95f);
        private static Color Yellow = new Color(1, 0.9f, 0);
        private static Color Orange = new Color(0.95f, 0.5f, 0);
        private static Color Purple = new Color(0.9f, 0, 1);
        private static Color Cyan = new Color(0, 1, 0.9f);

        public static Color GetColor(ColorName colorName)
        {
            switch (colorName)
            {
                case ColorName.Red:
                    return Red;
                case ColorName.Green:
                    return Green;
                case ColorName.Blue:
                    return Blue;
                case ColorName.Yellow:
                    return Yellow;
                case ColorName.Orange:
                    return Orange;
                case ColorName.Purple:
                    return Purple;
                case ColorName.Cyan:
                    return Cyan;
                default:
                    return new Color(1, 1, 1);
            }
        }
    }
    
}
