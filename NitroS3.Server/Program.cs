﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Minio;

namespace NitroS3.Server
{
    class Program
    {
        const int Port = 8080;

        public static void Main(string[] args)
        {

            var options = new List<ChannelOption>
                {
                    new ChannelOption(ChannelOptions.MaxSendMessageLength,32*1024*1024),
                    new ChannelOption(ChannelOptions.MaxReceiveMessageLength,32*1024*1024),
                    new ChannelOption(ChannelOptions.MaxConcurrentStreams,63),
                    new ChannelOption(ChannelOptions.SoReuseport,1)
                };


            IMinioClientVirtual minioClient = new MinioClientVirtual(
             "localhost:9000",
             "AKIAIOSFODNN7EXAMPLE",
             "wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY",
             "us-east-1");


            var server = new Grpc.Core.Server(options)
            {
                Services = { NitroS3Service.BindService(new NitroServerImpl(minioClient)) },
                Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
            };
            server.Start();

            Console.WriteLine("Greeter server listening on port " + Port);
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }

    public class NitroServerImpl : NitroS3Service.NitroS3ServiceBase
    {
        private IMinioClientVirtual _minioClient;

        public NitroServerImpl(IMinioClientVirtual minioClient)
        {
            _minioClient = minioClient;
        }

        public override async Task<ResultFile> SendFile(FileSend request, ServerCallContext context)
        {

            if (!(await _minioClient.BucketExistsAsync(request.Bucket))) await _minioClient.MakeBucketAsync(request.Bucket);

            using (Stream s = new MemoryStream(request.File.ToArray()))
                await _minioClient.PutObjectAsync(request.Bucket, $"{request.Name}.{request.Extension}", s, s.Length);

            return new ResultFile { IsSuccess = true };
        }
    }
}
