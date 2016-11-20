namespace MonadFs

module CombinedMonadSample = 

    open System
    open Builders

    let getData() = async { return Some("data") }

    let getMoreData(data : Option<String>) = async {
            return maybe.Bind(data, fun data -> Some(data + " more"))
        }

    let sendProcessedResults(data : Option<String>) = 
        async {
            return maybe.Bind(data, fun data -> Some("send_result: " + data))
        }

    let validate(data:String) = Some(data + " validated")
    let sanitize(data:String) = Some(data + " sanitized")
    let processData(data:String) = Some(data + " processed")

    let runWorflow() = // return type is Async<String<Option>>
        async {
            let! rawOpt = getData() // rawOpt is of type Option<String>
            let processed = maybe { // processed is of type Option<String>
                let! raw = rawOpt   // same as rawOpt.Bind(raw => ...
                let! validated = validate(raw)
                let! sanitized = sanitize(validated)
                return! processData(sanitized)
            }

            let! moreDataOpt = getMoreData(processed) // moreData is of type Option<String>

            let processedMore = maybe { // processedMore is of type Option<String>
                let! raw = moreDataOpt
                let! validated = validate(raw)
                let! sanitized = sanitize(validated)
                return! processData(sanitized)
            }

            return! sendProcessedResults(processedMore)
        }