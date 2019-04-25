using Google.Protobuf;
using Grpc.Core;
using Minio.DataModel;
using NitroS3.Server;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace NitroS3.Test
{
    public class UnitTest1 : IDisposable
    {

        const string Host = "localhost";
        private readonly Grpc.Core.Server _server;
        private readonly Channel _channel;
        private readonly NitroS3Service.NitroS3ServiceClient _client;
        private readonly NitroServerImpl _nitroServerImpl;

        public UnitTest1()
        {
            var minio = Substitute.For<IMinioClientVirtual>();

            minio.BucketExistsAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(true);

            minio.PutObjectAsync(
                Arg.Any<string>(), 
                Arg.Any<string>(),
                Arg.Any<Stream>(), 
                Arg.Any<long>(),
                Arg.Any<string>(),
                Arg.Any<Dictionary<string, string>>(), 
                Arg.Any<ServerSideEncryption>(), 
                Arg.Any<CancellationToken>()
                )
                .Returns(Task.CompletedTask);

            _nitroServerImpl = new NitroServerImpl(minio);

            var options = new List<ChannelOption>()
                {
                    new ChannelOption(ChannelOptions.MaxSendMessageLength,32*1024*1024),
                    new ChannelOption(ChannelOptions.MaxReceiveMessageLength,32*1024*1024),
                    new ChannelOption(ChannelOptions.MaxConcurrentStreams,63),
                    new ChannelOption(ChannelOptions.SoReuseport,0)
                };

            _server = new Grpc.Core.Server(options)
            {
                Services = { NitroS3Service.BindService(_nitroServerImpl) },
                Ports = { { Host, ServerPort.PickUnused, ServerCredentials.Insecure } }
            };
            _server.Start();
            _channel = new Channel(Host, _server.Ports.Single().BoundPort, ChannelCredentials.Insecure, options);
            _client = new NitroS3Service.NitroS3ServiceClient(_channel);
        }

        public void Dispose()
        {
            _channel.ShutdownAsync().Wait();
            _server.ShutdownAsync().Wait();
            GC.SuppressFinalize(this);
        }

        [Fact]
        public async Task Test1()
        {

            var path = Directory.GetFiles(Directory.GetCurrentDirectory()).First(x => x.Contains("Torre-Eiffel.jpg"));
            byte[] myByte = File.ReadAllBytes(path);

            var t = await _client.SendFileAsync(new FileSend
            {
                Bucket = "", 
                Extension = "", 
                Name = "",
                File = ByteString.CopyFrom(myByte)
            });

            Assert.True(t.IsSuccess);
        }
    }
}
