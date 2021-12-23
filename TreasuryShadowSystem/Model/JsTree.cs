using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace TreasuryShadowSystem.Model
{
    public class JsTree
    {
        public List<JsTreeNode> data;

        public JsTree()
        {
            data = new List<JsTreeNode>();
        }
    }

    public class JsTreeNode
    {
        public Attributes attributes { get; set; }
        public Data data { get; set; }
        public string state { get; set; }
        public List<JsTreeNode> children { get; set; }

        public JsTreeNode()
        {
            children = new List<JsTreeNode>();
            data = new Data();
            attributes = new Attributes();
        }
        public JsTreeNode(string title)
            : this()
        {
            data.title = title;
        }

    }
    public class Attributes
    {
        public string id { get; set; }
        public string rel { get; set; }
        public string mdata { get; set; }
    }

    public class Data
    {
        public string title { get; set; }
        public string icon { get; set; }
    }

    public static class JsTree_ExtensionMethods
    {
        public static string jsonString(this object _object)
        {
            return new JavaScriptSerializer().Serialize(_object);
        }

        public static JsTreeNode add_Node(this JsTree jsTree, string title)
        {
            var newJsTreeNode = new JsTreeNode(title);
            jsTree.data.Add(newJsTreeNode);
            return newJsTreeNode;
        }

        public static List<JsTreeNode> add_Nodes(this JsTree jsTree, params string[] titles)
        {
            var newJsTreeNodes = new List<JsTreeNode>();
            foreach (var title in titles)
                newJsTreeNodes.Add(jsTree.add_Node(title));
            return newJsTreeNodes;
        }

        public static JsTreeNode add_Node(this JsTreeNode jsTreeNode, string title)
        {
            var newJsTreeNode = new JsTreeNode(title);
            jsTreeNode.children.Add(newJsTreeNode);
            return newJsTreeNode;
        }

        public static List<JsTreeNode> add_Nodes(this JsTreeNode jsTreeNode, params string[] titles)
        {
            var newJsTreeNodes = new List<JsTreeNode>();
            foreach (var title in titles)
                newJsTreeNodes.Add(jsTreeNode.add_Node(title));
            return newJsTreeNodes;
        }
    }

    public class G_JSTree
    {
        public G_JsTreeAttribute attr;
        public G_JSTree[] children;
        public string data
        {
            get;
            set;
        }
        public int IdServerUse
        {
            get;
            set;
        }
        public string icons
        {
            get;
            set;
        }
        public string state
        {
            get;
            set;
        }
    }

    public class G_JsTreeAttribute
    {
        public string id;
        public bool selected;
        public bool undetermind;
    }
}
