using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
                    treeView.ForeColor = foreColor;
                    treeView.BackColor = backColorContrast1;
                    treeView.DrawMode = TreeViewDrawMode.OwnerDrawText;
                    treeView.DrawNode += new DrawTreeNodeEventHandler((sender, e) =>
                    {
                        if (e.Node == null) return;

                        // if treeview's HideSelection property is "True", 
                        // this will always returns "False" on unfocused treeview
                        var selected = (e.State & TreeNodeStates.Selected) == TreeNodeStates.Selected ||
                                       (e.State & TreeNodeStates.Focused) == TreeNodeStates.Focused;
                        var unfocused = !e.Node.TreeView.Focused;

                        // we need to do owner drawing only on a selected node
                        // and when the treeview is unfocused, else let the OS do it for us
                        if (selected)
                        {
                            var font = e.Node.NodeFont ?? e.Node.TreeView.Font;
                            Rectangle bounds = e.Bounds;
                            bounds.X = e.Node.TreeView.Bounds.X - e.Node.TreeView.Indent;
                            bounds.Width = e.Node.TreeView.Bounds.Width + e.Node.TreeView.Indent;
                            e.Graphics.FillRectangle(new SolidBrush(LaunchBoxSettings.DialogAccentColor), bounds);
                            TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Bounds, foreColor, TextFormatFlags.GlyphOverhangPadding);
                        }
                        else
                        {
                            e.DrawDefault = true;
                        }
                    });
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
                    comboBox.BackColor = backColor;
                    comboBox.FlatStyle = FlatStyle.Flat;
                    comboBox.DrawMode = DrawMode.OwnerDrawFixed;
                    comboBox.DrawItem += new DrawItemEventHandler((sender, e) =>
                    {
                        var combo = sender as ComboBox;

                        if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(LaunchBoxSettings.DialogAccentColor), e.Bounds);
                        }
                        else
                        {
                            e.Graphics.FillRectangle(new SolidBrush(backColor), e.Bounds);
                        }

                        if (e.Index < combo.Items.Count && e.Index >= 0)
                        {
                            e.Graphics.DrawString(combo.Items[e.Index].ToString(), e.Font, new SolidBrush(foreColor), new Point(e.Bounds.X, e.Bounds.Y));
                        }
                    });
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
                    listBox.ForeColor = foreColor;
                    listBox.BackColor = GetBackgroundColor(listBox);
                    if (listBox.BorderStyle == BorderStyle.Fixed3D)
                    {
                        listBox.BorderStyle = BorderStyle.FixedSingle;
                    }
                    listBox.DrawMode = DrawMode.OwnerDrawFixed;
                    listBox.DrawItem += new DrawItemEventHandler((sender, e) =>
                    {
                        var list = sender as ListBox;

                        if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                        {
                            e.Graphics.FillRectangle(new SolidBrush(LaunchBoxSettings.DialogAccentColor), e.Bounds);
                        }
                        else
                        {
                            e.Graphics.FillRectangle(new SolidBrush(backColorContrast1), e.Bounds);
                        }

                        if (e.Index < list.Items.Count && e.Index >= 0)
                        {
                            e.Graphics.DrawString(list.Items[e.Index].ToString(), e.Font, new SolidBrush(foreColor), new Point(e.Bounds.X, e.Bounds.Y));
                        }
                    });
                }
                else if (control is DataGridView)
                {
                    DataGridView dataGridView = control as DataGridView;
                    dataGridView.ForeColor = foreColor;
                    dataGridView.BackColor = backColor;
                    dataGridView.BackgroundColor = backColor;
                    dataGridView.DefaultCellStyle.BackColor = backColor;
                    dataGridView.DefaultCellStyle.ForeColor = foreColor;
                    dataGridView.DefaultCellStyle.SelectionBackColor = LaunchBoxSettings.DialogAccentColor;
                    dataGridView.DefaultCellStyle.SelectionForeColor = foreColor;
                    dataGridView.GridColor = backColorContrast1;
                    dataGridView.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
                    dataGridView.ColumnHeadersDefaultCellStyle.BackColor = backColorContrast2;
                    dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = foreColor;
                    dataGridView.ColumnHeadersDefaultCellStyle.SelectionBackColor = backColorContrast2;
                    dataGridView.ColumnHeadersDefaultCellStyle.SelectionForeColor = foreColor;
                    dataGridView.EnableHeadersVisualStyles = false;
                }
            }
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
}
