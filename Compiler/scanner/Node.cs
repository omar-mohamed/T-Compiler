using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scanner
{
    public class TreeNode
    {

        public enum NodeType
        {
            Statements, Add, Sub, Mul, Divide, Number, ID, Term, If, Repeat, Until, stmt_seq, Exp, SimpleExp, Write, Read, Assign, Equal, LessThan, GreaterThan, Else, Then,End
        }

        public List<TreeNode> childs = new List<TreeNode>();
        public NodeType type;
        public string value;


    }
}
