using Microsoft.Azure.Storage.Auth;
using Microsoft.Azure.Storage.Core;
using Microsoft.Azure.Storage.Core.Executor;
using Microsoft.Azure.Storage.Core.Util;
using Microsoft.Azure.Storage.File.Protocol;
using Microsoft.Azure.Storage.Shared.Protocol;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
namespace Microsoft.Azure.Storage.File
{
public class CloudFileDirectory : IListFileItem
{
    public CloudFileClient ServiceClient
    {
        get; private set;
    }

    public Uri Uri
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public StorageUri StorageUri
    {
        get; private set;
    }

    internal Uri SnapshotQualifiedUri
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    internal StorageUri SnapshotQualifiedStorageUri
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public FileDirectoryProperties Properties
    {
        get; internal set;
    }

    public IDictionary<string, string> Metadata
    {
        get; internal set;
    }

    public CloudFileShare Share
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public CloudFileDirectory Parent
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public string Name
    {
        get; private set;
    }

    public CloudFileDirectory(Uri directoryAbsoluteUri)
      : this(new StorageUri(directoryAbsoluteUri), (StorageCredentials) null)
    {
        throw new System.NotImplementedException();
    }
    public CloudFileDirectory(Uri directoryAbsoluteUri, StorageCredentials credentials)
      : this(new StorageUri(directoryAbsoluteUri), credentials)
    {
        throw new System.NotImplementedException();
    }
    public CloudFileDirectory(StorageUri directoryAbsoluteUri, StorageCredentials credentials)
    {
        throw new System.NotImplementedException();
    }
    internal CloudFileDirectory(StorageUri uri, string directoryName, CloudFileShare share)
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task CreateAsync()
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task CreateAsync(FileRequestOptions options, OperationContext operationContext)
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task CreateAsync(FileRequestOptions options, OperationContext operationContext, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task<bool> CreateIfNotExistsAsync()
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task<bool> CreateIfNotExistsAsync(FileRequestOptions options, OperationContext operationContext)
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task<bool> CreateIfNotExistsAsync(FileRequestOptions options, OperationContext operationContext, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task DeleteAsync()
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task DeleteAsync(AccessCondition accessCondition, FileRequestOptions options, OperationContext operationContext)
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task DeleteAsync(AccessCondition accessCondition, FileRequestOptions options, OperationContext operationContext, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task<bool> DeleteIfExistsAsync()
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task<bool> DeleteIfExistsAsync(AccessCondition accessCondition, FileRequestOptions options, OperationContext operationContext)
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task<bool> DeleteIfExistsAsync(AccessCondition accessCondition, FileRequestOptions options, OperationContext operationContext, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task<bool> ExistsAsync()
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task<bool> ExistsAsync(FileRequestOptions options, OperationContext operationContext)
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task<bool> ExistsAsync(FileRequestOptions options, OperationContext operationContext, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task FetchAttributesAsync()
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task FetchAttributesAsync(AccessCondition accessCondition, FileRequestOptions options, OperationContext operationContext)
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task FetchAttributesAsync(AccessCondition accessCondition, FileRequestOptions options, OperationContext operationContext, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task<FileResultSegment> ListFilesAndDirectoriesSegmentedAsync(FileContinuationToken currentToken)
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task<FileResultSegment> ListFilesAndDirectoriesSegmentedAsync(int? maxResults, FileContinuationToken currentToken, FileRequestOptions options, OperationContext operationContext)
    {
        throw new System.NotImplementedException();
    }
    public virtual Task<FileResultSegment> ListFilesAndDirectoriesSegmentedAsync(string prefix, int? maxResults, FileContinuationToken currentToken, FileRequestOptions options, OperationContext operationContext)
    {
        throw new System.NotImplementedException();
    }
    public virtual Task<FileResultSegment> ListFilesAndDirectoriesSegmentedAsync(int? maxResults, FileContinuationToken currentToken, FileRequestOptions options, OperationContext operationContext, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
    public virtual Task<FileResultSegment> ListFilesAndDirectoriesSegmentedAsync(string prefix, int? maxResults, FileContinuationToken currentToken, FileRequestOptions options, OperationContext operationContext, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
    public virtual Task SetMetdataAsync()
    {
        throw new System.NotImplementedException();
    }
    public virtual Task SetMetadataAsync(AccessCondition accessCondition, FileRequestOptions options, OperationContext operationContext)
    {
        throw new System.NotImplementedException();
    }
     
    public virtual Task SetMetadataAsync(AccessCondition accessCondition, FileRequestOptions options, OperationContext operationContext, CancellationToken cancellationToken)
    {
        throw new System.NotImplementedException();
    }
    internal void AssertNoSnapshot()
    {
        throw new System.NotImplementedException();
    }
    private IListFileItem SelectListFileItem(IListFileEntry protocolItem)
    {
        throw new System.NotImplementedException();
    }
    public virtual CloudFile GetFileReference(string fileName)
    {
        throw new System.NotImplementedException();
    }
    public virtual CloudFileDirectory GetDirectoryReference(string itemName)
    {
        throw new System.NotImplementedException();
    }
    private void ParseQueryAndVerify(StorageUri address, StorageCredentials credentials)
    {
        throw new System.NotImplementedException();
    }
}

}