using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CookBook.Domain.Test.v2
{
    public static class DocumentExtensions
    {
        public static IEnumerable<Document> SortTree(this IEnumerable<Document> documents)
        {
            var lookup = documents.ToLookup(x => x.ParentId);

            Func<int?, IEnumerable<Document>> heirarchySort = null;
            heirarchySort = pid =>
                lookup[pid].SelectMany(x => new[] { x }.Concat(heirarchySort(x.id)));

            IEnumerable<Document> sorted = heirarchySort(null);
            return sorted;
        }
    }
}
