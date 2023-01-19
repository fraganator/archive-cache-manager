using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArchiveCacheManager
{
    public static class UserInterface
    {
        static double contrast = LaunchBoxSettings.DialogContrastMultiplier;
        static Color foreColor = LaunchBoxSettings.DialogForegroundColor;
        static Color backColor = LaunchBoxSettings.DialogBackgroundColor;
        static Color backColorContrast1 = CalcContrast(backColor, contrast);
        static Color backColorContrast2 = CalcContrast(backColor, contrast / 2);

        public static DialogResult ErrorDialog(string message, IWin32Window owner = null)
        {
            return FlexibleMessageBox.Show(owner, message, "Archive Cache Manager", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button3);
        }

        // https://stackoverflow.com/a/2735242
        public static IEnumerable<T> Descendants<T>(this Control control) where T : class
        {
            foreach (Control child in control.Controls)
            {
                T childOfT = child as T;
                if (childOfT != null)
                {
                    yield return (T)childOfT;
                }

                if (child.HasChildren)
                {
                    foreach (T descendant in Descendants<T>(child))
                    {
                        yield return descendant;
                    }
                }
            }
        }

        // https://stackoverflow.com/questions/118528/horrible-redraw-performance-of-the-datagridview-on-one-of-my-two-screens
        public static void SetDoubleBuffered(Control control, bool doubleBuffer)
        {
            typeof(Control).InvokeMember(
               "DoubleBuffered",
               BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
               null,
               control,
               new object[] { doubleBuffer });
        }

        public static void SetColumnMinimumWidth(DataGridViewColumn column)
        {
            if (column is DataGridViewComboBoxColumn)
            {
                DataGridViewComboBoxColumn comboBoxColumn = column as DataGridViewComboBoxColumn;
                int minWidth = 5;

                foreach (var item in comboBoxColumn.Items)
                {
                    minWidth = Math.Max(minWidth, TextRenderer.MeasureText(item.ToString(), comboBoxColumn.DefaultCellStyle.Font).Width + 28);
                }

                comboBoxColumn.MinimumWidth = minWidth;
                if (comboBoxColumn.Width < minWidth)
                {
                    comboBoxColumn.Width = minWidth;
                }
            }
        }

        public static Bitmap GetMediaIcon(string platform)
        {
            Bitmap mediaIcon;

            if (platform.Contains("Microsoft Xbox 360")) mediaIcon = Resources.media_cd;
            else if (platform.Contains("Microsoft Xbox")) mediaIcon = Resources.media_cd;
            else if (platform.Contains("Nintendo 64")) mediaIcon = Resources.media_n64;
            else if (platform.Contains("Nintendo GameCube")) mediaIcon = Resources.media_gc;
            else if (platform.Contains("Nintendo Wii U")) mediaIcon = Resources.media_cd;
            else if (platform.Contains("Nintendo Wii")) mediaIcon = Resources.media_cd;
            else if (platform.Contains("Sega 32X")) mediaIcon = Resources.media_md;
            else if (platform.Contains("Sega CD")) mediaIcon = Resources.media_cd;
            else if (platform.Contains("Sega Mega-CD")) mediaIcon = Resources.media_cd;
            else if (platform.Contains("Sega Dreamcast")) mediaIcon = Resources.media_cd;
            else if (platform.Contains("Sega Genesis")) mediaIcon = Resources.media_md;
            else if (platform.Contains("Sega Mega Drive")) mediaIcon = Resources.media_md;
            else if (platform.Contains("Sega Saturn")) mediaIcon = Resources.media_cd;
            else if (platform.Contains("Sony Playstation 2")) mediaIcon = Resources.media_ps2;
            else if (platform.Contains("Sony Playstation")) mediaIcon = Resources.media_ps1;
            else if (platform.Contains("Sony PSP")) mediaIcon = Resources.media_psp;
            else mediaIcon = Resources.box_zipper;

            return mediaIcon;
        }

        private static Color CalcContrast(Color colour, double contrast)
        {
            return Color.FromArgb(CalcContrast(colour.R, contrast), CalcContrast(colour.G, contrast), CalcContrast(colour.B, contrast));
        }

        private static byte CalcContrast(byte colour, double contrast)
        {
            if (colour < 128)
                return (byte)((colour >> 1) * contrast + colour);
            else
                return 128;
        }

        public static Color GetBackgroundColor(Control control)
        {
            Color color = backColor;
            Control parent = control.Parent;
            if (parent is SplitContainer)
                color = backColorContrast1;
            else if (parent is TabPage)
                color = backColorContrast2;
            else if (parent.BackColor == backColor)
                color = backColorContrast1;
            else if (parent.BackColor == backColorContrast1)
                color = backColorContrast2;

            return color;
        }

        public static void ApplyTheme(Control rootControl)
        {
            List<Control> controls = new List<Control>(rootControl.Descendants<Control>());
            controls.Insert(0, rootControl);

            foreach (var control in controls)
            {
                if (control is FlexibleMessageBox.FlexibleMessageBoxForm)
                {
                    Form form = control as Form;
                    form.ForeColor = foreColor;
                    form.BackColor = backColorContrast1;
                }
                else if (control is Form)
                {
                    Form form = control as Form;
                    form.ForeColor = foreColor;
                    form.BackColor = backColor;
                }
                else if (control is TabPage)
                {
                    TabPage tabPage = control as TabPage;
                    tabPage.ForeColor = foreColor;
                    tabPage.BackColor = backColorContrast1;
                }
                else if (control is SplitterPanel)
                {
                    SplitterPanel splitterPanel = control as SplitterPanel;
                    splitterPanel.ForeColor = foreColor;
                    splitterPanel.BackColor = backColorContrast1;
                }
                else if (control is TreeView)
                {
                    TreeView treeView = control as TreeView;
                    double scale = treeView.DeviceDpi / 96.0;
                    treeView.ItemHeight = Convert.ToInt32(treeView.ItemHeight * scale);
                    treeView.ForeColor = foreColor;
                    treeView.BackColor = backColorContrast1;
                    treeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
                    treeView.DrawNode += TreeView_DrawNode;
                }
                else if (control is Button)
                {
                    Button button = control as Button;
                    button.ForeColor = foreColor;
                    button.BackColor = GetBackgroundColor(button);
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderColor = LaunchBoxSettings.DialogBorderColor;
                    button.FlatAppearance.MouseOverBackColor = LaunchBoxSettings.DialogHighlightColor;
                    button.GotFocus += new EventHandler((sender, e) =>
                    {
                        button.FlatAppearance.BorderColor = LaunchBoxSettings.DialogAccentColor;
                    });
                    (control as Button).LostFocus += new EventHandler((sender, e) =>
                    {
                        button.FlatAppearance.BorderColor = LaunchBoxSettings.DialogBorderColor;
                    });
                }
                else if (control is LinkLabel)
                {
                    LinkLabel linkLabel = control as LinkLabel;
                    linkLabel.LinkColor = LaunchBoxSettings.DialogAccentColor;
                }
                else if (control is Label)
                {
                    Label label = control as Label;
                    label.ForeColor = foreColor;
                    if (label.BackColor != label.Parent.BackColor)
                    {
                        label.BackColor = backColor;
                    }
                }
                else if (control is TextBox)
                {
                    TextBox textBox = control as TextBox;
                    textBox.ForeColor = foreColor;
                    textBox.BackColor = backColorContrast2;
                    if (textBox.BorderStyle == BorderStyle.Fixed3D)
                    {
                        textBox.BorderStyle = BorderStyle.FixedSingle;
                    }
                }
                else if (control is RichTextBox)
                {
                    RichTextBox richTextBox = control as RichTextBox;
                    richTextBox.ForeColor = foreColor;
                    richTextBox.BackColor = richTextBox.Parent.BackColor;
                    if (richTextBox.BorderStyle == BorderStyle.Fixed3D)
                    {
                        richTextBox.BorderStyle = BorderStyle.FixedSingle;
                    }
                }
                else if (control is ComboBox)
                {
                    ComboBox comboBox = control as ComboBox;
                    comboBox.ForeColor = foreColor;
                    comboBox.BackColor = GetBackgroundColor(comboBox);
                    comboBox.FlatStyle = FlatStyle.Flat;
                    comboBox.DrawMode = DrawMode.OwnerDrawFixed;
                    comboBox.DrawItem += ComboBox_DrawItem;
                }
                else if (control is FlowLayoutPanel)
                {
                    FlowLayoutPanel flowLayoutPanel = control as FlowLayoutPanel;
                    flowLayoutPanel.ForeColor = foreColor;
                    flowLayoutPanel.BackColor = control.Parent.BackColor;
                }
                else if (control is Panel)
                {
                    Panel panel = control as Panel;
                    panel.ForeColor = foreColor;
                    panel.BackColor = backColor;
                }
                else if (control is ListBox)
                {
                    ListBox listBox = control as ListBox;
                    double scale = listBox.DeviceDpi / 96.0;
                    listBox.ItemHeight = Convert.ToInt32(listBox.ItemHeight * scale);
                    listBox.ForeColor = foreColor;
                    listBox.BackColor = GetBackgroundColor(listBox);
                    if (listBox.BorderStyle == BorderStyle.Fixed3D)
                    {
                        listBox.BorderStyle = BorderStyle.FixedSingle;
                    }
                    listBox.DrawMode = DrawMode.OwnerDrawFixed;
                    listBox.DrawItem += ListBox_DrawItem;
                }
                else if (control is DataGridView)
                {
                    DataGridView dataGridView = control as DataGridView;
                    dataGridView.BorderStyle = BorderStyle.FixedSingle;
                    dataGridView.ForeColor = foreColor;
                    dataGridView.BackColor = backColorContrast2;
                    dataGridView.BackgroundColor = backColorContrast2;
                    dataGridView.DefaultCellStyle.BackColor = backColorContrast2;
                    dataGridView.DefaultCellStyle.ForeColor = foreColor;
                    dataGridView.DefaultCellStyle.SelectionBackColor = LaunchBoxSettings.DialogAccentColor;
                    dataGridView.DefaultCellStyle.SelectionForeColor = foreColor;
                    dataGridView.AlternatingRowsDefaultCellStyle.BackColor = backColorContrast1;
                    dataGridView.AlternatingRowsDefaultCellStyle.ForeColor = foreColor;
                    dataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor = LaunchBoxSettings.DialogAccentColor;
                    dataGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor = foreColor;
                    dataGridView.GridColor = backColorContrast1;
                    dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                    double colorDiv = 1.15;
                    Color headerColor = Color.FromArgb((int)(dataGridView.BackColor.R / colorDiv), (int)(dataGridView.BackColor.G / colorDiv), (int)(dataGridView.BackColor.B / colorDiv));
                    dataGridView.ColumnHeadersDefaultCellStyle.BackColor = headerColor;
                    dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = foreColor;
                    dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = headerColor;
                    dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = foreColor;
                    dataGridView.EnableHeadersVisualStyles = false;

                    dataGridView.CellMouseEnter += DataGridView_CellMouseEnter;
                    dataGridView.CellMouseLeave += DataGridView_CellMouseLeave;
                }
                else if (control is ProgressBar)
                {
                    ProgressBar progressBar = control as ProgressBar;
                    progressBar.BackColor = backColorContrast1;
                    progressBar.ForeColor = LaunchBoxSettings.DialogAccentColor;
                }
            }
        }

        /// <summary>
        /// Draw an icon in the cell of a DataGridView. Call from the DataGridView's CellPainting event.
        /// </summary>
        /// <param name="e">The event from CellPainting.</param>
        /// <param name="icon">The icon to draw. Recommended size is 16x16.</param>
        /// <param name="offset">Left offset of the icon. Default is 5.</param>
        /// <param name="paint">Whether to paint the cell before drawing the icon. The cell must be painted once. If additional icons are drawn in the same cell, set this false.</param>
        public static void DrawCellIcon(DataGridViewCellPaintingEventArgs e, Bitmap icon, int offset = 5, bool paint = true)
        {
            if (icon != null)
            {
                if (paint)
                {
                    e.Paint(e.CellBounds, DataGridViewPaintParts.All);
                }

                var w = icon.Width;
                var h = icon.Height;
                var x = e.CellBounds.Left + offset;// + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(icon, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        public static void ScaleControlFont(Control control, float scale)
        {
            Font controlFont = control.Font;
            control.Font = new Font(controlFont.FontFamily, controlFont.Size * scale, controlFont.Style);
        }

        private static void DataGridView_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var dataGridView = sender as DataGridView;
                foreach (DataGridViewCell cell in dataGridView.Rows[e.RowIndex].Cells)
                {
                    cell.Style.BackColor = e.RowIndex % 2 == 0 ? dataGridView.DefaultCellStyle.BackColor : dataGridView.AlternatingRowsDefaultCellStyle.BackColor;
                }
            }
        }

        private static void DataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var dataGridView = sender as DataGridView;
                foreach (DataGridViewCell cell in dataGridView.Rows[e.RowIndex].Cells)
                {
                    cell.Style.BackColor = LaunchBoxSettings.DialogHighlightColor;
                }
            }
        }

        private static void ListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            var list = sender as ListBox;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(LaunchBoxSettings.DialogAccentColor), e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(list.BackColor), e.Bounds);
            }

            if (e.Index < list.Items.Count && e.Index >= 0)
            {
                e.Graphics.DrawString(list.Items[e.Index].ToString(), e.Font, new SolidBrush(foreColor), new Point(e.Bounds.X, e.Bounds.Y));
            }
        }

        private static void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            var combo = sender as ComboBox;

            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(LaunchBoxSettings.DialogAccentColor), e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(combo.BackColor), e.Bounds);
            }

            if (e.Index < combo.Items.Count && e.Index >= 0)
            {
                e.Graphics.DrawString(combo.Items[e.Index].ToString(), e.Font, new SolidBrush(foreColor), new Point(e.Bounds.X, e.Bounds.Y));
            }
        }

        private static void TreeView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node == null) return;

            // if treeview's HideSelection property is "True", 
            // this will always returns "False" on unfocused treeview
            var selected = (e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected ||
                            (e.State & TreeNodeStates.Focused) == TreeNodeStates.Focused;
            var unfocused = !e.Node.TreeView.Focused;

            // we need to do owner drawing only on a selected node
            // and when the treeview is unfocused, else let the OS do it for us
            var font = e.Node.NodeFont ?? e.Node.TreeView.Font;
            Rectangle bounds = e.Bounds;
            bounds.X = e.Node.TreeView.Bounds.X - e.Node.TreeView.Indent;
            bounds.Width = e.Node.TreeView.Bounds.Width + e.Node.TreeView.Indent;
            if (selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(LaunchBoxSettings.DialogAccentColor), bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(backColorContrast1), bounds);
            }
            bounds = e.Bounds;
            bounds.Y += 8;
            TextRenderer.DrawText(e.Graphics, e.Node.Text, font, bounds, foreColor, TextFormatFlags.GlyphOverhangPadding);
        }
    }

    public class StackPanel : TabControl
    {
        protected override void WndProc(ref Message m)
        {
            // Hide tabs by trapping the TCM_ADJUSTRECT message
            if (m.Msg == 0x1328 && !DesignMode)
                m.Result = (IntPtr)1;
            else
                base.WndProc(ref m);
        }
    }

    public class ProgressBarFlat : ProgressBar
    {
        public ProgressBarFlat()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = new Rectangle(0, 0, this.Width, this.Height);
            double scaleFactor = (((double)Value - (double)Minimum) / ((double)Maximum - (double)Minimum));

            e.Graphics.FillRectangle(new SolidBrush(Color.Black), rec);
            e.Graphics.FillRectangle(new SolidBrush(BackColor), 1, 1, rec.Width - 2, rec.Height - 2);
            rec.Width = (int)((rec.Width * scaleFactor) - 2);
            rec.Height -= 2;
            e.Graphics.FillRectangle(new SolidBrush(ForeColor), 1, 1, rec.Width, rec.Height);
        }
    }
}
