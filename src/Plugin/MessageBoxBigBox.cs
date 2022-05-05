using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArchiveCacheManager
{
    public partial class MessageBoxBigBox : Form
    {
        public MessageBoxBigBox(string text)
        {
            InitializeComponent();

            UserInterface.ScaleControlFont(message, 96.0f / message.DeviceDpi);

            message.Text = text;
        }
    }
}
