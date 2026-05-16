using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AdvancedProgramming
{
    // Shared drawing helpers for cards, pills, and read-only content blocks.
    public static class AppUi
    {
        private const int CaptionHeight = 22;

        public static void PaintCard(Graphics g, Rectangle bounds, Color borderAccent, int radius)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
            using (var path = RoundedRect(rect, radius))
            using (var brush = new LinearGradientBrush(rect, AppColors.CardTop, AppColors.CardBottom, 90f))
            using (var pen = new Pen(borderAccent, 2))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }
        }

        public static void PaintInset(Graphics g, Rectangle bounds) => PaintInset(g, bounds, 12);

        public static void PaintInset(Graphics g, Rectangle bounds, int radius)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(bounds.X, bounds.Y, bounds.Width - 1, bounds.Height - 1);
            using (var path = RoundedRect(rect, radius))
            using (var brush = new SolidBrush(AppColors.InsetBack))
            using (var pen = new Pen(Color.FromArgb(35, 48, 70), 1f))
            {
                g.FillPath(brush, path);
                g.DrawPath(pen, path);
            }
        }

        public static Panel CreateCard(Color borderAccent, int radius)
        {
            var card = new Panel { BackColor = Color.Transparent };
            var accent = borderAccent;
            card.Paint += (s, e) => PaintCard(e.Graphics, card.ClientRectangle, accent, radius);
            card.Resize += (s, e) => card.Invalidate();
            return card;
        }

        public static Label CreateTypeChip(string type) =>
            new Label
            {
                Text = type ?? "",
                AutoSize = true,
                Font = new Font("Segoe UI", 9),
                ForeColor = AppColors.MutedText,
                BackColor = Color.Transparent,
            };

        public static Panel CreateBadge(string level)
        {
            Color color = Theme.GetLevelColor(level);
            var badge = new Panel { Size = new Size(80, 28), BackColor = Color.Transparent };
            badge.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, badge.Width - 1, badge.Height - 1);
                using (var path = RoundedRect(rect, 14))
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
            });
            return badge;
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
            pill.Paint += (s, e) =>
            {
                bool on = (bool)pill.Tag;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, pill.Width - 1, pill.Height - 1);
                using (var path = RoundedRect(rect, 14))
                {
                    Color fill = on ? Color.FromArgb(28, 42, 62) : Color.FromArgb(16, 22, 38);
                    Color border = on ? Color.FromArgb(100, accent) : Color.FromArgb(45, 58, 80);
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
                ForeColor = selected ? Color.White : AppColors.MutedText,
                BackColor = Color.Transparent,
            });
            return pill;
        }

        public static void WireTabPill(Panel pill, Action onSelect)
        {
            pill.Click += (s, e) => onSelect();
            foreach (Control c in pill.Controls)
                c.Click += (s, e) => onSelect();
        }

        public static void SetTabSelected(Panel pill, bool selected, Color accent)
        {
            pill.Tag = selected;
            pill.Invalidate();
            if (pill.Controls.Count > 0 && pill.Controls[0] is Label lbl)
            {
                lbl.Font = new Font("Segoe UI", 10, selected ? FontStyle.Bold : FontStyle.Regular);
                lbl.ForeColor = selected ? Color.White : AppColors.MutedText;
            }
        }

        public static Panel CreateStatusPill(string text, Color accent)
        {
            var font = new Font("Segoe UI", 8, FontStyle.Bold);
            int w = Math.Max(72, TextRenderer.MeasureText(text, font).Width + 24);
            var pill = new Panel { Size = new Size(w, 26), BackColor = Color.Transparent };
            pill.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, pill.Width - 1, pill.Height - 1);
                using (var path = RoundedRect(rect, 12))
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
            });
            return pill;
        }

        public static Panel CreateActionPill(string text, bool enabled, Color accent, EventHandler onClick)
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

            pill.Paint += (s, e) =>
            {
                bool hover = enabled && (bool)pill.Tag;
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, pill.Width - 1, pill.Height - 1);
                using (var path = RoundedRect(rect, 16))
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
                Cursor = pill.Cursor,
            };
            pill.Controls.Add(label);

            if (enabled)
            {
                pill.MouseEnter += (s, e) => { pill.Tag = true; pill.Invalidate(); };
                pill.MouseLeave += (s, e) => { pill.Tag = false; pill.Invalidate(); };
                label.MouseEnter += (s, e) => { pill.Tag = true; pill.Invalidate(); };
                label.MouseLeave += (s, e) => { pill.Tag = false; pill.Invalidate(); };
            }

            if (enabled && onClick != null)
            {
                pill.Click += onClick;
                label.Click += onClick;
            }

            return pill;
        }

        public static TextBox CreateInput(int width, int height, bool password) =>
            new TextBox
            {
                Size = new Size(width, height),
                BorderStyle = BorderStyle.None,
                BackColor = AppColors.InsetBack,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11),
                PasswordChar = password ? '*' : '\0',
            };

        public static TextBox CreateReadOnlyBox(string text, Font font, int width, int height) =>
            new TextBox
            {
                Text = text ?? string.Empty,
                ReadOnly = true,
                Multiline = true,
                Font = font,
                Size = new Size(width, height),
                BorderStyle = BorderStyle.None,
                BackColor = AppColors.InsetBack,
                ForeColor = Color.White,
                TabStop = false,
            };

        public static int MeasureWrappedText(string text, Font font, int width, int minHeight = 40)
        {
            if (string.IsNullOrWhiteSpace(text))
                return minHeight;
            var size = TextRenderer.MeasureText(text, font, new Size(width, 0), TextFormatFlags.WordBreak);
            return size.Height + 12;
        }

        public static Panel CreateInsetBlock(string caption, int width, int height, int insetRadius = 12)
        {
            var block = new Panel
            {
                Width = width,
                Height = height,
                BackColor = Color.Transparent,
            };
            block.Paint += (s, e) =>
            {
                var inset = new Rectangle(0, CaptionHeight + 6, block.Width, block.Height - CaptionHeight - 6);
                PaintInset(e.Graphics, inset, insetRadius);
            };
            block.Controls.Add(new Label
            {
                Text = caption,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = AppColors.MutedText,
                Location = new Point(0, 0),
                Size = new Size(width, CaptionHeight),
                BackColor = Color.Transparent,
            });
            return block;
        }

        public static int AddReadOnlySection(Panel parent, ref int y, string caption, string text, Font font, int contentWidth, int padX = 16, int insetRadius = 10)
        {
            int blockW = contentWidth - padX * 2;
            int textH = Math.Max(48, MeasureWrappedText(text, font, blockW - 24));
            int blockH = CaptionHeight + 14 + textH;

            var block = CreateInsetBlock(caption, blockW, blockH, insetRadius);
            block.Location = new Point(padX, y);

            var body = CreateReadOnlyBox(text, font, blockW - 24, textH);
            body.Location = new Point(12, CaptionHeight + 12);
            block.Controls.Add(body);

            parent.Controls.Add(block);
            y += blockH + 12;
            return y;
        }

        public static int AddFormField(Panel parent, ref int y, string label, int width, out TextBox input, int height)
        {
            int blockH = CaptionHeight + 8 + height;
            var block = new Panel
            {
                Location = new Point(0, y),
                Size = new Size(width, blockH),
                BackColor = Color.Transparent,
            };
            block.Paint += (s, e) =>
            {
                var inset = new Rectangle(0, CaptionHeight, block.Width, block.Height - CaptionHeight);
                PaintInset(e.Graphics, inset, 10);
            };
            block.Controls.Add(new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = AppColors.MutedText,
                Location = new Point(0, 0),
                Size = new Size(width, 18),
                BackColor = Color.Transparent,
            });
            input = CreateInput(width - 20, height, false);
            input.Location = new Point(10, 30);
            block.Controls.Add(input);
            parent.Controls.Add(block);
            y += blockH + 14;
            return y;
        }

        public static int AddPasswordField(Panel parent, ref int y, string label, int width, out TextBox input, out Label toggle, int height)
        {
            int blockH = CaptionHeight + 8 + height;
            var block = new Panel
            {
                Location = new Point(0, y),
                Size = new Size(width, blockH),
                BackColor = Color.Transparent,
            };
            block.Paint += (s, e) =>
            {
                var inset = new Rectangle(0, CaptionHeight, block.Width, block.Height - CaptionHeight);
                PaintInset(e.Graphics, inset, 10);
            };
            block.Controls.Add(new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 9, FontStyle.Bold),
                ForeColor = AppColors.MutedText,
                Location = new Point(0, 0),
                Size = new Size(width, 18),
                BackColor = Color.Transparent,
            });
            input = CreateInput(width - 56, height, true);
            input.Location = new Point(10, 30);
            toggle = new Label
            {
                Text = "\U0001f441",
                Size = new Size(32, height),
                Location = new Point(width - 40, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 11),
                ForeColor = AppColors.MutedText,
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand,
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

        public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            var path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
