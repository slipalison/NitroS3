using Minio;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace NitroS3.ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Exec().GetAwaiter().GetResult();

            Console.WriteLine("Hello World!");
        }

        static async Task Exec()
        {
            try
            {

                var path = Directory.GetFiles(Directory.GetCurrentDirectory()).First(x => x.Contains("Torre-Eiffel.jpg"));

                byte[] myByte = File.ReadAllBytes(path);
                var bucket = "testesbucket";
                var objName = $"objectName{Guid.NewGuid().ToString().Replace("-", "")}.jpg";


                var minioClient = new MinioClient(
                   "localhost:9000",
                   "AKIAIOSFODNN7EXAMPLE",
                   "wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY",
                   "us-east-1");

                var listBucket = await minioClient.ListBucketsAsync();

                if (!listBucket.Buckets.Any(x => x.Name == bucket)) await minioClient.MakeBucketAsync(bucket);

                using (Stream s = new MemoryStream(myByte))
                    await minioClient.PutObjectAsync(bucket, objName, s, s.Length);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }

}
