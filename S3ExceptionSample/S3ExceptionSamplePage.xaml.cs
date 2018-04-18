using System;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Transfer;
using Plugin.Media;
using Xamarin.Forms;

namespace S3ExceptionSample
{
    public partial class S3ExceptionSamplePage : ContentPage
    {
        public S3ExceptionSamplePage()
        {
            InitializeComponent();
            btnOpenAlbum.Clicked += ClickHanlder;
        }

        async void ClickHanlder(object Sender, EventArgs e)
        {
             var photo = await CrossMedia.Current.PickPhotoAsync();
                disImage.Source = photo.Path;

            try
            {
                await Test(photo.Path);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public Task Test(string filePath)
        {
            AmazonS3Config config = new AmazonS3Config();
            config.ServiceURL = "s3.amazonaws.com";//amazon s3 URL                            
            config.UseHttp = true;
            config.RegionEndpoint = Amazon.RegionEndpoint.APNortheast1;//your region

            AmazonS3Client client = new AmazonS3Client("AK",
                "SK", config);

            TransferUtility transferUtility = new TransferUtility(client);

            TransferUtilityUploadRequest request = new TransferUtilityUploadRequest();
            request.BucketName = "BucketName";
            request.FilePath = filePath;//local file path 
            request.Key = "tmp/test.png";
            request.CannedACL = S3CannedACL.PublicRead;
            //Test that the path is correct

            System.Threading.CancellationToken canellationToken = new System.Threading.CancellationToken();
            return transferUtility.UploadAsync(request, canellationToken);
        }
    }
}
