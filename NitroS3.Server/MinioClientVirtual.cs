using Minio;
using Minio.DataModel;
using Minio.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace NitroS3.Server
{
    public class MinioClientVirtual : MinioClient, IMinioClientVirtual
    {

        public MinioClientVirtual(string endpoint, string accessKey = "", string secretKey = "", string region = "", string sessionToken = "")
            : base(endpoint, accessKey, secretKey, region, sessionToken)
        {

        }

        public Task<bool> BucketExistsAsync(string bucketName, CancellationToken cancellationToken = default(CancellationToken))
            => base.BucketExistsAsync(bucketName, cancellationToken);

        public Task CopyObjectAsync(string bucketName, string objectName, string destBucketName, string destObjectName = null, CopyConditions copyConditions = null, Dictionary<string, string> metadata = null, ServerSideEncryption sseSrc = null, ServerSideEncryption sseDest = null, CancellationToken cancellationToken = default(CancellationToken))
            => base.CopyObjectAsync(bucketName, objectName, destBucketName, destObjectName, copyConditions, metadata, sseSrc, sseDest, cancellationToken);

        public Task<BucketNotification> GetBucketNotificationsAsync(string bucketName, CancellationToken cancellationToken = default(CancellationToken))
            => base.GetBucketNotificationsAsync(bucketName, cancellationToken);

        public Task GetObjectAsync(string bucketName, string objectName, Action<Stream> cb, ServerSideEncryption sse = null, CancellationToken cancellationToken = default(CancellationToken))
            => base.GetObjectAsync(bucketName, objectName, cb, sse, cancellationToken);

        public Task GetObjectAsync(string bucketName, string objectName, long offset, long length, Action<Stream> cb, ServerSideEncryption sse = null, CancellationToken cancellationToken = default(CancellationToken))
            => base.GetObjectAsync(bucketName, objectName, offset, length, cb, sse, cancellationToken);


        public Task GetObjectAsync(string bucketName, string objectName, string fileName, ServerSideEncryption sse = null, CancellationToken cancellationToken = default(CancellationToken))
            => base.GetObjectAsync(bucketName, objectName, fileName, sse, cancellationToken);

        public Task<string> GetPolicyAsync(string bucketName, CancellationToken cancellationToken = default(CancellationToken))
            => base.GetPolicyAsync(bucketName, cancellationToken);

        public Task<ListAllMyBucketsResult> ListBucketsAsync(CancellationToken cancellationToken = default(CancellationToken))
            => base.ListBucketsAsync(cancellationToken);

        public IObservable<Upload> ListIncompleteUploads(string bucketName, string prefix = null, bool recursive = true, CancellationToken cancellationToken = default(CancellationToken))
            => base.ListIncompleteUploads(bucketName, prefix, recursive, cancellationToken);

        public Task MakeBucketAsync(string bucketName, string location = "us-east-1", CancellationToken cancellationToken = default(CancellationToken))
            => base.MakeBucketAsync(bucketName, location, cancellationToken);

        public Task<string> PresignedGetObjectAsync(string bucketName, string objectName, int expiresInt, Dictionary<string, string> reqParams = null, DateTime? reqDate = null)
            => base.PresignedGetObjectAsync(bucketName, objectName, expiresInt, reqParams, reqDate);

        public Task<Tuple<string, Dictionary<string, string>>> PresignedPostPolicyAsync(PostPolicy policy)
            => base.PresignedPostPolicyAsync(policy);

        public Task<string> PresignedPutObjectAsync(string bucketName, string objectName, int expiresInt)
            => base.PresignedPutObjectAsync(bucketName, objectName, expiresInt);

        public Task PutObjectAsync(string bucketName, string objectName, Stream data, long size, string contentType = null, Dictionary<string, string> metaData = null, ServerSideEncryption sse = null, CancellationToken cancellationToken = default(CancellationToken))
            => base.PutObjectAsync(bucketName, objectName, data, size, contentType, metaData, sse, cancellationToken);

        public Task PutObjectAsync(string bucketName, string objectName, string fileName, string contentType = null, Dictionary<string, string> metaData = null, ServerSideEncryption sse = null, CancellationToken cancellationToken = default(CancellationToken))
               => base.PutObjectAsync(bucketName, objectName, fileName, contentType, metaData, sse, cancellationToken);

        public Task RemoveAllBucketNotificationsAsync(string bucketName, CancellationToken cancellationToken = default(CancellationToken))
            => base.RemoveAllBucketNotificationsAsync(bucketName, cancellationToken);

        public Task RemoveBucketAsync(string bucketName, CancellationToken cancellationToken = default(CancellationToken))
            => base.RemoveBucketAsync(bucketName, cancellationToken);

        public Task RemoveIncompleteUploadAsync(string bucketName, string objectName, CancellationToken cancellationToken = default(CancellationToken))
            => base.RemoveIncompleteUploadAsync(bucketName, objectName, cancellationToken);

        public Task RemoveObjectAsync(string bucketName, string objectName, CancellationToken cancellationToken = default(CancellationToken))
            => base.RemoveObjectAsync(bucketName, objectName, cancellationToken);

        public Task<IObservable<DeleteError>> RemoveObjectAsync(string bucketName, IEnumerable<string> objectNames, CancellationToken cancellationToken = default(CancellationToken))
            => base.RemoveObjectAsync(bucketName, objectNames, cancellationToken);

        public void SetAppInfo(string appName, string appVersion)
            => base.SetAppInfo(appName, appVersion);

        public  Task SetBucketNotificationsAsync(string bucketName, BucketNotification notification, CancellationToken cancellationToken = default(CancellationToken))
            => base.SetBucketNotificationsAsync(bucketName, notification, cancellationToken);

        public Task SetPolicyAsync(string bucketName, string policyJson, CancellationToken cancellationToken = default(CancellationToken))
            => base.SetPolicyAsync(bucketName, policyJson, cancellationToken);

        public void SetTraceOff()
            => base.SetTraceOff();

        public void SetTraceOn(IRequestLogger logger = null)
            => base.SetTraceOn(logger);

        public Task<ObjectStat> StatObjectAsync(string bucketName, string objectName, ServerSideEncryption sse = null, CancellationToken cancellationToken = default(CancellationToken))
            => base.StatObjectAsync(bucketName, objectName, sse, cancellationToken);

        public MinioClient WithProxy(IWebProxy proxy)
            => base.WithProxy(proxy);

        public MinioClient WithSSL()
            => base.WithSSL();
    }
}
