
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure;


namespace WEBSITESLK
{
  public  class Pics
    //metho to call images from blob.
    {
        public void ImagesBlobFileInput(string inputFileName, string inputFilepath )
        {
            
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnection"));
            var myClient = storageAccount.CreateCloudBlobClient();
            var container = myClient.GetContainerReference("images-backup");
            container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);

            var blockBlob = container.GetBlockBlobReference(@inputFileName);
            using (var fileStream = System.IO.File.OpenRead(@inputFilepath))
            {
                blockBlob.UploadFromStream(fileStream);
            }       
        }
        public void ImagesBlobFileOutput(string outputFileName, string outputFilePath)
        {

            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnection"));
            var myClient = storageAccount.CreateCloudBlobClient();
            var container = myClient.GetContainerReference("WEBSITESLK");
            container.CreateIfNotExists(BlobContainerPublicAccessType.Blob);

            var download = container.GetBlockBlobReference(@outputFileName);
            using (var fileStream = System.IO.File.OpenWrite(@outputFilePath))
            {
                download.DownloadToStream(fileStream);
            }
        }

    }
}
