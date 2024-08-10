using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MashtiHasanRestaurant.Core.Security
{
    public class CodeGenerator
    {
        public static string GenerateUniqCode()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
