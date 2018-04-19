using System;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
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
            try
            {
                var photo = await CrossMedia.Current.PickPhotoAsync();
                await DisplayAlert("path", photo.Path, "ok");
                disImage.Source = photo.Path;
                var  reuslt = Test(photo.Path);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public PutObjectResponse Test(string filePath)
        {
            AmazonS3Client client = new AmazonS3Client("AK",
                                                       "SK", Amazon.RegionEndpoint.APNortheast1);

           var task = client.PutObjectAsync(new Amazon.S3.Model.PutObjectRequest()
            {
                BucketName = "bucketName",
                FilePath = filePath,
                Key = "tmp/test.png"
            });
           

           // System.Threading.CancellationToken canellationToken = new System.Threading.CancellationToken();
            return task.Result;
        }
    }
}
