using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceAndMaths.Shared.Canals
{
    public class CanalSimulationResult
    {
        public CanalSimulationResult()
        {
            CanalStretchResults = new Dictionary<string, CanalStretchResult>();
        }

        public Dictionary<string, CanalStretchResult> CanalStretchResults { get; set; }

        public List<CanalPointResult> CanalPointResults
        {
            get { return CanalStretchResults.Values.SelectMany(csr => csr.CanalPointResults).OrderBy(csr => csr.X).ToList(); }
        }

        public CanalStretchResult GetCanalStretchResult(string canalStretchId)
        {
            if (CanalStretchResults.TryGetValue(canalStretchId, out CanalStretchResult result))
            {
                return result;
            }
            else
            {
                result = new CanalStretchResult();
                CanalStretchResults.Add(canalStretchId, result);

                return result;
            }
        }
        
        public void AddCanalPointResult(string canalStretchId, double x, double waterLevel)
        {
            if (!CanalStretchResults.ContainsKey(canalStretchId))
            {
                CanalStretchResults.Add(canalStretchId, new CanalStretchResult());
            }

            CanalStretchResults[canalStretchId].CanalPointResults.Add(new CanalPointResult(x, waterLevel));
        }

        public void AddRangeCanalPointResult(string canalStretchId, List<CanalPointResult> canalPointResults)
        {
            if (!CanalStretchResults.ContainsKey(canalStretchId))
            {
                CanalStretchResults.Add(canalStretchId, new CanalStretchResult());
            }

            CanalStretchResults[canalStretchId].CanalPointResults.AddRange(canalPointResults);
        }
    }
}
