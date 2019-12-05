// -----------------------------------------------------------------------
// <copyright file="VideoOverviewUI.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Deveknife.Blades.Overview
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public partial class VideoOverviewUI
    {
        private void TestCrit1(CriteriaOperator crit)
        {
            //crit.Accept(null);
            var lambda = CriteriaCompiler.ToLambda(crit, CriteriaCompilerContextDescriptorReflective.Instance);
            var compLambda = lambda.Compile();
            //compLambda.GetAttachedProperty()
            var queryList = new List<string>();
            queryList.Add("Matlock");
            queryList.Add("Older");
            var res = compLambda.DynamicInvoke(new object[] { this.LastFetchedLocalEitFiles });

            var cntOp = new ContainsOperator("EventName", "Matlock");
            // filt.Expression
            //var bla = cntOp.Accept(IsLogicalCriteriaChecker.Instance);
            //this.gridView1.filter = cntOp;

            //var result = filt.Criteria.Accept(BooleanComplianceChecker.Instance);
        }

        private void TestCrit2(CriteriaOperator crit)
        {
            //var pred = CriteriaCompiler.ToPredicate<EITFormatDisplay>(crit, CriteriaCompilerContextDescriptorReflective.Instance);
            var pred = CriteriaCompiler.ToPredicate<object>(crit, CriteriaCompilerContextDescriptorReflective.Instance);
            //pred.DynamicInvoke()

            var queryList = new List<string>();
            queryList.Add("Matlock");
            queryList.Add("Older");

            //var datarow = this.gridView1.GetRow(5);

            foreach (var eitDisp in this.eITFormatDisplayBindingSource)
            {
                var predicateMatches = pred.Invoke(eitDisp);
                var eit = eitDisp as EITFormatDisplay;
                var str = eit.EventName;

                if (predicateMatches)
                {
                }
                else
                {
                }
            }
            //var res2 = pred.Invoke("Arsch");
        }
    }
}
