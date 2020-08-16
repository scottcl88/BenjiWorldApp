using DataExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
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

    public class SelectedFolderModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class DocumentsBase : ComponentBase
    {
        public DocumentsBase()
        {
            DocumentModels = new List<DocumentModel>();
            FolderModels = new List<SelectedFolderModel>();
            NewFolderModel = new FolderModel();
            UploadUrl = "http://localhost:59006/document/upload/multiple";
        }
        public int SelectedFolderId { get; set; }
        public IEnumerable<DocumentModel> DocumentModels { get; set; }
        public List<SelectedFolderModel> FolderModels { get; set; }
        public FolderModel NewFolderModel { get; set; }
        public bool ShowEditFolder { get; set; }
        public string UploadUrl { get; set; }

        [Inject]
        public BenjiAPIClient Client { get; set; }
        [Inject]
        protected NotificationService NotificationService { get; set; }    
        protected override async Task OnInitializedAsync()
        {
            var docs = await Client.GetAllDocuments();
            DocumentModels = docs;
            var folders = await Client.GetAllFolders();
            FolderModels = folders.Select(x => new SelectedFolderModel() { Id = x.FolderId.Value, Name = x.Name }).ToList();

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
        ////////////////////////////////////////////////
        ///  
        public RadzenUpload upload;

        public int progress;
        public string info;

        public void OnProgress(UploadProgressArgs args, string name)
        {
            this.info = $"% '{name}' / {args.Loaded} of {args.Total} bytes.";
            this.progress = args.Progress;

            if (args.Progress == 100)
            {
                events.Clear();
                foreach (var file in args.Files)
                {
                    events.Add(DateTime.Now, $"Uploaded: {file.Name} / {file.Size} bytes");
                }
            }
        }

        public void Completed(UploadCompleteEventArgs args)
        {
            events.Add(DateTime.Now, $"Server response: {args.RawResponse}");
        }

        public void Change(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

            events.Add(DateTime.Now, $"{name} value changed to {str}");
            UploadUrl += $"/{value}";
            StateHasChanged();
        }

        public async Task HandleValidSubmit()
        {
            HttpResponseMessage result = null;
            if (NewFolderModel.FolderId == null || NewFolderModel.FolderId.Value == 0)
            {
                var request = new FolderCreateRequest();
                request.Folder.FolderId = NewFolderModel.FolderId;
                request.Folder.Name = NewFolderModel.Name;
                request.Folder.Created = NewFolderModel.Created;
                request.Folder.Modified = NewFolderModel.Modified;
                request.Folder.Deleted = NewFolderModel.Deleted;
                result = await Client.CreateFolder(request);
            }
            else
            {
                var request = new FolderUpdateRequest();
                request.Folder.FolderId = NewFolderModel.FolderId;
                request.Folder.Name = NewFolderModel.Name;
                request.Folder.Created = NewFolderModel.Created;
                request.Folder.Modified = NewFolderModel.Modified;
                request.Folder.Deleted = NewFolderModel.Deleted;
                result = await Client.UpdateFolder(request);
            }
            if (result.IsSuccessStatusCode)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Saved successfully");
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed", result.ReasonPhrase, 6000);
            }
        }

        public void AddFolder(MouseEventArgs e)
        {
            ShowEditFolder = true;
            StateHasChanged();
        }
        public void EditFolder(MouseEventArgs e)
        {
            ShowEditFolder = true;
            StateHasChanged();
        }
    }
}
