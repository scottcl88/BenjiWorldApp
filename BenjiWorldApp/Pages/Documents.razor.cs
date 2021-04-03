using DataExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Models.Shared;
using Radzen;
using Radzen.Blazor;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BenjiWorldApp.Pages
{
    public class Category
    {
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
            var builder = WebAssemblyHostBuilder.CreateDefault();
            APIUrl = builder.Configuration["APIUrl"];
            UploadUrl = APIUrl + "/document/upload/multiple";
        }

        public long SelectedFolderId { get; set; }
        public IEnumerable<DocumentModel> DocumentModels { get; set; }
        public List<SelectedFolderModel> FolderModels { get; set; }
        public FolderModel NewFolderModel { get; set; }
        public DocumentModel Model { get; set; }
        public bool ShowEditFolder { get; set; }
        public bool ShowEditDocument { get; set; }
        public string UploadUrl { get; set; }
        public string APIUrl { get; set; }

        [Inject]
        public BenjiAPIClient Client { get; set; }

        [Inject]
        protected NotificationService NotificationService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var docs = await Client.GetAllDocuments();
            DocumentModels = docs.OrderByDescending(x => x.Created);
            var folders = await Client.GetAllFolders();
            FolderModels = folders.Select(x => new SelectedFolderModel() { Id = x.FolderId.Value, Name = x.Name }).ToList();
            SelectedFolderId = FolderModels.First().Id;
            StateHasChanged();
        }

        ///////////////////////////////////////////
        public RadzenUpload Upload { get; set; }

        public double Progress { get; set; }

        public void OnProgress(UploadProgressArgs args)
        {
            Progress = args.Progress;
        }

        public async Task Completed(UploadCompleteEventArgs args)
        {
            NotificationService.Notify(NotificationSeverity.Success, "Uploaded Successfully");
            DocumentModels = await Client.GetAllDocuments();
            Progress = 0;
            StateHasChanged();
        }

        public void Error(UploadErrorEventArgs args)
        {
            NotificationService.Notify(NotificationSeverity.Error, "Failed: " + args.Message);
        }

        public void Change(object value)
        {
            SelectedFolderId = (long)value;
            StateHasChanged();
        }

        public async Task HandleValidFolderSubmit()
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
                DocumentModels = await Client.GetAllDocuments();
                ShowEditFolder = false;
                ShowEditDocument = false;
                StateHasChanged();
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed", result.ReasonPhrase, 6000);
            }
        }

        public async Task HandleValidDocumentSubmit()
        {
            HttpResponseMessage result = null;

            var request = new DocumentUpdateRequest();
            request.Document.DocumentId = Model.DocumentId;
            request.Document.FileName = Model.FileName;
            request.Document.Folder = new FolderModel() { FolderId = new FolderId() { Value = SelectedFolderId } };
            request.Document.Description = Model.Description;
            result = await Client.UpdateDocument(request);

            if (result.IsSuccessStatusCode)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Saved successfully");
                DocumentModels = await Client.GetAllDocuments();
                SelectedFolderId = FolderModels.First().Id;
                ShowEditFolder = false;
                ShowEditDocument = false;
                StateHasChanged();
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed", result.ReasonPhrase, 6000);
            }
        }

        public void EditData(MouseEventArgs e, DocumentModel model)
        {
            Model = model;
            SelectedFolderId = model.Folder.FolderId.Value;
            ShowEditDocument = true;
            StateHasChanged();
        }

        public void CancelEditData(MouseEventArgs e)
        {
            ShowEditFolder = false;
            ShowEditDocument = false;
            StateHasChanged();
        }

        public void EditFolder(MouseEventArgs e)
        {
            ShowEditFolder = true;
            StateHasChanged();
        }

        public async Task DeleteData(MouseEventArgs e)
        {
            var result = await Client.DeleteDocument(new DocumentDeleteRequest() { DocumentId = Model.DocumentId });
            if (result.IsSuccessStatusCode)
            {
                NotificationService.Notify(NotificationSeverity.Success, "Deleted successfully");
                DocumentModels = await Client.GetAllDocuments();
                ShowEditFolder = false;
                ShowEditDocument = false;
                StateHasChanged();
            }
            else
            {
                NotificationService.Notify(NotificationSeverity.Error, "Failed", result.ReasonPhrase, 6000);
            }
        }
    }
}