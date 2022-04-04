using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveCacheManager
{
    static class Utils
    {
        /// <summary>
        /// Convert the provided object into an array with the object as its single item.
        /// If the object is null, the resulting array will be null (NOT an array with a single null element)
        /// </summary>
        /// <typeparam name="T">The type of the object that will be provided and contained in the returned array.</typeparam>
        /// <param name="singleElement">The item which will be contained in the return array as its single item.</param>
        /// <returns>An array with <paramref name="singleElement"/> as its single item.</returns>
        public static T[] ToSingleArray<T>(this T singleElement) => singleElement != null ? new[] { singleElement } : null;
    }
}
