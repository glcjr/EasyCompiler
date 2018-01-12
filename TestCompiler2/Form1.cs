using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EasyCompile;
/*********************************************************************************************************************************
Copyright and Licensing Message

This code is copyright 2017 Gary Cole Jr. 

This code is licensed by Gary Cole to others under the GPLv.3 https://opensource.org/licenses/GPL-3.0 
If you find the code useful or just feel generous a donation is appreciated.

Donate with this link: paypal.me/GColeJr
Please choose Friends and Family

Alternative Licensing Options

If you prefer to license under the LGPL for a project, https://opensource.org/licenses/LGPL-3.0
Single Developers working on their own project can do so with a donation of $20 or more. 
Small and medium companies can do so with a donation of $50 or more. 
Corporations can do so with a donation of $1000 or more.


If you prefer to license under the MS-RL for a project, https://opensource.org/licenses/MS-RL
Single Developers working on their own project can do so with a donation of $40 or more. 
Small and medium companies can do so with a donation of $100 or more.
Corporations can do so with a donation of $2000 or more.


if you prefer to license under the MS-PL for a project, https://opensource.org/licenses/MS-PL
Single Developers working on their own project can do so with a donation of $1000 or more. 
Small and medium companies can do so with a donation of $2000 or more.
Corporations can do so with a donation of $10000 or more.


If you use the code in more than one project, a separate license is required for each project.


Any modifications to this code must retain this message. 
*************************************************************************************************************************************/

namespace TestCompiler2
{
    public partial class Form1 : Form
    {
        List<string> FileNames = new List<string>();
        List<string> Embeds = new List<string>();
        List<string> Assemblies = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "csharp files (*.cs)|*.cs|vbasic files(*.vb)|*.vb|All files (*.*)|*.*"; 
            if (of.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in of.FileNames)
                    FileNames.Add(file);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog of = new OpenFileDialog();
            of.Filter = "DLL(*.dll)|*.dll|All Files (*.*|*.*";
            if (of.ShowDialog() == DialogResult.OK)
            {
                foreach (var file in of.FileNames)
                    if (!(Embeds.Contains(file)))
                        Embeds.Add(file);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Assemblies.Add(txtassembly.Text);
            txtassembly.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Compiler compile;
            if (rbCSharp.Checked)
                compile = new Compiler(CompilerLanguages.csharp, new List<string>(), Assemblies, Embeds);
            else
                compile = new Compiler(CompilerLanguages.visualbasic, new List<string>(), Assemblies, Embeds);
            compile.AddSourceFiles(FileNames.ToArray());
            if (rbexe.Checked)
                compile.SetToOutputEXE();
            else if (rbmemory.Checked)
                compile.SetToMemoryOutputOnly();
            else
                compile.SetToOutputDLL();
            compile.SetResultFileName($"{AppDomain.CurrentDomain.BaseDirectory}\\result");
            if (compile.Compile())
            {
                if (rbmemory.Checked)
                    txtoutput.Text = "Compiled Successfully";
                else
                    txtoutput.Text = $"Compiled successfully to {compile.GetName()}";
            }
            else
                txtoutput.Text = $"There were {compile.ErrorCount} errors{Environment.NewLine}{compile.GetErrorsAsString()}";
        }
    }
}
