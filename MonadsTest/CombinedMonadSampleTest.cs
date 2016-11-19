using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Monads;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MonadsTest
{
    [TestClass]
    public class CombinedMonadSampleTest
    {
        [TestMethod]
        public void TestRunWorkflowNested()
        {
            var combinedMonadSample = new CombinedMonadSample();
            var workflow = combinedMonadSample.RunWorkflowNested();
            workflow.Wait();
            Assert.IsTrue(workflow.Result is Just<String>);
            Assert.AreEqual("send_result: data validated sanitized processed more validated sanitized processed", 
                (workflow.Result as Just<String>).Value);
        }

        [TestMethod]
        public void TestRunWorkflowFlat()
        {
            var combinedMonadSample = new CombinedMonadSample();
            var workflow = combinedMonadSample.RunWorkflowFlat();
            workflow.Wait();
            Assert.IsTrue(workflow.Result is Just<String>);
            Assert.AreEqual("send_result: data validated sanitized processed more validated sanitized processed", 
                (workflow.Result as Just<String>).Value);
        }

        class CombinedMonadSampleNoData : CombinedMonadSample
        {
            public override Task<Maybe<String>> GetData()
            {
                Trace.WriteLine("get data (no data)");
                return Task<Maybe<String>>.Factory.StartNew(
                    () => new Nothing<String>());
            }
        }


        [TestMethod]
        public void TestRunWorkflowNested_noData()
        {
            var combinedMonadSample = new CombinedMonadSampleNoData();
            var workflow = combinedMonadSample.RunWorkflowNested();
            workflow.Wait();
            Assert.IsTrue(workflow.Result is Nothing<String>);
        }

        [TestMethod]
        public void TestRunWorkflowFlat_noData()
        {
            var combinedMonadSample = new CombinedMonadSampleNoData();
            var workflow = combinedMonadSample.RunWorkflowFlat();
            workflow.Wait();
            Assert.IsTrue(workflow.Result is Nothing<String>);
        }

        class CombinedMonadSampleNoMoreData : CombinedMonadSample
        {
            public override Task<Maybe<String>> GetMoreData(Maybe<String> data)
            {
                Trace.WriteLine("get more data (no data)");
                return Task<Maybe<String>>.Factory.StartNew(
                    () => new Nothing<String>());
            }
        }

        [TestMethod]
        public void TestRunWorkflowNested_noMoreData()
        {
            var combinedMonadSample = new CombinedMonadSampleNoMoreData();
            var workflow = combinedMonadSample.RunWorkflowNested();
            workflow.Wait();
            Assert.IsTrue(workflow.Result is Nothing<String>);
        }

        [TestMethod]
        public void TestRunWorkflowFlat_noMoreData()
        {
            var combinedMonadSample = new CombinedMonadSampleNoMoreData();
            var workflow = combinedMonadSample.RunWorkflowFlat();
            workflow.Wait();
            Assert.IsTrue(workflow.Result is Nothing<String>);
        }

        class CombinedMonadSampleJustExtraData : CombinedMonadSample
        {
            public override Task<Maybe<String>> GetData()
            {
                Trace.WriteLine("get data (no data)");
                return Task<Maybe<String>>.Factory.StartNew(
                    () => new Nothing<String>());
            }

            public override Task<Maybe<String>> GetMoreData(Maybe<String> data)
            {
                Trace.WriteLine("get more data (replacement data)");
                return Task<Maybe<String>>.Factory.StartNew(
                    () => Maybe<String>.Return("replacement data"));
            }
        }

        [TestMethod]
        public void TestRunWorkflowNested_justExtra()
        {
            var combinedMonadSample = new CombinedMonadSampleJustExtraData();
            var workflow = combinedMonadSample.RunWorkflowNested();
            workflow.Wait();
            Assert.AreEqual("send_result: replacement data validated sanitized processed",
                (workflow.Result as Just<String>).Value);
        }

        [TestMethod]
        public void TestRunWorkflowFlat_justExtra()
        {
            var combinedMonadSample = new CombinedMonadSampleJustExtraData();
            var workflow = combinedMonadSample.RunWorkflowFlat();
            workflow.Wait();
            Assert.AreEqual("send_result: replacement data validated sanitized processed",
                (workflow.Result as Just<String>).Value);
        }
    }
}
