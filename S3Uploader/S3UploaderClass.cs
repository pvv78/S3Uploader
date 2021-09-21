using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Amazon.S3;
using Amazon.S3.Transfer;
//using Amazon.EC2;

namespace S3Uploader
{
    [ComVisible(true)]
    [Guid("EAA4976A-45C3-4BC5-BC0B-E474F4C3C83F")]
    public interface S3UploaderInterface
    {
        string SendFile(string p_filename,
                        string p_accessKey,
                        string p_secretKey,
                        string p_serviceURL,
                        string p_bucketName,
                        string p_subDirectoryInBucket,
                        string p_fileNameInS3);
    }

    [Guid("8be81d2b-2d6f-4da8-91d1-a9175ef2ec9d"),
        InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
    public interface S3UploaderEvents
    {
    }

    [ComVisible(true)]
    [Guid("fc032199-4ac7-41f1-8a5f-76cf8ba7c6db"),
        ClassInterface(ClassInterfaceType.None),
        ComSourceInterfaces(typeof(S3UploaderEvents))]
    [ProgId("S3.Upload")]
    public class S3UploaderClass : S3UploaderInterface
    {
        public string SendFile(string p_filename,
                               string p_accessKey,
                               string p_secretKey,
                               string p_serviceURL,
                               string p_bucketName,
                               string p_subDirectoryInBucket,
                               string p_fileNameInS3)
        {
            string accessKey = p_accessKey;
            string secretKey = p_secretKey;
            string localFilePath = p_filename;
            string bucketName = p_bucketName;
            string subDirectoryInBucket = p_subDirectoryInBucket;
            string fileNameInS3 = p_fileNameInS3;

            var S3ClientConfig = new AmazonS3Config
            {
                // Specify the endpoint explicitly
                ServiceURL = p_serviceURL,
                ForcePathStyle = true
            };

            using (IAmazonS3 client = new AmazonS3Client(accessKey, secretKey, S3ClientConfig))
                { 
                        //using (IAmazonS3 client = new AmazonS3Client(accessKey, secretKey))
                        //var client = Amazon.AWSClientFactory.CreateAmazonS3Client(accessKey, secretKey))

                        // FileStream(path)

                        //public bool sendMyFileToS3(string localFilePath, string bucketName, string subDirectoryInBucket, string fileNameInS3)
                        //{
                        // input explained :
                        // localFilePath = the full local file path e.g. "c:\mydir\mysubdir\myfilename.zip"
                        // bucketName : the name of the bucket in S3 ,the bucket should be alreadt created
                        // subDirectoryInBucket : if this string is not empty the file will be uploaded to
                        // a subdirectory with this name
                        // fileNameInS3 = the file name in the S3

                        // create an instance of IAmazonS3 class ,in my case i choose RegionEndpoint.EUWest1
                        // you can change that to APNortheast1 , APSoutheast1 , APSoutheast2 , CNNorth1
                        // SAEast1 , USEast1 , USGovCloudWest1 , USWest1 , USWest2 . this choice will not
                        // store your file in a different cloud storage but (i think) it differ in performance
                        // depending on your location

                        //IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(RegionEndpoint.EUWest1);
                        //IAmazonS3 client = new AmazonS3Client(accessKey, secretKey);
                    
                    // create a TransferUtility instance passing it the IAmazonS3 created in the first step
                    TransferUtility utility = new TransferUtility(client);
                    
                    // making a TransferUtilityUploadRequest instance
                    TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();

                    if (subDirectoryInBucket == "" || subDirectoryInBucket == null)
                    {
                        request.BucketName = bucketName; //no subdirectory just bucket name
                    }
                    else
                    {   // subdirectory and bucket name
                        request.BucketName = bucketName + @"/" + subDirectoryInBucket;
                    }
                    request.Key = fileNameInS3; //file name up in S3
                    request.FilePath = localFilePath; //local file name
                    utility.Upload(request); //commensing the transfer

                    return "Ok"; //indicate that the file was sent
                }
            }
    }
}
