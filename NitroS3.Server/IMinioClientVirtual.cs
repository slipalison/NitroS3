using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Minio;
using Minio.DataModel;
using Minio.Exceptions;

namespace NitroS3.Server
{
    public interface IMinioClientVirtual
    {
        Task<bool> BucketExistsAsync(string bucketName, CancellationToken cancellationToken = default(CancellationToken));
        Task CopyObjectAsync(string bucketName, string objectName, string destBucketName, string destObjectName = null, CopyConditions copyConditions = null, Dictionary<string, string> metadata = null, ServerSideEncryption sseSrc = null, ServerSideEncryption sseDest = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<BucketNotification> GetBucketNotificationsAsync(string bucketName, CancellationToken cancellationToken = default(CancellationToken));
        Task GetObjectAsync(string bucketName, string objectName, Action<Stream> cb, ServerSideEncryption sse = null, CancellationToken cancellationToken = default(CancellationToken));
        Task GetObjectAsync(string bucketName, string objectName, long offset, long length, Action<Stream> cb, ServerSideEncryption sse = null, CancellationToken cancellationToken = default(CancellationToken));
        Task GetObjectAsync(string bucketName, string objectName, string fileName, ServerSideEncryption sse = null, CancellationToken cancellationToken = default(CancellationToken));
        Task<string> GetPolicyAsync(string bucketName, CancellationToken cancellationToken = default(CancellationToken));
        Task<ListAllMyBucketsResult> ListBucketsAsync(CancellationToken cancellationToken = default(CancellationToken));
        IObservable<Upload> ListIncompleteUploads(string bucketName, string prefix = null, bool recursive = true, CancellationToken cancellationToken = default(CancellationToken));
        Task MakeBucketAsync(string bucketName, string location = "us-east-1", CancellationToken cancellationToken = default(CancellationToken));
        Task<string> PresignedGetObjectAsync(string bucketName, string objectName, int expiresInt, Dictionary<string, string> reqParams = null, DateTime? reqDate = null);
        Task<Tuple<string, Dictionary<string, string>>> PresignedPostPolicyAsync(PostPolicy policy);
        Task<string> PresignedPutObjectAsync(string bucketName, string objectName, int expiresInt);
        Task PutObjectAsync(string bucketName, string objectName, Stream data, long size, string contentType = null, Dictionary<string, string> metaData = null, ServerSideEncryption sse = null, CancellationToken cancellationToken = default(CancellationToken));
        Task PutObjectAsync(string bucketName, string objectName, string fileName, string contentType = null, Dictionary<string, string> metaData = null, ServerSideEncryption sse = null, CancellationToken cancellationToken = default(CancellationToken));
        Task RemoveAllBucketNotificationsAsync(string bucketName, CancellationToken cancellationToken = default(CancellationToken));
        Task RemoveBucketAsync(string bucketName, CancellationToken cancellationToken = default(CancellationToken));
        Task RemoveIncompleteUploadAsync(string bucketName, string objectName, CancellationToken cancellationToken = default(CancellationToken));
        Task<IObservable<DeleteError>> RemoveObjectAsync(string bucketName, IEnumerable<string> objectNames, CancellationToken cancellationToken = default(CancellationToken));
        Task RemoveObjectAsync(string bucketName, string objectName, CancellationToken cancellationToken = default(CancellationToken));
        void SetAppInfo(string appName, string appVersion);
        Task SetBucketNotificationsAsync(string bucketName, BucketNotification notification, CancellationToken cancellationToken = default(CancellationToken));
        Task SetPolicyAsync(string bucketName, string policyJson, CancellationToken cancellationToken = default(CancellationToken));
        void SetTraceOff();
        void SetTraceOn(IRequestLogger logger = null);
        Task<ObjectStat> StatObjectAsync(string bucketName, string objectName, ServerSideEncryption sse = null, CancellationToken cancellationToken = default(CancellationToken));
        MinioClient WithProxy(IWebProxy proxy);
        MinioClient WithSSL();
    }
}