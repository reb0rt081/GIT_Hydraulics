using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Shared.Canals
{
    public class CanalStretchResult
    {
        public CanalStretchResult()
        {
            CanalPointResults = new List<CanalPointResult>();
        }

        public List<CanalPointResult> CanalPointResults { get; set; }
    }
}
