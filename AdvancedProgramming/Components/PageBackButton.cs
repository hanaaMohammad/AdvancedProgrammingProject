using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming.Components
{
    public static class PageBackButton
    {
        private const int Width = 80;
        private const int Gap = 8;

        public static (Button back, Button home) Create(EventHandler onBack, EventHandler onHome)
        {
            int x = DesignTokens.Spacing.Md;
            var back = Make("\u2190 Back", x, onBack);
            var home = Make("Home", x + Width + Gap, onHome);
            return (back, home);
        }

        public static void AddTo(Control page, EventHandler onBack, EventHandler onHome)
        {
            var (back, home) = Create(onBack, onHome);
            page.Controls.Add(back);
            page.Controls.Add(home);
            back.BringToFront();
            home.BringToFront();
        }

        public static void AddHome(Control page, EventHandler onHome)
        {
            var btn = Make("Home", DesignTokens.Spacing.Md, onHome);
            page.Controls.Add(btn);
            btn.BringToFront();
        }

        public static void AddProfile(Control page, EventHandler onProfile)
        {
            var btn = Make("Profile", DesignTokens.Spacing.Md, onProfile);
            page.Controls.Add(btn);
            btn.BringToFront();
        }

        private static Button Make(string text, int x, EventHandler onClick)
        {
            var btn = new Button
            {
                Text = text,
                Location = new Point(x, CatalogUi.NavBarTop),
                Size = new Size(Width, CatalogUi.NavBarHeight),
                FlatStyle = FlatStyle.Flat,
                Font = DesignTokens.Typography.BodySmall,
                Tag = "Ghost",
                Cursor = Cursors.Hand,
            };
            btn.FlatAppearance.BorderSize = 0;
            if (onClick != null)
                btn.Click += onClick;
            return btn;
        }
    }
}
