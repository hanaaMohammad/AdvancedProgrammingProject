using System.Windows.Forms;

namespace AdvancedProgramming
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Theme.Apply(this);
        }
    }
}
