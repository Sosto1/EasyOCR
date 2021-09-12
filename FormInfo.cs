using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCR
{
    public partial class FormInfo : Form
    {
        string[] LangCes = new string[] {"Jazyk:", "Zkratka pro výběr", "Změny se projeví po restartování aplikace", "Informace", "Vývojářský režim", "Hlasitost"};
        string[] LangEng = new string[] { "Language:", "Capture shortcut" , "Changes will take effect after restarting app", "Information", "Developer mode", "Volume"};
        Settings1 settings = new Settings1();
        public FormInfo()
        {
            InitializeComponent();
            comboBox_Language.DropDownStyle = ComboBoxStyle.DropDownList; 
        }
        private void FormInfo_Load(object sender, EventArgs e)
        {
            label_InfoRestart.Visible = false;           
            comboBox_Language.SelectedIndex = settings.Language;
            checkBox_DevOpt.Checked = settings.DeveloperOptions;
            switch (settings.Language)
            {
                case 0:
                    label_InfoRestart.Text = LangCes[2];
                    label_Tutorial.Text = LangCes[1];
                    label_Lang.Text = LangCes[0];
                    this.Text = LangCes[3];
                    checkBox_DevOpt.Text = LangCes[4];
                    break;
                case 1:
                    label_InfoRestart.Text = LangEng[2];
                    label_Tutorial.Text = LangEng[1];
                    label_Lang.Text = LangEng[0];
                    this.Text = LangEng[3];
                    checkBox_DevOpt.Text = LangEng[4];
                    break;
            }
        }
        
        private void comboBox_Language_SelectedIndexChanged(object sender, EventArgs e)
        {
            settings.Language = comboBox_Language.SelectedIndex;
            settings.Save();
        }

        private void comboBox_Language_SelectionChangeCommitted(object sender, EventArgs e)
        {
            label_InfoRestart.Visible = true;
        }

        private void checkBox_DevOpt_CheckedChanged_1(object sender, EventArgs e)
        {
            settings.DeveloperOptions = checkBox_DevOpt.Checked;
            settings.Save();
        }
        private void checkBox_DevOpt_MouseDown_1(object sender, MouseEventArgs e)
        {
            label_InfoRestart.Visible = true;
        }

    }
}
