using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scanner
{
    public partial class ParserTree : Form
    {
        public ParserTree()
        {
            InitializeComponent();
            int index=0;
            foreach (TreeNode child in Parser.tree)
            {
                treeView1.Nodes.Add("");
                DisplayParseTree(child,treeView1.Nodes[index]);
                index++;
            }
        }

        private void ParserTree_Load(object sender, EventArgs e)
        {
            
        }


        public static void DisplayParseTree(TreeNode Node, System.Windows.Forms.TreeNode DisplayNode)
        {
            TreeNode OpNode;
            System.Windows.Forms.TreeNode[] T1 = new System.Windows.Forms.TreeNode[100];
            for (int i = 0; i < 100; i++)
                T1[i] = new System.Windows.Forms.TreeNode();
            int index = 0;
            switch (Node.type)
            {
                case TreeNode.NodeType.Statements:
                    OpNode = (TreeNode)Node;
                    DisplayNode.Text = "Statements";
                   // int index = 0;
                    foreach (TreeNode child in Node.childs)
                    {

                        DisplayParseTree(child, T1[index]);
                        DisplayNode.Nodes.Add(T1[index]);
                        index++; 
                    }

                    break;
                case TreeNode.NodeType.If:
                    OpNode = (TreeNode)Node;
                    DisplayNode.Text = "If";
                //    int index = 0;
                    foreach (TreeNode child in Node.childs)
                    {

                        DisplayParseTree(child, T1[index]);
                        DisplayNode.Nodes.Add(T1[index]);
                        index++; 
                    }
                    break;
                case TreeNode.NodeType.Then:
                    OpNode = (TreeNode)Node;
                    DisplayNode.Text = "Then";
                 //   int index = 0;
                    foreach (TreeNode child in Node.childs)
                    {

                        DisplayParseTree(child, T1[index]);
                        DisplayNode.Nodes.Add(T1[index]);
                        index++; 
                    }
                    break;
                case TreeNode.NodeType.Else:
                    OpNode = (TreeNode)Node;
                    DisplayNode.Text = "Else";
                 //   int index = 0;
                    foreach (TreeNode child in Node.childs)
                    {

                        DisplayParseTree(child, T1[index]);
                        DisplayNode.Nodes.Add(T1[index]);
                        index++; 
                    }
                    break;
                case TreeNode.NodeType.Add:
                    OpNode = (TreeNode)Node;
                    DisplayNode.Text = "+";
               //     int index = 0;
                    foreach (TreeNode child in Node.childs)
                    {

                        DisplayParseTree(child, T1[index]);
                        DisplayNode.Nodes.Add(T1[index]);
                        index++; 
                    }
                    break;
                case TreeNode.NodeType.Sub:
                    OpNode = (TreeNode)Node;
                    DisplayNode.Text = "-";
               //     int index = 0;
                    foreach (TreeNode child in Node.childs)
                    {

                        DisplayParseTree(child, T1[index]);
                        DisplayNode.Nodes.Add(T1[index]);
                        index++; 
                    }
                    break;
                case TreeNode.NodeType.Mul:
                    OpNode = (TreeNode)Node;
                    DisplayNode.Text = "*";
               //     int index = 0;
                    foreach (TreeNode child in Node.childs)
                    {

                        DisplayParseTree(child, T1[index]);
                        DisplayNode.Nodes.Add(T1[index]);
                        index++; 
                    }
                    break;
                case TreeNode.NodeType.Divide:
                    OpNode = (TreeNode)Node;
                    DisplayNode.Text = "/";
                 //   int index = 0;
                    foreach (TreeNode child in Node.childs)
                    {

                        DisplayParseTree(child, T1[index]);
                        DisplayNode.Nodes.Add(T1[index]);
                        index++; 
                    }
                    break;
                case TreeNode.NodeType.Assign:
                    OpNode = (TreeNode)Node;
                    DisplayNode.Text = ":=";
                  //  int index = 0;
                    foreach (TreeNode child in Node.childs)
                    {

                        DisplayParseTree(child, T1[index]);
                        DisplayNode.Nodes.Add(T1[index]);
                        index++; 
                    }
                    break;
                case TreeNode.NodeType.Equal:
                    OpNode = (TreeNode)Node;
                    DisplayNode.Text = "=";
                 //   int index = 0;
                    foreach (TreeNode child in Node.childs)
                    {

                        DisplayParseTree(child, T1[index]);
                        DisplayNode.Nodes.Add(T1[index]);
                        index++; 
                    }
                    break;
                case TreeNode.NodeType.GreaterThan:
                    OpNode = (TreeNode)Node;
                    DisplayNode.Text = ">";
                   // int index = 0;
                    foreach (TreeNode child in Node.childs)
                    {

                        DisplayParseTree(child, T1[index]);
                        DisplayNode.Nodes.Add(T1[index]);
                        index++; 
                    }
                    break;
                case TreeNode.NodeType.LessThan:
                    OpNode = (TreeNode)Node;
                    DisplayNode.Text = "<";
                  //  int index = 0;
                    foreach (TreeNode child in Node.childs)
                    {

                        DisplayParseTree(child, T1[index]);
                        DisplayNode.Nodes.Add(T1[index]);
                        index++; 
                    }
                    break;
                case TreeNode.NodeType.Exp:
                    OpNode = (TreeNode)Node;
                    DisplayNode.Text = "Exp";
                  //  int index = 0;
                    foreach (TreeNode child in Node.childs)
                    {

                        DisplayParseTree(child, T1[index]);
                        DisplayNode.Nodes.Add(T1[index]);
                        index++; 
                    }
                    break;

                case TreeNode.NodeType.SimpleExp:
                    OpNode = (TreeNode)Node;
                    DisplayNode.Text = "Simple Exp";
                   // int index = 0;
                    foreach (TreeNode child in Node.childs)
                    {

                        DisplayParseTree(child, T1[index]);
                        DisplayNode.Nodes.Add(T1[index]);
                        index++; 
                    }
                    break;

                case TreeNode.NodeType.Term:
                    OpNode = (TreeNode)Node;
                    DisplayNode.Text = "Term";
                   // int index = 0;
                    foreach (TreeNode child in Node.childs)
                    {

                        DisplayParseTree(child, T1[index]);
                        DisplayNode.Nodes.Add(T1[index]);
                        index++; 
                    }
                    break;
                case TreeNode.NodeType.Read:
                    OpNode = (TreeNode)Node;
                    DisplayNode.Text = "Read";
                   // int index = 0;
                    foreach (TreeNode child in Node.childs)
                    {

                        DisplayParseTree(child, T1[index]);
                        DisplayNode.Nodes.Add(T1[index]);
                        index++; 
                    }
                    break;
                case TreeNode.NodeType.Write:
                    OpNode = (TreeNode)Node;
                    DisplayNode.Text = "Write";
                   // int index = 0;
                    foreach (TreeNode child in Node.childs)
                    {
                        
                        DisplayParseTree(child, T1[index]);
                        DisplayNode.Nodes.Add(T1[index]);
                        index++; 
                    }
                    break;
                case TreeNode.NodeType.Repeat:
                    OpNode = (TreeNode)Node;
                    DisplayNode.Text = "Repeat";
                    // int index = 0;
                    foreach (TreeNode child in Node.childs)
                    {

                        DisplayParseTree(child, T1[index]);
                        DisplayNode.Nodes.Add(T1[index]);
                        index++; 
                    }
                    break;
                case TreeNode.NodeType.Until:
                    OpNode = (TreeNode)Node;
                    DisplayNode.Text = "Until";
                   // int index = 0;
                    foreach (TreeNode child in Node.childs)
                    {

                        DisplayParseTree(child, T1[index]);
                        DisplayNode.Nodes.Add(T1[index]);
                        index++; 
                    }
                    break;
                case TreeNode.NodeType.Number:
                    DisplayNode.Text = Node.value;
                    break;
                case TreeNode.NodeType.ID:
                    DisplayNode.Text = Node.value;
                    break;
                case TreeNode.NodeType.End:
                    DisplayNode.Text = "End";
                    break;
            }
        }



        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            treeView1.ExpandAll();
        }
    }


}
