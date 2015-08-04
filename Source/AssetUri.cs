// <copyright file="AssetUri.cs" company="None">
//     MyCompany.com. All rights reserved.
// </copyright>
// <author>Xposure</author>

namespace Xposure.Assets
{
    using System;

    /// <summary>
    /// Holds a path for the asset manager to reference data in the format of MODULE:TYPE:NAME
    /// </summary>
    public class AssetUri : IComparable<AssetUri>
    {
        #region Constants

        /// <summary>
        /// Separator character for fully qualified names.
        /// </summary>
        public const char SEPARATOR = ':';

        #endregion

        #region Static

        /// <summary>
        /// Readonly instance of an invalid AssetUri
        /// </summary>
        public static readonly AssetUri Invalid = new AssetUri();

        #endregion

        #region Members

        /// <summary>
        /// Asset type for the resolver look up
        /// </summary>
        private string type;

        /// <summary>
        /// Name of the module that the asset is stored in
        /// </summary>
        private string module;

        /// <summary>
        /// Name of the asset
        /// </summary>
        private string name;

        /// <summary>
        /// Fully qualified name of the asset
        /// </summary>
        private string fullyQualifiedName;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetUri" /> class from a simple string in the format of module:type:name.
        /// </summary>
        /// <param name="uri">Expected format for a uri string is module:type:name</param>
        public AssetUri(string uri)
        {
            var split = uri.Trim().Split(SEPARATOR);
            if (split.Length == 3)
            {
                this.module = split[0];
                this.type = split[1];
                this.name = split[2];

                this.fullyQualifiedName = this.module + SEPARATOR + this.type + SEPARATOR + this.name;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetUri" /> class set to an invalid state.
        /// </summary>
        protected AssetUri()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the type of the asset
        /// </summary>
        public string Type
        {
            get { return this.type; }
        }

        /// <summary>
        /// Gets the module associated with the asset
        /// </summary>
        public string Module
        {
            get { return this.module; }
        }

        /// <summary>
        /// Gets the name of the asset
        /// </summary>
        public string Name
        {
            get { return this.name; }
        }

        /// <summary>
        /// Gets the fully qualified name of this asset
        /// </summary>
        public string FullName
        {
            get { return this.fullyQualifiedName; }
        }

        /// <summary>
        /// Gets a value indicating whether this is a valid asset uri
        /// </summary>
        public bool IsValid
        {
            get { return !string.IsNullOrEmpty(this.fullyQualifiedName); }
        }

        #endregion Properties

        #region Operators

        /// <summary>
        /// Implicitly converts a uri to a string
        /// </summary>
        /// <param name="uri">Instance of the uri to convert</param>
        /// <returns>String version of the AssetUri</returns>
        public static implicit operator string(AssetUri uri)
        {
            if (!uri.IsValid)
            {
                return AssetUri.Invalid;
            }

            return uri.fullyQualifiedName;
        }

        /// <summary>
        /// Implicitly converts a string to a AssetUri
        /// </summary>
        /// <param name="val">String to convert to an AssetUri</param>
        /// <returns>AssetUri of the string</returns>
        public static implicit operator AssetUri(string val)
        {
            if (string.IsNullOrEmpty(val))
            {
                return AssetUri.Invalid;
            }

            return new AssetUri(val);
        }

        /// <summary>
        /// Compares to AssetUris for equality
        /// </summary>
        /// <param name="a">First instance of the AssetUri</param>
        /// <param name="b">Second instance of the AssetUri</param>
        /// <returns>Returns true if both uris are null or their full qualified names match</returns>
        public static bool operator ==(AssetUri a, AssetUri b)
        {
            // If both are null, or both are same instance, return true.
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return a.fullyQualifiedName == b.fullyQualifiedName;
        }

        /// <summary>
        /// Compares to AssetUris for inequality
        /// </summary>
        /// <param name="a">First instance of the AssetUri</param>
        /// <param name="b">Second instance of the AssetUri</param>
        /// <returns>Returns true if both uris are not equal</returns>
        public static bool operator !=(AssetUri a, AssetUri b)
        {
            return !(a == b);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Formats the AssertUri it a readable string.
        /// </summary>
        /// <returns>Returns a human readable format of the asset uri for debugging.</returns>
        public override string ToString()
        {
            if (!this.IsValid)
            {
                return "AssetUri: { Invalid }";
            }

            return string.Format("AssetUri: {{ Module: {0}, Type: {1}, Name: {2} }}", this.module, this.type, this.name);
        }

        /// <summary>
        /// Compare an AssetUri to another and return an integer for sorting
        /// </summary>
        /// <param name="other">The other AssetUri to compare to</param>
        /// <returns>Returns string.Compare of both fully qualified uris</returns>
        public int CompareTo(AssetUri other)
        {
            if (other == null)
            {
                return 0;
            }

            return string.Compare(this.fullyQualifiedName, other.fullyQualifiedName);
        }

        /// <summary>
        /// Checks if one AssetUri is equal to another
        /// </summary>
        /// <param name="uri">The other AssetUri to check</param>
        /// <returns>Returns true if the other uri as the same fully qualified name</returns>
        public bool Equals(AssetUri uri)
        {
            if (uri != null && this.IsValid && uri.IsValid)
            {
                return this.fullyQualifiedName == uri.fullyQualifiedName;
            }

            return false;
        }

        /// <summary>
        /// Returns a 32bit integer based on the fully qualified uri
        /// </summary>
        /// <returns>Returns the hash code of the fully qualified name or 0 if its invalid</returns>
        public override int GetHashCode()
        {
            if (!this.IsValid)
            {
                return 0;
            }

            return this.fullyQualifiedName.GetHashCode();
        }

        /// <summary>
        /// Returns true if the other object is an AssetUri and both have the same fully qualified name
        /// </summary>
        /// <param name="obj">The other object to compare to</param>
        /// <returns>Returns true if the other object is a matching AssetUri</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var uri = obj as AssetUri;
            if ((object)obj == null)
            {
                return false;
            }

            return this.fullyQualifiedName == uri.fullyQualifiedName;
        }

        #endregion Methods
    }
}
