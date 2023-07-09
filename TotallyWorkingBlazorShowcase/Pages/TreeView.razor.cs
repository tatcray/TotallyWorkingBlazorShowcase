// TreeView.razor.cs

using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace TotallyWorkingBlazorShowcase.Pages
{
    public class TreeNode {
        public string Name { get; set; }
        public List<TreeNode> Childrens { get; set; }
        public bool IsExpanded { get; set; } 
    }

    public class TreeViewModel: ComponentBase
    {
        // Модель данных дерева
        public TreeNode treeData = new()
        {
            Name = "User 1",
            Childrens = new List<TreeNode> { new() { Name = "User 2", Childrens = new List<TreeNode>(){ new() {Name = "USer 5", Childrens = new List<TreeNode>()},  new() {Name = "USer 6", Childrens = new List<TreeNode>(){ new() {Name = "USer 8", Childrens = new List<TreeNode>()}, new() {Name = "USer 9", Childrens = new List<TreeNode>()}}}} }, new() {Name = "USer 3", Childrens = new List<TreeNode>(){ new() {Name = "USer 4", Childrens = new List<TreeNode>()}}}, }
        };
        //{
            /*new TreeNode
            {
                Name = "User 1",
                Children = new List<TreeNode>
                {
                    new() { Name = "Computer 1" },
                    new() { Name = "Computer 2" }
                }
            },
            new TreeNode
            {
                Name = "User 2",
                Children = new List<TreeNode>
                {
                    new()
                    {
                        Name = "Nested User",
                        Children = new List<TreeNode>
                        {
                            new() { Name = "Nested Computer 1" },
                            new() { Name = "Nested Computer 2" }
                        }
                    }
                }
            }*/
        };
}
