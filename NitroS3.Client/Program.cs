using Google.Protobuf;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NitroS3.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Directory.GetFiles(Directory.GetCurrentDirectory()).First(x => x.Contains("Torre-Eiffel.jpg"));
            byte[] myByte = File.ReadAllBytes(path);

            var options = new List<ChannelOption>()
                {
                    new ChannelOption(ChannelOptions.MaxSendMessageLength,32*1024*1024),
                    new ChannelOption(ChannelOptions.MaxReceiveMessageLength,32*1024*1024),
                    new ChannelOption(ChannelOptions.MaxConcurrentStreams,63),
                    new ChannelOption(ChannelOptions.SoReuseport,1)
                };


            var channel = new Channel("127.0.0.1", 8080, ChannelCredentials.Insecure, options);

            var client = new NitroS3Service.NitroS3ServiceClient(channel);

            var reply = client.SendFile(new FileSend
            {
                Bucket = "testesbucket",
                Extension = "jpg",
                File = ByteString.CopyFrom(myByte),
                Name = $"olar/teste/GRPC"
            });

            Console.WriteLine($"Esta no S3 ?: { (reply.IsSuccess ? "Sim" : "Não")}");



            channel.ShutdownAsync().Wait();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
