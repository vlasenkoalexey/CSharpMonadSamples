getData :: IO (Maybe String) -- type declaration
-- return here stands for wrapping Maybe String into IO Monad
getData = return (Just "data") 

getMoreData :: Maybe String -> IO (Maybe String)
getMoreData dataOpt = 
    let moreDataOpt = do 
        d <- dataOpt -- unwraping maybe Monad
        -- ++ operator is used for string concatination
        return (d ++ " more") -- return data out of do block
    in return(moreDataOpt) -- wrapping moreDataOpt in IO

sendProcessedResults :: Maybe String -> IO (Maybe String)
sendProcessedResults dataOpt = 
    let sendResultOpt = do 
        d <- dataOpt
        return ("send_result: " ++ d) -- return data out of do block
    in return(sendResultOpt) -- wrapping moreDataOpt in IO

-- Haskell doesn't use brackets for function declaraions
-- It is not necessary to specify function type if compiler can deduct it.
validate d = Just(d ++ " validated") 
sanitize d = Just(d ++ " sanitized") 
process d = Just(d ++ " processed")

-- function without parametes
runWorflow = 
    do -- IO block
        rawOpt <- getData -- unwraping IO monad
        
        let processedResult = do  -- nested maybe block
            raw <- rawOpt
            validated <- validate raw
            sanitized <- sanitize validated
            processed <- process sanitized
            return processed -- getting out of maybe do block
        
        moreDataOpt <- getMoreData processedResult

        let processedMoreResult = do -- nested maybe block
            raw <- moreDataOpt
            validated <- validate raw
            sanitized <- sanitize validated
            processed <- process sanitized
            return processed -- getting out of maybe do block

        results <- sendProcessedResults processedMoreResult
        return results -- getting out of IO block
