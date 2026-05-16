using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming.Components
{
    public static class PageBackButton
    {
        private const int ToolbarHeight = 52;
        private const int ButtonWidth = 80;
        private const int ButtonHeight = 30;
        private const int ButtonGap = 8;

        public static Button Create(EventHandler onClick)
        {
            return CreateButton("\u2190 Back", new Point(DesignTokens.Spacing.Md, ToolbarHeight + DesignTokens.Spacing.Sm), onClick);
        }

        public static (Button back, Button home) Create(EventHandler onBack, EventHandler onHome)
        {
            var back = CreateButton("\u2190 Back", new Point(DesignTokens.Spacing.Md, ToolbarHeight + DesignTokens.Spacing.Sm), onBack);
            var home = CreateButton("Home", new Point(DesignTokens.Spacing.Md + ButtonWidth + ButtonGap, ToolbarHeight + DesignTokens.Spacing.Sm), onHome);
            return (back, home);
        }

        public static (Button back, Button home, Button profile) Create(
            EventHandler onBack, EventHandler onHome, EventHandler onProfile)
        {
            int y = ToolbarHeight + DesignTokens.Spacing.Sm;
            int x = DesignTokens.Spacing.Md;
            var back = CreateButton("\u2190 Back", new Point(x, y), onBack);
            x += ButtonWidth + ButtonGap;
            var home = CreateButton("Home", new Point(x, y), onHome);
            x += ButtonWidth + ButtonGap;
            var profile = CreateButton("Profile", new Point(x, y), onProfile);
            return (back, home, profile);
        }

        public static void AddTo(Control page, EventHandler onClick)
        {
            var btn = Create(onClick);
            page.Controls.Add(btn);
            btn.BringToFront();
        }

        public static void AddTo(Control page, EventHandler onBack, EventHandler onHome)
        {
            var (back, home) = Create(onBack, onHome);
            page.Controls.Add(back);
            page.Controls.Add(home);
            home.BringToFront();
            back.BringToFront();
        }

        public static void AddTo(Control page, EventHandler onBack, EventHandler onHome, EventHandler onProfile)
        {
            var (back, home, profile) = Create(onBack, onHome, onProfile);
            page.Controls.Add(back);
            page.Controls.Add(home);
            page.Controls.Add(profile);
            profile.BringToFront();
            home.BringToFront();
            back.BringToFront();
        }

        public static Button CreateHome(EventHandler onHome)
        {
            return CreateButton("Home", new Point(DesignTokens.Spacing.Md, ToolbarHeight + DesignTokens.Spacing.Sm), onHome);
        }

        public static Button CreateProfile(EventHandler onProfile)
        {
            return CreateButton("Profile", new Point(DesignTokens.Spacing.Md, ToolbarHeight + DesignTokens.Spacing.Sm), onProfile);
        }

        public static void AddHome(Control page, EventHandler onHome)
        {
            var btn = CreateHome(onHome);
            page.Controls.Add(btn);
            btn.BringToFront();
        }

        public static void AddProfile(Control page, EventHandler onProfile)
        {
            var btn = CreateProfile(onProfile);
            page.Controls.Add(btn);
            btn.BringToFront();
        }

        private static Button CreateButton(string text, Point location, EventHandler onClick)
        {
            var btn = new Button
            {
                Text = text,
                Location = location,
                Size = new Size(ButtonWidth, ButtonHeight),
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
