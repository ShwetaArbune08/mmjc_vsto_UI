using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sundeus.Clm.AzureStorage.Interface
{
    public interface IBlobRepository
    {
        void Initialize(string connectionString);
        Task<string> UploadFileToBlob(string containerName, string filePath);
        void CreatBlobIfNotExist(string containerName);
        Task<byte[]> DownloadBlobFiles(string containerName, string FileName);
    }
}
