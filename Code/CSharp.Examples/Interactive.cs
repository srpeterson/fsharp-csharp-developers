using System.Collections.Generic;
using System.Linq;

namespace CSharp.Examples
{
    public class Interactive
    {
        public List<int> GetLessThanFour()
        {
            var firstNames = new List<int> { 1, 2, 3, 4, 5 };
            return firstNames.Where(e => e < 4).ToList();
        }
    }
}
