using System.Windows.Forms;

namespace AdvancedProgramming
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            DatabaseManager.InitializeDatabase();
            InitializeComponent();
        }

        private void Form1_Load(object sender, System.EventArgs e)
        {

        }
    }
}
