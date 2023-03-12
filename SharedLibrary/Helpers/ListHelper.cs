
using System.Collections.Generic;

namespace SharedLibrary.Helpers
{
    public static class ListHelper
    {
        public static bool IsNullOrEmpty<T>(this List<T>? list)
        {
            if ( list == null || list.Count == 0 )
            {
                return true;
            }

            return false;
        }        
    }
}
