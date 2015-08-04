// <copyright file="IAssetEntry.cs" company="None">
//     MyCompany.com. All rights reserved.
// </copyright>
// <author>Xposure</author>

namespace Xposure.Assets
{
    using System.IO;

    /// <summary>
    /// References an asset in the module and has access to the underlying stream.
    /// </summary>
    public interface IAssetEntry
    {
        /// <summary>
        /// Gets the Asset Uri to this file entry
        /// </summary>
        AssetUri Uri { get; }

        /// <summary>
        /// Opens a stream to read the data for the asset.
        /// </summary>
        /// <returns>Returns a readable stream to the asset data.</returns>
        Stream OpenReadStream();
    }
}
