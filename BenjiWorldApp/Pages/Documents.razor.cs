using DataExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;
using Models;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace BenjiWorldApp.Pages
{
    public class Category {
    
        public string CategoryName { get; set; }
        public List<DocumentModel> Products { get; set; }
    }

    public class DocumentsBase : ComponentBase
    {
        public DocumentsBase()
        {
            DocumentModels = new List<DocumentModel>();
        }

        public List<DocumentModel> DocumentModels { get; set; }

        [Inject]
        public BenjiAPIClient Client { get; set; }
        [Inject]
        protected NotificationService NotificationService { get; set; }    
        protected override async Task OnInitializedAsync()
        {
            var docs = await Client.GetAllDocuments();
            DocumentModels = docs;
        }

        ///////////////////////////////////////////
        ///

        public IEnumerable<string> entries = null;
        public Dictionary<DateTime, string> events = new Dictionary<DateTime, string>();

        public void Log(string eventName, string value)
        {
            events.Add(DateTime.Now, $"{eventName}: {value}");
        }

        public void LogChange(TreeEventArgs args)
        {
            Log("Change", $"Item Text: {args.Text}");
        }

        public void LogExpand(TreeExpandEventArgs args)
        {
            if (args.Value is Category category)
            {
                Log("Expand", $"Text: {category.CategoryName}");
            }

            if (args.Value is string text)
            {
                Log("Expand", $"Text: {text}");
            }
        }

        protected override void OnInitialized()
        {
            entries = Directory.GetDirectories("/")
                               .Where(entry =>
                               {
                                   var name = Path.GetFileName(entry);

                                   return !name.StartsWith(".") && name != "bin" && name != "obj";
                               });
        }

        public string GetTextForNode(object data)
        {
            return Path.GetFileName((string)data);
        }

        public RenderFragment<RadzenTreeItem> FileOrFolderTemplate = (context) => builder =>
        {
            string path = context.Value as string;
            bool isDirectory = Directory.Exists(path);

            builder.OpenComponent<RadzenIcon>(0);
            builder.AddAttribute(1, "Icon", isDirectory ? "folder" : "insert_drive_file");
            if (!isDirectory)
            {
                builder.AddAttribute(2, "Style", "margin-left: 24px");
            }
            builder.CloseComponent();
            builder.AddContent(3, context.Text);
        };

        public void OnExpand(TreeExpandEventArgs args)
        {
            var category = args.Value as Category;

            args.Children.Data = category.Products;
            args.Children.TextProperty = "ProductName";
            args.Children.HasChildren = (product) => false;

            /* Optional template
            args.Children.Template = context => builder => {
                builder.OpenElement(1, "strong");
                builder.AddContent(2, (context.Value as Product).ProductName);
                builder.CloseElement();
            };
            */

            LogExpand(args);
        }

        public void LoadFiles(TreeExpandEventArgs args)
        {
            var directory = args.Value as string;

            args.Children.Data = Directory.EnumerateFileSystemEntries(directory);
            args.Children.Text = GetTextForNode;
            args.Children.HasChildren = (path) => Directory.Exists((string)path);
            args.Children.Template = FileOrFolderTemplate;

            Log("Expand", $"Text: {args.Text}");
        }

    }
}
