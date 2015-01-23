using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scanner
{
    class Parser
    {
        List<string> errors;
        bool insideWhile = false;
        bool insideIf = false;
      public static  List<TreeNode> tree;
        
        public static bool parserError;

        public void statementSequence(ref int index)
        {
            parserError = false;
            tree =new List<TreeNode>();
            errors = new List<string>();
            foreach (string res in Form1.scannerResults)
            {
                if (res == "Error1" || res == "Error2")
                {
                   
                    return;
                }
            }
            Form1.scannerResults.RemoveAll(item => item == "Comment");
            TreeNode node = new TreeNode();
            while (index <= Form1.scannerResults.Count - 1)
            {
                if (!checkStat(ref index, out node))
                {
                    //errors.Add(Form1.scannerResults[index]);
                    tree.Add(node);
                 if(index <= Form1.scannerResults.Count - 1)
                    parserError = true;
                    break;
                }
                else
                tree.Add(node);
            }
          
        }


        bool checkStat(ref int index,out TreeNode node)
        {
            node = new TreeNode();
            node.type = TreeNode.NodeType.Statements;
            TreeNode child=new TreeNode();
            if (index >= Form1.scannerResults.Count)
                return false;
            bool ret = false;
            if (Form1.scannerResults[index] == "if-stmt")
                ret = checkIf(ref index,out child);


            else if (Form1.scannerResults[index] == "repeat-stmt")
                ret = checkRepeat(ref index, out child);


            else if (Form1.scannerResults[index] == "Read-input-stmt")
                ret = checkRead(ref index, out child);


            else if (Form1.scannerResults[index] == "Write-output-stmt")
                ret = checkWrite(ref index, out child);

            else if (Form1.scannerResults[index] == "Assignment-symbol")
                ret = checkAssignment(ref index, out child);
            else if (Form1.scannerResults[index].Substring(0,2).Equals("ID") && index < Form1.scannerResults.Count - 1 && Form1.scannerResults[index + 1] == "Assignment-symbol")
            {
                index++;
                ret = checkAssignment(ref index, out child);
            }
            node.childs.Add(child);
            if (insideWhile || insideIf)
                return ret;
            
            else if ((ret && index >= Form1.scannerResults.Count-1) || (ret && Form1.scannerResults[index] == "Semi colon symbol"))
            {
                index++;
                return true;
            }
            return false;
        }


        bool checkIf(ref int index,out TreeNode node)
        {
            index++;
            node = new TreeNode();
            node.type = TreeNode.NodeType.If;
            TreeNode child ,childState,child2=new TreeNode(),child3=new TreeNode(),child4=new TreeNode(),child5=new TreeNode();

           
            insideIf = true;
            if (!checkExp(ref index,out child))
            {
                return false;
            }
            node.childs.Add(child);
            if (Form1.scannerResults[index] == "then-stmt")
            {
                index++;
                child2.type = TreeNode.NodeType.Then;
                node.childs.Add(child2);
                while (checkStat(ref index,out childState)) {node.childs.Add(childState);}

                if (Form1.scannerResults[index] == "end-stmt")
                {
                    index++;
                    child3.type = TreeNode.NodeType.End;
                    node.childs.Add(child3);
                    insideIf = false;
                    return true;
                }
                else if (Form1.scannerResults[index] == "else-stmt")
                {
                    index++;
                    child4.type = TreeNode.NodeType.Then;
                    node.childs.Add(child4);
                    while (checkStat(ref index, out childState)) { node.childs.Add(childState); }
                    if (Form1.scannerResults[index] == "end-stmt")
                    {
                        index++;
                       child5.type=TreeNode.NodeType.End;
                        node.childs.Add(child5);
                        insideIf = false;
                        return true;
                    }
                }
            }
            insideIf = false;
            return false;
        }

        bool checkRepeat(ref int index,out TreeNode node)
        {
            node = new TreeNode();
            node.type = TreeNode.NodeType.Repeat;
            index++;
            insideWhile = true;
            TreeNode childStat;
            while (checkStat(ref index, out childStat)) { node.childs.Add(childStat); }
            if (Form1.scannerResults[index] == "until-stmt")
            {
                index++;
                TreeNode child = new TreeNode();
                child.type = TreeNode.NodeType.Until;
                node.childs.Add(child);
                TreeNode child2 = new TreeNode();
                bool ret = checkExp(ref index,out child2);
                if(ret)
                    node.childs.Add(child2);
                insideWhile = false;
                return ret;
            }
            insideWhile = false;
            return false;

        }


        bool checkRead(ref int index, out TreeNode node)
        {
            node = new TreeNode();
            node.type = TreeNode.NodeType.Read;
            index++;
            if (index >= Form1.scannerResults.Count - 1)
                return false;

            if (Form1.scannerResults[index].Substring(0, 2).Equals("ID"))
            {
                TreeNode child = new TreeNode();
                child.type = TreeNode.NodeType.ID;
                child.value = Form1.scannerResults[index].Substring(2, Form1.scannerResults[index].Length - 2);
                node.childs.Add(child);
                index++;
                return true;
            }
            return false;
        }

        bool checkWrite(ref int index,out TreeNode node)
        {
            node = new TreeNode();
            node.type = TreeNode.NodeType.Write;
            index++;
            if (index >= Form1.scannerResults.Count - 1)
                return false;
            if (Form1.scannerResults[index].Substring(0, 2).Equals("ID"))
            {
                
                TreeNode child = new TreeNode();
                child.value = Form1.scannerResults[index].Substring(2, Form1.scannerResults[index].Length - 2);
                child.type=TreeNode.NodeType.ID;
                node.childs.Add(child);
                index++;
                return true;
            }
            return false;
        }

        bool checkSimpleExp(ref int index,out TreeNode node)
        {
            node = new TreeNode();
            node.type = TreeNode.NodeType.SimpleExp;
            TreeNode child1, child2=new TreeNode(), child3,child4;
            if (checkTerm(ref index,out child4))
            {
                while ((checkAdd_Sub(ref index, out child1) || checkMul(ref index, out child2)) && checkTerm(ref index, out child3)) 
                {
                    node.childs.Add(child1); node.childs.Add(child2); node.childs.Add(child3);
                }
                node.childs.Add(child4);     
                return true;
            }
            else
                return false;
        }
        bool checkTerm(ref int index,out TreeNode node)
        {
            node=new TreeNode();
            node.type=TreeNode.NodeType.Term;
            TreeNode child1=new TreeNode(),child2;
            if (index >= Form1.scannerResults.Count)
                return false;
            if ((Form1.scannerResults[index].Length>=6&& Form1.scannerResults[index].Substring(0, 6).Equals("Number")) || Form1.scannerResults[index].Substring(0, 2) == "ID")
            {
                if (Form1.scannerResults[index].Length >= 6 && Form1.scannerResults[index].Substring(0, 6).Equals("Number"))
                {
                    child1.value=Form1.scannerResults[index].Substring(6,Form1.scannerResults[index].Length-6 );
                    child1.type = TreeNode.NodeType.Number;
                }
                else
                {
                    child1.value = Form1.scannerResults[index].Substring(2, Form1.scannerResults[index].Length - 2);
                    child1.type = TreeNode.NodeType.ID;
                }
                node.childs.Add(child1);
                index++;
                return true;
            }
            if (index >= Form1.scannerResults.Count - 2)
                return false;
            if (Form1.scannerResults[index] == "Left bracket")
            {
                index++;
                if (checkExp(ref index,out child2))
                {
                    if (Form1.scannerResults[index] == "Right bracket")
                    {
                        node.childs.Add(child2);
                        index++;
                        return true;
                    }
                }
            }
            return false;

        }
        bool checkExp(ref int index,out TreeNode node)
        {
            node = new TreeNode();
            node.type = TreeNode.NodeType.Exp;
            TreeNode child1,child2,child3;
            if (checkSimpleExp(ref index,out child1))
            {
                if (checkComparision(ref index, out child2))
                {
                    
                    if (checkSimpleExp(ref index, out child3))
                    {
                        node.childs.Add(child1);
                        node.childs.Add(child2);
                        node.childs.Add(child3);
                        return true;
                    }
                }
                else
                {
                    node.childs.Add(child1);
                    return true;
                }
            }
        //    node = null;
            return false;
        }


        bool checkAdd_Sub(ref int index,out TreeNode node)
        {
            node = new TreeNode();
            if (index >= Form1.scannerResults.Count)
            {
                node = null;
                return false;
            }
            if (Form1.scannerResults[index] == "Addition-symbol" || Form1.scannerResults[index] == "Subtraction-symbol")
            {
                if (Form1.scannerResults[index] == "Addition-symbol")
                    node.type = TreeNode.NodeType.Add;
                else
                    node.type = TreeNode.NodeType.Sub;
                index++;
                return true;
            }
           // node = null;
            return false;
        }

        bool checkMul(ref int index,out TreeNode node)
        {
            node = new TreeNode();
            if (index >= Form1.scannerResults.Count)
            {
               // node = null;
                return false;
            }
            if (Form1.scannerResults[index] == "Multiplication-symbol")
            {
                index++;
                node.type = TreeNode.NodeType.Mul;
                return true;
            }
         //   node = null;
            return false;
        }

        bool checkComparision(ref int index,out TreeNode node)
        {
            node = new TreeNode();
           
            if (index >= Form1.scannerResults.Count)
                return false;
            if (Form1.scannerResults[index] == "Equal-symbol")
            {
                node.type = TreeNode.NodeType.Equal;
                index++;
                return true;
            }
            if (Form1.scannerResults[index] == "Greater-symbol")
            {
                
                node.type = TreeNode.NodeType.GreaterThan;
                index++;
                return true;
            }
            if (Form1.scannerResults[index] == "Smaller-symbol")
            {
                node.type = TreeNode.NodeType.LessThan;
                index++;
                return true;
            }
           // node = null;
            return false;
        }
        bool checkAssignment(ref int index,out TreeNode node)
        {
            node = new TreeNode();
            node.type = TreeNode.NodeType.Assign;
            TreeNode child1=new TreeNode(), child2;
            if (index <= 0 || index >= Form1.scannerResults.Count - 1)
            {
                return false;
            }
            if (Form1.scannerResults[index - 1].Substring(0, 2).Equals("ID"))
            {
                child1.value = Form1.scannerResults[index-1].Substring(2, Form1.scannerResults[index-1].Length - 2);
                child1.type = TreeNode.NodeType.ID;
                node.childs.Add(child1);
                index++;
                if (checkExp(ref index, out child2))
                {
                    node.childs.Add(child2);
                    return true;
                }
            }
            return false;
        }

    }
}