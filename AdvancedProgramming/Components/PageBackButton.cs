using System;
using System.Drawing;
using System.Windows.Forms;

namespace AdvancedProgramming.Components
{
    public static class PageBackButton
    {
        private const int ToolbarHeight = 52;

        public static Button Create(EventHandler onClick)
        {
            var btn = new Button
            {
                Text = "\u2190 Back",
                Location = new Point(DesignTokens.Spacing.Md, ToolbarHeight + DesignTokens.Spacing.Sm),
                Size = new Size(80, 30),
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

        public static void AddTo(Control page, EventHandler onClick)
        {
            var btn = Create(onClick);
            page.Controls.Add(btn);
            btn.BringToFront();
        }
    }
}
