using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonadsExtensions;

namespace Monads
{
    class CombinedMonadSample
    {
        public class RequestTransforerProcessor
        {
            public static Task<Maybe<String>> GetData()
            {
                return Task<Maybe<String>>.Factory.StartNew(
                    () => Maybe<String>.Return(""));
            }

            public static Task<Maybe<String>> GetMoreData(Maybe<String> data)
            {
                return Task<Maybe<String>>.Factory.StartNew(
                    () => Maybe<String>.Return(""));
            }

            public static Task<bool> SendProcessedResults(Maybe<String> data)
            {
                return Task<bool>.Factory.StartNew(() => data as Just<String> != null);
            }

            public static Maybe<String> Validate(String data)
            {
                return null;
            }

            public static Maybe<String> Sanitize(String data)
            {
                return Maybe<String>.Return("data");
            }

            public static Maybe<String> Process(String data)
            {
                return Maybe<String>.Return("data");
            }

            public static Task<bool> RunWorkflow()
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

            //TODO think if these 2 samples are eqvivalent, and run them to confirm

            public static Task<bool> RunWorkflow2()
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
}
