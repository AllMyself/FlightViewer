using System.Windows.Forms;
using BinHong.FlightViewerCore;

namespace BinHong.FlightViewerUI
{
    /// <summary>
    /// 简单的TreeNode
    /// 其是所有的TreeNode的父类，且为abstract
    /// </summary>
    public abstract class AbstractTreeNode : TreeNode
    {
        public string Path { get; set; }

        public virtual void OnClick(PageManager pageManager)
        {
            
        }

        public virtual void AddChildNode(TreeNode node)
        {
            this.Nodes.Add(node);
            this.ExpandAll();
            AbstractTreeNode child = (AbstractTreeNode)node;
            child.Path = this.Path + "_" + child.Name;
        }

        public AbstractTreeNode FindeTreeNodeByPath(AbstractTreeNode node, string path)
        {
            if (node == null)
            {
                return null;
            }
            if (node.Path == path)
            {
                return node;
            }
            foreach (AbstractTreeNode nodeItem in node.Nodes)
            {
                AbstractTreeNode findNode = FindeTreeNodeByPath(nodeItem, path);
                if (findNode != null)
                {
                    return findNode;
                }
            }

            return null;
        }
    }

    public class SimpleTreeNode : AbstractTreeNode
    {
        
    }

    public class TreeLocalHost : AbstractTreeNode
    {
        public TreeLocalHost()
        {
            Name = "localhost";
            Text = "localhost";
            Path = PathString;
        }

        public static string PathString = "host";
    }

    public class TreeBusNode : AbstractTreeNode
    {
        public TreeBusNode(string boardType)
        {
            ImageKey = TreeNodeImageName.BusImage;
            SelectedImageKey = TreeNodeImageName.BusImage;
            Text = boardType;
            Name = boardType;
        }
        public override void AddChildNode(TreeNode node)
        {
            base.AddChildNode(node);
            this.ExpandAll();
            node.ContextMenu = new TreeDeviceContexMenu(this.Name, node.Name);
        }
    }

    public class TreeDeviceNode : AbstractTreeNode
    {
        public TreeDeviceNode(string boardNo)
        {
            ImageKey = TreeNodeImageName.DeviceImage;
            SelectedImageKey = TreeNodeImageName.DeviceImage;
            Text = "Device" + boardNo;
            Name = boardNo;
        }

        public override void OnClick(PageManager pageManager)
        {
            pageManager.ShowRightPage(Path);
        }

        public override void AddChildNode(TreeNode node)
        {
            int index=FindProperIndex(node.Name);
            this.Nodes.Insert(index,node);
            AbstractTreeNode child = (AbstractTreeNode)node;
            child.Path = this.Path + "_" + child.Name;
        }

        /// <summary>
        /// 采用二分法查找恰当的索引
        /// </summary>
        /// <returns></returns>
        private int FindProperIndex(string valueStr)
        {
            if (Nodes.Count < 1)
            {
                return 0;
            }
           
            int maxIndex = Nodes.Count - 1;
            int minIndex = 0;
            int midIndex = (maxIndex + minIndex) / 2;

            int value = int.Parse(valueStr);
            int maxValue = int.Parse(Nodes[maxIndex].Name);
            int minValue = int.Parse(Nodes[midIndex].Name);

            int sign = 1;
            if (maxValue < minValue)
            {
                sign = -1;
            }
            while ((maxIndex - minIndex) / 2 != 0)
            {
                int midValue = int.Parse(Nodes[midIndex].Name);

                int isBigToMidValue = sign * (value - midValue);
                if (isBigToMidValue == 0)
                {
                    break;
                }

                if (isBigToMidValue > 0)
                {
                    minIndex = midIndex;
                }
                else
                {
                    maxIndex = midIndex;
                }
                midIndex = (maxIndex + minIndex) / 2;
            }

            maxValue = int.Parse(Nodes[maxIndex].Name);
            minValue = int.Parse(Nodes[midIndex].Name);

            if (sign * (value- maxValue) > 0)
            {
                return maxIndex + 1;
            }
            if (sign * (value - minValue) < 0)
            {
                return minIndex;
            }
            return maxIndex;
        }
    }
}
