using Microsoft.Office.Interop.Word;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Sundeus.Clm.AzureStorage.Interface;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Sundeus.Clm.AzureStorage.Repository
{
    public class BlobRepository : IBlobRepository
    {
        private CloudStorageAccount _cloudStorageAccount;
        private CloudBlobClient _cloudBlobClient;

        public void Initialize(string connectionString)
        {
            _cloudStorageAccount = CloudStorageAccount.Parse(connectionString);
            _cloudBlobClient = _cloudStorageAccount.CreateCloudBlobClient();
        }

        public async Task<string> UploadFileToBlob(string containerName, string filePath)
        {
            try
            {
                CreatBlobIfNotExist(containerName);
                string blobName = GenerateFileName(filePath);
                CloudBlobContainer cloudBlobContainer = _cloudBlobClient.GetContainerReference(containerName);

                //get Blob reference
                CloudBlockBlob cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(blobName);
                //using (var fileStream = System.IO.File.Open(filePath, FileMode.Open,F))
                //{
                //    cloudBlockBlob.UploadFromStreamAsync(fileStream).Wait();
                //    //await cloudBlockBlob.UploadFromStreamAsync(fileStream);
                //}
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    using (var ms = new MemoryStream())
                    {
                        cloudBlockBlob.UploadFromStreamAsync(ms).Wait();
                        //await cloudBlockBlob.UploadFromStreamAsync(ms);
                    }
                }

                return cloudBlockBlob.Uri.AbsoluteUri;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<byte[]> DownloadBlobFiles(string containerName, string FileName)
        {
            // Get Blob Container
            CloudBlobContainer cloudBlobContainer = _cloudBlobClient.GetContainerReference(containerName);

            // Get reference to blob (binary content)
            CloudBlockBlob blockBlob = cloudBlobContainer.GetBlockBlobReference(FileName);

            // Read content
            using (MemoryStream ms = new MemoryStream())
            {
                //await blockBlob.DownloadToStreamAsync(ms).Wait();
                blockBlob.DownloadToStreamAsync(ms).Wait();
                return ms.ToArray();
            }
        }

        public async void CreatBlobIfNotExist(string containerName)
        {
            CloudBlobContainer cloudBlobContainer = _cloudBlobClient.GetContainerReference(containerName);
            if (await cloudBlobContainer.CreateIfNotExistsAsync())
            {
                cloudBlobContainer.SetPermissionsAsync(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Blob
                }).Wait();

            }
        }

        private string GenerateFileName(string fileName)
        {
            string strFileName = string.Empty;
            string[] strName = fileName.Split('.');
            strFileName = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd") + "/" + DateTime.Now.ToUniversalTime().ToString("yyyyMMdd\\THHmmssfff") + "." + strName[strName.Length - 1];
            return strFileName;
        }
    }
}
