using Examine;
using Umbraco.Cms.Infrastructure.Examine;

namespace Umbraco.Community.CspReports.Examine.Index
{
    public class CspViolationIndexPopulator : IndexPopulator
    {
        public CspViolationIndexPopulator()
        {
            RegisterIndex(CspReportsExamineConstants.IndexName);
        }

        protected override void PopulateIndexes(IReadOnlyList<IIndex> indexes)
        {
        }
    }
}
