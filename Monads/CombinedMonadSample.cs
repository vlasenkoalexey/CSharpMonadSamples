using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonadsExtensions;
using System.Diagnostics;

namespace Monads
{
    public class CombinedMonadSample
    {
        public virtual Task<Maybe<String>> GetData()
        {
            Trace.WriteLine("get data");
            return Task<Maybe<String>>.Factory.StartNew(
                () => Maybe<String>.Return("data"));
        }

        public virtual Task<Maybe<String>> GetMoreData(Maybe<String> data)
        {
            Trace.WriteLine("get more data");
            return Task<Maybe<String>>.Factory.StartNew(
                () => data.Bind(d => Maybe<string>.Return(d + " more")));
        }

        public virtual Task<Maybe<String>> SendProcessedResults(Maybe<String> data)
        {
            Trace.WriteLine("send processed results");
            return Task<Maybe<String>>.Factory.StartNew(
                () => data.Bind(d => Maybe<string>.Return("send_result: " + d)));
        }

        public virtual Maybe<String> Validate(String data)
        {
            Trace.WriteLine("validate");
            return Maybe<String>.Return(data + " validated");
        }

        public virtual Maybe<String> Sanitize(String data)
        {
            Trace.WriteLine("sanitize");
            return Maybe<String>.Return(data + " sanitized");
        }

        public virtual Maybe<String> Process(String data)
        {
            Trace.WriteLine("process");
            return Maybe<String>.Return(data + " processed");
        }

        public Task<Maybe<String>> RunWorkflowNested()
        {
            return GetData().Bind(data =>
            {
                Maybe<String> processedData = data
                    .Bind(raw => Validate(raw)
                        .Bind(validated => Sanitize(validated)
                            .Bind(sanitized => Process(sanitized))));

                return GetMoreData(processedData).Bind(extraData =>
                {
                    Maybe<String> processedExtraData = extraData
                        .Bind(raw => Validate(raw)
                            .Bind(validated => Sanitize(validated)
                                .Bind(sanitized => Process(sanitized))));

                    return SendProcessedResults(processedExtraData);
                });
            });
        }

        // Another version
        public Task<Maybe<String>> RunWorkflowFlat()
        {
            return GetData().Bind(data =>
            {
                Maybe<String> processedData = data
                    .Bind(raw => Validate(raw))
                    .Bind(validated => Sanitize(validated))
                    .Bind(sanitized => Process(sanitized));

                return GetMoreData(processedData);
            }).Bind(extraData =>
            {
                Maybe<String> processedData = extraData
                    .Bind(raw => Validate(raw))
                    .Bind(validated => Sanitize(validated))
                    .Bind(sanitized => Process(sanitized));

                return SendProcessedResults(processedData);
            });
        }
    }
}
