using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AdvancedProgramming.Components
{
    public static class CatalogUi
    {
        public const int ToolbarHeight = 52;
        public const int NavBarTop = 56;
        public const int NavBarHeight = 32;
        public const int ContentTop = NavBarTop + NavBarHeight + 12;

        public static readonly Color PageBack = Color.FromArgb(7, 11, 20);
        public static readonly Color CardTop = Color.FromArgb(20, 28, 45);
        public static readonly Color CardBottom = Color.FromArgb(17, 24, 39);
        public static readonly Color InsetBack = Color.FromArgb(12, 18, 32);
        public static readonly Color MutedText = Color.FromArgb(180, 180, 180);
        public static readonly Color DefaultBorder = Color.FromArgb(40, 55, 75);

        public static void PaintCard(Graphics g, Rectangle bounds, Color borderAccent, int radius = 20)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
            using (var path = GraphicsHelper.RoundedRect(rect, radius))
            using (var brush = new LinearGradientBrush(rect, CardTop, CardBottom, 90f))
            using (var pen = new Pen(borderAccent, 2))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }
        }

        public static void PaintInset(Graphics g, Rectangle bounds, int radius = 12)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
            using (var path = GraphicsHelper.RoundedRect(rect, radius))
            using (var brush = new SolidBrush(InsetBack))
            using (var pen = new Pen(Color.FromArgb(35, 48, 70), 1f))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }
        }

        public static Panel CreateCard(Color borderAccent, int radius = 20)
        {
            var card = new Panel
            {
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
            EnableDoubleBuffer(card);
            var accent = borderAccent;
            card.Paint += (s, e) => PaintCard(e.Graphics, card.ClientRectangle, accent, radius);
            card.Resize += (s, e) => card.Invalidate();
            return card;
        }

        public static Panel CreateBadge(string level)
        {
            Color color = Theme.GetLevelColor(level);
            var badge = new Panel
            {
                Size = new Size(80, 28),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
            badge.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, badge.Width - 1, badge.Height - 1);
                using (var path = GraphicsHelper.RoundedRect(rect, 14))
                using (var brush = new SolidBrush(color))
                    e.Graphics.FillPath(brush, path);
            };
            badge.Controls.Add(new Label
            {
                Text = Theme.FormatLevel(level),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            });
            return badge;
        }

        public static Label CreateTypeChip(string type)
        {
            return new Label
            {
                Text = type ?? "",
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = MutedText,
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
        }

        public static Panel CreateTabPill(string text, bool selected, Color accent)
        {
            var pill = new Panel
            {
                Size = new Size(Math.Max(100, TextRenderer.MeasureText(text, new Font("Segoe UI", 10, FontStyle.Bold)).Width + 32), 36),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Tag = selected,
            };
            EnableDoubleBuffer(pill);
            var accentColor = accent;
            pill.Paint += (s, e) =>
            {
                bool on = (bool)pill.Tag;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, pill.Width - 1, pill.Height - 1);
                using (var path = GraphicsHelper.RoundedRect(rect, 14))
                {
                    Color fill = on ? Color.FromArgb(28, 42, 62) : Color.FromArgb(16, 22, 38);
                    Color border = on ? Color.FromArgb(100, accentColor) : Color.FromArgb(45, 58, 80);
                    using (var brush = new SolidBrush(fill))
                    using (var pen = new Pen(border, on ? 2f : 1f))
                    {
                        e.Graphics.FillPath(brush, path);
                        e.Graphics.DrawPath(pen, path);
                    }
                }
            };
            pill.Controls.Add(new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10, selected ? FontStyle.Bold : FontStyle.Regular),
                ForeColor = selected ? Color.White : MutedText,
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            });
            return pill;
        }

        public static void SetTabSelected(Panel pill, bool selected, Color accent)
        {
            pill.Tag = selected;
            pill.Invalidate();
            if (pill.Controls.Count > 0 && pill.Controls[0] is Label lbl)
            {
                lbl.Font = new Font("Segoe UI", 10, selected ? FontStyle.Bold : FontStyle.Regular);
                lbl.ForeColor = selected ? Color.White : MutedText;
            }
        }

        public static Panel CreateStatusPill(string text, Color accent)
        {
            var font = new Font("Segoe UI", 8, FontStyle.Bold);
            int w = Math.Max(72, TextRenderer.MeasureText(text, font).Width + 24);
            var pill = new Panel
            {
                Size = new Size(w, 26),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
            pill.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, pill.Width - 1, pill.Height - 1);
                using (var path = GraphicsHelper.RoundedRect(rect, 12))
                using (var brush = new SolidBrush(Color.FromArgb(40, accent)))
                using (var pen = new Pen(Color.FromArgb(120, accent), 1.5f))
                {
                    e.Graphics.FillPath(brush, path);
                    e.Graphics.DrawPath(pen, path);
                }
            };
            pill.Controls.Add(new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = font,
                ForeColor = accent,
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            });
            return pill;
        }

        public static Panel CreateActionPill(string text, bool enabled, Color accent, EventHandler onClick = null)
        {
            var font = new Font("Segoe UI", 10, FontStyle.Bold);
            int w = Math.Max(120, TextRenderer.MeasureText(text, font).Width + 36);

            Color normalFill = enabled ? Color.FromArgb(18, 42, 34) : Color.FromArgb(22, 30, 48);
            Color normalBorder = enabled ? Color.FromArgb(80, accent) : Color.FromArgb(45, 58, 80);
            Color hoverFill = Color.FromArgb(24, 58, 46);
            Color hoverBorder = Color.FromArgb(120, accent);

            var pill = new Panel
            {
                Size = new Size(w, 40),
                BackColor = Color.Transparent,
                Cursor = enabled ? Cursors.Hand : Cursors.Default,
                Tag = false,
            };
            EnableDoubleBuffer(pill);

            pill.Paint += (s, e) =>
            {
                bool hover = enabled && (bool)pill.Tag;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, pill.Width - 1, pill.Height - 1);
                using (var path = GraphicsHelper.RoundedRect(rect, 16))
                using (var brush = new SolidBrush(hover ? hoverFill : normalFill))
                using (var pen = new Pen(hover ? hoverBorder : normalBorder, 1.5f))
                {
                    e.Graphics.FillPath(brush, path);
                    e.Graphics.DrawPath(pen, path);
                }
            };

            var label = new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = font,
                ForeColor = enabled ? Color.FromArgb(220, 255, 240) : Color.FromArgb(120, 135, 155),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
                Cursor = pill.Cursor,
            };
            pill.Controls.Add(label);

            void WireHover(Control c)
            {
                if (!enabled) return;
                c.MouseEnter += (s, e) => { pill.Tag = true; pill.Invalidate(); };
                c.MouseLeave += (s, e) => { pill.Tag = false; pill.Invalidate(); };
            }
            WireHover(pill);
            WireHover(label);

            if (enabled && onClick != null)
            {
                pill.Click += onClick;
                label.Click += onClick;
            }

            return pill;
        }

        public static TextBox CreateInput(int width, int height = 36, bool password = false)
        {
            return new TextBox
            {
                Size = new Size(width, height),
                BorderStyle = BorderStyle.None,
                BackColor = InsetBack,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11),
                PasswordChar = password ? '*' : '\0',
                Tag = "NoTheme",
            };
        }

        public static int AddFormField(Panel parent, ref int y, string label, int width, out TextBox input, int height = 36)
        {
            int blockH = 22 + 8 + height;
            var block = new Panel
            {
                Location = new Point(0, y),
                Size = new Size(width, blockH),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
            EnableDoubleBuffer(block);
            block.Paint += (s, e) =>
            {
                var inset = new Rectangle(0, 22, block.Width, block.Height - 22);
                PaintInset(e.Graphics, inset, 10);
            };
            block.Controls.Add(new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = MutedText,
                Location = new Point(0, 0),
                Size = new Size(width, 18),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            });
            input = CreateInput(width - 20, height);
            input.Location = new Point(10, 30);
            block.Controls.Add(input);
            parent.Controls.Add(block);
            y += blockH + 14;
            return y;
        }

        public static int AddPasswordField(Panel parent, ref int y, string label, int width, out TextBox input, out Label toggle, int height = 36)
        {
            int blockH = 22 + 8 + height;
            var block = new Panel
            {
                Location = new Point(0, y),
                Size = new Size(width, blockH),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            };
            EnableDoubleBuffer(block);
            block.Paint += (s, e) =>
            {
                var inset = new Rectangle(0, 22, block.Width, block.Height - 22);
                PaintInset(e.Graphics, inset, 10);
            };
            block.Controls.Add(new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = MutedText,
                Location = new Point(0, 0),
                Size = new Size(width, 18),
                BackColor = Color.Transparent,
                Tag = "NoTheme",
            });
            input = CreateInput(width - 56, height, password: true);
            input.Location = new Point(10, 30);
            toggle = new Label
            {
                Text = "\U0001f441",
                Size = new Size(32, height),
                Location = new Point(width - 40, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 11),
                ForeColor = MutedText,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
                Tag = "NoTheme",
            };
            block.Controls.Add(input);
            block.Controls.Add(toggle);
            parent.Controls.Add(block);
            y += blockH + 14;
            return y;
        }

        public static void TogglePasswordVisibility(TextBox box, Label toggle, ref bool visible)
        {
            visible = !visible;
            box.PasswordChar = visible ? '\0' : '*';
            toggle.Text = visible ? "\U0001f648" : "\U0001f441";
        }

        public static void EnableDoubleBuffer(Control control)
        {
            typeof(Control).InvokeMember(
                "DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null,
                control,
                new object[] { true });
        }
    }
}
