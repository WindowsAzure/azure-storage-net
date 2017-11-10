using System;
namespace Microsoft.Azure.Storage.Table
{
[Flags]
public enum SharedAccessTablePermissions
{
    None = 0,
    Query = 1,
    Add = 2,
    Update = 4,
    Delete = 8,
}

}