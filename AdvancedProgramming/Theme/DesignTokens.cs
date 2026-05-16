using System.Drawing;

namespace AdvancedProgramming
{
    public static class DesignTokens
    {
        public const int FormWidth = 1100;
        public const int FormHeight = 800;
        public const int MinFormWidth = 850;
        public const int MinFormHeight = 520;

        public static class Spacing
        {
            public const int Xs = 4;
            public const int Sm = 8;
            public const int Md = 16;
            public const int Lg = 24;
            public const int Xl = 32;
            public const int Xxl = 48;
            public const int Xxxl = 64;
        }

        public static class Sizing
        {
            public const int ButtonHeight = 48;
            public const int ButtonWidthSm = 120;
            public const int ButtonWidthMd = 180;
            public const int ButtonWidthLg = 220;
            public const int InputHeight = 36;
            public const int InputWidth = 280;
            public const int LabelHeight = 22;
            public const int IconButtonSize = 38;
        }

        public static class Typography
        {
            public static readonly Font Family = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            public static readonly Font DisplayLarge = new Font("Segoe UI", 34F, FontStyle.Bold, GraphicsUnit.Point, 0);
            public static readonly Font DisplayMedium = new Font("Segoe UI", 28F, FontStyle.Bold, GraphicsUnit.Point, 0);
            public static readonly Font DisplaySmall = new Font("Segoe UI", 22F, FontStyle.Bold, GraphicsUnit.Point, 0);
            public static readonly Font HeadingLarge = new Font("Segoe UI", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            public static readonly Font HeadingMedium = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            public static readonly Font HeadingSmall = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            public static readonly Font BodyLarge = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point, 0);
            public static readonly Font BodyMedium = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            public static readonly Font BodySmall = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            public static readonly Font ButtonLabel = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point, 0);
            public static readonly Font Code = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            public static readonly Font CodeSmall = new Font("Consolas", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
        }

        public static class Animation
        {
            public const int FastMs = 150;
            public const int NormalMs = 300;
            public const int SlowMs = 500;
            public const int TimerInterval = 16;
        }

    }
}
